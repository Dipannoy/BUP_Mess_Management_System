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
    public class StoreInItemController : Controller
    {

        private readonly ApplicationDbContext _context;
        private UserManager<ApplicationUser> _userManager;
        private RoleManager<IdentityRole> _roleManager;

        public StoreInItemController(ApplicationDbContext context, UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager)
        {
            _context = context;
            _userManager = userManager;
            _roleManager = roleManager;
        }

        // GET: StoreInItem
        public ActionResult Index()
        {
            
            return View(_context.StoreInItem.Include(x => x.UnitType).Include(x => x.StoreInItemCategory).ToList());
        }


        // GET: StoreInItem/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: StoreInItem/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(StoreInItem model)
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

        // GET: StoreInItem/Edit/5
        public ActionResult Edit(int id)
        {
            var storeInItem = _context.StoreInItem.Where(x => x.Id == id).FirstOrDefault();
            if (storeInItem == null)
            {
                return NotFound();
            }
            return View(storeInItem);
        }

        // POST: StoreInItem/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, StoreInItem model)
        {
            try
            {
                var user = await _userManager.GetUserAsync(User);
                var userID = user.Id;

                var storeInItem = _context.StoreInItem.Where(x => x.Id == id).FirstOrDefault();

                storeInItem.Name = model.Name;
                storeInItem.IsOpen = model.IsOpen;
                storeInItem.UnitTypeId = model.UnitTypeId;
                storeInItem.StoreInCategoryId = model.StoreInCategoryId;

                storeInItem.LastModifiedBy = userID;
                storeInItem.LastModifiedDate = DateTime.Now;

                _context.Update(storeInItem);
                _context.SaveChanges();

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: StoreInItem/Delete/5
        public ActionResult Delete(int id)
        {
            var storeInItem = _context.StoreInItem.Include(x => x.UnitType).Include(x => x.StoreInItemCategory).Where(x => x.Id == id).FirstOrDefault();
            return View(storeInItem);
        }

        // POST: StoreInItem/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, StoreInItem model)
        {
            try
            {
                StoreInItem storeInItem = _context.StoreInItem.Where(x => x.Id == id).FirstOrDefault();

                _context.Remove(storeInItem);
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