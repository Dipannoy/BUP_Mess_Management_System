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
    public class MealTypeController : Controller
    {

        private readonly ApplicationDbContext _context;
        private UserManager<ApplicationUser> _userManager;
        private RoleManager<IdentityRole> _roleManager;

        public MealTypeController(ApplicationDbContext context, UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager)
        {
            _context = context;
            _userManager = userManager;
            _roleManager = roleManager;
        }

        // GET: MealType
        public ActionResult Index()
        {
            return View(_context.MealType.ToList());
        }


        // GET: MealType/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: MealType/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(MealType model)
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

        // GET: MealType/Edit/5
        public ActionResult Edit(int id)
        {

            var mealType = _context.MealType.Where(x => x.Id == id).FirstOrDefault();
            if (mealType == null)
            {
                return NotFound();
            }
            return View(mealType);
        }

        // POST: MealType/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, MealType model)
        {
            try
            {
                var user = await _userManager.GetUserAsync(User);
                var userID = user.Id;

                MealType mealType = _context.MealType.Where(x => x.Id == id).FirstOrDefault();

                mealType.Serial = model.Serial;
                mealType.Name = model.Name;
                mealType.IsAvailableForPreOrder = model.IsAvailableForPreOrder;
                mealType.PreOrderLastTime = model.PreOrderLastTime;

                mealType.LastModifiedBy = userID;
                mealType.LastModifiedDate = DateTime.Now;

                _context.Update(mealType);
                _context.SaveChanges();

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: MealType/Delete/5
        public ActionResult Delete(int id)
        {
            MealType mealType = _context.MealType.Where(x => x.Id == id).FirstOrDefault();
            return View(mealType);
        }

        // POST: MealType/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, MealType model)
        {
            try
            {
                MealType mealType = _context.MealType.Where(x => x.Id == id).FirstOrDefault();

                _context.Remove(mealType);
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