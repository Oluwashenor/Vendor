using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using static Vendor.Utilities.Enums;

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
        [DisplayName("Items")]
        public int MenuId { get; set; }
        public string CashierId { get; set; }
        public int CustomerId { get; set; }
        public List<Menu> Menu { get; set; }
        public Outlet outlet { get; set; }
        public Store store { get; set; }
        [EnumDataType(typeof(PaymentTypes))]
        public PaymentTypes PaymentTypes { get; set; }


    }
}
