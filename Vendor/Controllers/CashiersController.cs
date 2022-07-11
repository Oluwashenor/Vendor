//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Threading.Tasks;
//using Microsoft.AspNetCore.Mvc;
//using Microsoft.AspNetCore.Mvc.Rendering;
//using Microsoft.EntityFrameworkCore;
//using Vendor.Data;
//using Vendor.Models;

//namespace Vendor.Controllers
//{
//    public class CashiersController : Controller
//    {
//        private readonly ApplicationDbContext _context;

//        public CashiersController(ApplicationDbContext context)
//        {
//            _context = context;
//        }

//        // GET: Cashiers
//        public async Task<IActionResult> Index()
//        {
//           var applicationDbContext = _context.Cashiers.Include(c => c.Outlet);
//           return View(await applicationDbContext.ToListAsync());
//        }
//        // public void Index(int cashierId)
//        // {
//        //     //var loggedInUser = _userManager.GetUserId(User);
//        //     //var applicationDbContext = _context.Cashiers.Where().Include(c => c.Outlet);
//        //     //return View(await applicationDbContext.ToListAsync());
//        // } 

//        // GET: Cashiers/Details/5
//        public async Task<IActionResult> Details(int? id)
//        {
//            if (id == null)
//            {
//                return NotFound();
//            }

//            var cashier = await _context.Cashiers
//                .Include(c => c.Outlet)
//                .FirstOrDefaultAsync(m => m.Id == id);
//            if (cashier == null)
//            {
//                return NotFound();
//            }

//            return View(cashier);
//        }

//        public IActionResult CashierLogin()
//        {
//            return View();

//        }
        
//        [HttpPost]
//        public IActionResult CashierLogin(Cashier model)
//        {
//            var cashier = _context.Cashiers.FirstOrDefault(c => c.UserName == model.UserName);
//            if(cashier == null)
//            {
//                return View(model);
//            }
//            if(cashier.Password == model.Password)
//            {
//                return RedirectToAction("Index","Transactions", new { salesRep = cashier.Id});
//            }
//            return View();

//        }

//        // GET: Cashiers/Create
//        public IActionResult Create(int? id)
//        {

//            ViewData["OutletId"] = new SelectList(_context.Outlets, "Id", "Id", id);
//            return View();
//        }

//        // POST: Cashiers/Create
//        // To protect from overposting attacks, enable the specific properties you want to bind to.
//        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
//        [HttpPost]
//        [ValidateAntiForgeryToken]
//        public async Task<IActionResult> Create([Bind("UserName,Password,OutletId")] Cashier cashier)
//        {
//            if (ModelState.IsValid)
//            {
//                _context.Add(cashier);
//                await _context.SaveChangesAsync();
//                return RedirectToAction(nameof(Index));
//            }
//            ViewData["OutletId"] = new SelectList(_context.Outlets, "Id", "Id", cashier.OutletId);
//            return View(cashier);
//        }

//        // GET: Cashiers/Edit/5
//        public async Task<IActionResult> Edit(int? id)
//        {
//            if (id == null)
//            {
//                return NotFound();
//            }

//            var cashier = await _context.Cashiers.FindAsync(id);
//            if (cashier == null)
//            {
//                return NotFound();
//            }
//            ViewData["OutletId"] = new SelectList(_context.Outlets, "Id", "Id", cashier.OutletId);
//            return View(cashier);
//        }

//        // POST: Cashiers/Edit/5
//        // To protect from overposting attacks, enable the specific properties you want to bind to.
//        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
//        [HttpPost]
//        [ValidateAntiForgeryToken]
//        public async Task<IActionResult> Edit(int id, [Bind("Id,UserName,Password,OutletId")] Cashier cashier)
//        {
//            if (id != cashier.Id)
//            {
//                return NotFound();
//            }

//            if (ModelState.IsValid)
//            {
//                try
//                {
//                    _context.Update(cashier);
//                    await _context.SaveChangesAsync();
//                }
//                catch (DbUpdateConcurrencyException)
//                {
//                    if (!CashierExists(cashier.Id))
//                    {
//                        return NotFound();
//                    }
//                    else
//                    {
//                        throw;
//                    }
//                }
//                return RedirectToAction(nameof(Index));
//            }
//            ViewData["OutletId"] = new SelectList(_context.Outlets, "Id", "Id", cashier.OutletId);
//            return View(cashier);
//        }

//        // GET: Cashiers/Delete/5
//        public async Task<IActionResult> Delete(int? id)
//        {
//            if (id == null)
//            {
//                return NotFound();
//            }

//            var cashier = await _context.Cashiers
//                .Include(c => c.Outlet)
//                .FirstOrDefaultAsync(m => m.Id == id);
//            if (cashier == null)
//            {
//                return NotFound();
//            }

//            return View(cashier);
//        }

//        // POST: Cashiers/Delete/5
//        [HttpPost, ActionName("Delete")]
//        [ValidateAntiForgeryToken]
//        public async Task<IActionResult> DeleteConfirmed(int id)
//        {
//            var cashier = await _context.Cashiers.FindAsync(id);
//            _context.Cashiers.Remove(cashier);
//            await _context.SaveChangesAsync();
//            return RedirectToAction(nameof(Index));
//        }

//        private bool CashierExists(int id)
//        {
//            return _context.Cashiers.Any(e => e.Id == id);
//        }
//    }
//}
