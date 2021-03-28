
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
using System.Collections;
using Microsoft.AspNetCore.Hosting;

namespace Mess_Management_System_Alpha_V2.Controllers
{
    //[Authorize]
    public class ConsumerController : Controller
    {

        private readonly ApplicationDbContext _context;
        private UserManager<ApplicationUser> _userManager;
        private SignInManager<ApplicationUser> _signInManager;
        private IHostingEnvironment _env;

        public ConsumerController(ApplicationDbContext context, UserManager<ApplicationUser> userManager, 
            SignInManager<ApplicationUser> signInManager, IHostingEnvironment env)
            
        {
            _context = context;
            _userManager = userManager;
            _signInManager = signInManager;
            _env = env;
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

        [HttpPost]
        public async Task<IActionResult> AddExtraChitItem(string ItemList, string MealType, string Remarks)
        {
            string usrName = SessionExtensions.GetString(HttpContext.Session, "user");

            if (usrName == null)
            {
                return Json(new { success = false, responseText = "Expire" });

            }

            var user = await _userManager.FindByNameAsync(usrName);
            string UserId = user.Id;
            long parentId = 0;
            try
            {
                List<ItemIds> tempItemList = (List<ItemIds>)JsonConvert.DeserializeObject(ItemList, typeof(List<ItemIds>));
                var prevParent = _context.ConsumerMealWiseExtraChitParent.Where(x => x.UserId == UserId && x.MealTypeId == Int32.Parse(MealType)).FirstOrDefault();
                if(prevParent == null)
                {
                    ConsumerMealWiseExtraChitParent ccp = new ConsumerMealWiseExtraChitParent();
                    ccp.MealTypeId = Int32.Parse(MealType);
                    ccp.OrderTypeId = 4;
                    ccp.UserId = UserId;
                    ccp.Remarks = Remarks;
                    ccp.CreatedBy = UserId;
                    ccp.CreatedDate = DateTime.Now;
                    ccp.LastModifiedBy = UserId;
                    ccp.LastModifiedDate = DateTime.Now;
                    _context.ConsumerMealWiseExtraChitParent.Add(ccp);
                    _context.SaveChanges();
                    parentId = ccp.Id;
                }
                else
                {
                    prevParent.Remarks = Remarks;
                    _context.ConsumerMealWiseExtraChitParent.Update(prevParent);
                    parentId = prevParent.Id;
                }
                
                foreach (var item in tempItemList)
                {
                    var _prevObj = _context.ConsumerMealWiseExtrachit.Where(x => x.StoreOutItemId == long.Parse(item.ItemId) && 
                                    x.ConsumerMealWiseExtraChitParentId == parentId).FirstOrDefault();

                    if (_prevObj == null)
                    {
                        ConsumerMealWiseExtrachit cc = new ConsumerMealWiseExtrachit();
                   
                        cc.StoreOutItemId = long.Parse(item.ItemId);
                        cc.quantity = double.Parse(item.Quantity);
                        cc.CreatedBy = UserId;
                        cc.LastModifiedBy = UserId;
                        cc.ConsumerMealWiseExtraChitParentId = parentId;
                        cc.CreatedDate = DateTime.Now;
                        cc.LastModifiedDate = DateTime.Now;
                        _context.ConsumerMealWiseExtrachit.Add(cc);
                        _context.SaveChanges();
                    }
                    else
                    {
                        _prevObj.quantity = double.Parse(item.Quantity);
                        _context.ConsumerMealWiseExtrachit.Update(_prevObj);
                        _context.SaveChanges();
                    }
                }

            }
            catch (Exception ex)
            {
                return Json(new { success = false, responseText = ex.Message });

            }
            return Json(new { success = true, responseText = "Successfully added" });

        }
        [HttpPost]
        public async Task<JsonResult> DeleteDate(string DetailId)
        {
            string usrName = SessionExtensions.GetString(HttpContext.Session, "user");

            if (usrName == null)
            {
                return Json(new { success = false, responseText = "Expire" });

            }
            var dateObj = _context.UserDateChoiceDetail.Where(x => x.Id == long.Parse(DetailId)).FirstOrDefault();
            _context.UserDateChoiceDetail.Remove(dateObj);
            _context.SaveChanges();
            return Json(new { success = true, responseText = "Date is removed successfully." });

        }

        public JsonResult GetSearchPerson(string search)
        {
            var PersonList = _userManager.Users.Where(x => x.BUPFullName.Contains(search) || x.OfficeName.Contains(search) || x.BUPNumber.Contains(search)).ToList();

            return Json(PersonList);
        }

        public JsonResult GetSearchOffice(string search)
        {
            var OfficeList = _context.Office.Where(x => x.Name.Contains(search)).ToList();

            return Json(OfficeList);
        }

        public JsonResult GetExtraChit()
        {

            int day = (int)DateTime.Today.DayOfWeek + 1;
            List<ExtraChitObject> exChtList = new List<ExtraChitObject>();

            //var OnSpotItemList = _context.MenuItem.Where(x => x.Day == day).ToList();
            var OnSpotItemList = _context.DailyOfferItem.Where(x => x.Date.Date == DateTime.Now.Date || x.IsActive == true).ToList();

            foreach (var i in OnSpotItemList)
            {
                //var ext = _context.ExtraItem.Where(x => x.Id == i.ExtraItemId).FirstOrDefault();
                var itemCount = _context.CustomerChoiceV2.Where(x => x.Date.Date == DateTime.Now.Date && x.OrderTypeId == 10002
                                 && x.MealTypeId == 10005 && x.StoreOutItemId == i.StoreOutItemId).Sum(x => x.quantity);

                var availableAmount = i.OrderLimit - itemCount;
                var storeOutItem = _context.StoreOutItem.Where(x => x.Id == i.StoreOutItemId).FirstOrDefault();
                ExtraChitObject o = new ExtraChitObject();
                o.Id = i.StoreOutItemId.ToString();
                o.ItemName = storeOutItem.Name + "[" + availableAmount.ToString() + "pcs available]";
                exChtList.Add(o);
            }
            return Json(exChtList);

        }


        public string GetOnSpotItemList(string ParentId)
        {
            int day = (int)DateTime.Today.DayOfWeek + 1;
            List<ExtraChitObject> exChtList = new List<ExtraChitObject>();
            List<IList> full  = new List<IList>();


            //var OnSpotItemList = _context.MenuItem.Where(x => x.Day == day).ToList();
            var OnSpotItemList = _context.DailyOfferItem.Where(x => x.Date.Date == DateTime.Now.Date || x.IsActive == true).ToList();


            foreach (var i in OnSpotItemList)
            {
                //var ext = _context.ExtraItem.Where(x => x.Id == i.ExtraItemId).FirstOrDefault();
                var itemCount = _context.CustomerChoiceV2.Where(x => x.Date.Date == DateTime.Now.Date && x.OrderTypeId == 10002
                               && x.MealTypeId == 10005 && x.StoreOutItemId == i.StoreOutItemId).Sum(x => x.quantity);
                var availableAmount = i.OrderLimit - itemCount;

                var storeOutItem = _context.StoreOutItem.Where(x => x.Id == i.StoreOutItemId).FirstOrDefault();
                ExtraChitObject o = new ExtraChitObject();
                o.Id = i.StoreOutItemId.ToString();
                o.ItemName = storeOutItem.Name + "[" + availableAmount.ToString() + "pcs available]";

                exChtList.Add(o);
            }
            List<CustomerChoiceV2> ccList =  _context.CustomerChoiceV2.Where(x => x.OnSpotParentId == long.Parse(ParentId)).ToList();

            full.Add(exChtList);
            full.Add(ccList);
            string json = JsonConvert.SerializeObject(full, Formatting.Indented, new JsonSerializerSettings
            {
                ReferenceLoopHandling = ReferenceLoopHandling.Ignore
            });
            return  json;
        }

        public string GetExtraItemList(string ConsumerExtraChitParentId)
        {
            
            List<ConsumerMealWiseExtrachit> exChtList = new List<ConsumerMealWiseExtrachit>();
            List<IList> full = new List<IList>();


            var parentObj = _context.ConsumerMealWiseExtraChitParent.Where(x => x.Id == long.Parse(ConsumerExtraChitParentId)).ToList();

         
            exChtList = _context.ConsumerMealWiseExtrachit.Where(x => x.ConsumerMealWiseExtraChitParentId == long.Parse(ConsumerExtraChitParentId)).ToList();

       
            var storeOutItem = _context.StoreOutItem.Where(x => x.IsOpen == true).ToList();
            full.Add(storeOutItem);
            full.Add(exChtList);
            full.Add(parentObj);
            string json = JsonConvert.SerializeObject(full, Formatting.Indented, new JsonSerializerSettings
            {
                ReferenceLoopHandling = ReferenceLoopHandling.Ignore
            });
            return json;
        }

        public async Task<ActionResult> Index()
        {
            string usrName = SessionExtensions.GetString(HttpContext.Session, "user");
          
            if (usrName == null)
            {
                return LocalRedirect("~/AccessCheck/Index");

            }


            var user = await _userManager.FindByNameAsync(usrName);
            string UserId = user.Id;

            var fullName = user.BUPFullName;
            var email = user.BUPEmail;
            var userId = user.Id;

            ViewBag.FullName = fullName;
            ViewBag.Email = email;
            ViewBag.UserId = userId;
            ViewBag.ErrorMessage = null;
            ViewBag.MenuId = 1;


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

            if (usrName == null)
            {
                return LocalRedirect("~/AccessCheck/Index");

            }
            var user = await _userManager.FindByNameAsync(usrName);
            //var user = await _userManager.GetUserAsync(User);
            var fullName = user.BUPFullName;
            var email = user.BUPEmail;
            string UserId = user.Id;

            ViewBag.FullName = fullName;
            ViewBag.Email = email;
            ViewBag.UserId = UserId;
            ViewBag.ErrorMessage = null;



            return View();
        }
        public async Task<ActionResult> PaymentSlipUpload()
        {
            string usrName = SessionExtensions.GetString(HttpContext.Session, "user");

            if (usrName == null)
            {
                return LocalRedirect("~/AccessCheck/Index");

            }
            var user = await _userManager.FindByNameAsync(usrName);
            //var user = await _userManager.GetUserAsync(User);
            var fullName = user.BUPFullName;
            var email = user.BUPEmail;
            string UserId = user.Id;

            ViewBag.FullName = fullName;
            ViewBag.Email = email;
            ViewBag.UserId = UserId;
            ViewBag.ErrorMessage = null;
            ViewBag.MenuId = 3;




            return View();
        }


        [HttpPost]
        public async Task<JsonResult> UploadAttachment(string Method, string Mobile, string TransId,
                                                   string Bank, string Account, string Amount,
                                                   string File, string FileExtension, string LinkUrl, string PaymentDate,
                                                   string Remarks)
        {
            string usrName = SessionExtensions.GetString(HttpContext.Session, "user");

            if (usrName == null)
            {
                return Json(new { success = false, responseText = "Expire" });

            }
            try
            {
                int method = Int32.Parse(Method);

                var user = await _userManager.FindByNameAsync(usrName);
                string UserId = user.Id;
                DateTime pmntDate = DateTime.Now;


                if (method == 1 || method == 2)
                {
                    string[] DateFormatarray = PaymentDate.Split("-");

                    string datesv = DateFormatarray[1] + '/' + DateFormatarray[0] + '/' + DateFormatarray[2];
                    pmntDate = DateTime.ParseExact(datesv, "M/d/yyyy", CultureInfo.InvariantCulture);

                }
                var billParentObj = _context.ConsumerBillParent.Where(x => x.UserId == UserId).FirstOrDefault();
                long paymentInfoId = 0;
                string dateString = DateTime.Now.Month.ToString() + "_" + DateTime.Now.Year.ToString();
                string fileName = billParentObj.Id.ToString() + "_" + dateString + "." + FileExtension;

                UploadFile("~/Uploads/", fileName, File);

                if (method == 1)
                {
                    var paymentObj = _context.ConsumerPaymentInfo.
                        Where(x => x.MobileNumber == Mobile && x.TransactionID == TransId &&
                        x.ConsumerBillParentId == billParentObj.Id).FirstOrDefault();
                    if (paymentObj == null)
                    {
                        ConsumerPaymentInfo cpi = new ConsumerPaymentInfo();
                        cpi.CreatedDate = DateTime.Now;
                        cpi.LastModifiedDate = DateTime.Now;
                        cpi.CreatedBy = UserId;
                        cpi.LastModifiedBy = UserId;
                        cpi.PaymentMethodId = method;
                        cpi.MobileNumber = Mobile;
                        cpi.TransactionID = TransId;
                        cpi.ConsumerBillParentId = billParentObj.Id;
                        cpi.IsActive = true;

                        _context.ConsumerPaymentInfo.Add(cpi);
                        _context.SaveChanges();
                        paymentInfoId = cpi.Id;
                    }
                    else
                    {
                        paymentInfoId = paymentObj.Id;
                    }
                }
                else if (method == 2)
                {
                    var paymentObj = _context.ConsumerPaymentInfo.
                       Where(x => x.BankName == Bank && x.AccountNumber == Account &&
                       x.ConsumerBillParentId == billParentObj.Id).FirstOrDefault();
                    if (paymentObj == null)
                    {
                        ConsumerPaymentInfo cpi = new ConsumerPaymentInfo();
                        cpi.CreatedDate = DateTime.Now;
                        cpi.LastModifiedDate = DateTime.Now;
                        cpi.CreatedBy = UserId;
                        cpi.LastModifiedBy = UserId;
                        cpi.PaymentMethodId = method;
                        cpi.BankName = Bank;
                        cpi.AccountNumber = Account;
                        cpi.ConsumerBillParentId = billParentObj.Id;
                        cpi.IsActive = true;
                        _context.ConsumerPaymentInfo.Add(cpi);
                        _context.SaveChanges();
                        paymentInfoId = cpi.Id;
                    }
                    else
                    {
                        paymentInfoId = paymentObj.Id;

                    }
                }
                else
                {
                   
                }

                //var billHistoryObj = _context.ConsumerBillHistory.Where(x => x.ConsumerBillParentId == billParentObj.Id).LastOrDefault();
                //if(billHistoryObj == null)
                //{
                ConsumerPaymentAttachment cbh = new ConsumerPaymentAttachment();
                cbh.ConsumerBillParentId = billParentObj.Id;
                cbh.ConsumerPaymentInfoId = paymentInfoId;
                cbh.UploadDate = pmntDate;
                cbh.Attachment = fileName;
                cbh.Remarks = Remarks;
                cbh.Amount = double.Parse(Amount);
                cbh.EntryDone = false;

                _context.ConsumerPaymentAttachment.Add(cbh);
                _context.SaveChanges();
                //}

            }
            catch (Exception ex)
            {
                return Json(new { success = false, responseText = ex.Message });

            }
            return Json(new { success = true, responseText = "Payment is successful" });

        }


        public void UploadFile(string fullDirectoryPath, string fullname, string base64BinaryString)
        {

            var webRoot = _env.WebRootPath;
            webRoot += "/Upload/";
            var file = System.IO.Path.Combine(webRoot, fullname);
            // System.IO.File.WriteAllText(file, "Hello World!");
            System.IO.File.WriteAllBytes(file, Convert.FromBase64String(base64BinaryString));
            //string uniqueFileNameString = Guid.NewGuid().ToString();
            //string fileNameToSave = fullname;
            //string fullPhotoPath = Path.Combine(fullDirectoryPath, fileNameToSave);
            //try
            //{

            //    if (!Directory.Exists(Server.MapPath(fullDirectoryPath)))
            //    {
            //        Directory.CreateDirectory(HttpContext.Current.Server.MapPath(fullDirectoryPath));
            //    }
            //    if (System.IO.File.Exists(System.Web.HttpContext.Current.Server.MapPath(fullPhotoPath)))
            //    {
            //        System.IO.File.Delete(System.Web.HttpContext.Current.Server.MapPath(fullPhotoPath));
            //    }
            //    System.IO.File.WriteAllBytes(System.Web.HttpContext.Current.Server.MapPath(fullPhotoPath), Convert.FromBase64String(base64BinaryString));

            //}
            //catch (Exception ex) { return ""; }
            //finally { }

            //return fullPhotoPath;
        }

        public JsonResult GetAllItems()
        {
            var itemList = _context.StoreOutItem.Where(x => x.Id > 0).ToList();

            return Json(itemList);
        }
        public async Task<ActionResult> OnSpotOrder()
        {
            string usrName = SessionExtensions.GetString(HttpContext.Session, "user");

            if (usrName == null)
            {
                return LocalRedirect("~/AccessCheck/Index");

            }
            var user = await _userManager.FindByNameAsync(usrName);
            //var user = await _userManager.GetUserAsync(User);
            var fullName = user.BUPFullName;
            var email = user.BUPEmail;
            string UserId = user.Id;

            ViewBag.FullName = fullName;

            ViewBag.FullUser = "[" + user.OfficeName + "]-" + user.BUPNumber + "-" + user.BUPFullName;

            ViewBag.Email = email;
            ViewBag.UserId = UserId;
            ViewBag.ErrorMessage = null;
            ViewBag.MenuId = 2;




            return View();
        }

        [HttpPost]
        [AutoValidateAntiforgeryToken]
        public async Task<IActionResult> FetchMenu()
        {
            string usrName = SessionExtensions.GetString(HttpContext.Session, "user");
            if (usrName == null)
            {
                return LocalRedirect("~/AccessCheck/Index");

            }
            var user = await _userManager.FindByNameAsync(usrName);
            string UserId = user.Id;
            string d = Request.Form["menudate"];

            try
            {
                string[] DateFormatarray = d.Split("-");
                string datesv = DateFormatarray[1] + '/' + DateFormatarray[0] + '/' + DateFormatarray[2];
                DateTime date = DateTime.ParseExact(datesv, "M/d/yyyy", CultureInfo.InvariantCulture);
                if(date.Date <= DateTime.Now.Date)
                {
                    ViewBag.MenuDate = DateTime.Now.AddDays(1);
                    //ViewBag.MenuDate = DateTime.Parse(d);
                    ViewBag.UserId = UserId;
                    ViewBag.ErrorMessage = "Today's date or previous date is not allowed.";

                    return View("Index");
                }
                ViewBag.MenuDate = date;
                //ViewBag.MenuDate = DateTime.Parse(d);
                ViewBag.UserId = UserId;

                return View("Index");
            }
            catch
            {
                ViewBag.UserId = UserId;
                ViewBag.ErrorMessage = "Invalid date";

                return View("Index");
            }

        }



        [HttpPost]
        [AutoValidateAntiforgeryToken]
        public async Task<IActionResult> EditDefaultChoice(int MealId, string dateString)
        {
            string usrName = SessionExtensions.GetString(HttpContext.Session, "user");
            if(usrName == null)
            {
                return LocalRedirect("~/AccessCheck/Index");

            }

            var lastTime = _context.Constants.Where(x => x.Name == "Last Time").FirstOrDefault();
            DateTime dateTime = DateTime.ParseExact(lastTime.Value, "HH:mm",
                                        CultureInfo.InvariantCulture);
            string currTime = DateTime.Now.ToShortTimeString();
            var user = await _userManager.FindByNameAsync(usrName);
            string UserId = user.Id;
            if (DateTime.Now.TimeOfDay >= dateTime.TimeOfDay)
            {
                ViewBag.UserId = UserId;
                ViewBag.ErrorMessage = "Time up for placing order";

                return View("Index");
            }
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
            ViewBag.ErrorMessage = null;

            return View("Index");
        }
        [HttpPost]
        [AutoValidateAntiforgeryToken]
        public async Task<IActionResult> EditExtraSMChoice(int MealId, string dateString)
        {
            string usrName = SessionExtensions.GetString(HttpContext.Session, "user");
            if (usrName == null)
            {
                return LocalRedirect("~/AccessCheck/Index");

            }

            var lastTime = _context.Constants.Where(x => x.Name == "Last Time").FirstOrDefault();
            DateTime dateTime = DateTime.ParseExact(lastTime.Value, "HH:mm",
                                        CultureInfo.InvariantCulture);
            string currTime = DateTime.Now.ToShortTimeString();
         

            var user = await _userManager.FindByNameAsync(usrName);
            string UserId = user.Id;
            if (DateTime.Now.TimeOfDay >= dateTime.TimeOfDay)
            {
                ViewBag.UserId = UserId;
                ViewBag.ErrorMessage = "Time up for placing order";

                return View("Index");
            }
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
            ViewBag.ErrorMessage = null;


            return View("Index");


        }
        

        [HttpPost]
        public async Task<JsonResult> AddConsumerOrderDate(string DateList, string MealId, string Check)
        {
            string usrName = SessionExtensions.GetString(HttpContext.Session, "user");

            if (usrName == null)
            {
                return Json(new { success = false, responseText = "Expire" });

            }

            List<DateObject> tempDateList = (List<DateObject>)JsonConvert.DeserializeObject(DateList, typeof(List<DateObject>));
            try
            {
                var user = await _userManager.FindByNameAsync(usrName);
                string UserId = user.Id;
                long masterId = 0;
                var userObj = _context.UserDateChoiceMaster.Where(x => x.UserId == UserId && x.MealTypeId == Int32.Parse(MealId)).FirstOrDefault();
                if (userObj == null)
                {
                    UserDateChoiceMaster udcm = new UserDateChoiceMaster();
                    udcm.CreatedBy = UserId;
                    udcm.CreatedDate = DateTime.Now;
                    udcm.UserId = UserId;
                    udcm.MealTypeId = Int32.Parse(MealId);
                    _context.UserDateChoiceMaster.Add(udcm);
     
                     _context.SaveChanges();
                    masterId = udcm.Id;
                }
                else
                {
                    masterId = userObj.Id;
                }
                foreach (var i in tempDateList)
                {
                    string[] DateFormatarray = i.Date.Split("-");
                    string datesv = DateFormatarray[1] + '/' + DateFormatarray[0] + '/' + DateFormatarray[2];
                    DateTime choiceDate = DateTime.ParseExact(datesv, "M/d/yyyy", CultureInfo.InvariantCulture);
                    var mealDateFound = _context.UserDateChoiceDetail.Where(x => x.UserDateChoiceMasterId == masterId && x.Date.Date == choiceDate.Date).FirstOrDefault();
                    if (mealDateFound == null)
                    {
                        UserDateChoiceDetail udcd = new UserDateChoiceDetail();
                        udcd.UserDateChoiceMasterId = masterId;
                        udcd.Date = choiceDate;
                        udcd.IsOrderSet = Check == "false" ? false : true;
                        _context.UserDateChoiceDetail.Add(udcd);
                        _context.SaveChanges();
                    }
                }
            }
            catch(Exception ex)
            {
                return Json(new { success = false, responseText = ex.Message });

            }

            return Json(new { success = true, responseText = "Dates have been added successfully" });

        }

        [HttpPost]
        public async Task<JsonResult> UpdateExtraItemList(string ExtraItemList, string MealId, string Date)
        {
            string usrName = SessionExtensions.GetString(HttpContext.Session, "user");

            if (usrName == null)
            {
                return Json(new { success = false, responseText = "Expire" });

            }
            var lastTime = _context.Constants.Where(x => x.Name == "Last Time").FirstOrDefault();
            DateTime dateTime = DateTime.ParseExact(lastTime.Value, "HH:mm",
                                        CultureInfo.InvariantCulture);
            string currTime = DateTime.Now.ToShortTimeString();
            if(DateTime.Now.TimeOfDay >= dateTime.TimeOfDay)
            {
                return Json(new { success = false, responseText = "Time up for placing order" });


            }
            long mealid = long.Parse(MealId);
            string[] DateFormatarray = Date.Split("-");
            string datesv = DateFormatarray[1] + '/' + DateFormatarray[0] + '/' + DateFormatarray[2];
            DateTime choiceDate = DateTime.ParseExact(datesv, "M/d/yyyy", CultureInfo.InvariantCulture);

            List<ItemIds> tempSetMenuList = (List<ItemIds>)JsonConvert.DeserializeObject(ExtraItemList, typeof(List<ItemIds>));
            var prevCCRecord = _context.CustomerChoiceV2.Where(x => x.MealTypeId == mealid && x.Date.ToShortDateString() == choiceDate.ToShortDateString() && x.OrderTypeId == 4).ToList();
            
            
            //if (prevCCRecord.Count > 0)
            //{
            //    foreach (var item in prevCCRecord)
            //    {
            //        _context.CustomerChoiceV2.Remove(item);
            //        _context.SaveChanges();

            //    }
            //}

            var user = await _userManager.FindByNameAsync(usrName);
            string UserId = user.Id;
            var orderItemList = _context.CustomerChoiceV2.Where(x => x.MealTypeId == mealid &&
                                x.Date.ToShortDateString() == choiceDate.ToShortDateString() &&
                                x.OrderTypeId == 4 && x.UserId == UserId).ToList();
            try
            {
                foreach (var item in tempSetMenuList)
                {

                    var mnItm = _context.MenuItem.Where(x => x.Id == Int32.Parse(item.ItemId)).FirstOrDefault();

                    var itemObj = _context.CustomerChoiceV2.Where(x => x.MealTypeId == mealid && 
                    x.Date.ToShortDateString() == choiceDate.ToShortDateString() && 
                    x.OrderTypeId == 4 && x.ExtraItemId == mnItm.ExtraItemId && x.UserId == UserId).FirstOrDefault();

                    if (itemObj != null)
                    {
                        itemObj.quantity = double.Parse(item.Quantity);
                        _context.CustomerChoiceV2.Update(itemObj);
                        _context.SaveChanges();
                    }
                    else
                    {
                        CustomerChoiceV2 cc = new CustomerChoiceV2();

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

                bool delete = true;
                foreach(var ob in orderItemList)
                {
                    foreach(var i in tempSetMenuList)
                    {
                        var mnItm = _context.MenuItem.Where(x => x.Id == Int32.Parse(i.ItemId)).FirstOrDefault();
                        if(ob.ExtraItemId == mnItm.ExtraItemId)
                        {
                            delete = false;
                            break;
                        }

                    }
                    if(delete == true)
                    {
                        _context.CustomerChoiceV2.Remove(ob);
                        _context.SaveChanges();
                    }
                    delete = true;
                }

            }
            catch (Exception ex)
            {
                return Json(ex);
            }

            return Json(new { success = true, responseText = "Order has been updated successfully." });

        }


        [HttpPost]
        public async Task<JsonResult> AddOnSpotItem(string ItemList, string IsOffice, string Person, string Office, string Bearer)
        {
            string usrName = SessionExtensions.GetString(HttpContext.Session, "user");

            if (usrName == null)
            {
                return Json(new { success = false, responseText = "Expire" });

            }

            try
            {
                bool isOffice = IsOffice == "true" ? true : false;
                OnSpotParent onSpotObj = new OnSpotParent();
                var consumerId = "";
                long officeId = 0;
                var bearerId = "";
                var bearer = new ApplicationUser();
                long onSpotParentId = 0;
                var user = await _userManager.FindByNameAsync(usrName);
                string UserId = user.Id;
                if(Bearer == "No")
                {
                    bearer = null;
                }
                else
                {
                    var personArray = Bearer.Split("-");
                    var prsndtl2 = _userManager.Users.Where(x => x.BUPNumber == personArray[1]).FirstOrDefault();
                     bearer =  prsndtl2;
                }
             
                if (isOffice == false)
                {
                    if(Person == "No")
                    {
                        consumerId = UserId;

                    }
                    else
                    {
                        var personArray2 = Person.Split("-");
                        var prsndtl = _userManager.Users.Where(x => x.BUPNumber == personArray2[1]).FirstOrDefault();
                        consumerId =  prsndtl.Id;
                    }
                 
                    //officeId = null;

                    if (bearer == null)
                    {
                        bearerId = null;
                        //onSpotObj = _context.OnSpotParent.Where(x => x.UserId == consumerId && x.Date.Date == DateTime.Now.Date && x.IsOfficeOrder == isOffice && x.BearerId == null).FirstOrDefault();

                    }
                    else
                    {
                        bearerId = bearer.Id;
                        //onSpotObj = _context.OnSpotParent.Where(x => x.UserId == consumerId && x.Date.Date == DateTime.Now.Date && x.BearerId == bearer.Id && x.IsOfficeOrder == isOffice).FirstOrDefault();

                    }

                }
                else
                {
                    var officeDtl = _context.Office.Where(x => x.Name == Office).FirstOrDefault();
                    officeId = officeDtl.Id;
                    consumerId = UserId;

                    if (bearer == null)
                    {
                        bearerId = null;
                        //onSpotObj = _context.OnSpotParent.Where(x => x.OfficeId == officeDtl.Id && x.UserId == consumerId && x.Date.Date == DateTime.Now.Date && x.IsOfficeOrder == isOffice && x.BearerId == null).FirstOrDefault();

                    }
                    else
                    {
                        bearerId = bearer.Id;
                        //onSpotObj = _context.OnSpotParent.Where(x => x.OfficeId == officeDtl.Id && x.UserId == consumerId && x.Date.Date == DateTime.Now.Date && x.BearerId == bearer.Id && x.IsOfficeOrder == isOffice).FirstOrDefault();

                    }
                }
                //if (onSpotObj == null)
                //{
                    if (officeId > 0)
                    {
                        OnSpotParent osp = new OnSpotParent();
                        osp.OfficeId = officeId;
                        osp.UserId = consumerId;
                        osp.BearerId = bearerId;
                        osp.IsOfficeOrder = isOffice;
                        osp.Date = DateTime.Now;
                        osp.CreatedDate = DateTime.Now;
                        osp.CreatedBy = UserId;
                        _context.OnSpotParent.Add(osp);
                        _context.SaveChanges();
                        onSpotParentId = osp.Id;
                    }
                    else
                    {
                        OnSpotParent osp = new OnSpotParent();
                        osp.UserId = consumerId;
                        osp.BearerId = bearerId;
                        osp.IsOfficeOrder = isOffice;
                        osp.Date = DateTime.Now;
                        osp.CreatedDate = DateTime.Now;
                        osp.CreatedBy = UserId;
                        _context.OnSpotParent.Add(osp);
                        _context.SaveChanges();
                        onSpotParentId = osp.Id;
                    }

                //}
                //else
                //{
                //    onSpotParentId = onSpotObj.Id;
                //}
                List<OnSpotItemObject> tempItemList = (List<OnSpotItemObject>)JsonConvert.DeserializeObject(ItemList, typeof(List<OnSpotItemObject>));
                //var user = await _userManager.FindByNameAsync(usrName);
                //string UserId = user.Id;
                foreach (var it in tempItemList)
                {
                    var prevObj = _context.CustomerChoiceV2.Where(x => x.UserId == UserId && x.Date.Date == DateTime.Now.Date &&
                    x.StoreOutItemId == long.Parse(it.Id) && x.OrderTypeId == 10002 && x.MealTypeId == 10005 && x.OnSpotParentId == onSpotParentId).FirstOrDefault();

                    var itemCount = _context.CustomerChoiceV2.Where(x => x.Date.Date == DateTime.Now.Date && x.OrderTypeId == 10002
                               && x.MealTypeId == 10005 && x.StoreOutItemId == long.Parse(it.Id)).Sum(x => x.quantity);
                    var dailyObj = _context.DailyOfferItem.Where(x => x.StoreOutItemId == long.Parse(it.Id) && (x.Date.Date == DateTime.Now.Date || x.IsActive == true)).FirstOrDefault();
                    var availableAmount = dailyObj.OrderLimit - (itemCount + double.Parse(it.Quantity));
                    var storeOutObj = _context.StoreOutItem.Where(x => x.Id == long.Parse(it.Id)).FirstOrDefault();
                    if (availableAmount < 0)
                    {
                        return Json(new { success = false, responseText = "Not sufficient amount for " + storeOutObj.Name });

                    }
                    if (prevObj == null)
                    {
                        CustomerChoiceV2 cc = new CustomerChoiceV2();
                        cc.UserId = consumerId;
                        cc.StoreOutItemId = long.Parse(it.Id);
                        cc.quantity = double.Parse(it.Quantity);
                        cc.OrderTypeId = 10002;
                        cc.MealTypeId = 10005;
                        cc.Date = DateTime.Now;
                        cc.OnSpotParentId = onSpotParentId;
                        cc.ExtraChitParentId = 1;
                        cc.CreatedBy = UserId;
                        cc.CreatedDate = DateTime.Now;
                        cc.LastModifiedBy = UserId;

                        cc.LastModifiedDate = DateTime.Now;
                        _context.CustomerChoiceV2.Add(cc);
                        _context.SaveChanges();
                    }
                    else
                    {
                        prevObj.quantity = double.Parse(it.Quantity);
                        _context.CustomerChoiceV2.Update(prevObj);
                        _context.SaveChanges();
                    }
                }
            }
            catch(Exception ex)
            {
                return Json(new { success = false, responseText = ex.Message });

            }
            return Json(new { success = true, responseText = "Items have been added successfully.." });

        }


        public async Task<string> GetOrderDetails(string Day,string Month, string Year,string Type)
        {
            List<OrderHistoryVr2> ordList = new List<OrderHistoryVr2>();
            string usrName = SessionExtensions.GetString(HttpContext.Session, "user");
            List<IList> full = new List<IList>();

            if (usrName == null)
            {
                //return Json(ordList);
            }
            else
            {
                string datesv = Month + '/' + Day + '/' + Year;
                DateTime ordDate = DateTime.ParseExact(datesv, "M/d/yyyy", CultureInfo.InvariantCulture);
                var user = await _userManager.FindByNameAsync(usrName);
                string UserId = user.Id;
                ordList = _context.OrderHistoryVr2.Where(x => x.UserId == UserId && x.OrderDate.Date == ordDate.Date && x.MealTypeId == Int32.Parse(Type)).ToList();
                var storeOutItemList = _context.StoreOutItem.Where(x => x.IsOpen == true).ToList();
                full.Add(ordList);
                full.Add(storeOutItemList);
            }
            string json = JsonConvert.SerializeObject(full, Formatting.Indented, new JsonSerializerSettings
            {
                ReferenceLoopHandling = ReferenceLoopHandling.Ignore
            });
            return json;
            //return Json(ordList);
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
                var onSpotParentId = _context.CustomerChoiceV2.Where(x => x.Id == long.Parse(tempItemList.ElementAt(0).OrderId)).FirstOrDefault().OnSpotParentId;
                var consumerId = _context.OnSpotParent.Where(x => x.Id == onSpotParentId).FirstOrDefault().UserId;
                foreach (var item in tempItemList)
                {
                    if(item.IsDelete == "1")
                    {
                        var ordObj = _context.CustomerChoiceV2.Where(x => x.Id == long.Parse(item.OrderId)).FirstOrDefault();

                        _context.CustomerChoiceV2.Remove(ordObj);
                        _context.SaveChanges();
                    }
                    else if(item.IsNew == "1")
                    {
                        var itemCount = _context.CustomerChoiceV2.Where(x => x.Date.Date == DateTime.Now.Date && x.OrderTypeId == 10002
                              && x.MealTypeId == 10005 && x.StoreOutItemId == long.Parse(item.ItemId)).Sum(x => x.quantity);
                        var dailyObj = _context.DailyOfferItem.Where(x => x.StoreOutItemId == long.Parse(item.ItemId) && (x.Date.Date == DateTime.Now.Date || x.IsActive == true)).FirstOrDefault();
                        var availableAmount = dailyObj.OrderLimit - (itemCount + double.Parse(item.Quantity));
                        var storeOutObj = _context.StoreOutItem.Where(x => x.Id == long.Parse(item.ItemId)).FirstOrDefault();
                        if (availableAmount < 0)
                        {
                            return Json(new { success = false, responseText = "Not sufficient amount for " + storeOutObj.Name });

                        }
                        CustomerChoiceV2 cc = new CustomerChoiceV2();
                        cc.UserId = consumerId;
                        cc.StoreOutItemId = long.Parse(item.ItemId);
                        cc.quantity = double.Parse(item.Quantity);
                        cc.OrderTypeId = 10002;
                        cc.MealTypeId = 10005;
                        cc.Date = DateTime.Now;
                        cc.OnSpotParentId = onSpotParentId;
                        cc.ExtraChitParentId = 1;
                        cc.CreatedBy = UserId;
                        cc.CreatedDate = DateTime.Now;
                        cc.LastModifiedBy = UserId;

                        cc.LastModifiedDate = DateTime.Now;
                        _context.CustomerChoiceV2.Add(cc);
                        _context.SaveChanges();
                    }
                    else
                    {
                        var ordObj = _context.CustomerChoiceV2.Where(x => x.Id == long.Parse(item.OrderId)).FirstOrDefault();
                        var qnt = ordObj.quantity - double.Parse(item.Quantity);
                        var itemCount = _context.CustomerChoiceV2.Where(x => x.Date.Date == DateTime.Now.Date && x.OrderTypeId == 10002
                            && x.MealTypeId == 10005 && x.StoreOutItemId == long.Parse(item.ItemId)).Sum(x => x.quantity);
                        var dailyObj = _context.DailyOfferItem.Where(x => x.StoreOutItemId == long.Parse(item.ItemId) && (x.Date.Date == DateTime.Now.Date || x.IsActive == true)).FirstOrDefault();
                        var availableAmount = dailyObj.OrderLimit - (itemCount - qnt);
                        var storeOutObj = _context.StoreOutItem.Where(x => x.Id == long.Parse(item.ItemId)).FirstOrDefault();
                        if (availableAmount < 0)
                        {
                            return Json(new { success = false, responseText = "Not sufficient amount for " + storeOutObj.Name });

                        }
                        ordObj.StoreOutItemId = long.Parse(item.ItemId);
                        ordObj.quantity = double.Parse(item.Quantity);
                        _context.CustomerChoiceV2.Update(ordObj);
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


        [HttpPost]
        public async Task<JsonResult> EditExtraItem(string ItemList, string Remarks)
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
                var exChitObjId = long.Parse(tempItemList.ElementAt(0).OrderId);
                var parentObjId = _context.ConsumerMealWiseExtrachit.Where(x => x.Id == exChitObjId).FirstOrDefault().ConsumerMealWiseExtraChitParentId;
                var parentObj = _context.ConsumerMealWiseExtraChitParent.Where(x => x.Id == parentObjId).FirstOrDefault();
                parentObj.Remarks = Remarks;
                _context.ConsumerMealWiseExtraChitParent.Update(parentObj);
                _context.SaveChanges();
                foreach (var item in tempItemList)
                {
                    if (item.IsDelete == "1")
                    {
                        var ordObj = _context.ConsumerMealWiseExtrachit
                            .Where(x => x.Id == long.Parse(item.OrderId)).FirstOrDefault();
                        _context.ConsumerMealWiseExtrachit.Remove(ordObj);
                        _context.SaveChanges();
                    }
                    else
                    {
                        var ordObj = _context.ConsumerMealWiseExtrachit.Where(x => x.Id == long.Parse(item.OrderId)).FirstOrDefault();
                        ordObj.StoreOutItemId = long.Parse(item.ItemId);
                        ordObj.quantity = double.Parse(item.Quantity);
                        _context.ConsumerMealWiseExtrachit.Update(ordObj);
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


        [HttpPost]
        public async Task<JsonResult> CreateDailyMenu(string ItemList)
        {
            string usrName = SessionExtensions.GetString(HttpContext.Session, "user");

            if (usrName == null)
            {
                return Json(new { success = false, responseText = "Expire" });

            }
            var user = await _userManager.FindByNameAsync(usrName);
            string UserId = user.Id;
            List<ItemObject> tempItemList = (List<ItemObject>)JsonConvert.DeserializeObject(ItemList, typeof(List<ItemObject>));
            var orderItemList = _context.CustomerDailyMenuChoice.Where(x => x.UserId == UserId).ToList();

            try
            {
                foreach (var o in tempItemList)
                {
                    var menuObj = _context.CustomerDailyMenuChoice.Where(x => x.UserId == UserId &&
                                                                x.MealTypeId == long.Parse(o.Meal) &&
                                                                x.ExtraItemId == long.Parse(o.ItemId) &&
                                                                x.Day == Int32.Parse(o.Day)).FirstOrDefault();
                    if (menuObj == null)
                    {
                        CustomerDailyMenuChoice cdmc = new CustomerDailyMenuChoice();
                        cdmc.UserId = UserId;
                        cdmc.ExtraItemId = long.Parse(o.ItemId);
                        cdmc.Day = Int32.Parse(o.Day);
                        cdmc.MealTypeId = long.Parse(o.Meal);
                        cdmc.CreatedBy = UserId;
                        cdmc.CreatedDate = DateTime.Now;
                        cdmc.LastModifiedBy = UserId;
                        cdmc.OrderTypeId = 10003;
                        cdmc.quantity = double.Parse(o.Quantity);
                        cdmc.LastModifiedDate = DateTime.Now;
                        _context.CustomerDailyMenuChoice.Add(cdmc);
                        _context.SaveChanges();
                    }
                    else
                    {
                        menuObj.quantity = double.Parse(o.Quantity);
                        _context.CustomerDailyMenuChoice.Update(menuObj);
                        _context.SaveChanges();
                    }
                }
                bool delete = true;
                foreach (var ob in orderItemList)
                {
                    foreach (var i in tempItemList)
                    {
                        if (ob.ExtraItemId == long.Parse(i.ItemId))
                        {
                            delete = false;
                            break;
                        }

                    }
                    if (delete == true)
                    {
                        _context.CustomerDailyMenuChoice.Remove(ob);
                        _context.SaveChanges();
                    }
                    delete = true;
                }
            }
            catch (Exception ex)
            {
                return Json(new { success = false, responseText = ex.Message });

            }
            return Json(new { success = true, responseText = "Daily menu has been modified successfully" });

        }

        [HttpPost]
        public async Task<JsonResult> UserPeriodModify(string From, string To, string Checked, string MealId)
        {
            Period p = new Period();
            string usrName = SessionExtensions.GetString(HttpContext.Session, "user");

            if (usrName == null)
            {
                return Json(new { success = false, responseText = "Expire" });

            }
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

            if (usrName == null)
            {
                return Json(new { success = false, responseText = "Expire" });

            }
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
            var pos = _context.PreOrderSchedule.Where(x => x.UserId == UserId && x.MealTypeId == mealId).FirstOrDefault();
            if (pos != null)
            {
                pos.IsPreOrderSet = preorderset;
                _context.PreOrderSchedule.Update(pos);
                _context.SaveChanges();
            }
            else
            {
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
            }

            //PreOrderSchedule po = new PreOrderSchedule();
            //po.LastModifiedDate = DateTime.Now;
            //po.CreatedDate = DateTime.Now;
            //po.UserId = UserId;
            //po.IsPreOrderSet = preorderset;
            //po.LastConfigurationUpdateDate = DateTime.Now;
            //po.MealTypeId = mealId;
            //_context.PreOrderSchedule.Add(po);
            //_context.SaveChanges();

            return Json(new { success = true, responseText = "On/Off has been modified successfully.It will work from tomorrow. It will remain unchanged until any further changes." });

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
        public class DateObject
        {
            public string Date { set; get; }
        }
        public class ItemObject
        {
            public string ItemId { set; get; }
            public string Quantity { set; get; }
            public string Day { set; get; }
            public string Meal { set; get; }


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
        }

        public class EditableOnSpotItemObject
        {
            public string IsDelete;
            public string OrderId;
            public string ItemId;
            public string Quantity;
            public string IsNew;
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