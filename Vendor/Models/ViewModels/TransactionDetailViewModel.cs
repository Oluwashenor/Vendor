using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Vendor.Models.ViewModels
{
    public class TransactionDetailViewModel
    {

        public TransactionDetailViewModel()
        {

            //foreach (var sale in Sales)
            //{
            //    Amount = sale.Menu.Amount + Amount;
            //}
            Amount = 100;
        }
        public Transaction Transaction { get; set; }
        public List<Sale> Sales { get; set; }
        public Store Store { get; set; }
        public Double Amount { get; set; }
    }
}
