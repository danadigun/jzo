using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using jzo.Data;

namespace jzo.Controllers
{
    public class GroupController : Controller
    {
        private ApplicationDbContext _context;

        public GroupController(ApplicationDbContext context)
        {
            _context = context;
        }
        public IActionResult Index()
        {           
            return View(_context.ItemGroup.ToList());
        }
    }
}