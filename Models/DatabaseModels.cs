using System;

namespace jzo.Models
{
    public class Order
    {
        public int Id { get; set; }
        public string description { get; set; }
        public bool  isProcessed { get; set; }
    }

}