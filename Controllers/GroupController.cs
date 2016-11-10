using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using jzo.Data;
using jzo.Models.ItemViewModels;
using Microsoft.AspNetCore.Authorization;

namespace jzo.Controllers
{
    [Authorize]
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

        public IActionResult details(int itemId)
        {
            var getItemViewModel = new GetItemViewModel();
            var item = _context.Item.SingleOrDefault(x => x.Id == itemId);
            var relatedItem = _context.Item.OrderBy(x=>x.Id).Take(4).ToList();

            getItemViewModel.item = item;
            getItemViewModel.relatedItems = relatedItem;

            return View(getItemViewModel);
        }

       
    }
}