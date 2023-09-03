using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared
{
    class PaymantFailedEvent
    {
        public int OrderID { get; set; }
        public string BuyerID { get; set; }
        public string Message { get; set; }
    }
}
