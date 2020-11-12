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
    public class StoreOutItemRecipeController : Controller
    {
        private readonly ApplicationDbContext _context;
        private UserManager<ApplicationUser> _userManager;
        private RoleManager<IdentityRole> _roleManager;

        public StoreOutItemRecipeController(ApplicationDbContext context, UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager)
        {
            _context = context;
            _userManager = userManager;
            _roleManager = roleManager;
        }

        // GET: MessStore
        public ActionResult Index()
        {
            return View();
            //return View(_context.StoreOutItemRecipe.Include(x => x.StoreOutItem).Include(x => x.StoreInItem).ToList());
        }


        // GET: MessStore/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: MessStore/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(StoreOutItemRecipe model)
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

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateStoreOutRecipe()
        {
            StoreOutItemRecipe storeOutItemRecipe = new StoreOutItemRecipe();
            storeOutItemRecipe.CreatedDate = DateTime.Now;
            var user = await _userManager.GetUserAsync(User);
            var userID = user.Id;
            storeOutItemRecipe.CreatedBy = userID;
            storeOutItemRecipe.LastModifiedDate = DateTime.Now;
            storeOutItemRecipe.MinimumStoreOutUnit = 5;
            storeOutItemRecipe.RequiredStoreInUnit = double.Parse(Request.Form["q"]);
            storeOutItemRecipe.StoreInItemId = Int32.Parse(Request.Form["si"]);
            storeOutItemRecipe.StoreOutItemId  = Int32.Parse(Request.Form["so"]);
            _context.Add(storeOutItemRecipe);
            _context.SaveChanges();



            return View("Index");

        }


        // GET: MessStore/Edit/5
        //public ActionResult Edit(int id)
        //{
        //    StoreOutItemRecipe storeOutItemRecipe = _context.StoreOutItemRecipe.Include(x=> x.StoreOutItem).Include(x => x.StoreInItem).Where(x => x.Id == id).FirstOrDefault();
        //    if (storeOutItemRecipe == null)
        //    {
        //        return NotFound();
        //    }
        //    return View(storeOutItemRecipe);
        //}

        public ActionResult Edit(int id)
        {
            StoreOutItemRecipe storeOutItemRecipe = _context.StoreOutItemRecipe.Include(x => x.StoreOutItem).Include(x => x.StoreInItem).Where(x => x.Id == id).FirstOrDefault();
            if (storeOutItemRecipe == null)
            {
                return NotFound();
            }
            ViewBag.EditableId = id;

            return View("Index");
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditSave(int id)
        {
            int StoreInItemId = Int32.Parse(Request.Form["Sin"]);
            double RequiredStoreInUnit = double.Parse(Request.Form["qnt"]);
            var user = await _userManager.GetUserAsync(User);
            var userID = user.Id;

            StoreOutItemRecipe storeOutItemRecipe = _context.StoreOutItemRecipe.Where(x => x.Id == id).FirstOrDefault();

            //storeOutItemRecipe.StoreOutItemId = model.StoreOutItemId;
            storeOutItemRecipe.StoreInItemId = StoreInItemId;
            storeOutItemRecipe.RequiredStoreInUnit = RequiredStoreInUnit;

            storeOutItemRecipe.LastModifiedBy = userID;
            storeOutItemRecipe.LastModifiedDate = DateTime.Now;

            _context.Update(storeOutItemRecipe);
            _context.SaveChanges();

            return View("Index");

        }

        // POST: MessStore/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, StoreOutItemRecipe model)
        {
            try
            {
                var user = await _userManager.GetUserAsync(User);
                var userID = user.Id;

                StoreOutItemRecipe storeOutItemRecipe = _context.StoreOutItemRecipe.Where(x => x.Id == id).FirstOrDefault();

                storeOutItemRecipe.StoreOutItemId = model.StoreOutItemId;
                storeOutItemRecipe.StoreInItemId = model.StoreInItemId;
                storeOutItemRecipe.MinimumStoreOutUnit = model.MinimumStoreOutUnit;
                storeOutItemRecipe.RequiredStoreInUnit = model.RequiredStoreInUnit;

                storeOutItemRecipe.LastModifiedBy = userID;
                storeOutItemRecipe.LastModifiedDate = DateTime.Now;

                _context.Update(storeOutItemRecipe);
                _context.SaveChanges();

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: MessStore/Delete/5
        //public ActionResult Delete(int id)
        //{
        //    StoreOutItemRecipe storeOutItemRecipe = _context.StoreOutItemRecipe.Include(x => x.StoreOutItem).Include(x => x.StoreInItem).Where(x => x.Id == id).FirstOrDefault();
        //    return View(storeOutItemRecipe);
        //}

        public ActionResult Delete(int id)
        {
            try
            {
                StoreOutItemRecipe storeOutItemRecipe = _context.StoreOutItemRecipe.Where(x => x.Id == id).FirstOrDefault();

                _context.Remove(storeOutItemRecipe);
                _context.SaveChanges();

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // POST: MessStore/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, StoreOutItemRecipe model)
        {
            try
            {
                StoreOutItemRecipe storeOutItemRecipe = _context.StoreOutItemRecipe.Where(x => x.Id == id).FirstOrDefault();

                _context.Remove(storeOutItemRecipe);
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