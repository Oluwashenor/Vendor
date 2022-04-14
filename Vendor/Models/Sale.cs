using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Vendor.Models
{
    public class Sale
    {
        public int Id { get; set; }
        public ApplicationUser Customer { get; set; }
        public string CustomerId { get; set; }
        public ApplicationUser Cashier { get; set; }
        public string CashierId { get; set; }
    }
}
