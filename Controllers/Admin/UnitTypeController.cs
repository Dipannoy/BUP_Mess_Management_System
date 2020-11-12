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
    public class UnitTypeController : Controller
    {
        private readonly ApplicationDbContext _context;
        private UserManager<ApplicationUser> _userManager;
        private RoleManager<IdentityRole> _roleManager;

        public UnitTypeController(ApplicationDbContext context, UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager)
        {
            _context = context;
            _userManager = userManager;
            _roleManager = roleManager;
        }

        // GET: UnitType
        public ActionResult Index()
        {
            return View(_context.UnitType.ToList());
        }


        // GET: UnitType/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: UnitType/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(UnitType model)
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

        // GET: UnitType/Edit/5
        public ActionResult Edit(int id)
        {
            var unitType = _context.UnitType.Where(x => x.Id == id).FirstOrDefault();
            if (unitType == null)
            {
                return NotFound();
            }
            return View(unitType);
        }

        // POST: UnitType/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, UnitType model)
        {
            try
            {
                var user = await _userManager.GetUserAsync(User);
                var userID = user.Id;

                UnitType unitType = _context.UnitType.Where(x => x.Id == id).FirstOrDefault();

                unitType.Name = model.Name;
                unitType. ShortName = model.ShortName;
                unitType.LastModifiedBy = userID;
                unitType.LastModifiedDate = DateTime.Now;

                _context.Update(unitType);
                _context.SaveChanges();

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: UnitType/Delete/5
        public ActionResult Delete(int id)
        {
            UnitType unitType = _context.UnitType.Where(x => x.Id == id).FirstOrDefault();
            return View(unitType);
        }

        // POST: UnitType/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, UnitType model)
        {
            try
            {
                UnitType unitType = _context.UnitType.Where(x => x.Id == id).FirstOrDefault();

                _context.Remove(unitType);
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