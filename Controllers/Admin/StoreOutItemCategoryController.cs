using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Mess_Management_System_Alpha_V2.Data;
using Mess_Management_System_Alpha_V2.Models.MessModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Mess_Management_System_Alpha_V2.Controllers.Admin
{
    public class StoreOutItemCategoryController : Controller
    {

        private readonly ApplicationDbContext _context;
        private UserManager<ApplicationUser> _userManager;
        private RoleManager<IdentityRole> _roleManager;

        public StoreOutItemCategoryController(ApplicationDbContext context, UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager)
        {
            _context = context;
            _userManager = userManager;
            _roleManager = roleManager;
        }

        // GET: StoreOutItemCategory
        public ActionResult Index()
        {
            return View(_context.StoreOutItemCategory.ToList());
        }


        // GET: StoreOutItemCategory/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: StoreOutItemCategory/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(StoreOutItemCategory model)
        {
            try
            {
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

        // GET: StoreOutItemCategory/Edit/5
        public ActionResult Edit(int id)
        {
            var storeOutItemCategory = _context.StoreOutItemCategory.Where(x => x.Id == id).FirstOrDefault();
            if (storeOutItemCategory == null)
            {
                return NotFound();
            }
            return View(storeOutItemCategory);
        }

        // POST: StoreOutItemCategory/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, StoreOutItemCategory model)
        {
            try
            {
                var user = await _userManager.GetUserAsync(User);
                var userID = user.Id;

                var storeOutItemCategory = _context.StoreOutItemCategory.Where(x => x.Id == id).FirstOrDefault();

                storeOutItemCategory.Name = model.Name;
                storeOutItemCategory.LastModifiedBy = userID;
                storeOutItemCategory.LastModifiedDate = DateTime.Now;

                _context.Update(storeOutItemCategory);
                _context.SaveChanges();

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: StoreOutItemCategory/Delete/5
        public ActionResult Delete(int id)
        {
            var storeOutItemCategory = _context.StoreOutItemCategory.Where(x => x.Id == id).FirstOrDefault();
            return View(storeOutItemCategory);
        }

        // POST: StoreOutItemCategory/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}