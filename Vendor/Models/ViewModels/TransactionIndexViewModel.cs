using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Vendor.Models.ViewModels
{
    public class TransactionIndexViewModel
    {
        public int OutletId { get; set; }
        public IEnumerable<Transaction> Transactions { get; set; }
    }
}
