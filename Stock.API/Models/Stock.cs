using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Stock.API.Models
{
    public class Stock
    {
        public int StockID { get; set; }
        public int ProductID { get; set; }
        public int Count { get; set; }
    }
}
