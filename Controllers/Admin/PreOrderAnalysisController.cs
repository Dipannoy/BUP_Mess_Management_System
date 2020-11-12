using Mess_Management_System_Alpha_V2.Data;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Mess_Management_System_Alpha_V2.Controllers.Admin
{
    public class PreOrderAnalysisController : Controller
    {


        private readonly ApplicationDbContext _context;
        private UserManager<ApplicationUser> _userManager;
        private RoleManager<IdentityRole> _roleManager;

        public PreOrderAnalysisController(ApplicationDbContext context, UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager)
        {
            _context = context;
            _userManager = userManager;
            _roleManager = roleManager;
        }

        // GET: PreOrderAnalysis
        public ActionResult Index()
        {
            return View();
        }

        // GET: PreOrderAnalysis/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: PreOrderAnalysis/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: PreOrderAnalysis/Create
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

        // GET: PreOrderAnalysis/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: PreOrderAnalysis/Edit/5
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

        // GET: PreOrderAnalysis/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: PreOrderAnalysis/Delete/5
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