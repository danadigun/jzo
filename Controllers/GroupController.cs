using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using jzo.Data;
using jzo.Models.ItemViewModels;
using Microsoft.AspNetCore.Authorization;
using System.Text;
using Microsoft.AspNetCore.Http;
using jzo.Services;
using jzo.Models;
using Microsoft.EntityFrameworkCore;
using jzo.Models.OrdersViewModel;

namespace jzo.Controllers
{
    [Authorize]
    public class GroupController : Controller
    {
        private ApplicationDbContext _context;

        public const string CartSessionKey = "CartId";
        public string ShoppingCartId { get; set; }

        public GroupController(ApplicationDbContext context)
        {
            _context = context;
        }

        [Authorize(Policy = "CanManageStore")]
        public IActionResult Index()
        {
            var viewModelList = new List<GetAllItemGroupViewModel>();

            var allGroups = _context.ItemGroup.ToList();
            foreach(var group in allGroups)
            {
                //collections
                var group_items = _context.Item.Where(x => x.ItemGroupId == group.Id).ToList();

                //new orders
                var newOrder = _context.Order.Where(x => x.isPending == true).ToList().Count();

                var viewModel = new GetAllItemGroupViewModel
                {
                    group = group,
                    items = group_items,
                    newOrders = newOrder
                };
                viewModelList.Add(viewModel);
            }
            return View(viewModelList);
        }

        [Authorize(Policy = "CanManageStore")]
        public IActionResult pending()
        {
            var viemModelList = new List<PendingOrder>();

            //get pending orders
            var _orders = _context.Order.Where(x => x.isPending == true).ToList();

            //populate pending order details
            foreach(var order in _orders)
            {
                //get user
                var _user = _context.Users.Where(x => x.UserName == order.user).FirstOrDefault();

                //get shopping cart items for this  order
                var selectedItems = _context.SelectedItem
                            .Where(x => x.OrderReferenceId == order.referenceId)
                            .ToList();

                //get actual item details
                var actualItemsList = new List<PurchasedItem>();
                foreach(var item in selectedItems)
                {
                    var _item = _context.Item.Where(x => x.Id == item.ItemId).SingleOrDefault();

                    actualItemsList.Add(new PurchasedItem
                    {
                        image_url = _item.image_url,
                        name = _item.name,
                        price = _item.price,
                        qty = item.quantity,
                        size = item.size,
                        totalPrice = item.totalPrice
                    });
                }

                //populate pending order
                PendingOrder _pendingOrder = new PendingOrder
                {
                    Name = _user.firstname + " " + _user.lastname,
                    address = _user.mailingAddress,
                    email = _user.UserName,
                    phone = _user.PhoneNumber,
                    referenceId = order.referenceId,
                    items = actualItemsList,
                    totalPriceOfOrder = actualItemsList.Select(x=>x.totalPrice).Sum(),
                    dateCreated = order.dateCreated
                };


                //add pending order to viewmodel list
                viemModelList.Add(_pendingOrder);
            }
                     
            return View(viemModelList);
        }
        /// <summary>
        /// Vies details of an item to add to cart or order
        /// </summary>
        /// <param name="itemId"></param>
        /// <returns></returns>
        public IActionResult details(int itemId)
        {

            var getItemViewModel = new GetItemViewModel();
            var item = _context.Item.SingleOrDefault(x => x.Id == itemId);
            var relatedItem = _context.Item.OrderBy(x=>x.Id).Take(4).ToList();

            getItemViewModel.item = item;
            getItemViewModel.relatedItems = relatedItem;

            return View(getItemViewModel);
        }

        /// <summary>
        /// Adds an item to cart
        /// </summary>
        /// <param name="itemId"></param>
        /// <returns></returns>
        public IActionResult addToCart(int itemId, int? quantity, decimal price, string size)
        {
            // Retrieve the product from the database.           
            ShoppingCartId = GetCartId();

            var cartItem = _context.SelectedItem.SingleOrDefault(
                x => x.CartId == ShoppingCartId
                && x.ItemId == itemId && x.isCheckedOut == false);

            if(cartItem == null)
            {
                if (!quantity.HasValue)
                {
                    //create a new cart item if no cart item exists
                    cartItem = new Models.SelectedItems
                    {
                        ItemId = itemId,
                        CartId = ShoppingCartId,
                        dateCreated = DateTime.Now,
                        isCheckedOut = false,
                        quantity = 1,
                        totalPrice = price,
                        size = size,
                        user = User.Identity.Name,
                        
                    };

                }else
                {
                    //create a new cart item if no cart item exists
                    int qty = quantity.Value;
                    cartItem = new Models.SelectedItems
                    {
                        ItemId = itemId,
                        CartId = ShoppingCartId,
                        dateCreated = DateTime.Now,
                        isCheckedOut = false,
                        quantity = quantity.Value,
                        totalPrice = qty * price,
                        size = size,
                        user = User.Identity.Name
                    };

                }
                _context.Add(cartItem);
            }else
            {
                // If the item does exist in the cart,                  
                // then add one to the quantity.                 
                cartItem.quantity++;
            }
            _context.SaveChanges();

            return Json(new {CartItem = cartItem });
        }

