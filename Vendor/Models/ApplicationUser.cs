using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Vendor.Models
{
    public class ApplicationUser : IdentityUser
    {
        public ApplicationUser()
        {
            MaxStores = 0;
        }
        public string Name { get; set; }
        public int MaxStores { get; set; }
        [NotMapped]
        public string Role { get; set; }
    }
}
