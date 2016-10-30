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
    [Route("api/SelectedItems")]
    public class SelectedItemsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public SelectedItemsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: api/SelectedItems
        [HttpGet]
        public IEnumerable<SelectedItems> GetSelectedItem()
        {
            return _context.SelectedItem;
        }

        // GET: api/SelectedItems/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetSelectedItems([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            SelectedItems selectedItems = await _context.SelectedItem.SingleOrDefaultAsync(m => m.Id == id);

            if (selectedItems == null)
            {
                return NotFound();
            }

            return Ok(selectedItems);
        }

        // PUT: api/SelectedItems/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutSelectedItems([FromRoute] int id, [FromBody] SelectedItems selectedItems)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != selectedItems.Id)
            {
                return BadRequest();
            }

            _context.Entry(selectedItems).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!SelectedItemsExists(id))
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

        // POST: api/SelectedItems
        [HttpPost]
        public async Task<IActionResult> PostSelectedItems([FromBody] SelectedItems selectedItems)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _context.SelectedItem.Add(selectedItems);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (SelectedItemsExists(selectedItems.Id))
                {
                    return new StatusCodeResult(StatusCodes.Status409Conflict);
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtAction("GetSelectedItems", new { id = selectedItems.Id }, selectedItems);
        }

        // DELETE: api/SelectedItems/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteSelectedItems([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            SelectedItems selectedItems = await _context.SelectedItem.SingleOrDefaultAsync(m => m.Id == id);
            if (selectedItems == null)
            {
                return NotFound();
            }

            _context.SelectedItem.Remove(selectedItems);
            await _context.SaveChangesAsync();

            return Ok(selectedItems);
        }

        private bool SelectedItemsExists(int id)
        {
            return _context.SelectedItem.Any(e => e.Id == id);
        }
    }
}