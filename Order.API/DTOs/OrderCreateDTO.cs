using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Order.API.DTOs
{
    public class OrderCreateDTO
    {
        public List<OrderItemDTO> orderItems { get; set; }
        public string BuyerID { get; set; }
        public PaymentDTO payment { get; set; }
        public AdresDTO adres { get; set; }


    }
    public class PaymentDTO
    {
        public string CardName { get; set; }
        public string CardNumber { get; set; }
        public string Expiration { get; set; }
        public string CVV { get; set; }
        public string TotalPrice { get; set; }


    }
    public class OrderItemDTO
    {

        public int ProductID { get; set; }
        public int Count { get; set; }
        public decimal Price { get; set; }
    }
    public class AdresDTO
    {
        public string Line { get; set; }
        public string Province { get; set; }
        public string District { get; set; }

    }
}
