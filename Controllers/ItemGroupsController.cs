using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using jzo.Data;
using jzo.Models;
using jzo.Models.ItemViewModels;

namespace jzo.Controllers
{
    [Produces("application/json")]
    [Route("api/ItemGroups")]
    public class ItemGroupsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public ItemGroupsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: api/ItemGroups
        [HttpGet]
        public IEnumerable<GetAllItemGroupViewModel> GetItemGroup()
        {
            var  itemGroupViewModel = new List<GetAllItemGroupViewModel>();

            var itemGroups = _context.ItemGroup.ToList();

            foreach(var group in itemGroups)
            {
                var itemInItemGroup = _context.Item.Where(x => x.itemGroup.Id == group.Id).ToList();
                itemGroupViewModel.Add(new GetAllItemGroupViewModel
                {
                    group = group,
                    items = itemInItemGroup
                });
            }
            return itemGroupViewModel;
        }

        // GET: api/ItemGroups/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetItemGroup([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            ItemGroup itemGroup = await _context.ItemGroup.SingleOrDefaultAsync(m => m.Id == id);
            List<Item> items =  _context.Item.Where(x => x.itemGroup.Id == id).ToList();

            if (itemGroup == null)
            {
                return NotFound();
            }

            return Json(new {id = id, itemGroup = itemGroup, items = items });
        }

        // PUT: api/ItemGroups/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutItemGroup([FromRoute] int id, [FromBody] ItemGroup itemGroup)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != itemGroup.Id)
            {
                return BadRequest();
            }

            _context.Entry(itemGroup).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ItemGroupExists(id))
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

        // POST: api/ItemGroups
        [HttpPost]
        public async Task<IActionResult> PostItemGroup([FromBody] ItemGroup itemGroup)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _context.ItemGroup.Add(itemGroup);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (ItemGroupExists(itemGroup.Id))
                {
                    return new StatusCodeResult(StatusCodes.Status409Conflict);
                }
                else
                {
                    throw;
                }
            }

            return Json(new { id = itemGroup.Id, status = "success" });
        }

        // DELETE: api/ItemGroups/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteItemGroup([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            ItemGroup itemGroup = await _context.ItemGroup.SingleOrDefaultAsync(m => m.Id == id);
            if (itemGroup == null)
            {
                return NotFound();
            }

            _context.ItemGroup.Remove(itemGroup);
            await _context.SaveChangesAsync();

            return Ok(itemGroup);
        }

        private bool ItemGroupExists(int id)
        {
            return _context.ItemGroup.Any(e => e.Id == id);
        }
    }
}