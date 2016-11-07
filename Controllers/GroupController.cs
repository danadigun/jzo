using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using jzo.Data;
using jzo.Models.ItemViewModels;

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
    }
}