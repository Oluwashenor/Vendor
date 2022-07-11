using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Vendor.Models.ViewModels
{
    public class MenuIndexViewModel
    {
        public IEnumerable<Menu> Menus { get; set; }
        public int OutletId { get; set; }
    }
}
