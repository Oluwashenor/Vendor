using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Vendor.Constants;
using Vendor.Data;
using Vendor.Models;

namespace Vendor.Controllers
{
    public class OutletsController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<IdentityUser> _userManager;

        public OutletsController(ApplicationDbContext context, UserManager<IdentityUser> userManager) 
        {
            _context = context;
            _userManager = userManager;
        }

        // GET: Outlets
        public async Task<IActionResult> Index(int? storeId)
        {
            //if(storeId == null)
            //{
            //    return NotFound();
            //}
            var user = _userManager.GetUserId(User);
            var stores = _context.Stores.Where(s =>s.UserId == user);
            var outlets =new List<Outlet>();
            foreach (var store in stores)
            {
                var outletForStore = await _context.Outlets.Include(l => l.Store).Where(o => o.StoreId == store.Id).ToListAsync();
                foreach (var outlet in outletForStore)
                {
                    outlets.Add(outlet);
                }
            }
            return View(outlets);
        }

        // GET: Outlets/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var outlet = await _context.Outlets
                .Include(o => o.Store)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (outlet == null)
            {
                return NotFound();
            }

            return View(outlet);
        }

        // GET: Outlets/Create
        public IActionResult Create()
        {
            //ViewData["StoreId"] = new SelectList(_context.Stores, "Id", "Name");
            var loggedInUser = _userManager.GetUserId(User);
            var selectList = _context.Stores.Where(s=> s.UserId == loggedInUser);
            ViewBag.StoreId = new SelectList(selectList, "Id", "Name");
            return View();
        }

        // POST: Outlets/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Name,StoreId")] Outlet outlet)
        {
            if (ModelState.IsValid)
            {
                _context.Add(outlet);
                await _context.SaveChangesAsync();
                  TempData[SD.Success] = "Outlet Created Successfully, Please proceed to creating Menus for this store";
                return RedirectToAction("Index", new { storeId = outlet.StoreId });
            }
            ViewData["StoreId"] = new SelectList(_context.Stores, "Id", "Id", outlet.StoreId);
              TempData[SD.Error] = "Unable to Create Outlet";
            return View(outlet);
        }

        // GET: Outlets/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var outlet = await _context.Outlets.FindAsync(id);
            if (outlet == null)
            {
                return NotFound();
            }
            ViewData["StoreId"] = new SelectList(_context.Stores, "Id", "Id", outlet.StoreId);
            return View(outlet);
        }

        // POST: Outlets/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,StoreId")] Outlet outlet)
        {
            if (id != outlet.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(outlet);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!outletExists(outlet.Id))
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
            ViewData["StoreId"] = new SelectList(_context.Stores, "Id", "Id", outlet.StoreId);
            return View(outlet);
        }

        // GET: Outlets/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var outlet = await _context.Outlets
                .Include(l => l.Store)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (outlet == null)
            {
                return NotFound();
            }

            return View(outlet);
        }

        // POST: Outlets/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var outlet = await _context.Outlets.FindAsync(id);
            _context.Outlets.Remove(outlet);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool outletExists(int id)
        {
            return _context.Outlets.Any(e => e.Id == id);
        }
    }
}
