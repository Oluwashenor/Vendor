using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Vendor.Constants;
using Vendor.Models;

namespace Vendor.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly SignInManager<IdentityUser> _signInManager;

        public HomeController(ILogger<HomeController> logger, SignInManager<IdentityUser> signInManager)
        {
            _logger = logger;
            _signInManager = signInManager;
        }

        public IActionResult Index()
        {
          
          if(_signInManager.IsSignedIn(User)){
            if(User.IsInRole(Roles.Uncleshenor)){
                TempData[SD.Success] = "Welcome Uncle Shenor";
            }
            else if(User.IsInRole(Roles.StoreOwner)){
                TempData[SD.Success] = "Welcome {0}, Please click on store to create a store";
            }
            else if(User.IsInRole(Roles.Cashier)){
                TempData[SD.Success] = "Welcome {0}, Please click on store to create a store";
            }
            else{
                 TempData[SD.Success] = "You have not been assigned a role yet";
            }
            
          }
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
