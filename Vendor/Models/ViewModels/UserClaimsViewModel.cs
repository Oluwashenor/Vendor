using System.Collections.Generic;

namespace Vendor.Models.ViewModels
{
    public class UserClaimsViewModel
    {
        public UserClaimsViewModel()
        {
            claims = new List<UserClaim>();
        }
        public List<UserClaim> claims { get; set; }
        public string RoleId { get; set; }
    }

    public class UserClaim
    {
        public string ClaimName { get; set; }
        public bool isSelected { get; set; }
    }
}
