﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Vendor.Models.ViewModels
{
    public class StoreIndexViewModel
    {
       public IEnumerable<Store> Store { get; set; }
       public ApplicationUser User { get; set; }
    }
}
