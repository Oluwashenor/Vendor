
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Vendor.Data;
using Vendor.Models.ViewModels;

namespace Vendor.Controllers
{
    public class UsersController : Controller
    {

        private readonly ApplicationDbContext _context;
        private readonly UserManager<IdentityUser> _userManager;

        public UsersController(ApplicationDbContext context, UserManager<IdentityUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }
        public async Task<IActionResult> Index()
        {
            var users = await _context.ApplicationUsers.ToListAsync();
            var userRoles = await _context.UserRoles.ToListAsync();
            var roles = await _context.Roles.ToListAsync();

            foreach (var user in users)
            {
                var role = userRoles.Where(u => u.UserId == user.Id).FirstOrDefault();
                if(role == null)
                {
                    user.Role = "Unassigned";
                }
                else
                {
                    user.Role = roles.FirstOrDefault(r=> r.Id == role.RoleId).Name;
                }
            }
            var viewModel = new UserIndexViewModel()
            {
                Users = users
            }; 
            return View(viewModel);
        }


        public IActionResult Edit(string userId)
        {
            var userFromDb = _context.ApplicationUsers.FirstOrDefault(u => u.Id == userId);
            if (userFromDb == null)
            {
                return NotFound();
            }
            var viewModel = new EditUserViewModel()
            {
                User = userFromDb,
            };
            var userRoles = _context.UserRoles.ToList();
            var roles = _context.Roles.ToList();
            var role = userRoles.FirstOrDefault(u => u.UserId == userFromDb.Id);
            if(role != null)
            {
                viewModel.RoleId = roles.FirstOrDefault(r => r.Id == role.RoleId).Id; 
            }
            else
            {
                viewModel.RoleId = null;
            }

            viewModel.Roles = _context.Roles.Select(r => new Microsoft.AspNetCore.Mvc.Rendering.SelectListItem
            {
                Text = r.Name,
                Value = r.Id
            });
            ViewData["Roles"] = new SelectList(_context.Roles, "Id", "Name", viewModel.RoleId);
            return View(viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(EditUserViewModel model)
        {
            if (ModelState.IsValid)
            {
                var userFromDb = _context.ApplicationUsers.FirstOrDefault(u => u.Id == model.User.Id);
                if (userFromDb == null)
                {
                    return NotFound();
                }
                var userRole = _context.UserRoles.FirstOrDefault(u => u.UserId == model.User.Id);
                if (userRole != null)
                {
                    var previousRole = _context.Roles.Where(u => u.Id == userRole.RoleId).Select(e => e.Name).FirstOrDefault();
                    // Remove Old Role
                    await _userManager.RemoveFromRoleAsync(userFromDb, previousRole);

                }
                // Add new Role 
                await _userManager.AddToRoleAsync(userFromDb, _context.Roles.FirstOrDefault(u => u.Id == model.Role).Name);
                userFromDb.Name = model.User.Name;
                userFromDb.MaxStores = model.User.MaxStores;
                _context.SaveChanges();
                return RedirectToAction(nameof(Index));
            }
            else
            {
                model.Roles = _context.Roles.Select(u => new Microsoft.AspNetCore.Mvc.Rendering.SelectListItem
                {
                    Text = u.Name,
                    Value = u.Id
                });
                return View(model);
            }
        }

    }
}
