using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace jzo.Models
{
    public class Order
    {
        public int Id { get; set; }
        public int referenceId { get; set; }
        public string description { get; set; }
        public bool  isPending { get; set; }
        public bool isShipped { get; set; }
        public DateTime dateCreated { get; set; }
        public DateTime dateShipped{ get; set; }
        public string user { get; set; }
    }

    public class ItemGroup
    {
        public int Id { get; set; }
        public string name { get; set; }
        public string description { get; set; }
        public string imageUrl { get; set; }

    }

    public class Item
    {
        public int Id { get; set; }
        public string image_url { get; set; }
        public decimal price { get; set; }
        public string name { get; set; }
        public string description { get; set; }
        public DateTime dateCreated { get; set; }
        public DateTime dateModified { get; set; }
        public int ItemGroupId { get; set; }
        public int quantity { get; set; }

    }

    public class SelectedItems
    {
        public int Id { get; set; }
        public string CartId { get; set; }
        public int ItemId { get; set; }
        public DateTime dateCreated { get; set; }
        public int quantity { get; set; }     
        public decimal totalPrice { get; set; }
        public bool isCheckedOut{ get; set; }
        public int OrderReferenceId { get; set; }
        public string user { get; set; }

        //size of dress ordered
        public string size { get; set; }
    }

    public class Checkout
    {
        public int Id { get; set; }
        public DateTime dateCreated { get; set; }
        public List<SelectedItems> Items { get; set; }
        public int Quantity { get; set; }
        public decimal totalPrice { get; set; }
        public bool isSold { get; set; }
        public ApplicationUser user { get; set; }
    }

    public class CustomOrder
    {
        public int Id { get; set; }
        public int OrderId { get; set; }
        public int ItemId { get; set; }
        public string ItemName { get; set; }
        public decimal neck { get; set; }
        public decimal shoudler { get; set; } 
        public decimal mid { get; set; }
        public decimal chest { get; set; }
        public decimal waist { get; set; }
        public decimal shortSleeve { get; set; }
        public decimal longSleeve { get; set; }
        public decimal forearm { get; set; }
        public decimal wrist { get; set; }
        public decimal bicep { get; set; }
        public decimal bodyLength { get; set; }
        public decimal kaftanLength { get; set; }
        public decimal trouserWaist { get; set; }
        public decimal hip { get; set; }
        public decimal thigh { get; set; }
        public decimal knee { get; set; }
        public decimal feet { get; set; }
        public decimal trouserLength { get; set; }
    }
}