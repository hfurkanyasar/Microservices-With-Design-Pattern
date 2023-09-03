using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared
{
    public class PaymentSuccessedEvent
    {
        public int OrderID { get; set; }
        public string BuyerID { get; set; }
    }
}
