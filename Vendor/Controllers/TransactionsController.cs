using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Vendor.Data;
using Vendor.Models;
using Vendor.Models.ViewModels;

namespace Vendor.Controllers
{
    public class TransactionsController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<IdentityUser> _userManager;

        public TransactionsController(ApplicationDbContext context, UserManager<IdentityUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        // GET: Sales
        public async Task<IActionResult> Index(int? outletId)
        {
            var user = _userManager.GetUserId(User);
            //.Include(s => s.Cashier).Include(s => s.Customer).Include(s => s.Outlet)
            var applicationDbContext = _context.Transactions.Where(t=> t.OutletId == outletId);
            var transactions = await applicationDbContext.ToListAsync();
            var viewModel = new TransactionIndexViewModel()
            {
                OutletId = (int)outletId,
                Transactions = transactions,
            };
            return View(viewModel);
        }

        // GET: Sales/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var transaction = await _context.Transactions
                .Include(t => t.Cashier)
                .Include(t => t.Customer)
                .Include(t => t.Outlet)
                .FirstOrDefaultAsync(m => m.Id == id);

            var store = await _context.Stores.FirstOrDefaultAsync(s=>s.Id == transaction.Outlet.StoreId);
            var sales = await _context.Sales.Where(s => s.TransactionId == id).Include(s=>s.Menu).ToListAsync();
            if (transaction == null)
            {
                return NotFound();
            }

            var viewModel = new TransactionDetailViewModel()
            {
                Transaction = transaction,
                Sales = sales,
                Store = store,
            };

            return View(viewModel);
        }

        // GET: Sales/Create
        public IActionResult Create()
        {
        //    ViewData["CashierId"] = new SelectList(_context.Cashiers, "Id", "UserName");
            ViewData["CustomerId"] = new SelectList(_context.Customers, "Id", "UserName");
            ViewData["OutletId"] = new SelectList(_context.Outlets, "Id", "Name");
            return View();
        }

        // POST: Sales/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("OutletId,CustomerId,CashierId")] Transaction transaction)
        {
           transaction.TransactionTime = DateTime.Now;
            if (ModelState.IsValid)
            {
                _context.Add(transaction);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["CashierId"] = new SelectList(_context.ApplicationUsers, "Id", "Id", transaction.CashierId);
            ViewData["CustomerId"] = new SelectList(_context.ApplicationUsers, "Id", "Id", transaction.CustomerId);
            ViewData["OutletId"] = new SelectList(_context.Outlets, "Id", "Id", transaction.OutletId);
            return View(transaction);
        }

        // GET: Sales/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var transaction = await _context.Transactions.FindAsync(id);
            if (transaction == null)
            {
                return NotFound();
            }
            ViewData["CashierId"] = new SelectList(_context.ApplicationUsers, "Id", "Name", transaction.CashierId);
            ViewData["CustomerId"] = new SelectList(_context.ApplicationUsers, "Id", "Name", transaction.CustomerId);
            ViewData["OutletId"] = new SelectList(_context.Outlets, "Id", "Name", transaction.OutletId);
            return View(transaction);
        }

        // POST: Sales/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("OutletId,CustomerId,CashierId,TransactionTime")] Transaction transaction)
        {
            if (id != transaction.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(transaction);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TransactionExists(transaction.Id))
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
            ViewData["CashierId"] = new SelectList(_context.ApplicationUsers, "Id", "Id", transaction.CashierId);
            ViewData["CustomerId"] = new SelectList(_context.ApplicationUsers, "Id", "Id", transaction.CustomerId);
            ViewData["OutletId"] = new SelectList(_context.Outlets, "Id", "Id", transaction.OutletId);
            return View(transaction);
        }

        // GET: Sales/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var transaction = await _context.Transactions
                .Include(t => t.Cashier)
                .Include(t => t.Customer)
                .Include(t => t.Outlet)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (transaction == null)
            {
                return NotFound();
            }

            return View(transaction);
        }

        // POST: Sales/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var sale = await _context.Transactions.FindAsync(id);
            _context.Transactions.Remove(sale);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool TransactionExists(int id)
        {
            return _context.Transactions.Any(e => e.Id == id);
        }
    }
}
