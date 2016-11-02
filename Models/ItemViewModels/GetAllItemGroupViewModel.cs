using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace jzo.Models.ItemViewModels
{
    public class GetAllItemGroupViewModel
    {
        public ItemGroup group { get; set; }
        public List<Item> items { get; set; }
    }
}
