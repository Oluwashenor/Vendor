using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Vendor.Models
{
    public class Menu
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public double Amount { get; set; }
        public Outlet Outlet { get; set; }
        public int OutletId { get; set; }
    }
}
