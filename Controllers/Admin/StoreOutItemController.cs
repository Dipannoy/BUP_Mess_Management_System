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
    public class StoreOutItemController : Controller
    {

        private readonly ApplicationDbContext _context;
        private UserManager<ApplicationUser> _userManager;
        private RoleManager<IdentityRole> _roleManager;

        public StoreOutItemController(ApplicationDbContext context, UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager)
        {
            _context = context;
            _userManager = userManager;
            _roleManager = roleManager;
        }

        // GET: MessStore
        public ActionResult Index()
        {
            return View(_context.StoreOutItem.Include(x=> x.UnitType).Include(x => x.StoreOutItemCategory).ToList());
        }


        // GET: MessStore/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: MessStore/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(StoreOutItem model)
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

        // GET: MessStore/Edit/5
        public ActionResult Edit(int id)
        {
            var storeOutItem = _context.StoreOutItem.Include(x => x.UnitType).Include(x => x.StoreOutItemCategory).Where(x => x.Id == id).FirstOrDefault();
            if (storeOutItem == null)
            {
                return NotFound();
            }
            return View(storeOutItem);
        }

        // POST: MessStore/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, StoreOutItem model)
        {
            try
            {

                var user = await _userManager.GetUserAsync(User);
                var userID = user.Id;

                StoreOutItem storeOutItem = _context.StoreOutItem.Where(x => x.Id == id).FirstOrDefault();

                storeOutItem.Name = model.Name;
                storeOutItem.IsOpen = model.IsOpen;
                storeOutItem.MinimumProductionUnit = model.MinimumProductionUnit;
                storeOutItem.MinimumProductionUnitMultiplier = model.MinimumProductionUnitMultiplier;
                storeOutItem.UnitTypeId = model.UnitTypeId;
                storeOutItem.StoreOutCategoryId = model.StoreOutCategoryId;


                storeOutItem.LastModifiedBy = userID;
                storeOutItem.LastModifiedDate = DateTime.Now;

                _context.Update(storeOutItem);
                _context.SaveChanges();

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: MessStore/Delete/5
        public ActionResult Delete(int id)
        {
            StoreOutItem storeOutItem = _context.StoreOutItem.Where(x => x.Id == id).FirstOrDefault();
            return View(storeOutItem);
        }

        // POST: MessStore/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, StoreOutItem model)
        {
            try
            {
                StoreOutItem storeOutItem = _context.StoreOutItem.Where(x => x.Id == id).FirstOrDefault();

                _context.Remove(storeOutItem);
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