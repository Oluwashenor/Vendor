using System.Collections.Generic;
using System.Security.Claims;

namespace Vendor.Data
{
    public class ClaimStore
    {
        public static List<Claim> claimsList = new List<Claim>()
        {
            new Claim("Create Store", "Create Store"),
            new Claim("Edit Store", "Edit Store"),
            new Claim("Delete Store", "Delete Store"),
            
            new Claim("Create User", "Create User"),
            new Claim("Edit User", "Edit User"),
            new Claim("Delete User", "Delete User"),
           
            new Claim("Create Transaction", "Create Transaction"),
            new Claim("Edit Transaction", "Edit Transaction"),
            new Claim("Delete Transaction", "Delete Transaction"),

        };
    }
}
