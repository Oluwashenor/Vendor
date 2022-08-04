using Microsoft.AspNetCore.Mvc;
using Vendor.Constants;
using Vendor.Data;

namespace Vendor.Controllers
{
    public class BaseController : Controller
    {
      
        public BaseController()
        {
           
        }
        public void DisplayAlert(string message)
        {
            TempData[SD.Success] = message;
        }

        public void DisplayError(string message)
        {
            TempData[SD.Error] = message;
        }

        public bool UserValid(string userId, string objectId)
        {
          
            if(userId != objectId)
            {
                DisplayError("You can not access this property as it is not assigned to you");
                return false;
               
            }
            return true;
           
        }

    }
}
