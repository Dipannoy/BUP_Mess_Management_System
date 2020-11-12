using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Mess_Management_System_Alpha_V2.Data;
using Mess_Management_System_Alpha_V2.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Mess_Management_System_Alpha_V2.Controllers
{
    [Authorize(Roles = "Admin")]
    public class RoleController : Controller
    {
        


        private readonly ApplicationDbContext _context;
        private UserManager<ApplicationUser> _userManager;
        private RoleManager<IdentityRole> _roleManager;

        public RoleController(ApplicationDbContext context, UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager)
        {
            _context = context;
            _userManager = userManager;
            _roleManager = roleManager;
        }


        public IActionResult Index()
        {
            var roleList = _roleManager.Roles.ToList();

            List<Role> role = new List<Role>();
            foreach (var tempdata in roleList)
            {
                Role r = new Role();
                r.Id = tempdata.Id;
                r.RoleName = tempdata.Name;
                

                role.Add(r);
            }

            return View(role);
        }






        // GET: Role/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Role/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Role collection)
        {
            try
            {
                string roleName = collection.RoleName;

                Task<bool> roleExists = _roleManager.RoleExistsAsync(roleName);
                roleExists.Wait();

                if (!roleExists.Result)
                {
                    Task<IdentityResult> roleResult = _roleManager.CreateAsync(new IdentityRole(roleName));
                    roleResult.Wait();
                }

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: Role/Edit/5
        public IActionResult Edit(string id)
        {
            var role = _roleManager.Roles.Where(x => x.Id == id).FirstOrDefault();

            Role r = new Role();
            r.Id = role.Id;
            r.RoleName = role.Name;

            return View(r);
        }

        // POST: Role/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, Role collection)
        {
            try
            {
                var role = await _roleManager.FindByIdAsync(id);
                if (role != null)
                {
                    role.Name = collection.RoleName;
                    var idResult = await _roleManager.UpdateAsync(role);
                    if (!idResult.Succeeded)
                    {
                        ModelState.AddModelError("RoleName", "Error Updating Role..!!");
                    }
                }

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: Role/Delete/5
        public async Task<IActionResult> Delete(string id)
        {
            var role = await _roleManager.FindByIdAsync(id);

            Role r = new Role();
            r.Id = role.Id;
            r.RoleName = role.Name;

            return View(r);
        }

        // POST: Role/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(string id, Role collection)
        {
            try
            {
                var role = await _roleManager.FindByIdAsync(id);

                if (role != null)
                {
                    var idResult = await _roleManager.DeleteAsync(role);

                    if (!idResult.Succeeded)
                    {
                        ModelState.AddModelError("RoleName", "Error Deleting Role..!!");
                    }
                }


                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }











    }
}