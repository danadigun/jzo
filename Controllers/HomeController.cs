using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using jzo.Data;
using jzo.Models.ItemViewModels;

namespace jzo.Controllers
{
    public class HomeController : Controller
    {
        private ApplicationDbContext _context;

        public HomeController(ApplicationDbContext context)
        {
            _context = context;
        }
        public IActionResult Index()
        {
            SalesViewModel salesViewModel = new SalesViewModel();

            //top three featured collections 
            var featuredCollections = _context.ItemGroup.Take(3).ToList();
            salesViewModel.featuredCollections = featuredCollections;

            //top three featured items 
            var featuredItems = _context.Item.Take(12).ToList();
            salesViewModel.featuredItems = featuredItems;

            return View(salesViewModel);
        }

        public IActionResult About()
        {
            ViewData["Message"] = "Your application description page.";

            return View();
        }

        public IActionResult Contact()
        {
            ViewData["Message"] = "Your contact page.";

            return View();
        }

        public IActionResult Error()
        {
            return View();
        }

        //send message
        public IActionResult send(string message)
        {
            //api

            return Json(new { message = "Successfully sent message" });
        }
      
    }
}
