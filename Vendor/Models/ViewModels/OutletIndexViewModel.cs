using System.Collections.Generic;
using System.ComponentModel;

namespace Vendor.Models.ViewModels
{
    public class OutletIndexViewModel
    {
        public IEnumerable<Outlet> Outlets { get; set; }
        public int? storeId { get; set; }
        [DisplayName("Store Name")]
        public string StoreName { get; set; }
    }
}
