using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Vendor.Models
{
    public class Location
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public Store Store { get; set; }
        public int StoreId { get; set; }
    }
}
