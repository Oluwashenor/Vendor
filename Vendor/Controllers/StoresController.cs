﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Vendor.Constants;
using Vendor.Data;
using Vendor.Models;
using Vendor.Models.DTOs;
using Vendor.Models.ViewModels;

namespace Vendor.Controllers
{
    public class StoresController : BaseController
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<IdentityUser> _userManager;
        private readonly SignInManager<IdentityUser> _signInManager;

        public StoresController(ApplicationDbContext context, UserManager<IdentityUser> userManager, SignInManager<IdentityUser> signInManager) 
        {
            _context = context;
            _signInManager = signInManager;
            _userManager = userManager;
        }

        // GET: Stores
        public async Task<IActionResult> Index()
        {
            if (User.IsInRole("Cashier"))
            {
                return BadRequest();
            }
            if (User.IsInRole("StoreOwner"))
            {
                var loggedInUser = _userManager.GetUserId(User);
                var user = _context.ApplicationUsers.Where(u => u.Id == loggedInUser).FirstOrDefault();
                var stores = _context.Stores.Where(s => s.UserId == loggedInUser).Include(s => s.User);
                var data = await stores.ToListAsync();
                if(data.Count() < 1){
                    if(user.MaxStores < 1)
                    {
                        DisplayAlert("Welcome, Please Reach out to Admin to Enable you Create A Store");
                       
                    }
                    else
                    {
                        DisplayAlert("Welcome, Please Click on \'Create New\' to Begin");

                    }
                }
               
                var viewModel = new StoreIndexViewModel()
                {
                    Store = data,
                    User = user
                };
                return View(viewModel);
            }
            if (User.IsInRole("UncleShenor"))
            {
                var stores =await _context.Stores.Include(s=>s.User).ToListAsync();
                var viewModel = new StoreIndexViewModel()
                {
                    Store = stores
                };
                return View("IndexSuperAdmin", viewModel);
            }
            else
            {
                return BadRequest();
            }
           
        }

        // GET: Stores/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var loggedInUser = _userManager.GetUserId(User);
            var store = await _context.Stores
                .Include(s => s.User)
                .FirstOrDefaultAsync(m => m.Id == id);

            if (store == null)
            {
                DisplayError("Store not available");
                return NotFound();
            }
            var availableOutlets = await _context.Outlets.Where(o => o.StoreId == store.Id).ToListAsync();

            if(!UserValid(loggedInUser, store.UserId))
            {
                return RedirectToAction("Index");
            }

          

            var model = new StoreDetailsViewModel()
            {
                Store = store,
                Outlets = availableOutlets.Count()
            };

            return View(model);
        }

        // GET: Stores/Create
        public async Task<IActionResult> Create()
        {
            var loggedInUser = _userManager.GetUserId(User);
            var createdStores = storeCount(loggedInUser);
            var user = _context.ApplicationUsers.Find(loggedInUser);
            var userRole = await _userManager.IsInRoleAsync(user,Roles.Uncleshenor);
            if (!userRole)
            {
                if (createdStores >= user.MaxStores)
                {
                    return RedirectToAction("Index");
                }
            }
            ViewData["UserId"] = new SelectList(_context.ApplicationUsers, "Id", "Name");
            return View();
        }

        // POST: Stores/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Name,Description,UserId,IsActive,IsLocked,IsDeleted")] Store store)
        {
            if(store.UserId == null)
            {
                var loggedInUser = _userManager.GetUserId(User);
                store.UserId = loggedInUser;
            }
            var user = _context.ApplicationUsers.Find(store.UserId);
            var userRole = await _userManager.IsInRoleAsync(user, Roles.Uncleshenor);
            if (!userRole)
            {
                var maxStores = user.MaxStores;
                var createdStores = storeCount(user.Id);

                if (createdStores >= maxStores)
                {
                    DisplayError("You cannot create more store at the moment as you have reached your store creation limit. Please reach out to the Admin");
                    return RedirectToAction("Index");
                }
            }
          
                if (ModelState.IsValid)
                {
                    _context.Add(store);
                    await _context.SaveChangesAsync();
                    DisplayAlert("Store Created Successfully, Please proceed to creating Outlets for this store");
                    return RedirectToAction(nameof(Index));
                }
                ViewData["UserId"] = new SelectList(_context.ApplicationUsers, "Id", "Id", store.UserId);
                TempData[SD.Error] = "Unable to Create store, Please Try Again";
                return View(store);

           
        }

        // GET: Stores/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var loggedInUser = _userManager.GetUserId(User);
            var store = await _context.Stores.FindAsync(id);
            if (!UserValid(loggedInUser, store.UserId))
            {
                return RedirectToAction("Index");
            }
            if (store == null)
            {
                return NotFound();
            }
            ViewData["UserId"] = new SelectList(_context.ApplicationUsers, "Id", "Id", store.UserId);
            return View(store);
        }

        // POST: Stores/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,Description,UserId,IsActive,IsLocked,IsDeleted")] Store store)
        {
            if (id != store.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    //var loggedInUser = _userManager.GetUserId(User);
                    //store.UserId = loggedInUser;
                    _context.Update(store);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!StoreExists(store.Id))
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
            ViewData["UserId"] = new SelectList(_context.ApplicationUsers, "Id", "Id", store.UserId);
            return View(store);
        }

        // GET: Stores/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var loggedInUser = _userManager.GetUserId(User);
            var store = await _context.Stores
                .Include(s => s.User)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (store.UserId != loggedInUser)
            {
                DisplayError("You cant access a store that is not assigned to you");
                return View(store);
            }
            if (store == null)
            {
                return NotFound();
            }

            return View(store);
        }

        // POST: Stores/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var store = await _context.Stores.FindAsync(id);
            _context.Stores.Remove(store);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool StoreExists(int id)
        {
            return _context.Stores.Any(e => e.Id == id);
        }

        private int storeCount(String userId)
        {
            var createdStores = _context.Stores.Where(s => s.UserId == userId).Count();
            return createdStores;
        }

        
    }
}
