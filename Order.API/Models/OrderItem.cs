using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Order.API.Models
{
    public class OrderItem
    {

        public int OrderItemID { get; set; }
        public int ProductID { get; set; }

        [Column(TypeName ="decimal(18,2)")] // toplam 18 karakter 2 tane virgülden sonra demek.
        public decimal Price { get; set; }
        public int OrderID { get; set; }
        public Order Order { get; set; }
        public int Count { get; set; }
    }
}
