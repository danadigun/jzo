using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using jzo.Data;
using jzo.Models;

namespace jzo.Controllers
{
    [Produces("application/json")]
    [Route("api/Checkouts")]
    public class CheckoutsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public CheckoutsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: api/Checkouts
        [HttpGet]
        public IEnumerable<Checkout> GetCheckout()
        {
            return _context.Checkout;
        }

        // GET: api/Checkouts/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetCheckout([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            Checkout checkout = await _context.Checkout.SingleOrDefaultAsync(m => m.Id == id);

            if (checkout == null)
            {
                return NotFound();
            }

            return Ok(checkout);
        }

        // PUT: api/Checkouts/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutCheckout([FromRoute] int id, [FromBody] Checkout checkout)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != checkout.Id)
            {
                return BadRequest();
            }

            _context.Entry(checkout).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CheckoutExists(id))
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

        // POST: api/Checkouts
        [HttpPost]
        public async Task<IActionResult> PostCheckout([FromBody] Checkout checkout)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _context.Checkout.Add(checkout);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (CheckoutExists(checkout.Id))
                {
                    return new StatusCodeResult(StatusCodes.Status409Conflict);
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtAction("GetCheckout", new { id = checkout.Id }, checkout);
        }

        // DELETE: api/Checkouts/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCheckout([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            Checkout checkout = await _context.Checkout.SingleOrDefaultAsync(m => m.Id == id);
            if (checkout == null)
            {
                return NotFound();
            }

            _context.Checkout.Remove(checkout);
            await _context.SaveChangesAsync();

            return Ok(checkout);
        }

        private bool CheckoutExists(int id)
        {
            return _context.Checkout.Any(e => e.Id == id);
        }
    }
}