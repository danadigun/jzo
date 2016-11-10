using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace jzo.Models.ItemViewModels
{
    public class GetItemViewModel
    {
        public Item item { get; set; }
        public List<Item> relatedItems { get; set; }
    }
}
