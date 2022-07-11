using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Vendor.Models.DTOs
{
    public class CreateMenuDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public double Amount { get; set; }
        public int OutletId { get; set; }
        public int Quantity { get; set; }
    }
}
