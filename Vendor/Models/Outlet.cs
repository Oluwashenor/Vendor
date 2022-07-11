using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Vendor.Models
{
    public class Outlet
    {
        public Outlet()
        {
            PointOfSales = 1;
        }
        public int Id { get; set; }
        public string Name { get; set; }
        public Store Store { get; set; }
        public int StoreId { get; set; }
        public int PointOfSales { get; set; }
    }
}
