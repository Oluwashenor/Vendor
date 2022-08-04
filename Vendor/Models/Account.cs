using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using static Vendor.Utilities.Enums;

namespace Vendor.Models
{
    public class Account
    {
        public Account()
        {
            TransactionTime = DateTime.Now;
        }
        public int Id { get; set; }
        public Outlet Outlet { get; set; }
        public int OutletId { get; set; }
        public Customer Customer { get; set; }
        public int CustomerId { get; set; }
        public ApplicationUser Cashier { get; set; }
        public string CashierId { get; set; }
        public DateTime TransactionTime { get; set; }
        public double Amount { get; set; }
        public int PaymentType { get; set; }
    }
}
