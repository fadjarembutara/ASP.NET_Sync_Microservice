using OrderServices.Models;
using System.ComponentModel.DataAnnotations.Schema;

namespace OrderServices.Dtos
{
    public class ReadOrderDto
    {
        // public int OrderId { get; set; }
        // public DateTime OrderDate { get; set; }
        // public int Quantity { get; set; }
        // public int TotalPay { get; set; }
        // public string Username {get; set; }
        // public int ProductId { get; set; }

        public int OrderId { get; set; }
        public DateTime OrderDate { get; set; }
        public int Quantity { get; set; }   
        //public decimal Price { get; set; }
        public string username { get; set; }
        public int ProductId { get; set; }
        //public virtual Product Product { get; set; }
        public int TotalPay {get; set; }
    }
}
