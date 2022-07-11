using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Vendor.Models.ViewModels
{
    public class SalesCreateViewModel
    {
        public SalesCreateViewModel()
        {
            Menu = new List<Menu>();
        }
        public int Id { get; set; }
        public int TransactionId { get; set; }
        public int MenuId { get; set; }
        public string CashierId { get; set; }
        public int CustomerId { get; set; }
        public List<Menu> Menu { get; set; }

    }
}
