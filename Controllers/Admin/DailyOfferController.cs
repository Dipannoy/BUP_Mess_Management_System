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
    public class DailyOfferController : Controller
    {
        private readonly ApplicationDbContext _context;
        private UserManager<ApplicationUser> _userManager;
        private RoleManager<UserIdentityRole> _roleManager;


        public DailyOfferController(ApplicationDbContext context, UserManager<ApplicationUser> userManager,
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

        public JsonResult GetExtraChit()
        {

            

            var OnSpotItemList = _context.StoreOutItem.Where(x => x.IsOpen == true).OrderBy(x=>x.Name).ToList();
            List<ExtraChitObject> exChtList = new List<ExtraChitObject>();


            foreach (var i in OnSpotItemList)
            {
          
                ExtraChitObject o = new ExtraChitObject();
                o.Id = i.Id.ToString();
                o.ItemName = i.Name;
                exChtList.Add(o);
            }
            return Json(exChtList);

        }

        [HttpPost]
        public async Task<JsonResult> AddOnSpotItem(string ItemList)
        {
            string usrName = SessionExtensions.GetString(HttpContext.Session, "user");

            if (usrName == null)
            {
                return Json(new { success = false, responseText = "Expire" });

            }

            try
            {
                var user = await _userManager.FindByNameAsync(usrName);
                string UserId = user.Id;
                List<OnSpotItemObject> tempItemList = (List<OnSpotItemObject>)JsonConvert.DeserializeObject(ItemList, typeof(List<OnSpotItemObject>));

                foreach(var i in tempItemList)
                {
                    DailyOfferItem di = new DailyOfferItem();
                    di.StoreOutItemId = long.Parse(i.Id);
                    di.OrderLimit = long.Parse(i.Quantity);
                    di.IsActive = i.IsContinued == "true"?  true : false;
                    di.CreatedDate = DateTime.Now;
                    di.LastModifiedDate = DateTime.Now;
                    
                    di.CreatedBy = UserId;
                    di.LastModifiedBy = UserId;
                    di.Date = DateTime.Now;
                    _context.DailyOfferItem.Add(di);
                    _context.SaveChanges();
                    
                }
            }
            catch (Exception ex)
            {
                return Json(new { success = false, responseText = ex.Message });

            }
            return Json(new { success = true, responseText = "Items have been added successfully.." });

        }


        public string GetOnSpotItemList()
        {
            int day = (int)DateTime.Today.DayOfWeek + 1;
            List<ExtraChitObject> exChtList = new List<ExtraChitObject>();
            List<IList> full = new List<IList>();
            var stoList = _context.StoreOutItem.Where(x => x.IsOpen == true).ToList();


            var OnSpotItemList = _context.DailyOfferItem.Where(x => x.Date.Date == DateTime.Now.Date || x.IsActive == true).ToList();

            foreach (var i in OnSpotItemList)
            {
                var storeOutItem = _context.StoreOutItem.Where(x => x.Id == i.StoreOutItemId).FirstOrDefault();
                ExtraChitObject o = new ExtraChitObject();
                o.Id = i.StoreOutItemId.ToString();
                o.ItemName = storeOutItem.Name;
                exChtList.Add(o);
            }
            //List<CustomerChoiceV2> ccList = _context.CustomerChoiceV2.Where(x => x.OnSpotParentId == long.Parse(ParentId)).ToList();

            full.Add(exChtList);
            full.Add(OnSpotItemList);
            full.Add(stoList);
            string json = JsonConvert.SerializeObject(full, Formatting.Indented, new JsonSerializerSettings
            {
                ReferenceLoopHandling = ReferenceLoopHandling.Ignore
            });
            return json;
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



        [HttpPost]
        public async Task<JsonResult> EditOnSpotItem(string ItemList)
        {
            string usrName = SessionExtensions.GetString(HttpContext.Session, "user");

            if (usrName == null)
            {
                return Json(new { success = false, responseText = "Expire" });

            }
            try
            {
                var user = await _userManager.FindByNameAsync(usrName);
                string UserId = user.Id;
                List<EditableOnSpotItemObject> tempItemList = (List<EditableOnSpotItemObject>)JsonConvert.DeserializeObject(ItemList, typeof(List<EditableOnSpotItemObject>));

                foreach (var item in tempItemList)
                {
                    if (item.IsDelete == "1")
                    {
                        var ordObj = _context.DailyOfferItem.Where(x => x.Id == long.Parse(item.OfferId)).FirstOrDefault();
                        _context.DailyOfferItem.Remove(ordObj);
                        _context.SaveChanges();
                    }

                    else
                    {
                        var ordObj = _context.DailyOfferItem.Where(x => x.Id == long.Parse(item.OfferId)).FirstOrDefault();
                        ordObj.StoreOutItemId = long.Parse(item.ItemId);
                        ordObj.OrderLimit = long.Parse(item.Quantity);
                        _context.DailyOfferItem.Update(ordObj);
                        _context.SaveChanges();
                    }
                }

            }
            catch (Exception ex)
            {
                return Json(new { success = false, responseText = ex.Message });

            }

            return Json(new { success = true, responseText = "Successfully updated..." });

        }
        public class ExtraChitObject
        {
            public string Id;
            public string ItemName;
        }
        public class OnSpotItemObject
        {
            public string Id;
            public string Quantity;
            public string IsContinued;
        }

        public class EditableOnSpotItemObject
        {
            public string IsDelete;
            public string ItemId;
            public string Quantity;
            public string OfferId;
        }
    }
}
