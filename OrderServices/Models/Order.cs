﻿using System.ComponentModel.DataAnnotations.Schema;

namespace OrderServices.Models
{
    public class Order
    {
        public int OrderId { get; set; }
        public DateTime OrderDate { get; set; }
        public int Quantity { get; set; }   
        //public decimal Price { get; set; }
        public string username { get; set; }
        public int ProductId { get; set; }
        //public virtual Product Product { get; set; }
    }
}
