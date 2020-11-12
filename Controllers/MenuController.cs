using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Mess_Management_System_Alpha_V2.Data;
using Mess_Management_System_Alpha_V2.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace Mess_Management_System_Alpha_V2.Controllers
{
    //[Authorize(Roles = "Admin")]
    public class MenuController : Controller
    {
        
        private readonly ApplicationDbContext _context;
        private UserManager<ApplicationUser> _userManager;
        private RoleManager<IdentityRole> _roleManager;

        public MenuController(ApplicationDbContext context, UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager)
        {
            _context = context;
            _userManager = userManager;
            _roleManager = roleManager;
        }

        // GET: Menu
        public ActionResult Index()
        {
            var menulist = _context.Menu.ToList();
            var submenulist = _context.SubMenu.Include(x => x.Menu).ToList();

            var tuple = new Tuple<List<Menu>, List<MenuSub>>(menulist, submenulist);


            return View(tuple);
        }


        // GET: Menu/Create
        public ActionResult MenuCreate()
        {
            return View();
        }

        // POST: Menu/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> MenuCreate(Menu collection)
        {
            try
            {
                var user = await _userManager.GetUserAsync(User);
                var userID = user.Id;

                collection.CreatedBy = userID;
                collection.CreatedDate = DateTime.Now;
                

                _context.Menu.Add(collection);
                await _context.SaveChangesAsync();

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }




        // GET: Menu/Create
        public ActionResult SubMenuCreate()
        {
            ViewBag.MenuMasterList = _context.Menu.ToList();

            return View();
        }

        // POST: Menu/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> SubMenuCreate(MenuSub collection)
        {
            try
            {
                var user = await _userManager.GetUserAsync(User);
                var userID = user.Id;

                collection.CreatedBy = userID;
                collection.CreatedDate = DateTime.Now;

                _context.SubMenu.Add(collection);
                await _context.SaveChangesAsync();

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }












        // GET: Menu/Edit/5
        public ActionResult MenuEdit(int id)
        {

            Menu menu = _context.Menu.Where(x => x.Id == id).FirstOrDefault();

            return View(menu);
        }

        // POST: Menu/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> MenuEdit(int id, Menu collection)
        {
            try
            {
                var user = await _userManager.GetUserAsync(User);
                var userID = user.Id;

                Menu menu = _context.Menu.Where(x => x.Id == id).FirstOrDefault();

                menu.MenuName = collection.MenuName;
                menu.ModifiedBy = userID;
                menu.ModifiedDate = DateTime.Now;

                _context.Menu.Update(menu);
                await _context.SaveChangesAsync();

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }


        // GET: Menu/Edit/5
        public ActionResult SubMenuEdit(int id)
        {
            MenuSub subMenu = _context.SubMenu.Where(x => x.Id == id).FirstOrDefault();



            List<Menu> menuList = _context.Menu.ToList();

            List<SelectListItem> selectMenu = new List<SelectListItem>();
            foreach (var tempData in menuList)
            {
                SelectListItem selectListItem = new SelectListItem
                {
                    Text = tempData.MenuName,
                    Value = tempData.Id.ToString(),
                    Selected = tempData.Id == subMenu.MenuId ? true : false
                };
                selectMenu.Add(selectListItem);
            }
            ViewBag.MenuList = selectMenu;

           return View(subMenu);

        }

        // POST: Menu/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> SubMenuEdit(int id, MenuSub collection)
        {
            try
            {
                var user = await _userManager.GetUserAsync(User);
                var userID = user.Id;

                collection.ModifiedBy = userID;
                collection.ModifiedDate = DateTime.Now;

                _context.SubMenu.Update(collection);
                await _context.SaveChangesAsync();

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }











        // GET: Menu/Delete/5
        public ActionResult MenuDelete(int id)
        {
            Menu menu = _context.Menu.Where(x => x.Id == id).FirstOrDefault();

            return View(menu);
        }

        // POST: Menu/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult MenuDelete(int id, Menu collection)
        {
            try
            {
                Menu menu = _context.Menu.Where(x => x.Id == id).FirstOrDefault();

                _context.Menu.Remove(menu);
                _context.SaveChanges();

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }



        // GET: Menu/Delete/5
        public ActionResult SubMenuDelete(int id)
        {
            MenuSub subMenu = _context.SubMenu.Where(x => x.Id == id).FirstOrDefault();

            return View(subMenu);
        }

        // POST: Menu/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult SubMenuDelete(int id, MenuSub collection)
        {
            try
            {
                MenuSub subMenu = _context.SubMenu.Where(x => x.Id == id).FirstOrDefault();

                _context.SubMenu.Remove(subMenu);
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