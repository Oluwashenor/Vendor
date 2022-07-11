﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Vendor.Models
{
    public class Transaction
    {
        public Transaction()
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
    }
}
