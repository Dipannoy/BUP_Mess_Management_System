using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Mess_Management_System_Alpha_V2.Data;
using Mess_Management_System_Alpha_V2.Models.MessModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Mess_Management_System_Alpha_V2.Controllers.Admin
{
    public class StoreInItemCategoryController : Controller
    {

        private readonly ApplicationDbContext _context;
        private UserManager<ApplicationUser> _userManager;
        private RoleManager<IdentityRole> _roleManager;

        public StoreInItemCategoryController(ApplicationDbContext context, UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager)
        {
            _context = context;
            _userManager = userManager;
            _roleManager = roleManager;
        }

        // GET: StoreInItemCategory
        public IActionResult Index()
        {
            return View(_context.StoreInItemCategory.ToList());
        }


        // GET: StoreInItemCategory/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: StoreInItemCategory/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(StoreInItemCategory model)
        {
            try
            {
                if (_context.StoreInItemCategory.Count(a => a.Name.ToLower() == model.Name.ToLower()) > 0)
                {
                    ModelState.AddModelError("Name", "Name Exists");
                }

                if (ModelState.IsValid)
                {
                    var user = await _userManager.GetUserAsync(User);
                    var userID = user.Id;

                    model.CreatedBy = userID;
                    model.CreatedDate = DateTime.Now;
                    model.LastModifiedDate = DateTime.Now;

                    _context.Add(model);
                    _context.SaveChanges();

                    return RedirectToAction(nameof(Index));
                }

                return View(model);
            }
            catch
            {
                return View();
            }
        }

        // GET: StoreInItemCategory/Edit/5
        public IActionResult Edit(int id)
        {
            var storeInItemCategory = _context.StoreInItemCategory.Where(x => x.Id == id).FirstOrDefault();
            if (storeInItemCategory == null)
            {
                return NotFound();
            }
            return View(storeInItemCategory);
        }

        // POST: StoreInItemCategory/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, StoreInItemCategory model)
        {
            try
            {
                if (_context.StoreInItemCategory.Count(a => a.Id != model.Id && a.Name.ToLower() == model.Name.ToLower()) > 0)
                {
                    ModelState.AddModelError("Name", "Name Exists");
                }

                var user = await _userManager.GetUserAsync(User);
                var userID = user.Id;

                StoreInItemCategory storeInItemCategory = _context.StoreInItemCategory.Where(x => x.Id == id).FirstOrDefault();

                storeInItemCategory.Name = model.Name;
                storeInItemCategory.LastModifiedBy = userID;
                storeInItemCategory.LastModifiedDate = DateTime.Now;

                _context.StoreInItemCategory.Update(storeInItemCategory);
                _context.SaveChanges();

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: StoreInItemCategory/Delete/5
        public IActionResult Delete(int id)
        {
            var storeInItemCategory = _context.StoreInItemCategory.Where(x => x.Id == id).FirstOrDefault();
            return View(storeInItemCategory);
        }

        // POST: StoreInItemCategory/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Delete(int id, StoreInItemCategory model)
        {
            try
            {
                StoreInItemCategory storeInItemCategory = _context.StoreInItemCategory.Where(x => x.Id == id).FirstOrDefault();

                _context.StoreInItemCategory.Remove(storeInItemCategory);
                _context.SaveChanges();

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}