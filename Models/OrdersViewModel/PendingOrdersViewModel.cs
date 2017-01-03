using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace jzo.Models.OrdersViewModel
{
    public class PendingOrders
    {
        public List<PendingOrder> pendingOrders { get; set; }
    }
    public class PendingOrder
    {
        public string Name { get; set; }
        public string email { get; set; }
        public string phone { get; set; }
        public string address { get; set; }
        public int referenceId { get; set; }
        public bool isShipped { get; set; }
        public DateTime dateCreated { get; set; }

        //Selected Items from Cart
        public List<PurchasedItem> items { get; set; }

        //total price of Order
        public decimal totalPriceOfOrder { get; set; }
    }

    public class PurchasedItem
    {
        public string name { get; set; }
        public string image_url { get; set; }
        public decimal price { get; set; }
        public int qty { get; set; }
        public decimal totalPrice { get; set; }
        public string size { get; set; }
    }
}
