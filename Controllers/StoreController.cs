using jzo.Data;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace jzo.Controllers
{
    public class StoreController : Controller
    {
        private ApplicationDbContext _context;

        public StoreController(ApplicationDbContext context)
        {
            _context = context;
        }
        public IActionResult Index(int? groupId)
        {
            //return all items in store
            if (groupId.HasValue)
            {
                ViewData["CollectionName"] = _context.ItemGroup.SingleOrDefault(x => x.Id == groupId.Value).name + " pieces";
                return View(_context.Item.Where(x => x.ItemGroupId == groupId.Value).ToList());
            }
            ViewData["CollectionName"] = "Our Pieces";
            return View(_context.Item.ToList());
        }
    }
}
