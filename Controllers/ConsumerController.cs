
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;
using Mess_Management_System_Alpha_V2.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Mess_Management_System_Alpha_V2.Models.MessModels;

using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace Mess_Management_System_Alpha_V2.Controllers
{
    //[Authorize]
    public class ConsumerController : Controller
    {

        private readonly ApplicationDbContext _context;
        private UserManager<ApplicationUser> _userManager;
        private SignInManager<ApplicationUser> _signInManager;
        public ConsumerController(ApplicationDbContext context, UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager)
            
        {
            _context = context;
            _userManager = userManager;
            _signInManager = signInManager;
        }

        public class TodaysSetMenu
        {
            public long MealTypeId { get; set; }
            public string MealItemName { get; set; }
            public decimal? SetMenuPrice { get; set; }
        }

        [HttpPost]
        public async Task<IActionResult> Signout()
        {
            return Redirect("http://localhost:44381/Security/LogOut.aspx");

        }

        // GET: Consumer
        public async Task<ActionResult> Index()
        {
            string usrName = SessionExtensions.GetString(HttpContext.Session, "user");


            var user = await _userManager.FindByNameAsync(usrName);
            string UserId = user.Id;

            var fullName = user.BUPFullName;
            var email = user.BUPEmail;
            var userId = user.Id;

            ViewBag.FullName = fullName;
            ViewBag.Email = email;
            ViewBag.UserId = userId;

            ViewBag.CountAddedMealItem = null; //_context.MealItem.Where(x => x.IsAddedMealItem == true).Count();


            #region Getting User Settings

            //ConsumerMealActivation cma = new ConsumerMealActivation();
            //cma = _context.ConsumerMealActivation.Where(x => x.UserId == UserId && x.IsActive == true).FirstOrDefault();

            //if (cma != null)
            //{
            //    ViewBag.UserSettings = cma;
            //}
            ViewBag.UserSettings = null;

            #endregion



            //var todaysSetMenuList = (from smd in _context.SetMenuDetails
            //                         join smm in _context.SetMenuMaster on smd.SetMenuMasterId equals smm.Id
            //                         join mi in _context.MealItem on smd.MealItemId equals mi.Id
            //                         where (smm.SetMenuCreationDate.Date == DateTime.Now.Date)
            //                         select new TodaysSetMenu()
            //                         {
            //                             MealTypeId = smm.MealTypeId,
            //                             MealItemName = mi.MealItemName,
            //                             SetMenuPrice = smm.SetMenuPrice
            //                         }).ToList();

            ViewBag.BreakfastSetMenu = null; //todaysSetMenuList.Count > 0 ? todaysSetMenuList.Where(x => x.MealTypeId == 1).OrderBy(x => x.MealItemName).ToList() : null;
            ViewBag.LunchSetMenu = null; //todaysSetMenuList.Count > 0 ? todaysSetMenuList.Where(x => x.MealTypeId == 2).OrderBy(x => x.MealItemName).ToList() : null;
            ViewBag.TeaBreakSetMenu = null; //todaysSetMenuList.Count > 0 ? todaysSetMenuList.Where(x => x.MealTypeId == 3).OrderBy(x => x.MealItemName).ToList() : null;

            ViewBag.BreakfastTotalPrice = null; //todaysSetMenuList.Count > 0 ? todaysSetMenuList.Where(x => x.MealTypeId == 1).Select(x => x.SetMenuPrice).FirstOrDefault() : null;
            ViewBag.LunchTotalPrice = null; //todaysSetMenuList.Count > 0 ? todaysSetMenuList.Where(x => x.MealTypeId == 2).Select(x => x.SetMenuPrice).FirstOrDefault() : null;
            ViewBag.TeaBreakTotalPrice = null; //todaysSetMenuList.Count > 0 ? todaysSetMenuList.Where(x => x.MealTypeId == 3).Select(x => x.SetMenuPrice).FirstOrDefault() : null;

            #region Get 7 Days Set Meny

            DateTime today = DateTime.Now;
            var dayName = DateTime.Now.DayOfWeek;

            DateTime day2 = DateTime.Now.AddDays(1);
            var day2Name = DateTime.Now.AddDays(1).DayOfWeek;
            ViewBag.Day2Name = day2Name.ToString();

            DateTime day3 = DateTime.Now.AddDays(2);
            var day3Name = DateTime.Now.AddDays(2).DayOfWeek;
            ViewBag.Day3Name = day3Name.ToString();

            DateTime day4 = DateTime.Now.AddDays(3);
            var day4Name = DateTime.Now.AddDays(3).DayOfWeek;
            ViewBag.Day4Name = day4Name.ToString();

            DateTime day5 = DateTime.Now.AddDays(4);
            var day5Name = DateTime.Now.AddDays(4).DayOfWeek;
            ViewBag.Day5Name = day5Name.ToString();

            //var day2SetMenuList = (from smd in _context.SetMenuDetails
            //                       join smm in _context.SetMenuMaster on smd.SetMenuMasterId equals smm.Id
            //                       join mi in _context.MealItem on smd.MealItemId equals mi.Id
            //                       where (smm.SetMenuCreationDate.Date == day2.Date)
            //                       select new TodaysSetMenu()
            //                       {
            //                           MealTypeId = smm.MealTypeId,
            //                           MealItemName = mi.MealItemName,
            //                           SetMenuPrice = smm.SetMenuPrice
            //                       }).ToList();

            ViewBag.BreakfastSetMenuDay2 = null; //day2SetMenuList.Count > 0 ? day2SetMenuList.Where(x => x.MealTypeId == 1).OrderBy(x => x.MealItemName).ToList() : null;
            ViewBag.LunchSetMenuDay2 = null; //day2SetMenuList.Count > 0 ? day2SetMenuList.Where(x => x.MealTypeId == 2).OrderBy(x => x.MealItemName).ToList() : null;
            ViewBag.TeaBreakSetMenuDay2 = null; //day2SetMenuList.Count > 0 ? day2SetMenuList.Where(x => x.MealTypeId == 3).OrderBy(x => x.MealItemName).ToList() : null;


            //var day3SetMenuList = (from smd in _context.SetMenuDetails
            //                       join smm in _context.SetMenuMaster on smd.SetMenuMasterId equals smm.Id
            //                       join mi in _context.MealItem on smd.MealItemId equals mi.Id
            //                       where (smm.SetMenuCreationDate.Date == day3.Date)
            //                       select new TodaysSetMenu()
            //                       {
            //                           MealTypeId = smm.MealTypeId,
            //                           MealItemName = mi.MealItemName,
            //                           SetMenuPrice = smm.SetMenuPrice
            //                       }).ToList();

            ViewBag.BreakfastSetMenuDay3 = null; //day3SetMenuList.Count > 0 ? day3SetMenuList.Where(x => x.MealTypeId == 1).OrderBy(x => x.MealItemName).ToList() : null;
            ViewBag.LunchSetMenuDay3 = null; //day3SetMenuList.Count > 0 ? day3SetMenuList.Where(x => x.MealTypeId == 2).OrderBy(x => x.MealItemName).ToList() : null;
            ViewBag.TeaBreakSetMenuDay3 = null; //day3SetMenuList.Count > 0 ? day3SetMenuList.Where(x => x.MealTypeId == 3).OrderBy(x => x.MealItemName).ToList() : null;



            //var day4SetMenuList = (from smd in _context.SetMenuDetails
            //                       join smm in _context.SetMenuMaster on smd.SetMenuMasterId equals smm.Id
            //                       join mi in _context.MealItem on smd.MealItemId equals mi.Id
            //                       where (smm.SetMenuCreationDate.Date == day4.Date)
            //                       select new TodaysSetMenu()
            //                       {
            //                           MealTypeId = smm.MealTypeId,
            //                           MealItemName = mi.MealItemName,
            //                           SetMenuPrice = smm.SetMenuPrice
            //                       }).ToList();

            ViewBag.BreakfastSetMenuDay4 = null; //day4SetMenuList.Count > 0 ? day4SetMenuList.Where(x => x.MealTypeId == 1).OrderBy(x => x.MealItemName).ToList() : null;
            ViewBag.LunchSetMenuDay4 = null; //day4SetMenuList.Count > 0 ? day4SetMenuList.Where(x => x.MealTypeId == 2).OrderBy(x => x.MealItemName).ToList() : null;
            ViewBag.TeaBreakSetMenuDay4 = null; //day4SetMenuList.Count > 0 ? day4SetMenuList.Where(x => x.MealTypeId == 3).OrderBy(x => x.MealItemName).ToList() : null;


            //var day5SetMenuList = (from smd in _context.SetMenuDetails
            //                       join smm in _context.SetMenuMaster on smd.SetMenuMasterId equals smm.Id
            //                       join mi in _context.MealItem on smd.MealItemId equals mi.Id
            //                       where (smm.SetMenuCreationDate.Date == day5.Date)
            //                       select new TodaysSetMenu()
            //                       {
            //                           MealTypeId = smm.MealTypeId,
            //                           MealItemName = mi.MealItemName,
            //                           SetMenuPrice = smm.SetMenuPrice
            //                       }).ToList();

            ViewBag.BreakfastSetMenuDay5 = null; //day5SetMenuList.Count > 0 ? day5SetMenuList.Where(x => x.MealTypeId == 1).OrderBy(x => x.MealItemName).ToList() : null;
            ViewBag.LunchSetMenuDay5 = null; //day5SetMenuList.Count > 0 ? day5SetMenuList.Where(x => x.MealTypeId == 2).OrderBy(x => x.MealItemName).ToList() : null;
            ViewBag.TeaBreakSetMenuDay5 = null; //day5SetMenuList.Count > 0 ? day5SetMenuList.Where(x => x.MealTypeId == 3).OrderBy(x => x.MealItemName).ToList() : null;



            #endregion



            return View();
        }

        // GET: Admin/Details/5
        public async Task<ActionResult> PaymentHistory()
        {
            string usrName = SessionExtensions.GetString(HttpContext.Session, "user");
            var user = await _userManager.FindByNameAsync(usrName);
            //var user = await _userManager.GetUserAsync(User);
            var fullName = user.BUPFullName;
            var email = user.BUPEmail;

            ViewBag.FullName = fullName;
            ViewBag.Email = email;

            return View();
        }

        [HttpPost]
        [AutoValidateAntiforgeryToken]
        public async Task<IActionResult> FetchMenu()
        {

            var user = await _userManager.GetUserAsync(User);
            string UserId = user.Id;
            string d = Request.Form["menudate"];

            string[] DateFormatarray = d.Split("-");
            string datesv = DateFormatarray[1] + '/' + DateFormatarray[0] + '/' + DateFormatarray[2];
            DateTime date = DateTime.ParseExact(datesv, "M/d/yyyy", CultureInfo.InvariantCulture);
            ViewBag.MenuDate = date;
            //ViewBag.MenuDate = DateTime.Parse(d);
            ViewBag.UserId = UserId;

            return View("Index");

        }



        [HttpPost]
        [AutoValidateAntiforgeryToken]
        public async Task<IActionResult> EditDefaultChoice(int MealId, string dateString)
        {
            string usrName = SessionExtensions.GetString(HttpContext.Session, "user");
            var user = await _userManager.FindByNameAsync(usrName);
            string UserId = user.Id;
            long setMenuId = 0;


            if (MealId == 1)
            {
                setMenuId = long.Parse(Request.Form["setmenu"]);
            }
            else if (MealId == 2)
            {
                setMenuId = long.Parse(Request.Form["setmenu2"]);
            }
            else
            {
                setMenuId = long.Parse(Request.Form["setmenu3"]);
            }
            var prevCCRecord = _context.CustomerChoiceV2.Where(x => x.MealTypeId == MealId && x.UserId == UserId && x.Date.ToShortDateString() == dateString && x.OrderTypeId == 10003).ToList();
            if (prevCCRecord.Count > 0)
            {
                foreach (var item in prevCCRecord)
                {
                    _context.CustomerChoiceV2.Remove(item);
                    _context.SaveChanges();

                }
            }


            try
            {
                var SetMenuDetail = _context.SetMenuDetails.Where(x => x.SetMenuId == setMenuId).ToList();
                foreach (var i in SetMenuDetail)
                {
                    CustomerChoiceV2 cc = new CustomerChoiceV2();
                    cc.LastModifiedBy = UserId;
                    cc.LastModifiedDate = DateTime.Now;
                    cc.Date = DateTime.Parse(dateString);

                    cc.quantity = 1;
                    cc.UserId = UserId;
                    cc.OrderTypeId = 10003;
                    cc.CreatedBy = UserId;
                    cc.MealTypeId = MealId;
                    cc.SetMenuId = setMenuId;
                    cc.ExtraItemId = i.ExtraItemId;
                    cc.CreatedDate = DateTime.Now;
                    _context.CustomerChoiceV2.Add(cc);
                    _context.SaveChanges();

                }




            }
            catch (Exception ex)
            {
                return Json(ex);
            }
            ViewBag.UserId = UserId;

            return View("Index");
        }
        [HttpPost]
        [AutoValidateAntiforgeryToken]
        public async Task<IActionResult> EditExtraSMChoice(int MealId, string dateString)
        {

            string usrName = SessionExtensions.GetString(HttpContext.Session, "user");
            var user = await _userManager.FindByNameAsync(usrName);
            string UserId = user.Id;
            long setMenuId = 0;
            double q = 0;

            if (MealId == 1)
            {
                setMenuId = long.Parse(Request.Form["exsetmenu"]);
                q = double.Parse(Request.Form["EsmBrkQ"]);
            }
            else if(MealId == 2)
            {
                setMenuId = long.Parse(Request.Form["exsetmenu2"]);
                q = double.Parse(Request.Form["EsmLnQ"]);
            }
            else
            {
                setMenuId = long.Parse(Request.Form["exsetmenu3"]);
                q = double.Parse(Request.Form["EsmTQ"]);
            }
          
            var prevCCRecord = _context.CustomerChoiceV2.Where(x => x.MealTypeId == MealId && x.UserId == UserId && x.SetMenuId == setMenuId && x.Date.ToShortDateString() == dateString && x.OrderTypeId == 3).ToList();
            if (prevCCRecord.Count > 0)
            {
                foreach (var item in prevCCRecord)
                {
                    _context.CustomerChoiceV2.Remove(item);
                    _context.SaveChanges();

                }
            }


            try
            {

                var SetMenuDetail = _context.SetMenuDetails.Where(x => x.SetMenuId == setMenuId).ToList();
                foreach (var i in SetMenuDetail)
                {
                    CustomerChoiceV2 cc = new CustomerChoiceV2();
                    cc.LastModifiedBy = UserId;
                    cc.LastModifiedDate = DateTime.Now;
                    cc.Date = DateTime.Parse(dateString);

                    cc.quantity = q;
                    cc.UserId = UserId;
                    cc.OrderTypeId = 3;
                    cc.CreatedBy = UserId;
                    cc.MealTypeId = MealId;
                    cc.SetMenuId = setMenuId;
                    cc.ExtraItemId = i.ExtraItemId;
                    cc.CreatedDate = DateTime.Now;
                    _context.CustomerChoiceV2.Add(cc);
                    _context.SaveChanges();

                }


            }
            catch (Exception ex)
            {
                return Json(ex);
            }


            ViewBag.UserId = UserId;

            return View("Index");


        }

        [HttpPost]
        public async Task<JsonResult> UpdateExtraItemList(string ExtraItemList, string MealId, string Date)
        {
            long mealid = long.Parse(MealId);
            string[] DateFormatarray = Date.Split("-");
            string datesv = DateFormatarray[1] + '/' + DateFormatarray[0] + '/' + DateFormatarray[2];
            DateTime choiceDate = DateTime.ParseExact(datesv, "M/d/yyyy", CultureInfo.InvariantCulture);

            List<ItemIds> tempSetMenuList = (List<ItemIds>)JsonConvert.DeserializeObject(ExtraItemList, typeof(List<ItemIds>));
            var prevCCRecord = _context.CustomerChoiceV2.Where(x => x.MealTypeId == mealid && x.Date.ToShortDateString() == choiceDate.ToShortDateString() && x.OrderTypeId == 4).ToList();
            if (prevCCRecord.Count > 0)
            {
                foreach (var item in prevCCRecord)
                {
                    _context.CustomerChoiceV2.Remove(item);
                    _context.SaveChanges();

                }
            }

            string usrName = SessionExtensions.GetString(HttpContext.Session, "user");
            var user = await _userManager.FindByNameAsync(usrName);
            string UserId = user.Id;
            try
            {
                foreach (var item in tempSetMenuList)
                {
                    CustomerChoiceV2 cc = new CustomerChoiceV2();

                    var mnItm = _context.MenuItem.Where(x => x.Id == Int32.Parse(item.ItemId)).FirstOrDefault(); 
                    cc.LastModifiedBy = UserId;
                    cc.LastModifiedDate = DateTime.Now;
                    cc.Date = choiceDate;
                    cc.ExtraItemId = mnItm.ExtraItemId;
                    cc.quantity = double.Parse(item.Quantity);
                    cc.UserId = UserId;
                    cc.OrderTypeId = 4;
                    cc.CreatedBy = UserId;
                    cc.MealTypeId = mealid;
                    cc.CreatedDate = DateTime.Now;
                    _context.CustomerChoiceV2.Add(cc);
                    _context.SaveChanges();
                }

            }
            catch (Exception ex)
            {
                return Json(ex);
            }

            return Json(new { success = true, responseText = "" });

        }




        [HttpPost]
        public async Task<JsonResult> UserPeriodModify(string From, string To, string Checked, string MealId)
        {
            Period p = new Period();

            Boolean active = false;
            if (Checked == "false")
            {
                active = false;
            }
            else
            {
                active = true;
            }
            string[] DateFormatarray = From.Split("-");

            string datesv = DateFormatarray[1] + '/' + DateFormatarray[0] + '/' + DateFormatarray[2];
            DateTime F = DateTime.ParseExact(datesv, "M/d/yyyy", CultureInfo.InvariantCulture);

            string[] DateFormatarray2 = To.Split("-");
            string datesv2 = DateFormatarray2[1] + '/' + DateFormatarray2[0] + '/' + DateFormatarray2[2];
            DateTime T = DateTime.ParseExact(datesv2, "M/d/yyyy", CultureInfo.InvariantCulture);
            string usrName = SessionExtensions.GetString(HttpContext.Session, "user");
            var user = await _userManager.FindByNameAsync(usrName);
            string UserId = user.Id;
            p.UserId = UserId;
            p.MealTypeId = Int32.Parse(MealId);
            p.From = F;
            p.To = T;
            p.Active = active;
            p.LastModifiedDate = DateTime.Now;
            p.CreatedDate = DateTime.Now;
            _context.Period.Add(p);
            _context.SaveChanges();

            // return View("Index");

            return Json(new { success = true, responseText = "Period has been modified successfully." });
        }


        [HttpPost]
        public async Task<JsonResult> OnOffModify(string Checked, string MealId)
        {
            string usrName = SessionExtensions.GetString(HttpContext.Session, "user");
            var user = await _userManager.FindByNameAsync(usrName);
            int mealId = Int32.Parse(MealId);
            string UserId = user.Id;
            bool preorderset = false;
            if (Checked == "false")
            {
                preorderset = false;
            }
            else
            {
                preorderset = true;
            }
            var pos = _context.PreOrderSchedule.Where(x => x.UserId == UserId && x.LastModifiedDate.ToShortDateString() == DateTime.Now.ToShortDateString() && x.MealTypeId == mealId).FirstOrDefault();
            if (pos != null)
            {
                _context.PreOrderSchedule.Remove(pos);
                _context.SaveChanges();
            }
            _context.PreOrderSchedule.Add(new Models.MessModels.PreOrderSchedule
            {
                UserId = UserId,
                IsPreOrderSet = preorderset,
                MealTypeId = mealId,
                LastConfigurationUpdateDate = DateTime.Now,
                CreatedDate = DateTime.Now,
                LastModifiedDate = DateTime.Now,
                CreatedBy = UserId,
                LastModifiedBy = UserId,
                //SetMenuId = setMenuTomorrow.FirstOrDefault().Id

            });
            _context.SaveChanges();

            //PreOrderSchedule po = new PreOrderSchedule();
            //po.LastModifiedDate = DateTime.Now;
            //po.CreatedDate = DateTime.Now;
            //po.UserId = UserId;
            //po.IsPreOrderSet = preorderset;
            //po.LastConfigurationUpdateDate = DateTime.Now;
            //po.MealTypeId = mealId;
            //_context.PreOrderSchedule.Add(po);
            //_context.SaveChanges();

            return Json(new { success = true, responseText = "On/Off has been modified successfully." });

        }
        [HttpPost]
        public async Task<JsonResult> UpdateMealActivation(bool CheckBoxValue, int categoryIdValue)
        {
            try
            {
                //var user = await _userManager.GetUserAsync(User);
                //string UserId = user.Id;

                //ConsumerMealActivation tempcma = _context.ConsumerMealActivation.Where(x => x.UserId == UserId).FirstOrDefault();


                //if (categoryIdValue == 1) // Breakfast
                //{
                //    if (tempcma != null)
                //    {
                //        tempcma.IsBreakfastOnOff = CheckBoxValue;
                //        tempcma.ModifiedBy = UserId;
                //        tempcma.ModifiedDate = DateTime.Now;

                //        _context.ConsumerMealActivation.Update(tempcma);
                //        _context.SaveChanges();

                //        return Json(new { success = true, responseText = "Data Update Successful." });
                //    }
                //    else
                //    {
                //        return Json(new { success = true, responseText = "Data Update Failed !!!" });
                //    }
                //}
                //else if (categoryIdValue == 2) // Lunch
                //{
                //    if (tempcma != null)
                //    {
                //        tempcma.IsLunchOnOff = CheckBoxValue;
                //        tempcma.ModifiedBy = UserId;
                //        tempcma.ModifiedDate = DateTime.Now;

                //        _context.ConsumerMealActivation.Update(tempcma);
                //        _context.SaveChanges();

                //        return Json(new { success = true, responseText = "Data Update Successful." });
                //    }
                //    else
                //    {
                //        return Json(new { success = true, responseText = "Data Update Failed !!!" });
                //    }
                //}
                //else // Tea Break
                //{
                //    if (tempcma != null)
                //    {
                //        tempcma.IsTeaBreakOnOff = CheckBoxValue;
                //        tempcma.ModifiedBy = UserId;
                //        tempcma.ModifiedDate = DateTime.Now;

                //        _context.ConsumerMealActivation.Update(tempcma);
                //        _context.SaveChanges();

                //        return Json(new { success = true, responseText = "Data Update Successful." });
                //    }
                //    else
                //    {
                //        return Json(new { success = true, responseText = "Data Update Failed !!!" });
                //    }
                //}

                return Json(new { success = true, responseText = "Data Update Failed !!!" });
            }
            catch (Exception ex)
            {
                return Json(new { success = false, responseText = "Data Update Failed !!!" + " " + ex.Message });
            }

        }

        public class ItemIds
        {
            public string ItemId { set; get; }
            public string Quantity { set; get; }
        }






















        //// GET: Consumer
        //public ActionResult Index()
        //{
        //    return View();
        //}

        //// GET: Consumer/Details/5
        //public ActionResult Details(int id)
        //{
        //    return View();
        //}

        //// GET: Consumer/Create
        //public ActionResult Create()
        //{
        //    return View();
        //}

        //// POST: Consumer/Create
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public ActionResult Create(IFormCollection collection)
        //{
        //    try
        //    {
        //        // TODO: Add insert logic here

        //        return RedirectToAction(nameof(Index));
        //    }
        //    catch
        //    {
        //        return View();
        //    }
        //}

        //// GET: Consumer/Edit/5
        //public ActionResult Edit(int id)
        //{
        //    return View();
        //}

        //// POST: Consumer/Edit/5
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public ActionResult Edit(int id, IFormCollection collection)
        //{
        //    try
        //    {
        //        // TODO: Add update logic here

        //        return RedirectToAction(nameof(Index));
        //    }
        //    catch
        //    {
        //        return View();
        //    }
        //}

        //// GET: Consumer/Delete/5
        //public ActionResult Delete(int id)
        //{
        //    return View();
        //}

        //// POST: Consumer/Delete/5
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public ActionResult Delete(int id, IFormCollection collection)
        //{
        //    try
        //    {
        //        // TODO: Add delete logic here

        //        return RedirectToAction(nameof(Index));
        //    }
        //    catch
        //    {
        //        return View();
        //    }
        //}
    }
}