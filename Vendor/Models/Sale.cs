using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Vendor.Models
{
    public class Sale
    {
        public int Id { get; set; }
        public int TransactionId { get; set; }
        public Transaction Transaction { get; set; }
        public int MenuId { get; set; }
        public Menu Menu { get; set; }
        public int IsDeleted { get; set; }
        public int Quantity { get; set; }

    }
}
