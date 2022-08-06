using System;

namespace Vendor.Models.DTOs
{
    public class ViewAccountDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public double Amount { get; set; }
        public DateTime Period { get; set; }
        public int Month { get; set; }
        public int Day { get; set; }
        public string Unique { get; set; }

    }
}
