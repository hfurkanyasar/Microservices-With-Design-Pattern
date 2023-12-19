using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Order.API.Models
{
    public class Order
    {
        public int OrderID { get; set; }
        public DateTime CreatedDate { get; set; }
        public string BuyerID { get; set; }
        public Adress Adress { get; set; }
        public ICollection<OrderItem> orderItems { get; set; } = new List<OrderItem>();
        public OrderStatus Status { get; set; }
        public string FailMessage { get; set; }
    }
    public enum OrderStatus
    {
        Suspend,
        Complete,
        Fail
    }
}
