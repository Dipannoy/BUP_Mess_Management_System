using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Mess_Management_System_Alpha_V2.Data;
using Mess_Management_System_Alpha_V2.Models.MessModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System.Data.SqlClient;
using System.Data;
using Newtonsoft.Json;

//using System.Web.Script.Serialization;


using System.Globalization;
using Microsoft.AspNetCore.Authorization;
using System.Collections;

namespace Mess_Management_System_Alpha_V2.Controllers.SessionChecker
{
    public class SessionUserController : Controller
    {
        private readonly ApplicationDbContext _context;
        private UserManager<ApplicationUser> _userManager;
        private RoleManager<IdentityRole> _roleManager;

        private SignInManager<ApplicationUser> _signInManager;
        private string userName;

        public SessionUserController(ApplicationDbContext context, UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager, SignInManager<ApplicationUser> signInManager)
        {
            _context = context;
            _userManager = userManager;
            _roleManager = roleManager;
            _signInManager = signInManager;
        }
        public SessionUserController()
        {
            userName = SessionExtensions.GetString(HttpContext.Session, "user");
        }
        public IActionResult Index()
        {
            return View();
        }

        public bool SessionExist()
        {
           // string usrName = SessionExtensions.GetString(HttpContext.Session, "user");
            //var user = await _userManager.FindByNameAsync(usrName);

            if(userName != null)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        public async Task<bool> UserExistMess()
        {
            //string usrName = SessionExtensions.GetString(HttpContext.Session, "user");
            var user = await _userManager.FindByNameAsync(userName);

            if (user != null)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public async Task<string> GetLogInUserRoleAsync()
        {
            string usrName = SessionExtensions.GetString(HttpContext.Session, "user");
            var user = await _userManager.FindByNameAsync(usrName);
            var userrole = _context.UserRoles.Where(x => x.UserId == user.Id).FirstOrDefault();

            var role = await _roleManager.FindByIdAsync(userrole.RoleId);

            return role.Name;
        }


    }
}
