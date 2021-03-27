using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Mess_Management_System_Alpha_V2.Data;
using Mess_Management_System_Alpha_V2.Models.MessModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System.Globalization;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;
using System.Net.Http;
using Microsoft.IdentityModel.Protocols.OpenIdConnect;
using System.Web;
using Microsoft.AspNetCore.Authentication;

namespace Mess_Management_System_Alpha_V2.Controllers.Admin
{
    public class AdminReportController : Controller
    {
        private readonly ApplicationDbContext _context;
        private UserManager<ApplicationUser> _userManager;
        private RoleManager<UserIdentityRole> _roleManager;
        public static List<double> weightedPrice = new List<double>();
        public AdminReportController(ApplicationDbContext context, UserManager<ApplicationUser> userManager, RoleManager<UserIdentityRole> roleManager)
        {
            _context = context;
            _userManager = userManager;
            _roleManager = roleManager;
        }

        public async Task<ActionResult> StoreOutReport()
        {
            if (SessionExist())
            {
                if (await UserExistMess())
                {
                    var role = await GetLogInUserRoleObjectAsync();
                    var roleMenuList = _context.RoleMenu.Where(x => x.UserIdentityRoleId == role.Id).ToList();
                    var nevMenuList = _context.NavigationMenu.Where(x => x.Id > 0).ToList();
                    var pr = from r in roleMenuList
                             join n in nevMenuList
                             on r.NavigationMenuId equals n.Id
                             // where o.LastModifiedDate.ToShortDateString() == OD.AddDays(-1).ToShortDateString() && o.MealTypeId == 1
                             select n;
                    var filterMenuList = pr.ToList();

                    ViewBag.FilterMenuList = filterMenuList;
                    ViewBag.FullName = await GetUserName();

                    //ViewBag.FullName = await GetUserName();

                    return View();
                    //if (await GetLogInUserRoleAsync() == "Admin" || await GetLogInUserRoleAsync() == "MessAdmin")
                    //{
                    //    //ViewBag.StoreId = storeId;
                    //    ViewBag.FullName = await GetUserName();

                    //    return View("WarehouseStorageIn");
                    //}
                    //else
                    //{
                    //    return LocalRedirect("~/AccessCheck/Index");

                    //}
                }
                else
                {
                    return LocalRedirect("~/AccessCheck/Index");

                }

            }
            else
            {
                return LocalRedirect("~/AccessCheck/Index");

            }

        }


        public bool SessionExist()
        {
            string usrName = SessionExtensions.GetString(HttpContext.Session, "user");
            //var user = await _userManager.FindByNameAsync(usrName);

            if (usrName != null)
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
            string usrName = SessionExtensions.GetString(HttpContext.Session, "user");
            var user = await _userManager.FindByNameAsync(usrName);

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
        public async Task<UserIdentityRole> GetLogInUserRoleObjectAsync()
        {
            string usrName = SessionExtensions.GetString(HttpContext.Session, "user");
            var user = await _userManager.FindByNameAsync(usrName);
            var userrole = _context.UserRoles.Where(x => x.UserId == user.Id).FirstOrDefault();

            var role = await _roleManager.FindByIdAsync(userrole.RoleId);

            return role;
        }
        public async Task<string> GetUserName()
        {
            string usrName = SessionExtensions.GetString(HttpContext.Session, "user");
            var user = await _userManager.FindByNameAsync(usrName);


            return user.BUPFullName;
        }
    }
}
