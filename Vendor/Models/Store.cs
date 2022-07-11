using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Vendor.Models
{
    public class Store
    {
        public Store()
        {
            Outlets = 1;
        }
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public ApplicationUser User { get; set; }
        public string UserId { get; set; }
        public int Outlets { get; set; }
        public bool IsActive { get; set; }
        public bool IsLocked { get; set; }
        public bool IsDeleted { get; set; }
      

    }
}
