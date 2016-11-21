using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace jzo.Models.ItemViewModels
{
    public class SalesViewModel
    {
        public List<ItemGroup> featuredCollections { get; set; }
        public List<Item> featuredItems { get; set; }
    }
}
