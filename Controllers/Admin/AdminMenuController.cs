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
using Microsoft.AspNetCore.Authorization;


// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Mess_Management_System_Alpha_V2.Controllers.Admin
{
    //[Authorize(Roles = "Admin,MessAdmin")]

    public class AdminMenuController : Controller
    {
        // GET: /<controller>/

        private readonly ApplicationDbContext _context;
        private UserManager<ApplicationUser> _userManager;
        private RoleManager<UserIdentityRole> _roleManager;

        public AdminMenuController(ApplicationDbContext context, UserManager<ApplicationUser> userManager, RoleManager<UserIdentityRole> roleManager)
        {
            _context = context;
            _userManager = userManager;
            _roleManager = roleManager;
        }
        //public AdminMenuController(ApplicationDbContext context)
        //{
        //    _context = context;
          
        //}


        public IActionResult AdminOrderShow()
        {
            return View();
        }
        public async Task<IActionResult> Menu()
        {

            if (SessionExist())
            {
                if (await UserExistMess())
                {
                   
                    var role2 = await GetLogInUserRoleObjectAsync();
                    var roleMenuList2 = _context.RoleMenu.Where(x => x.UserIdentityRoleId == role2.Id).ToList();
                    var nevMenuList2 = _context.NavigationMenu.Where(x => x.Id > 0).ToList();
                    var pr2 = from r in roleMenuList2
                              join n in nevMenuList2
                              on r.NavigationMenuId equals n.Id
                              // where o.LastModifiedDate.ToShortDateString() == OD.AddDays(-1).ToShortDateString() && o.MealTypeId == 1
                              select n;
                    var filterMenuList2 = pr2.ToList();

                    ViewBag.FilterMenuList = filterMenuList2;
                    ViewBag.MealItemList = _context.StoreOutItem.ToList();
                    ViewBag.ExtraItemList = _context.StoreOutItem.ToList();
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
        public IActionResult Test()
        {
            ViewBag.MealItemList = _context.StoreOutItem.ToList();
            ViewBag.ExtraItemList = _context.StoreOutItem.ToList();
            return View();

        }
        public ActionResult BillProcess()
        {
            //var result = _context.WarehouseStorage.Include(x => x.StoreInItem).ToList();
            return View();
        }

        [HttpPost]
        [AutoValidateAntiforgeryToken]
        public async Task<IActionResult> DeleteDayMenu(int StrItmId,string submit)
        {

            //if (submit == "Delete")
            //{
                try
                {
                    MenuItem mit = _context.MenuItem.Where(x => x.Id == StrItmId).FirstOrDefault();
                 
                    _context.MenuItem.Remove(mit);
                    _context.SaveChanges();

                    return RedirectToAction(nameof(Menu));
                }
                catch(Exception ex)
                {
                    return RedirectToAction(nameof(Menu));
                }
 
        }
        [HttpPost]
        [AutoValidateAntiforgeryToken]
        public async Task<IActionResult> EditDayMenu(int StrItmId)
        {
            var ExtraItem = _context.ExtraItem.Where(x => x.Id == StrItmId).FirstOrDefault();
            ViewBag.ExtraEditedItem = ExtraItem;
            ViewBag.FullName = await GetUserName();

            return View("Menu");


        }
        [HttpPost]
        [AutoValidateAntiforgeryToken]
        public async Task<IActionResult> DeleteSetMenu(int SetMenuId)
        {

            try
            {
                DailySetMenu dst = _context.DailySetMenu.Where(x => x.Id == SetMenuId).FirstOrDefault();


               

                _context.DailySetMenu.Remove(dst);
                _context.SaveChanges();



                return RedirectToAction(nameof(Menu));

            }
            catch
            {
                return RedirectToAction(nameof(Menu));

            }



        }
        [HttpPost]
        [AutoValidateAntiforgeryToken]
        public async Task<IActionResult> ExtraItemSave()
        {
            int MealId = Int32.Parse(Request.Form["meal"]);
            int DayId = Int32.Parse(Request.Form["day"]);
            int storeoutItemId = Int32.Parse(Request.Form["item"]);
            double price = Int32.Parse(Request.Form["price"]);


            //var user = await _userManager.GetUserAsync(User);
            //var userID = user.Id;

            ExtraItem et = new ExtraItem();

            et.Date = DateTime.Now;
            et.MenuDate = DateTime.Now;
            et.Price = price;
            et.SetMenuId = 50107;
            et.MealTypeId = MealId;
            et.Day = DayId;
            et.StoreOutItemId = storeoutItemId;

           // et.CreatedBy = userID;
            et.CreatedDate = DateTime.Now;
            et.LastModifiedDate = DateTime.Now;


            _context.ExtraItem.Add(et);
            _context.SaveChanges();





            return RedirectToAction(nameof(Menu));

        }


        [HttpPost]
        [AutoValidateAntiforgeryToken]
        public async Task<IActionResult> SaveNewItem()
        {
            string item = Request.Form["newItem"];
            int CategoryId = Int32.Parse(Request.Form["DDLItemCategory"]);
            int UnitId = Int32.Parse(Request.Form["DDLUnitCategory"]);
            int MealId = Int32.Parse(Request.Form["DDLMealCategory"]);




            //var user = await _userManager.GetUserAsync(User);
            //var userID = user.Id;

            StoreOutItem stn = new StoreOutItem();
          //  stn.CreatedBy = user.Id;
            stn.Name = item;
            stn.IsOpen = true;
            stn.Day = 4;
            stn.MealTypeId = MealId;
            stn.CreatedDate = DateTime.Now;
            stn.MinimumProductionUnit = 10;
            stn.MinimumProductionUnitMultiplier = 5;
            stn.UnitTypeId = UnitId;
            stn.StoreOutCategoryId = CategoryId;
            _context.StoreOutItem.Add(stn);
            _context.SaveChanges();
            long storeOutItemId = stn.Id;


            return RedirectToAction(nameof(Menu));

        }

        [HttpPost]
        [AutoValidateAntiforgeryToken]
        public async Task<IActionResult> SaveDayItem(int itemId)
        {
            double price = double.Parse(Request.Form["itemPriceAdded"]);
            ExtraItem et = new ExtraItem();
            et.StoreOutItemId = itemId;
            return RedirectToAction(nameof(Menu));

        }
        [HttpPost]
        [AutoValidateAntiforgeryToken]
        public async Task<IActionResult> Check(int id, string userid)
        {
            var value = Request.Form["meal"];
            double quantity = 0;
            Double.TryParse(Request.Form["quantity"], out quantity);
            int mealTypeId = Int32.Parse(Request.Form["meal"]);

            //var user = await _userManager.GetUserAsync(User);
            //var userID = user.Id;

            var HistoryForExtraItem = _context.ExtraItem.Where(x => x.MenuDate.ToShortDateString() == DateTime.Now.ToShortDateString() && x.StoreOutItemId == id).FirstOrDefault();



            OrderHistory oh = new OrderHistory();
            oh.UserId = userid;
            oh.MealTypeId = mealTypeId;
            oh.StoreOutItemId = id;
            oh.UnitOrdered = quantity;
            oh.OrderAmount = Convert.ToDouble(HistoryForExtraItem.Price * quantity);
            oh.IsPreOrder = false;
            oh.OrderDate = DateTime.Now;

           // oh.CreatedBy = userID;
            oh.CreatedDate = DateTime.Now;
            oh.LastModifiedDate = DateTime.Now;


            _context.OrderHistory.Add(oh);
            _context.SaveChanges();
            long orderHistoryId = oh.Id;


            return RedirectToAction(nameof(Menu));
        }

        [HttpPost]
        //[ValidateAntiForgeryToken]

        public async Task<IActionResult> CreateOnspotOrder(string MealTypeId, string ItemId)
        {
            //return RedirectToAction(nameof(home));
            return Json(new { success = true, responseText = "" });

        }


        public ActionResult DatePerson(DateTime? from, string searchInput)
        {
           // var user = _userManager.Users.Where(x => x.BUPFullName == searchInput).FirstOrDefault();
            ViewBag.OrderDate = from;
           // ViewBag.UserId = user.Id;
            return View("home");

        }

        public JsonResult GetSearchPerson(string search)
        {
            // var PersonList = _userManager.Users.Where(x => x.BUPFullName.Contains(search)).ToList();
            //List<CountryModel> allsearch = db.Countries.Where(x => x.Name.Contains(search)).Select(x => new CountryModel
            //{
            //    Id = x.Id,
            //    Name = x.Name
            //}).ToList();
            // return Json(PersonList);
            return Json("Must be Modified");
        }

        [HttpPost]
        public async Task<IActionResult> SaveNewMenuItem(string Day, string MealTypeId, string Item)
        {


            //var user = await _userManager.GetUserAsync(User);
            //var userID = user.Id;

            //var exObj = _context.ExtraItem.(x => x.Day == Int32.Parse(Day) && x.MealTypeId == Int32.Parse(MealTypeId) && x.StoreOutItemId == Int32.Parse(Item)).FirstOrDefault();
            //if(exObj == null)
            //{

            //}

            
            
            var menuList = _context.MenuItem.Where(x => x.Day == Int32.Parse(Day) && x.MealTypeId == Int32.Parse(MealTypeId)).ToList();
            
            foreach(var m in menuList)
            {
                var exObj = _context.ExtraItem.Where(x => x.Id == m.ExtraItemId).FirstOrDefault();
                if(exObj.StoreOutItemId == Int32.Parse(Item))
                {
                    return Json(new { success = false, responseText = "Item already exists" });

                }
                
            }
            //if(obj != null)
            //{
            //    return Json(new { success = false, responseText = "Item already exists" });

            //}

            ExtraItem et = new ExtraItem();

            et.Date = DateTime.Now;
            et.MenuDate = DateTime.Now;
            et.Price = 10;
            et.SetMenuId = 50107;
            et.MealTypeId = Int32.Parse(MealTypeId);
            et.Day = Int32.Parse(Day);
            et.StoreOutItemId = Int32.Parse(Item);

          //  et.CreatedBy = userID;
            et.CreatedDate = DateTime.Now;
            et.LastModifiedDate = DateTime.Now;

            //ExtraItem stn = new ExtraItem();
            //stn.CreatedBy = user.Id;
            //stn.Day = Int32.Parse(Day);
            //stn.MealTypeId = Int32.Parse(MealTypeId);
            //stn.CreatedDate = DateTime.Now;
   
          
            _context.ExtraItem.Add(et);

           

            _context.SaveChanges();
            long ExtraItemId = et.Id;

            MenuItem mt = new MenuItem();
            mt.CreatedDate = DateTime.Now;
            mt.LastModifiedDate = DateTime.Now;
            mt.ExtraItemId = ExtraItemId;
            mt.Day = Int32.Parse(Day);
            mt.MealTypeId = Int32.Parse(MealTypeId);

            _context.MenuItem.Add(mt);



            _context.SaveChanges();


            return Json(new { success = true, responseText = "" });


        }

        [HttpPost]
        public async Task<IActionResult> SaveSetMenu(string Day, string MealTypeId, string Date,string SetMenuItem)
        {
            //var user = await _userManager.GetUserAsync(User);
            //var userID = user.Id;
            List<ItemIds> tempSetMenuList = (List<ItemIds>)JsonConvert.DeserializeObject(SetMenuItem, typeof(List<ItemIds>));
            SetMenu tempData = new SetMenu();
            long setMenuMasterId;

            tempData.SetMenuDate = Convert.ToDateTime(DateTime.Parse(Date));
            tempData.Serial = Convert.ToInt32(Int32.Parse(MealTypeId));
            tempData.IsAvailableAsExtra = false;
            tempData.SetMenuPrice = 100;
            tempData.Day = Convert.ToInt32(Int32.Parse(Day));
            tempData.MealTypeId = Convert.ToInt32(Int32.Parse(MealTypeId));

           // tempData.CreatedBy = userID;
            tempData.CreatedDate = DateTime.Now;
            tempData.LastModifiedDate = DateTime.Now;

            _context.SetMenu.Add(tempData);
            _context.SaveChanges();
            setMenuMasterId = tempData.Id;

            if (setMenuMasterId > 0)
            {




                if (tempSetMenuList.Count > 0)
                {

                    foreach (var data in tempSetMenuList)
                    {
                        SetMenuDetails setMenuDetails = new SetMenuDetails();
                        long MenuItmId = Convert.ToInt64(data.ItemId);
                        var MenuItem = _context.MenuItem.Where(x => x.Id == MenuItmId).FirstOrDefault();
                        var Extraitemid = MenuItem.ExtraItemId;
                        var ExtraItem = _context.ExtraItem.Where(x => x.Id == Extraitemid).FirstOrDefault();



                        setMenuDetails.SetMenuId = setMenuMasterId;
                        setMenuDetails.StoreOutItemId = ExtraItem.StoreOutItemId;
                        setMenuDetails.ExtraItemId = Extraitemid;

                       // setMenuDetails.CreatedBy = userID;
                        setMenuDetails.CreatedDate = DateTime.Now;
                        setMenuDetails.LastModifiedDate = DateTime.Now;


                        _context.SetMenuDetails.Add(setMenuDetails);
                        _context.SaveChanges();

                        //----------------------------------------------------------------------


                    }

                }
            }

            DailySetMenu dsm = new DailySetMenu();
            dsm.LastModifiedDate = DateTime.Now;
            dsm.CreatedDate = DateTime.Now;
            dsm.MealTypeId = Int32.Parse(MealTypeId);
            dsm.SetMenuDate = DateTime.Parse(Date);
            dsm.Day = Int32.Parse(Day);
            dsm.SetMenuId = setMenuMasterId;

            _context.DailySetMenu.Add(dsm);
            _context.SaveChanges();


            return Json(new { success = true, responseText = "" });


        }

        public class ItemIds
        {
            public string ItemId { set; get; }
        }


        //[HttpPost]
        ////[ValidateAntiForgeryToken]

        //public async Task<IActionResult> CreateOrderHistoryOfSetMenu(string MealTypeId)
        //{
        //    List<SqlParameter> parameterListV3 = new List<SqlParameter>();
        //    int mealTypeId = Convert.ToInt32(MealTypeId);
        //    var user = await _userManager.GetUserAsync(User);
        //    var userID = user.Id;
        //    //var preOrderScheduleList = _context.PreOrderSchedule.Include(x => x.MealType).Include(x => x.ApplicationUser).ToList();
        //    var setMenuList = _context.SetMenu.Include(x => x.SetMenuDetailsList).Include(a => a.MealType).Where(x => x.SetMenuDate.ToShortDateString() == DateTime.Now.ToShortDateString()).ToList();

        //    var setMenuOnMeal = setMenuList.Where(x => x.MealTypeId == mealTypeId).FirstOrDefault();
        //    //foreach (var itm in preOrderScheduleList.Select(a => a.MealType).Distinct().OrderBy(a => a.Serial).ToList())
        //    //{
        //    //    foreach (var a in itm.PreOrderScheduleList) {

        //    if (setMenuOnMeal != null)
        //    {

        //        //var existingEntry = _context.OrderHistory.Where(a => a.CreatedBy == userID && a.CreatedDate == DateTime.Now && a.MealTypeId == mealTypeId && a.SetMenuId == setMenuOnMeal.Id).FirstOrDefault();
        //        var existingEntry = _context.OrderHistory.Where(a => a.CreatedBy == userID && a.CreatedDate.ToShortDateString() == DateTime.Now.ToShortDateString() && a.MealTypeId == mealTypeId && a.SetMenuId == setMenuOnMeal.Id).ToList();

        //        if (existingEntry.Count == 0)
        //        {
        //            try
        //            {
        //                parameterListV3.Add(new SqlParameter { ParameterName = "MealTypeId", SqlDbType = System.Data.SqlDbType.BigInt, Value = mealTypeId });
        //                parameterListV3.Add(new SqlParameter { ParameterName = "UserId", SqlDbType = System.Data.SqlDbType.NVarChar, Value = userID });


        //                string spNameV3 = "CreateOrderHistoryOfSetMenu";

        //                string connStrV3 = Startup.ConnectionString;

        //                DataTable dtV3 = new DataTable();
        //                using (SqlConnection connection = new SqlConnection(connStrV3))
        //                {
        //                    SqlCommand command = new SqlCommand(spNameV3, connection);
        //                    command.CommandType = System.Data.CommandType.StoredProcedure;
        //                    foreach (var item in parameterListV3)
        //                    {
        //                        command.Parameters.Add(item);
        //                    }

        //                    try
        //                    {
        //                        connection.Open();
        //                        SqlDataReader reader;
        //                        reader = command.ExecuteReader();
        //                        dtV3.Load(reader);
        //                    }
        //                    finally
        //                    {
        //                        connection.Close();
        //                    }
        //                }
        //                if (dtV3.Rows.Count > 0)
        //                {


        //                    foreach (DataRow row in dtV3.Rows)
        //                    {
        //                        if (Convert.ToInt32(row["ROWCOUNT"]) > 0)

        //                        {
        //                            return Json(new { success = true, responseText = "Order has been generated successfully for this meal." });

        //                        }
        //                        else
        //                        {
        //                            return Json(new { success = true, responseText = "Something is wrong.Order is not getting generated." });

        //                        }
        //                    }





        //                }
        //            }
        //            catch (Exception ex)
        //            {
        //                return Json(new { success = true, responseText = ex.Message });

        //            }
        //        }
        //        else
        //        {
        //            return Json(new { success = true, responseText = "Order has already been generated for this meal" });

        //        }
        //    }
        //    else
        //    {
        //        return Json(new { success = true, responseText = "Today SetMenu is not defined for this meal" });

        //    }
        //    return RedirectToAction(nameof(Index));




        //}

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

    }
}

