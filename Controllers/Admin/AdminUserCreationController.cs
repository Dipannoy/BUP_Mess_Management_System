using Mess_Management_System_Alpha_V2.Data;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Mess_Management_System_Alpha_V2.Controllers.Admin
{
    public class AdminUserCreationController : Controller
    {

        private readonly ApplicationDbContext _context;
        private UserManager<ApplicationUser> _userManager;
        private RoleManager<IdentityRole> _roleManager;

        public AdminUserCreationController(ApplicationDbContext context, UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager)
        {
            _context = context;
            _userManager = userManager;
            _roleManager = roleManager;
        }



        // GET: AdminUserCreation
        public ActionResult Index()
        {
            return View();
        }

        // GET: AdminUserCreation/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: AdminUserCreation/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: AdminUserCreation/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(IFormCollection collection)
        {
            try
            {
                // TODO: Add insert logic here

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: AdminUserCreation/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: AdminUserCreation/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
        {
            try
            {
                // TODO: Add update logic here

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: AdminUserCreation/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: AdminUserCreation/Delete/5
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