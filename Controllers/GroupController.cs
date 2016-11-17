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

namespace jzo.Controllers
{
    public class GroupController : Controller
    {
        private ApplicationDbContext _context;

        public const string CartSessionKey = "CartId";
        public string ShoppingCartId { get; set; }

        public GroupController(ApplicationDbContext context)
        {
            _context = context;
        }

        [Authorize]
        public IActionResult Index()
        {
            var viewModelList = new List<GetAllItemGroupViewModel>();

            var allGroups = _context.ItemGroup.ToList();
            foreach(var group in allGroups)
            {
                var group_items = _context.Item.Where(x => x.ItemGroupId == group.Id).ToList();
                var viewModel = new GetAllItemGroupViewModel
                {
                    group = group,
                    items = group_items
                };
                viewModelList.Add(viewModel);
            }
            return View(viewModelList);
        }

        [Authorize]
        public IActionResult details(int itemId)
        {
            ViewData["CartId"] = GetCartId();

            var getItemViewModel = new GetItemViewModel();
            var item = _context.Item.SingleOrDefault(x => x.Id == itemId);
            var relatedItem = _context.Item.OrderBy(x=>x.Id).Take(4).ToList();

            getItemViewModel.item = item;
            getItemViewModel.relatedItems = relatedItem;

            return View(getItemViewModel);
        }

        [HttpPost]
        //[Authorize]
        public IActionResult addToCart(int itemId, int? quantity, string size)
        {
            // Retrieve the product from the database.           
            ShoppingCartId = GetCartId();

            var cartItem = _context.SelectedItem.SingleOrDefault(
                x => x.CartId == ShoppingCartId
                && x.ItemId == itemId);

            var item = _context.Item.SingleOrDefault(x => x.Id == itemId);

            if(cartItem == null)
            {
                //set quantity
                if (!quantity.HasValue)
                {
                    quantity = 1;
                }
                //create a new cart item if no cart item exists
                cartItem = new Models.SelectedItems
                {
                    ItemId = itemId,
                    CartId = ShoppingCartId,
                    dateCreated = DateTime.Now,
                    isCheckedOut = false,
                    quantity = quantity.Value,
                    totalPrice = item.price * quantity.Value,
                    user = HttpContext.User.Identity.Name,
                    size = size
                };
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

        public IActionResult getCartItems()
        {
            ShoppingCartId = GetCartId();

            var cartItems = _context.SelectedItem.Where(x => x.CartId == ShoppingCartId).ToList();
            return View(cartItems);
        }
        public string GetCartId()
        {
            if (HttpContext.Session.GetString(CartSessionKey) == null)
            {
                if (!string.IsNullOrWhiteSpace(HttpContext.User.Identity.Name))
                {
                    // Generate a new random GUID using System.Guid class.     
                    Guid tempCartId = Guid.NewGuid();
                    HttpContext.Session.SetString("CartId", tempCartId.ToString());
                    //HttpContext.Session.SetString("CartId", HttpContext.User.Identity.Name);
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

    }
}