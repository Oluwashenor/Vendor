﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using static Vendor.Utilities.Enums;

namespace Vendor.Models.DTOs
{
    public class CreateSalesDTO
    {
        public string CashierId { get; set; }
        public int CustomerId { get; set; }
        public int OutletId { get; set; }
        public double Amount { get; set; }
        public List<CreateMenuDTO> Menus { get; set; }
        public int PaymentType { get; set; }
    }
}
