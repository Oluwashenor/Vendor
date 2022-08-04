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
    public class HomeController : BaseController
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
                    DisplayAlert("Welcome Uncle Shenor");
                }
            else if(User.IsInRole(Roles.StoreOwner)){
                    DisplayAlert("Welcome");
                    
            }
            else if(User.IsInRole(Roles.Cashier)){
                    DisplayAlert("Welcome {0}, Please click on store to create a store");
            }
            else{
                    DisplayAlert("You have not been assigned a role yet");
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
