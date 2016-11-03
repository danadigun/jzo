using System;
using System.Collections.Generic;

namespace jzo.Models
{
    public class Order
    {
        public int Id { get; set; }
        public string description { get; set; }
        public bool  isPending { get; set; }
        public bool isShipped { get; set; }
        public DateTime dateCreated { get; set; }
        public DateTime dateShipped{ get; set; }
        public ApplicationUser user { get; set; }
    }

    public class ItemGroup
    {
        public int Id { get; set; }
        public string name { get; set; }
        public string description { get; set; }

    }

    public class Item
    {
        public int Id { get; set; }
        public string image_url { get; set; }
        public decimal price { get; set; }
        public string name { get; set; }
        public string description { get; set; }
        public DateTime dateCreated { get; set; }
        public ItemGroup itemGroup { get; set; }
        public int quantity { get; set; }

    }

    public class SelectedItems
    {
        public int Id { get; set; }
        public DateTime dateCreated { get; set; }
        public Item item { get; set; }
        public int quantity { get; set; }     
        public decimal totalPrice { get; set; }
        public bool isCheckedOut{ get; set; }
        public Order order { get; set; }
        public ApplicationUser user { get; set; }
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
}