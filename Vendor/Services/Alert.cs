using Vendor.Constants;
using Vendor.Controllers;

namespace Vendor.Services
{
    public class Alert : BaseController
    {
        public string Message { get; set; }

        public void Display(string message )
        {
            TempData[SD.Success] = message;
        }

        public void DisplayError(string message)
        {
            TempData[SD.Error] = message;
        }
    }


 
}