        /// <summary>
        /// Get Cart items or view cart items 
        /// </summary>
        /// <returns></returns>
        public IActionResult getCartItems()
        {
            ShoppingCartId = GetCartId();

            var cartItems = _context.SelectedItem.Where(x => x.CartId == ShoppingCartId && x.isCheckedOut == false).ToList();
            return View(cartItems);
        }

        /// <summary>
        /// Get total amount of cart items
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public IActionResult getTotal()
        {
            ShoppingCartId = GetCartId();

            var cartItems = _context.SelectedItem.Where(x => x.CartId == ShoppingCartId && x.isCheckedOut == false).ToList();

            return Json(new { total = cartItems.Select(x => x.totalPrice).Sum(), noOfItems = cartItems.Count });
        }

        /// <summary>
        /// returns cart id
        /// </summary>
        /// <returns></returns>
        public string GetCartId()
        {
            if (HttpContext.Session.GetString(CartSessionKey) == null)
            {
                if (!string.IsNullOrWhiteSpace(HttpContext.User.Identity.Name))
                {
                    //HttpContext.Session.SetString("CartId", HttpContext.User.Identity.Name);
                    Guid tempCartId = Guid.NewGuid();
                    HttpContext.Session.SetString("CartId", tempCartId.ToString());
                }
                else
                {
                    // Generate a new random GUID using System.Guid class.     
                    Guid tempCartId = Guid.NewGuid();
                    HttpContext.Session.SetString("CartId", tempCartId.ToString());
                }
            }
            return HttpContext.Session.GetString("CartId");

        }


        /// <summary>
        /// API to return cart id
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public IActionResult getCart()
        {
            ShoppingCartId = GetCartId();

            var cartItems = _context.SelectedItem.Where(x => x.CartId == ShoppingCartId && x.isCheckedOut == false).ToList();

            return Json(new { cartId = ShoppingCartId, noOfItems = cartItems.Count });
        }

        [Authorize]
        public async Task<IActionResult> paystackCallback(string cartId, string reference)
        {
            //verify if reference exists
            if (PaystackService.IsPaymentExist(reference).Result == false)
            {
                ViewData["status"] = "invalid transaction";
                return View();
            }

            else
            {
                //verify if the transaction has been checked out
                //_context.Database.ExecuteSqlCommand("SET IDENTITY_INSERT [jzofashion].[dbo].[Order] ON");

                var _cart = _context.SelectedItem.Where(x => x.CartId == cartId).ToList();
                if (_cart.First().isCheckedOut == true)
                {
                    ViewData["status"] = "invalid transaction";
                    return View();
                }

                else
                {
                    ViewData["status"] = "valid transaction";

                    
                    //set order reference Id
                    foreach (var item in _cart)
                    {
                        //set isCheckout to true
                        item.isCheckedOut = true;
                        item.OrderReferenceId = Convert.ToInt32(reference);

                        //create new order with reference
                        var order = new Order
                        {
                            dateCreated = DateTime.Now,
                            referenceId = Convert.ToInt32(reference),
                            isPending = true,
                            isShipped = false,
                            user = User.Identity.Name,
                                                        
                        };
                        _context.Order.Add(order);

                    }

                    //create new checkout record for order
                    var checkout = new Checkout
                    {
                        Items = _cart,
                        dateCreated = DateTime.Now,
                        isSold = true,
                        totalPrice = _cart.Select(x => x.totalPrice).Sum()
                    };
                    _context.Checkout.Add(checkout);
                    _context.SaveChanges();


                    //send sms to customer
                    var _user = _context.Users.Where(x => x.UserName == User.Identity.Name)
                       
                        .FirstOrDefault();

                  bool status = InfoBipService.sendMessage(_user.PhoneNumber,
                        $"Hello {_user.firstname}, " + "\n\n" +
                        "Thanks for your making an Order on jzofashion.com. Here is your reference number: " + reference + " ").Result;


                    //send email/sms to admin/store manager
                    var _allAdmins = _context.Admins.ToList();

                    foreach (var admin in _allAdmins)
                    {
                      bool status_1 = InfoBipService.sendMessage(admin.phone,
                              " You have a new Order request referenced: " + reference + " made on jzofashion.com.").Result;

                        await new AuthMessageSender().SendEmailAsync(admin.email, "Order Request. Reference: " + reference,
                            "Hi Admin, " + "<br><br>" +
                              " You have a new Order request referenced: " + reference + " made on jzofashion.com.");

                    }

                    //return success page
                    return View();

                }
            }
        }

        [HttpGet]
        public IActionResult checkRef(string reference)
        {
            return Json(new { status = PaystackService.IsPaymentExist(reference).Result });
        }

        [HttpGet]
        public IActionResult send()
        {
            return Json(new {
                status = InfoBipService.sendMessage("2347038025189", "this is test").Result,
                user = User.Identity.Name,
                phone = _context.Users.Where(x=>x.UserName == User.Identity.Name).Select(x=>x.PhoneNumber).SingleOrDefault()
            });
        }

        [HttpGet]
        public IActionResult GetShippingDetails(int orderReference)
        {

            var user_email = _context.Order.Where(x => x.referenceId == orderReference).Select(x => x.user).SingleOrDefault();
            if (user_email != null)
            {
                var user_details = _context.Users.Where(x => x.UserName == user_email).FirstOrDefault();
                return Json(new { user = user_details });

            }
            else
            {
                return Json(new { erro = "order doesnt exist" });

            }

        }
    }
}