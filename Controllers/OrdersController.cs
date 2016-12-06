using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using jzo.Data;
using jzo.Models;
using System.Net.Http;
using System.Net.Http.Headers;
using jzo.Models.PaystackModels;
using Newtonsoft.Json;
using Microsoft.AspNetCore.Authorization;

namespace jzo.Controllers
{
    [Produces("application/json")]
    [Route("api/Orders")]
    public class OrdersController : Controller
    {
        private readonly ApplicationDbContext _context;
        private string paymentUrl = "https://api.paystack.co/transaction/initialize";

        public OrdersController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: api/Orders
        [HttpGet]
        public IEnumerable<Order> GetOrder()
        {
            return _context.Order;
        }

        // GET: api/Orders/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetOrder([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            Order order = await _context.Order.SingleOrDefaultAsync(m => m.Id == id);
            List<SelectedItems> orderItems = _context.SelectedItem.Where(x => x.order.Id == id).ToList();

            if (order == null)
            {
                return NotFound();
            }

            return Json(new { id = order.Id, orderItems = orderItems });
        }

        // PUT: api/Orders/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutOrder([FromRoute] int id, [FromBody] Order order)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != order.Id)
            {
                return BadRequest();
            }

            _context.Entry(order).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!OrderExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/Orders
        //[HttpPost]
        //public async Task<IActionResult> PostOrder([FromBody] Order order)
        //{
        //    if (!ModelState.IsValid)
        //    {
        //        return BadRequest(ModelState);
        //    }

        //    _context.Order.Add(order);
        //    try
        //    {
        //        await _context.SaveChangesAsync();
        //    }
        //    catch (DbUpdateException)
        //    {
        //        if (OrderExists(order.Id))
        //        {
        //            return new StatusCodeResult(StatusCodes.Status409Conflict);
        //        }
        //        else
        //        {
        //            throw;
        //        }
        //    }
        //    return Json(new { id = order.Id, status = "success" });
        //}

        // DELETE: api/Orders/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteOrder([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            Order order = await _context.Order.SingleOrDefaultAsync(m => m.Id == id);
            if (order == null)
            {
                return NotFound();
            }

            _context.Order.Remove(order);
            await _context.SaveChangesAsync();

            return Ok(order);
        }

        private bool OrderExists(int id)
        {
            return _context.Order.Any(e => e.Id == id);
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> PostOrder(string cartId)
        {
           // get total items purchased
            var order = _context.SelectedItem.Where(x => x.CartId == cartId).ToList();
            decimal amount = 0m;
            foreach (var item in order)
            {
                amount = amount + (_context.Item.SingleOrDefault(x => x.Id == item.Id).price) * item.quantity;
            }

            //process Paystack payment
            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri(paymentUrl);

            // Add an Accept header for JSON format.
            client.DefaultRequestHeaders.Accept.Add(
            new MediaTypeWithQualityHeaderValue("application/x-www-form-urlencoded"));

            //add authorization header for paystack
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", "sk_test_f26f77b1e6f0890258f40bec1026de5d9733ca9d");

            //post payment request
            var content = new FormUrlEncodedContent(new[]
            {
                new KeyValuePair<string, string>("reference", new Random().Next().ToString()),
                new KeyValuePair<string, string>("email", User.Identity.Name),
                new KeyValuePair<string, string>("amount", amount.ToString())
            });

            var response = await client.PostAsync(paymentUrl, content);

            var jsonResponse = await response.Content.ReadAsStringAsync();
            if(jsonResponse != null)
            {
                var transaction = JsonConvert.DeserializeObject<Jsonresponse>(jsonResponse);
                return Json(new { url = transaction.data.authorization_url });

            }
            else
            {
                return Json(new { message = "unable to generate transaction url" });

            }

        }
    }
}