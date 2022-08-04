using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Vendor.Constants
{
    public class Constant
    {

    }

 
    public static class Roles
    {
        public static List<string> AllRoles = new List<string>
        {
            Uncleshenor, StoreOwner, Admin, Cashier
        };
        public const string Uncleshenor = "UncleShenor";
        public const string StoreOwner = "StoreOwner";
        public const string Admin = "Admin";
        public const string Cashier = "Cashier";
    }

        public static class SD
    {
        public static string Success = "Success";
        public static string Error = "Error";
        public static string Warning = "Warning";
        public static string Info = "Info";
    }
}