using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;
using System.Transactions;
using Vendor.Data;

namespace Vendor.Controllers
{
    public class AccountsController : BaseController
    {

        private ApplicationDbContext _context;
        private readonly UserManager<IdentityUser> _userManager;

        public AccountsController(ApplicationDbContext context, UserManager<IdentityUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }
        public async Task<IActionResult> Index(int? id, string Range="Daily")
        {
            var outlet = _context.Outlets.Find(id);
            if (outlet == null)
            {
                return NotFound();
            }
            var loggedInUser = _userManager.GetUserId(User);
            var outletOwner = _context.Stores.Find(outlet.StoreId);
            if (!UserValid(loggedInUser, outletOwner.UserId))
            {
                return Unauthorized();
            }
           
            var transactions = await _context.Transactions.Where(t => t.OutletId == outlet.Id).ToListAsync();
            var data =  transactions.GroupBy(t => t.TransactionTime.Month).Select(t => new
            {
                Name = "Monthly Transaction",
                Date = t.Select(t => t.TransactionTime.Month).First(),
                Sum = t.Select(t => t.Amount).Sum(),
            });
            return View(); 
            //return Ok(data);
        }
    }
}
