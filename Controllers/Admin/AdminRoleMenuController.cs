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
using Microsoft.AspNetCore.Hosting;

using System.Globalization;
using Microsoft.AspNetCore.Authorization;
using System.Collections;
using System.IO;
using BupMessManagement.Email;


// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Mess_Management_System_Alpha_V2.Controllers.Admin
{
    public class AdminRoleMenuController : Controller
    {
        private readonly ApplicationDbContext _context;
        private UserManager<ApplicationUser> _userManager;
        private RoleManager<UserIdentityRole> _roleManager;


        public AdminRoleMenuController(ApplicationDbContext context, UserManager<ApplicationUser> userManager,
            RoleManager<UserIdentityRole> roleManager)
        {
            _context = context;
            _userManager = userManager;
            _roleManager = roleManager;
      
        }
        public async Task<IActionResult> Index()
        {
            if (SessionExist())
            {
                if (await UserExistMess())
                {
                    if (await GetLogInUserRoleAsync() == "Admin" || await GetLogInUserRoleAsync() == "MessAdmin")
                    {
                        ViewBag.FullName = await GetUserName();

                        return View();

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
            else
            {
                return LocalRedirect("~/AccessCheck/Index");

            }
        }

        public async Task<IActionResult> RoleManagement()
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

                    return View();
            
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

        public async Task<IActionResult> MenuManagement()
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

                    return View();
                    //if (await GetLogInUserRoleAsync() == "Admin" || await GetLogInUserRoleAsync() == "MessAdmin")
                    //{
                    //    ViewBag.FullName = await GetUserName();

                    //    return View();

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

        [HttpPost]
        public async Task<JsonResult> SaveRole(string Role)
        {
            try
            {
                UserIdentityRole userIdentityRole = new UserIdentityRole
                {
                    Name = Role
                };

                await _roleManager.CreateAsync(userIdentityRole);
            }
            catch(Exception ex)
            {
                return Json(new { success = true, responseText = ex.Message });

            }
            return Json(new { success = true, responseText = "Insert is successful" });

        }

        [HttpPost]
        public async Task<JsonResult> SaveMenu(string Menu, string Controller, string Action, string Parent,
                                                string Route, string RouteValue)
        {
            try
            {
                NavigationMenu nvm = new NavigationMenu();
                nvm.Name = Menu;
                nvm.Controller = Controller;
                nvm.Action = Action;
                nvm.ParentId = long.Parse(Parent);
                nvm.RouteVariable = Route;
                nvm.RouteVariableValue = RouteValue;
                nvm.CreatedDate = DateTime.Now;
                nvm.LastModifiedDate = DateTime.Now;
                _context.NavigationMenu.Add(nvm);
                _context.SaveChanges();
            }
            catch (Exception ex)
            {
                return Json(new { success = true, responseText = ex.Message });

            }
            return Json(new { success = true, responseText = "Insert is successful" });

        }
        [HttpPost]
        public async Task<JsonResult> SetRole(string ItemList)
        {
            try
            {
                List<RolePersonObject> tempRoleList = (List<RolePersonObject>)JsonConvert.DeserializeObject(ItemList, typeof(List<RolePersonObject>));
                foreach(var o in tempRoleList)
                {
                    var person = _userManager.Users.Where(x => x.BUPFullName == o.Person).FirstOrDefault();

                    var userRole = _context.UserRoles.Where(x => x.UserId == person.Id).FirstOrDefault();
                    var role = await _roleManager.FindByIdAsync(o.Role);
                    if (userRole == null)
                    {
                        await _userManager.AddToRoleAsync(person, role.Name);
                        _context.SaveChanges();
                    }
                    else
                    {
                        //userRole.RoleId = o.Role;
                        _context.UserRoles.Remove(userRole);
                        _context.SaveChanges();
                        await _userManager.AddToRoleAsync(person, role.Name);
                        _context.SaveChanges();


                    }
                }
            }
            catch (Exception ex)
            {
                return Json(new { success = true, responseText = ex.Message });

            }
            return Json(new { success = true, responseText = "Role modify is successful" });

        }

        [HttpPost]
        public async Task<JsonResult> SetRoleMenu(string ItemList)
        {
            try
            {
                List<RoleMenuObject> tempRoleList = (List<RoleMenuObject>)JsonConvert.DeserializeObject(ItemList, typeof(List<RoleMenuObject>));

                var roleMenuList = _context.RoleMenu.Where(x => x.UserIdentityRoleId == tempRoleList.ElementAt(0).Role).ToList();

                foreach(var i in roleMenuList)
                {
                    _context.RoleMenu.Remove(i);
                    _context.SaveChanges();
                }
                foreach (var o in tempRoleList)
                {
                       RoleMenu rm = new RoleMenu();
                        rm.CreatedDate = DateTime.Now;
                        rm.LastModifiedDate = DateTime.Now;
                        rm.UserIdentityRoleId = o.Role;
                        rm.NavigationMenuId = long.Parse(o.Menu);
                        _context.RoleMenu.Add(rm);
                        _context.SaveChanges();
             
                }
            }
            catch (Exception ex)
            {
                return Json(new { success = true, responseText = ex.Message });

            }
            return Json(new { success = true, responseText = "Role modify is successful" });

        }

        public JsonResult GetSearchPerson(string search)
        {
            var PersonList = _userManager.Users.Where(x => x.BUPFullName.Contains(search)).ToList();

            return Json(PersonList);
        }

        public string GetRoleMenu(string search)
        {
            var role = _roleManager.Roles.Where(x => x.Name == search).FirstOrDefault();
            var roleMenuList = _context.RoleMenu.Where(x => x.UserIdentityRoleId == role.Id).ToList();
            //var PersonList = _userManager.Users.Where(x => x.BUPFullName.Contains(search)).ToList();
            string json = JsonConvert.SerializeObject(roleMenuList, Formatting.Indented, new JsonSerializerSettings
            {
                ReferenceLoopHandling = ReferenceLoopHandling.Ignore
            });
            return json;
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
        public async Task<string> GetUserName()
        {
            string usrName = SessionExtensions.GetString(HttpContext.Session, "user");
            var user = await _userManager.FindByNameAsync(usrName);


            return user.BUPFullName;
        }

        public async Task<UserIdentityRole> GetLogInUserRoleObjectAsync()
        {
            string usrName = SessionExtensions.GetString(HttpContext.Session, "user");
            var user = await _userManager.FindByNameAsync(usrName);
            var userrole = _context.UserRoles.Where(x => x.UserId == user.Id).FirstOrDefault();

            var role = await _roleManager.FindByIdAsync(userrole.RoleId);

            return role;
        }
        public class RolePersonObject
        {
            public string Person;
            public string Role;
        }
        public class RoleMenuObject
        {
            public string Role;
            public string Menu;
        }

    }
}
