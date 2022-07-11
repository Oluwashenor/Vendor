using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Vendor.Data;
using Vendor.Models;
using Vendor.Models.ViewModels;

namespace Vendor.Controllers
{
    [Authorize]
    public class SalesController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<IdentityUser> _userManager;


        public SalesController(ApplicationDbContext context, UserManager<IdentityUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        // GET: Sales
        public async Task<IActionResult> Index(int? outletId)
        {
            var applicationDbContext = _context.Sales.Include(s => s.Menu).Include(s => s.Transaction);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: Sales/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var sale = await _context.Sales
                .Include(s => s.Menu)
                .Include(s => s.Transaction)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (sale == null)
            {
                return NotFound();
            }

            return View(sale);
        }

        // GET: Sales/Create
        public IActionResult Create(int outletId)
        {
            
            //ViewData["TransactionId"] = new SelectList(_context.Transactions, "Id", "Id");
            var menu = _context.Menus.Where(m=>m.OutletId== outletId).ToList();
            var viewModel = new SalesCreateViewModel()
            {
                Menu = menu
            };
            ViewData["MenuId"] = new SelectList(menu, "Id", "Name");
            ViewData["OutletId"] = outletId;
            return View(viewModel);
        }

        // POST: Sales/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(SalesCreateViewModel sale)
        {

          
            var transaction = new Transaction()
            {
                OutletId = 1,
                CashierId = _context.ApplicationUsers.FirstOrDefault(c=>c.Id == sale.CashierId).Id,
                CustomerId = _context.Customers.FirstOrDefault(c=>c.Id ==sale.CustomerId).Id,
            };
            _context.Transactions.Add(transaction);
           var saveTransaction = await _context.SaveChangesAsync();
            if (saveTransaction.Equals(0))
            {
                return BadRequest();
            }
            if (ModelState.IsValid)
            {
                var menu1 = new Menu
                {
                    Id = 1,
                    Amount = 1000
                };
                var menu2 = new Menu
                {
                    Id = 3,
                    Amount = 500
                };

                sale.Menu.Add(menu1); sale.Menu.Add(menu2);
                foreach (var item in sale.Menu)
                {
                    var s = new Sale()
                    {
                        TransactionId = transaction.Id,
                        MenuId = item.Id
                    };
                    _context.Add(s);
                    await _context.SaveChangesAsync();
                }
               
                return RedirectToAction(nameof(Index));
            }
            ViewData["MenuId"] = new SelectList(_context.Menus, "Id", "Id", sale.MenuId);
            ViewData["TransactionId"] = new SelectList(_context.Transactions, "Id", "Id", sale.TransactionId);
            return View(sale);


        }

        // GET: Sales/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var sale = await _context.Sales.FindAsync(id);
            if (sale == null)
            {
                return NotFound();
            }
            ViewData["MenuId"] = new SelectList(_context.Menus, "Id", "Id", sale.MenuId);
            ViewData["TransactionId"] = new SelectList(_context.Transactions, "Id", "Id", sale.TransactionId);
            return View(sale);
        }

        // POST: Sales/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,TransactionId,MenuId,IsDeleted")] Sale sale)
        {
            if (id != sale.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(sale);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!SaleExists(sale.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["MenuId"] = new SelectList(_context.Menus, "Id", "Id", sale.MenuId);
            ViewData["TransactionId"] = new SelectList(_context.Transactions, "Id", "Id", sale.TransactionId);
            return View(sale);
        }

        // GET: Sales/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var sale = await _context.Sales
                .Include(s => s.Menu)
                .Include(s => s.Transaction)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (sale == null)
            {
                return NotFound();
            }

            return View(sale);
        }

        // POST: Sales/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var sale = await _context.Sales.FindAsync(id);
            _context.Sales.Remove(sale);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool SaleExists(int id)
        {
            return _context.Sales.Any(e => e.Id == id);
        }
    }
}
