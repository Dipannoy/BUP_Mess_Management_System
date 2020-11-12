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
using Mess_Management_System_Alpha_V2.Controllers.SessionChecker;

//using System.Web.Script.Serialization;


using System.Globalization;
using Microsoft.AspNetCore.Authorization;
using System.Collections;
using System.Text;
using System.Net;
using System.Collections.Specialized;


// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Mess_Management_System_Alpha_V2.Controllers.Admin
{

    //[Authorize(Roles = "Admin,MessAdmin")]
    public class AdminShowOrderController : Controller
    {
        // GET: /<controller>/

        private readonly ApplicationDbContext _context;
        private UserManager<ApplicationUser> _userManager;
        private RoleManager<IdentityRole> _roleManager;

        private SignInManager<ApplicationUser> _signInManager;
        public AdminShowOrderController(ApplicationDbContext context, UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager, SignInManager<ApplicationUser> signInManager)
        {
            _context = context;
            _userManager = userManager;
            _roleManager = roleManager;
            _signInManager = signInManager;
        }
        //public AdminShowOrderController(ApplicationDbContext context)
        //{
        //    _context = context;
    
        //}


        public IActionResult AdminOrderShow()
        {
            return View();
        }
        public IActionResult home()
        {
            return View();
        }

        public async Task<IActionResult> GoUCAM()
        {
            HttpContext.Session.Remove("user");
          return Redirect("https://ucam.bup.edu.bd/");
            //return Redirect("http://localhost:44381/Security/Home.aspx?mmi=41485d2c6c554d494e63");


        }
        public async Task<IActionResult> Dashboard()
        {

            if (!SessionExist())
            {
                string tokenType = HttpContext.Request.Query["TokenType"].ToString();
                string accessToken = HttpContext.Request.Query["AccessToken"].ToString();

                if (GetUserInfoFromSSO(tokenType, accessToken) == null)
                {

                    return LocalRedirect("~/AccessCheck/Index");

                }
                else
                {
                    User usr = GetUserInfoFromSSO(tokenType, accessToken);
                    string unEncode = HttpContext.Request.Headers["UserName"].ToString();
                    string un = DecodeServerName(unEncode);

                    SessionExtensions.SetString(HttpContext.Session, "user", usr.name);
                    string usrName = SessionExtensions.GetString(HttpContext.Session, "user");

                    if (usrName == null)
                    {
                        HttpContext.Session.Remove("user");

                        return LocalRedirect("~/AccessCheck/Index");

                    }
                    else
                    {
                        if (await UserExistMess())
                        {
                            if (await GetLogInUserRoleAsync() == "Admin"
                                || await GetLogInUserRoleAsync() == "MessAdmin")
                            {
                                ViewBag.FullName = await GetUserName();
                                return View();
                            }
                            else
                            {
                                return LocalRedirect("~/Consumer/Index");

                            }
                        }
                        else
                        {
                            HttpContext.Session.Remove("user");

                            return LocalRedirect("~/AccessCheck/Index");

                        }
                    }
                }



            }
            else
            {
                if (await UserExistMess())
                {
                    if (await GetLogInUserRoleAsync() == "Admin"
                        || await GetLogInUserRoleAsync() == "MessAdmin")
                    {
                        ViewBag.FullName = await GetUserName();

                        return View();
                    }
                    else
                    {
                        return LocalRedirect("~/Consumer/Index");

                    }
                }
                else
                {
                    HttpContext.Session.Remove("user");

                    return LocalRedirect("~/AccessCheck/Index");

                }
            }







            //    string usrName = SessionExtensions.GetString(HttpContext.Session, "user");

            //    string pass = HttpContext.Request.Query["Password"].ToString();
            //    var result = await _signInManager.PasswordSignInAsync(un, pass, true, true);

            //    var nm = Request.QueryString["UserName"];
            //    return LocalRedirect("~/Consumer/Index");

            //return View();
        }

        //public async Task<IActionResult> Dashboard()
        //{

        //    if (!SessionExist())
        //    {


        //        string unEncode = HttpContext.Request.Query["UserName"].ToString();
        //        string un = DecodeServerName(unEncode);

        //        SessionExtensions.SetString(HttpContext.Session, "user", un);
        //        string usrName = SessionExtensions.GetString(HttpContext.Session, "user");

        //        if (usrName == null)
        //        {
        //            HttpContext.Session.Remove("user");

        //            return LocalRedirect("~/AccessCheck/Index");

        //        }
        //        else
        //        {
        //            if (await UserExistMess())
        //            {
        //                if (await GetLogInUserRoleAsync() == "Admin"
        //                    || await GetLogInUserRoleAsync() == "MessAdmin")
        //                {
        //                    ViewBag.FullName = await GetUserName();
        //                    return View();
        //                }
        //                else
        //                {
        //                    return LocalRedirect("~/Consumer/Index");

        //                }
        //            }
        //            else
        //            {
        //                HttpContext.Session.Remove("user");

        //                return LocalRedirect("~/AccessCheck/Index");

        //            }
        //        }




        //    }
        //    else
        //    {
        //        if (await UserExistMess())
        //        {
        //            if (await GetLogInUserRoleAsync() == "Admin"
        //                || await GetLogInUserRoleAsync() == "MessAdmin")
        //            {
        //                ViewBag.FullName = await GetUserName();

        //                return View();
        //            }
        //            else
        //            {
        //                return LocalRedirect("~/Consumer/Index");

        //            }
        //        }
        //        else
        //        {
        //            HttpContext.Session.Remove("user");

        //            return LocalRedirect("~/AccessCheck/Index");

        //        }
        //    }







        //    // string usrName = SessionExtensions.GetString(HttpContext.Session, "user");

        //    // string pass = HttpContext.Request.Query["Password"].ToString();
        //    //var result = await _signInManager.PasswordSignInAsync(un,pass,true,true);

        //    //var nm = Request.QueryString["UserName"];
        //    //return LocalRedirect("~/Consumer/Index");

        //    // return View();
        //}




        public IActionResult Schedule()
        {
            return View();
        }
        public IActionResult ItemBase()
        {
            return View();
        }
        public ActionResult BillProcess()
        {
            //var result = _context.WarehouseStorage.Include(x => x.StoreInItem).ToList();
            return View();
        }

        public async Task<IActionResult> Signout()
        {
            // await _signInManager.SignOutAsync();
            HttpContext.Session.Remove("user");
            return Redirect("https://ucam.bup.edu.bd/Security/LogOut.aspx");

            //return RedirectToAction("Index", "Home");
        }


        //[Authorize]
        //[HttpGet]
        //public async Task<IActionResult> Logout()
        //{
        //   return await HttpContext.Authentication.SignOutAsync();

        //   // return RedirectToAction("Index");
        //}

        //public IActionResult Signout()
        //{
        //    return new SignOutResult(new[] {"Cookies" });
        //}

        [HttpPost]
        [AutoValidateAntiforgeryToken]
        public async Task<ActionResult> FetchOrderDate()
        {
            //DateTime dt = DateTime.Parse("{9/15/2019 12:00:00 AM}");

            try
            {
                string d = Request.Form["datepicker"];
                int index = d.IndexOf('-');


                string[] DateFormatarray = index > -1 ? d.Split("-") : d.Split("/");
                string datesv = DateFormatarray[1] + '/' + DateFormatarray[0] + '/' + DateFormatarray[2];
                DateTime date = DateTime.ParseExact(datesv, "M/d/yyyy", CultureInfo.InvariantCulture);
                ViewBag.OrderDate = date;
                ViewBag.FullName = await GetUserName();
                return View("Dashboard");
            }
            catch(Exception ex)
            {
                ViewBag.OrderDate = DateTime.Now;
                ViewBag.FullName = await GetUserName();
                return View("Dashboard");
            }

            //DateTime date = DateTime.Parse(Request.Form["datepicker"]);
           
        }
        [HttpPost]
        [AutoValidateAntiforgeryToken]
        public async Task<IActionResult> Check(int id,string userid)
        {
            var value = Request.Form["meal"];
            double quantity = 0;
            Double.TryParse(Request.Form["quantity"], out quantity);
            int mealTypeId=Int32.Parse(Request.Form["meal"]);

            string usrName = SessionExtensions.GetString(HttpContext.Session, "user");
            var user = await _userManager.FindByNameAsync(usrName);
            var userID = user.Id;



            var HistoryForExtraItem = _context.ExtraItem.Where(x => x.MenuDate.ToShortDateString() == DateTime.Now.ToShortDateString()  && x.StoreOutItemId == id).FirstOrDefault();



            OrderHistory oh = new OrderHistory();
            oh.UserId = userid;
            oh.MealTypeId = mealTypeId;
            oh.StoreOutItemId = id;
            oh.UnitOrdered = quantity;
            oh.OrderAmount = Convert.ToDouble(HistoryForExtraItem.Price * quantity);
            oh.IsPreOrder = false;
            oh.OrderDate = DateTime.Now;

            oh.CreatedBy = userID;
            oh.CreatedDate = DateTime.Now;
            oh.LastModifiedDate = DateTime.Now;


            _context.OrderHistory.Add(oh);
            _context.SaveChanges();
            long orderHistoryId = oh.Id;
            ViewBag.UserId = userid;

            return View("home");

        }
        [HttpPost]
        [AutoValidateAntiforgeryToken]
        public async Task<IActionResult> OnSpotOrderCreate(string userid)
        {
             int mealId = Int32.Parse(Request.Form["meal"]);
            string person = Request.Form["searchInput"];
            string onSpotMessage = "";
            var prsndtl = _userManager.Users.Where(x => x.BUPFullName == person).FirstOrDefault();
            string usrName = SessionExtensions.GetString(HttpContext.Session, "user");
            var user = await _userManager.FindByNameAsync(usrName);
            var userID = user.Id;
            var pn = Request.Form["setmenu"];
            var osi = Request.Form["onspotItem"];

            if(Int32.Parse(pn[0]) == -1 && Int32.Parse(osi[0]) == -1)
            {
                ViewBag.FullName = await GetUserName();
                ViewBag.OnSpotMessage = "Please select any item or set menu";


                return View("Dashboard");
            }

                if (Int32.Parse(pn[0]) != -1 )
            {
                if (!String.IsNullOrEmpty(Request.Form["stmQ"]) && decimal.Parse(Request.Form["stmQ"]) > 0)
                {
                    double stmQ = Double.Parse(Request.Form["stmQ"]);
                    decimal stmQt = decimal.Parse(Request.Form["stmQ"]);
                    long setmenuId = long.Parse(Request.Form["setmenu"]);

                    var smdtl = _context.SetMenu.Where(x => x.Id == setmenuId).FirstOrDefault();
                    var setMenuDetail = _context.SetMenuDetails.Where(x => x.SetMenuId == setmenuId).ToList();

                    foreach (var item in setMenuDetail)
                    {
                        CustomerChoiceV2 cc = new CustomerChoiceV2();
                        // cc.LastModifiedBy = userID;
                        cc.LastModifiedDate = DateTime.Now;
                        cc.Date = DateTime.Now;
                        cc.ExtraItemId = item.ExtraItemId;
                        cc.quantity = stmQ;
                        cc.UserId = prsndtl.Id;
                        cc.OrderTypeId = 10002;
                        //cc.OrderTypeId = 10002;Should Be Updated

                        cc.SetMenuId = setmenuId;
                        cc.CreatedBy = userID;
                        cc.MealTypeId = mealId;
                        cc.CreatedDate = DateTime.Now;
                        _context.CustomerChoiceV2.Add(cc);
                        _context.SaveChanges();

                    }
                }
                else
                {
                    onSpotMessage = "Invalid input format";
                }

                //OrderHistory oh = new OrderHistory();
                //oh.UserId = prsndtl.Id;
                //oh.MealTypeId = mealId;
                ////oh.StoreOutItemId = itmdtl.Id;
                //oh.UnitOrdered = stmQ;
                //oh.SetMenuId = setmenuId;
                //oh.OrderAmount = Convert.ToDouble(smdtl.SetMenuPrice * stmQt);
                ////Here 20 will be omitted in future when pricing issue will be solved.
                //oh.IsPreOrder = false;
                //oh.OrderDate = DateTime.Now;

                //oh.CreatedBy = userID;
                //oh.CreatedDate = DateTime.Now;
                //oh.LastModifiedDate = DateTime.Now;


                //_context.OrderHistory.Add(oh);
                //_context.SaveChanges();
                //long orderHistoryId = oh.Id;


            }
            if (Int32.Parse(osi[0]) !=  -1)
            {
                if (!String.IsNullOrEmpty(Request.Form["itmQ"]) && Double.Parse(Request.Form["itmQ"]) > 0)
                {
                    string item = Request.Form["onspotItem"];
                    var exItm = _context.ExtraItem.Where(x => x.Id == long.Parse(item)).FirstOrDefault();
                    var itmdtl = _context.StoreOutItem.Where(x => x.Id == exItm.StoreOutItemId).FirstOrDefault();
                    double itemQ = Double.Parse(Request.Form["itmQ"]);
                    //    OrderHistory oh = new OrderHistory();
                    //    oh.UserId = prsndtl.Id;
                    //    oh.MealTypeId = mealId;
                    //    oh.StoreOutItemId = itmdtl.Id;
                    //    oh.UnitOrdered = itemQ;
                    //    oh.OrderAmount = Convert.ToDouble(20 * itemQ);
                    //    //Here 20 will be omitted in future when pricing issue will be solved.
                    //    oh.IsPreOrder = false;
                    //    oh.OrderDate = DateTime.Now;

                    //    oh.CreatedBy = userID;
                    //    oh.CreatedDate = DateTime.Now;
                    //    oh.LastModifiedDate = DateTime.Now;


                    //    _context.OrderHistory.Add(oh);
                    //    _context.SaveChanges();

                    CustomerChoiceV2 cc = new CustomerChoiceV2();
                    //  cc.LastModifiedBy = userID;
                    cc.LastModifiedDate = DateTime.Now;
                    cc.Date = DateTime.Now;
                    cc.ExtraItemId = Int32.Parse(Request.Form["onspotItem"]);
                    cc.quantity = itemQ;
                    cc.UserId = prsndtl.Id;
                    cc.OrderTypeId = 10002;
                    //cc.OrderTypeId = 10002;Should Be updated

                    cc.CreatedBy = userID;
                    cc.MealTypeId = mealId;
                    cc.CreatedDate = DateTime.Now;
                    _context.CustomerChoiceV2.Add(cc);
                    _context.SaveChanges();
                }
                else
                {
                    onSpotMessage = "Invalid input format";
                }

                //long orderHistoryId = oh.Id;

            }
           

            //double quantity = 0;
            //Double.TryParse(Request.Form["quantity"], out quantity);
            //int mealTypeId = Int32.Parse(Request.Form["meal"]);



            //var HistoryForExtraItem = _context.ExtraItem.Where(x => x.MenuDate.ToShortDateString() == DateTime.Now.ToShortDateString() && x.StoreOutItemId == id).FirstOrDefault();




            //ViewBag.UserId = userid;
            if(mealId == 1)
            {
                ViewBag.submenu = 1;
            }
            else if (mealId == 2)
            {
                ViewBag.submenu = 2;
            }
            else if (mealId == 3)
            {
                ViewBag.submenu = 3;
            }
            else
            {
                ViewBag.submenu = 10005;
               // ViewBag.submenu = 10005;Updated
            }

            ViewBag.FullName = await GetUserName();
            ViewBag.OnSpotMessage = onSpotMessage;


            return View("Dashboard");


        }

        [HttpPost]
        //[ValidateAntiForgeryToken]

        public async Task<IActionResult> CreateOnspotOrder(string MealTypeId,string ItemId)
        {
            //return RedirectToAction(nameof(home));
            return Json(new { success = true, responseText = "" });
   
        }


        public ActionResult DatePerson(DateTime ? from,string searchInput)
        {
           // var user = _userManager.Users.Where(x => x.BUPFullName == searchInput).FirstOrDefault();
            ViewBag.OrderDate = from;
         //   ViewBag.UserId = user.Id;
            return View("home");

        }

        public async Task<ActionResult> FetchPersonOrder(int MealId,int SubMenuId)
        {
            var person = "";
            if(MealId == 1)
            {
                person = Request.Form["person"].ToString();
            }
            else if(MealId == 2)
            {
                person = Request.Form["person3"].ToString();
            }
            else if(MealId == 3)
            {
                person = Request.Form["person2"].ToString();
            }
            else
            {
                person = Request.Form["person4"].ToString();
            }

            ViewBag.person = person;
            ViewBag.MealId = MealId;
            ViewBag.SubMenuId = SubMenuId;
            ViewBag.FullName = await GetUserName();

            return View("Dashboard");

        }

        public async Task<ActionResult> FetchItemOrder(int MealId, int SubMenuId)
        {
            var item = "";
            if(MealId == 1)
            {
                item = Request.Form["item"].ToString();

            }
            else if (MealId == 2)
            {
                item = Request.Form["item3"].ToString();

            }
            else if(MealId == 3)
            {
                item = Request.Form["item2"].ToString();
            }
            else
            {
                item = Request.Form["item4"].ToString();
            }
            ViewBag.item = item;
            ViewBag.MealId = MealId;
            ViewBag.SubMenuId = SubMenuId;
            ViewBag.FullName = await GetUserName();

            return View("Dashboard");

        }

        public ActionResult DateMeal(DateTime? from, string meal)
        {
            var mealtype = _context.MealType.Where(x => x.Id == Int32.Parse(meal)).FirstOrDefault();
            ViewBag.OrderDate = from;
            ViewBag.mealId = mealtype.Id;
            return View("Schedule");

        }
        public ActionResult DateMealItem(DateTime? from, string meal)
        {
            var mealtype = _context.MealType.Where(x => x.Id == Int32.Parse(meal)).FirstOrDefault();
            ViewBag.OrderDate = from;
            ViewBag.mealId = mealtype.Id;
            return View("ItemBase");

        }

        public JsonResult GetSearchPerson(string search)
        {
            var PersonList = _userManager.Users.Where(x => x.BUPFullName.Contains(search)).ToList();

            return Json(PersonList);
        }
        public JsonResult GetSearchItem(string search)
        {
            var ItemList = _context.StoreOutItem.Where(x=>x.Name.Contains(search)).ToList();

            var jsn = Json(ItemList);


            return Json(ItemList);
        }
        public JsonResult GetSearchOffice(string search)
        {
            var ItemList = _context.Office.Where(x => x.Name.Contains(search)).ToList();

            var jsn = Json(ItemList);


            return Json(ItemList);
        }

        public async Task<string> AddPartyItem(string Person, string Item, string Date, string Quantity, string Price)
        {
            List<String> ErrorList = new List<string>();
            List<String> ErrorBit = new List<string>();

            List<IList> TtlList = new List<IList>();

             var person = _userManager.Users.Where(x => x.BUPFullName == Person).FirstOrDefault();
            if (Person == null || Item == null || Date == null || Quantity == null)
            {
                ErrorList.Add("Empty Field is not allowed");
                ErrorBit.Add("1");
                TtlList.Add(ErrorList);
                TtlList.Add(ErrorBit);

            }
            else if (person == null)
            {
                ErrorList.Add("Given user does not exist in Mess");
                ErrorBit.Add("1");
                TtlList.Add(ErrorList);
                TtlList.Add(ErrorBit);
            }
            else
            {
                long prntID = 0;
                long strId = 0;
                string usrName = SessionExtensions.GetString(HttpContext.Session, "user");
                var user = await _userManager.FindByNameAsync(usrName);
                var ItemList = _context.StoreOutItem.Where(x => x.Name == Item).FirstOrDefault();




                if (ItemList == null &&  int.Parse(Price) != 0)
                {
                    double price = double.Parse(Price);

                    StoreOutItem soi = new StoreOutItem();
                    soi.CreatedDate = DateTime.Now;
                    soi.LastModifiedDate = DateTime.Now;
                    soi.LastModifiedBy = "1";//Should Be changed............
                    soi.CreatedBy = "1";
                    soi.Name = Item;
                    soi.IsOpen = true;
                    soi.MinimumProductionUnit = 5;
                    soi.MinimumProductionUnitMultiplier = 5;
                    soi.Price = price;
                    soi.UnitTypeId = 8;
                    soi.StoreOutCategoryId = 8;

                    //soi.UnitTypeId = 8;
                    //soi.StoreOutCategoryId = 8;
                    //will be checked

                    _context.StoreOutItem.Add(soi);
                    _context.SaveChanges();

                    strId = soi.Id;

                }
                else if(ItemList == null && Price == null)
                {
                    ErrorList.Add("New item price should be mentioned.");
                    ErrorBit.Add("1");
                    TtlList.Add(ErrorList);
                    TtlList.Add(ErrorBit);
                }
                else
                {
                    strId = ItemList.Id;
                }

                string d = Date;
                string[] DateFormatarray = d.Split("-");
                string datesv = DateFormatarray[1] + '/' + DateFormatarray[0] + '/' + DateFormatarray[2];
                DateTime date = DateTime.ParseExact(datesv, "M/d/yyyy", CultureInfo.InvariantCulture);

                var spclPrnt = _context.SpecialMenuParent.Where(x => x.UserId == person.Id && x.MealTypeId == 10006 && x.OrderDate.ToShortDateString() == date.ToShortDateString()).FirstOrDefault();

                if (spclPrnt == null)
                {
                    SpecialMenuParent smp = new SpecialMenuParent();
                    smp.UserId = person.Id;
                    smp.OrderDate = date;
                    smp.MealTypeId = 10006;
                    smp.CreatedBy = user.Id;
                    smp.LastModifiedBy = user.Id;
                    smp.CreatedDate = DateTime.Now;
                    smp.LastModifiedDate = DateTime.Now;

                    //smp.MealTypeId = 10006; will be checked

                    _context.SpecialMenuParent.Add(smp);
                    _context.SaveChanges();

                    prntID = smp.Id;

                }
                else
                {
                    prntID = spclPrnt.Id;
                }

                SpecialMenuOrder smo = new SpecialMenuOrder();
                smo.StoreOutItemId = strId;
                smo.LastModifiedDate = DateTime.Now;
                smo.CreatedDate = DateTime.Now;
                smo.SpecialMenuParentId = prntID;

                smo.UnitOrdered = Double.Parse(Quantity);

                _context.SpecialMenuOrder.Add(smo);
                _context.SaveChanges();

                var spclList = _context.SpecialMenuOrder.Where(x => x.SpecialMenuParentId == prntID).ToList();

                var storeOutItem = _context.StoreOutItem.Where(x => x.Id > 0).ToList();
                ErrorBit.Add("0");

                TtlList.Add(spclList);
                TtlList.Add(storeOutItem);
                TtlList.Add(ErrorBit);

                //JsonConvert.DeserializeObject


            }


            string json = JsonConvert.SerializeObject(TtlList, Formatting.Indented, new JsonSerializerSettings
            {
                ReferenceLoopHandling = ReferenceLoopHandling.Ignore
            });


            //string st = JsonConvert.SerializeObject(spclList {
            //    ReferenceLoopHandling = ReferenceLoopHandling.Ignore;
            //});
            //js.MaxJsonLength = 2147483644;
            //var serializer = new JavaScriptSerializer() { MaxJsonLength = 500000000 };
            //var serializedResult = js.Serialize(myLists);
            return json;
            //var jsn = Json(spclList);
            //return "ko";
        }

        public string AddOfficeItem(string Person, string Office, string Item, string Date, string Quantity, string Price)
        {
            var person = _userManager.Users.Where(x => x.BUPFullName == Person).FirstOrDefault();
            var office = _context.Office.Where(x => x.Name == Office).FirstOrDefault();
            double price = double.Parse(Price);
            List<String> ErrorList = new List<string>();
            List<String> ErrorBit = new List<string>();


            long prntID = 0;
            long strId = 0;

            List<IList> TtlList = new List<IList>();

            if (Person == null || Item == null || Date == null || Quantity == null || Office == null)
            {
                ErrorList.Add("Empty Field is not allowed");
                ErrorBit.Add("1");
                TtlList.Add(ErrorList);
                TtlList.Add(ErrorBit);
            }
            else if (person == null)
            {
                ErrorList.Add("Given user does not exist in Mess");
                ErrorBit.Add("1");
                TtlList.Add(ErrorList);
                TtlList.Add(ErrorBit);
            }
            else if(office == null)
            {
                ErrorList.Add("Given office does not exist in Mess");
                ErrorBit.Add("1");
                TtlList.Add(ErrorList);
                TtlList.Add(ErrorBit);
            }
            else
            {
                var ItemList = _context.StoreOutItem.Where(x => x.Name == Item).FirstOrDefault();


                if (ItemList == null && Price != null)
                {
                    StoreOutItem soi = new StoreOutItem();
                    soi.CreatedDate = DateTime.Now;
                    soi.LastModifiedDate = DateTime.Now;
                    soi.LastModifiedBy = "1";//Should Be changed............
                    soi.CreatedBy = "1";
                    soi.Name = Item;
                    soi.IsOpen = true;
                    soi.MinimumProductionUnit = 5;
                    soi.MinimumProductionUnitMultiplier = 5;
                    soi.Price = price;
                    soi.StoreOutCategoryId = 8;
                    soi.UnitTypeId = 8;
                    //soi.StoreOutCategoryId = 8;
                    //soi.UnitTypeId = 8;

                    _context.StoreOutItem.Add(soi);
                    _context.SaveChanges();
                    strId = soi.Id;

                }
                else if(ItemList == null && Price == null)
                {
                    ErrorList.Add("New item price should be mentioned.");
                    ErrorBit.Add("1");
                    TtlList.Add(ErrorList);
                    TtlList.Add(ErrorBit);
                }
                else
                {
                    strId = ItemList.Id;
                }

                if (ErrorList.Count == 0)
                {

                    string d = Date;
                    string[] DateFormatarray = d.Split("-");
                    string datesv = DateFormatarray[1] + '/' + DateFormatarray[0] + '/' + DateFormatarray[2];
                    DateTime date = DateTime.ParseExact(datesv, "M/d/yyyy", CultureInfo.InvariantCulture);

                    var spclPrnt = _context.SpecialMenuParent.Where(x => x.OfficeId == office.Id && x.MealTypeId == 10007 && x.OrderDate.ToShortDateString() == date.ToShortDateString()).FirstOrDefault();

                    if (spclPrnt == null)
                    {
                        SpecialMenuParent smp = new SpecialMenuParent();
                        smp.UserId = person.Id;
                        smp.OrderDate = date;
                        smp.OfficeId = office.Id;
                        smp.MealTypeId = 10007;
                        //smp.MealTypeId = 10007;
                        _context.SpecialMenuParent.Add(smp);
                        _context.SaveChanges();

                        prntID = smp.Id;

                    }
                    else
                    {
                        prntID = spclPrnt.Id;
                    }

                    SpecialMenuOrder smo = new SpecialMenuOrder();
                    smo.StoreOutItemId = strId;
                    smo.LastModifiedDate = DateTime.Now;
                    smo.CreatedDate = DateTime.Now;
                    smo.SpecialMenuParentId = prntID;

                    smo.UnitOrdered = Double.Parse(Quantity);

                    _context.SpecialMenuOrder.Add(smo);
                    _context.SaveChanges();

                    var spclList = _context.SpecialMenuOrder.Where(x => x.SpecialMenuParentId == prntID).ToList();

                    var storeOutItem = _context.StoreOutItem.Where(x => x.Id > 0).ToList();
                    ErrorBit.Add("0");

                    TtlList.Add(spclList);
                    TtlList.Add(storeOutItem);
                    TtlList.Add(ErrorBit);
                }
            }
            //JsonConvert.DeserializeObject


            string json = JsonConvert.SerializeObject(TtlList, Formatting.Indented, new JsonSerializerSettings
            {
                ReferenceLoopHandling = ReferenceLoopHandling.Ignore
            });


            //string st = JsonConvert.SerializeObject(spclList {
            //    ReferenceLoopHandling = ReferenceLoopHandling.Ignore;
            //});
            //js.MaxJsonLength = 2147483644;
            //var serializer = new JavaScriptSerializer() { MaxJsonLength = 500000000 };
            //var serializedResult = js.Serialize(myLists);
            return json;
            //var jsn = Json(spclList);
            //return "ko";
        }



        public string GetSpecialMenuEdit()
        {


            //JsonConvert.DeserializeObject
            //var spclParent = _context.SpecialMenuParent.Where(x => x.OrderDate >= DateTime.Now && x.MealTypeId == 10006).ToList();
            // will be checked above........................
            var spclParent = _context.SpecialMenuParent.Where(x => x.OrderDate >= DateTime.Now && x.MealTypeId == 10006).ToList();

            var specialMenu = _context.SpecialMenuOrder.Where(x => x.Id > 0).ToList();
            //            var distinctPersonDateList = specialMenu
            //.GroupBy(m => new { m.UserId, m.OrderDate })
            //.Select(group => group.First())  // instead of First you can also apply your logic here what you want to take, for example an OrderBy
            //.ToList();
            var storeOutItem = _context.StoreOutItem.Where(x => x.Id > 0).ToList();
            var User = _userManager.Users.Where(x => x.Id != null).ToList();

            List<IList> TtlList = new List<IList>();


            TtlList.Add(specialMenu);
            TtlList.Add(spclParent);

            TtlList.Add(storeOutItem);
            TtlList.Add(User);



            string json = JsonConvert.SerializeObject(TtlList, Formatting.Indented, new JsonSerializerSettings
            {
                ReferenceLoopHandling = ReferenceLoopHandling.Ignore
            });


            //string st = JsonConvert.SerializeObject(spclList {
            //    ReferenceLoopHandling = ReferenceLoopHandling.Ignore;
            //});
            //js.MaxJsonLength = 2147483644;
            //var serializer = new JavaScriptSerializer() { MaxJsonLength = 500000000 };
            //var serializedResult = js.Serialize(myLists);
            return json;
            //var jsn = Json(spclList);
        }




        public string GetSpecialOfficeMenuEdit()
        {


            //JsonConvert.DeserializeObject
           // var spclParent = _context.SpecialMenuParent.Where(x => x.OrderDate >= DateTime.Now && x.MealTypeId == 10007).ToList();
            //will be checked
            var spclParent = _context.SpecialMenuParent.Where(x => x.OrderDate >= DateTime.Now && x.MealTypeId == 10007).ToList();

            var specialMenu = _context.SpecialMenuOrder.Where(x => x.Id > 0).ToList();
            //            var distinctPersonDateList = specialMenu
            //.GroupBy(m => new { m.UserId, m.OrderDate })
            //.Select(group => group.First())  // instead of First you can also apply your logic here what you want to take, for example an OrderBy
            //.ToList();
            var storeOutItem = _context.StoreOutItem.Where(x => x.Id > 0).ToList();
            var Office = _context.Office.Where(x => x.Id > 0).ToList();

            List<IList> TtlList = new List<IList>();


            TtlList.Add(specialMenu);
            TtlList.Add(spclParent);

            TtlList.Add(storeOutItem);
            TtlList.Add(Office);



            string json = JsonConvert.SerializeObject(TtlList, Formatting.Indented, new JsonSerializerSettings
            {
                ReferenceLoopHandling = ReferenceLoopHandling.Ignore
            });


            //string st = JsonConvert.SerializeObject(spclList {
            //    ReferenceLoopHandling = ReferenceLoopHandling.Ignore;
            //});
            //js.MaxJsonLength = 2147483644;
            //var serializer = new JavaScriptSerializer() { MaxJsonLength = 500000000 };
            //var serializedResult = js.Serialize(myLists);
            return json;
            //var jsn = Json(spclList);
        }





        public string SaveSpecialMenuEdit(string Id, string Item, string Quantity)
        {


            //JsonConvert.DeserializeObject
            List<string> ErrorList = new List<string>();
            List<string> ErrorBit = new List<string>();
            List<IList> TtlList = new List<IList>();
            var storeout = _context.StoreOutItem.Where(x => x.Name == Item).FirstOrDefault();



            if (Item == null || Quantity == null || double.Parse(Quantity) <= 0)
            {
                ErrorList.Add("Input field is not valid");
                ErrorBit.Add("1");
                TtlList.Add(ErrorList);
                TtlList.Add(ErrorBit);
            }
            else if (storeout == null)
            {
                ErrorList.Add("Given item is not available in mess");
                ErrorBit.Add("1");
                TtlList.Add(ErrorList);
                TtlList.Add(ErrorBit); 
            }
            else
            {

                long spId = long.Parse(Id);



                var specialMenu = _context.SpecialMenuOrder.Where(x => x.Id == spId).FirstOrDefault();

                specialMenu.StoreOutItemId = storeout.Id;
                specialMenu.UnitOrdered = double.Parse(Quantity);

                _context.SpecialMenuOrder.Update(specialMenu);
                _context.SaveChanges();

                // var spclParent = _context.SpecialMenuParent.Where(x => x.OrderDate >= DateTime.Now && x.MealTypeId == 10006).ToList();
                // check here 
                var spclParent = _context.SpecialMenuParent.Where(x => x.OrderDate >= DateTime.Now && x.MealTypeId == 10006).ToList();

                var specialMenu2 = _context.SpecialMenuOrder.Where(x => x.Id > 0).ToList();

                //            var distinctPersonDateList = specialMenu
                //.GroupBy(m => new { m.UserId, m.OrderDate })
                //.Select(group => group.First())  // instead of First you can also apply your logic here what you want to take, for example an OrderBy
                //.ToList();
                var storeOutItem = _context.StoreOutItem.Where(x => x.Id > 0).ToList();
                var User = _userManager.Users.Where(x => x.Id != null).ToList();



                TtlList.Add(specialMenu2);
                TtlList.Add(spclParent);

                TtlList.Add(storeOutItem);
                TtlList.Add(User);
                ErrorBit.Add("0");
                TtlList.Add(ErrorBit);

            }



            string json = JsonConvert.SerializeObject(TtlList, Formatting.Indented, new JsonSerializerSettings
            {
                ReferenceLoopHandling = ReferenceLoopHandling.Ignore
            });


            //string st = JsonConvert.SerializeObject(spclList {
            //    ReferenceLoopHandling = ReferenceLoopHandling.Ignore;
            //});
            //js.MaxJsonLength = 2147483644;
            //var serializer = new JavaScriptSerializer() { MaxJsonLength = 500000000 };
            //var serializedResult = js.Serialize(myLists);
            return json;
            //var jsn = Json(spclList);
        }



        public string SaveSpecialMenuDateEdit(string Date, string Id)
        {


            //JsonConvert.DeserializeObject

            List<string> ErrorList = new List<string>();
            List<string> ErrorBit = new List<string>();
            List<IList> TtlList = new List<IList>();
            if (Date == null)
            {
                ErrorList.Add("Empty field not allowed");
                ErrorBit.Add("1");
                TtlList.Add(ErrorList);
                TtlList.Add(ErrorBit);
            }
            else
            {

                long spId = long.Parse(Id);


                string[] DateFormatarray = Date.Split("-");
                string datesv = DateFormatarray[1] + '/' + DateFormatarray[0] + '/' + DateFormatarray[2];
                DateTime date = DateTime.ParseExact(datesv, "M/d/yyyy", CultureInfo.InvariantCulture);

                //var storeout = _context.StoreOutItem.Where(x => x.Name == Item).FirstOrDefault();

                var specialMenu = _context.SpecialMenuParent.Where(x => x.Id == spId).FirstOrDefault();

                specialMenu.OrderDate = date;

                _context.SpecialMenuParent.Update(specialMenu);
                _context.SaveChanges();



                var specialMenu2 = _context.SpecialMenuOrder.Where(x => x.Id > 0).ToList();
                TtlList.Add(specialMenu2);
                //if (specialMenu.MealTypeId == 10006) check here 

                if (specialMenu.MealTypeId == 10006)
                {

                    var spclParent = _context.SpecialMenuParent.Where(x => x.OrderDate >= DateTime.Now && x.MealTypeId == 10006).ToList();
                    //var spclParent = _context.SpecialMenuParent.Where(x => x.OrderDate >= DateTime.Now && x.MealTypeId == 10006).ToList();
                    // Check above...........................................
                    TtlList.Add(spclParent);

                    var storeOutItem = _context.StoreOutItem.Where(x => x.Id > 0).ToList();
                    var User = _userManager.Users.Where(x => x.Id != null).ToList();




                    TtlList.Add(storeOutItem);
                    TtlList.Add(User);
                    

                }
                else
                {
                    var spclParent = _context.SpecialMenuParent.Where(x => x.OrderDate >= DateTime.Now && x.MealTypeId == 10007).ToList();

                    // var spclParent = _context.SpecialMenuParent.Where(x => x.OrderDate >= DateTime.Now && x.MealTypeId == 10007).ToList();
                    //check above
                    TtlList.Add(spclParent);

                    var storeOutItem = _context.StoreOutItem.Where(x => x.Id > 0).ToList();
                    var Office = _context.Office.Where(x => x.Id != null).ToList();




                    TtlList.Add(storeOutItem);
                    TtlList.Add(Office);

                }
                ErrorBit.Add("0");
                TtlList.Add(ErrorBit);
            }



            //            var distinctPersonDateList = specialMenu
            //.GroupBy(m => new { m.UserId, m.OrderDate })
            //.Select(group => group.First())  // instead of First you can also apply your logic here what you want to take, for example an OrderBy
            //.ToList();




            string json = JsonConvert.SerializeObject(TtlList, Formatting.Indented, new JsonSerializerSettings
            {
                ReferenceLoopHandling = ReferenceLoopHandling.Ignore
            });


            //string st = JsonConvert.SerializeObject(spclList {
            //    ReferenceLoopHandling = ReferenceLoopHandling.Ignore;
            //});
            //js.MaxJsonLength = 2147483644;
            //var serializer = new JavaScriptSerializer() { MaxJsonLength = 500000000 };
            //var serializedResult = js.Serialize(myLists);
            return json;
            //var jsn = Json(spclList);
        }



        public string SaveSpecialMenuNameEdit(string Person, string Id)
        {


            //JsonConvert.DeserializeObject

            long spId = long.Parse(Id);

            long exId = 0;


            List<string> ErrorList = new List<string>();
            List<string> ErrorBit = new List<string>();
            List<IList> TtlList = new List<IList>();

            if(Person == null)
            {
                ErrorList.Add("Empty field is not allowed");
                ErrorBit.Add("1");
                TtlList.Add(ErrorList);
                TtlList.Add(ErrorBit);
            }

            //string[] DateFormatarray = Date.Split("-");
            //string datesv = DateFormatarray[1] + '/' + DateFormatarray[0] + '/' + DateFormatarray[2];
            //DateTime date = DateTime.ParseExact(datesv, "M/d/yyyy", CultureInfo.InvariantCulture);

            //var storeout = _context.StoreOutItem.Where(x => x.Name == Item).FirstOrDefault();


            var specialMenu = _context.SpecialMenuParent.Where(x => x.Id == spId).FirstOrDefault();
            //if (specialMenu.MealTypeId == 10006) check here
            if (ErrorList.Count == 0)
            {
                if (specialMenu.MealTypeId == 10006)
                {
                    var prsn = _userManager.Users.Where(x => x.BUPFullName == Person).FirstOrDefault();
                    if (prsn != null)
                    {
                        specialMenu.UserId = prsn.Id;

                        _context.SpecialMenuParent.Update(specialMenu);
                        _context.SaveChanges();
                    }
                    else
                    {
                        ErrorList.Add("Person doesn't exist in the mess");
                        ErrorBit.Add("1");
                        TtlList.Add(ErrorList);
                        TtlList.Add(ErrorBit);
                    }
                }
                else
                {
                    var off = _context.Office.Where(x => x.Name == Person).FirstOrDefault();
                    if (off != null)
                    {
                        specialMenu.OfficeId = off.Id;

                        _context.SpecialMenuParent.Update(specialMenu);
                        _context.SaveChanges();
                    }
                    else
                    {
                        ErrorList.Add("Office doesn't exist in the mess");
                        ErrorBit.Add("1");
                        TtlList.Add(ErrorList);
                        TtlList.Add(ErrorBit);
                    }

                }
            }





            if (ErrorList.Count == 0)
            {
                var specialMenu2 = _context.SpecialMenuOrder.Where(x => x.Id > 0).ToList();
                TtlList.Add(specialMenu2);


                if (specialMenu.MealTypeId == 10006)
                {

                    var spclParent = _context.SpecialMenuParent.Where(x => x.OrderDate >= DateTime.Now && x.MealTypeId == 10006).ToList();
                    // var spclParent = _context.SpecialMenuParent.Where(x => x.OrderDate >= DateTime.Now && x.MealTypeId == 10006).ToList();

                    TtlList.Add(spclParent);
                    var storeOutItem = _context.StoreOutItem.Where(x => x.Id > 0).ToList();
                    var User = _userManager.Users.Where(x => x.Id != null).ToList();




                    TtlList.Add(storeOutItem);
                    TtlList.Add(User);

                }
                else
                {
                    var spclParent = _context.SpecialMenuParent.Where(x => x.OrderDate >= DateTime.Now && x.MealTypeId == 10007).ToList();
                    // var spclParent = _context.SpecialMenuParent.Where(x => x.OrderDate >= DateTime.Now && x.MealTypeId == 10007).ToList();

                    TtlList.Add(spclParent);

                    var storeOutItem = _context.StoreOutItem.Where(x => x.Id > 0).ToList();
                    var Office = _context.Office.Where(x => x.Id != null).ToList();




                    TtlList.Add(storeOutItem);
                    TtlList.Add(Office);

                }
                ErrorBit.Add("0");
                TtlList.Add(ErrorBit);
            }


            //var spclParent = _context.SpecialMenuParent.Where(x => x.OrderDate >= DateTime.Now && x.MealTypeId == 10006).ToList();


            //            var distinctPersonDateList = specialMenu
            //.GroupBy(m => new { m.UserId, m.OrderDate })
            //.Select(group => group.First())  // instead of First you can also apply your logic here what you want to take, for example an OrderBy
            //.ToList();







            string json = JsonConvert.SerializeObject(TtlList, Formatting.Indented, new JsonSerializerSettings
            {
                ReferenceLoopHandling = ReferenceLoopHandling.Ignore
            });


            //string st = JsonConvert.SerializeObject(spclList {
            //    ReferenceLoopHandling = ReferenceLoopHandling.Ignore;
            //});
            //js.MaxJsonLength = 2147483644;
            //var serializer = new JavaScriptSerializer() { MaxJsonLength = 500000000 };
            //var serializedResult = js.Serialize(myLists);
            return json;
            //var jsn = Json(spclList);
        }



        public string DeleteSpecialMenu(string Id)
        {



            long spId = long.Parse(Id);



            var specialMenu = _context.SpecialMenuOrder.Where(x => x.Id == spId).FirstOrDefault();
            var spclPrnt = _context.SpecialMenuParent.Where(x => x.Id == specialMenu.SpecialMenuParentId).FirstOrDefault();


            _context.SpecialMenuOrder.Remove(specialMenu);
            _context.SaveChanges();

            List<IList> TtlList = new List<IList>();



            var specialMenu2 = _context.SpecialMenuOrder.Where(x => x.Id > 0).ToList();
            TtlList.Add(specialMenu2);


            if (spclPrnt.MealTypeId == 10006)
            {

                var spclParent = _context.SpecialMenuParent.Where(x => x.OrderDate >= DateTime.Now && x.MealTypeId == 10006).ToList();

                // var spclParent = _context.SpecialMenuParent.Where(x => x.OrderDate >= DateTime.Now && x.MealTypeId == 10006).ToList();
                TtlList.Add(spclParent);
                var storeOutItem = _context.StoreOutItem.Where(x => x.Id > 0).ToList();
                var User = _userManager.Users.Where(x => x.Id != null).ToList();




                TtlList.Add(storeOutItem);
                TtlList.Add(User);

            }
            else
            {
                var spclParent = _context.SpecialMenuParent.Where(x => x.OrderDate >= DateTime.Now && x.MealTypeId == 10007).ToList();
                //var spclParent = _context.SpecialMenuParent.Where(x => x.OrderDate >= DateTime.Now && x.MealTypeId == 10007).ToList();

                TtlList.Add(spclParent);

                var storeOutItem = _context.StoreOutItem.Where(x => x.Id > 0).ToList();
                var Office = _context.Office.Where(x => x.Id != null).ToList();




                TtlList.Add(storeOutItem);
                TtlList.Add(Office);

            }






            string json = JsonConvert.SerializeObject(TtlList, Formatting.Indented, new JsonSerializerSettings
            {
                ReferenceLoopHandling = ReferenceLoopHandling.Ignore
            });


            return json;
        }


        public string FetchSpecialMenu(string Id)
        {

            long spId = long.Parse(Id);

            var specialMenu2 = _context.SpecialMenuOrder.Where(x => x.Id > 0).ToList();


            List<IList> TtlList = new List<IList>();


            TtlList.Add(specialMenu2);


            if (spId == 10006)
            {

                var spclParent = _context.SpecialMenuParent.Where(x => x.OrderDate >= DateTime.Now && x.MealTypeId == 10006).ToList();
                // var spclParent = _context.SpecialMenuParent.Where(x => x.OrderDate >= DateTime.Now && x.MealTypeId == 10006).ToList();

                TtlList.Add(spclParent);
                var storeOutItem = _context.StoreOutItem.Where(x => x.Id > 0).ToList();
                var User = _userManager.Users.Where(x => x.Id != null).ToList();




                TtlList.Add(storeOutItem);
                TtlList.Add(User);

            }
            else
            {
                var spclParent = _context.SpecialMenuParent.Where(x => x.OrderDate >= DateTime.Now && x.MealTypeId == 10007).ToList();
                // var spclParent = _context.SpecialMenuParent.Where(x => x.OrderDate >= DateTime.Now && x.MealTypeId == 10007).ToList();

                TtlList.Add(spclParent);

                var storeOutItem = _context.StoreOutItem.Where(x => x.Id > 0).ToList();
                var Office = _context.Office.Where(x => x.Id != null).ToList();




                TtlList.Add(storeOutItem);
                TtlList.Add(Office);

            }


            //JsonConvert.DeserializeObject


            //string[] DateFormatarray = Date.Split("-");
            //string datesv = DateFormatarray[1] + '/' + DateFormatarray[0] + '/' + DateFormatarray[2];
            //DateTime date = DateTime.ParseExact(datesv, "M/d/yyyy", CultureInfo.InvariantCulture);

            //var storeout = _context.StoreOutItem.Where(x => x.Name == Item).FirstOrDefault();

            //var prsn = _userManager.Users.Where(x => x.BUPFullName == Person).FirstOrDefault();





            //

            //            var distinctPersonDateList = specialMenu
            //.GroupBy(m => new { m.UserId, m.OrderDate })
            //.Select(group => group.First())  // instead of First you can also apply your logic here what you want to take, for example an OrderBy
            //.ToList();




            string json = JsonConvert.SerializeObject(TtlList, Formatting.Indented, new JsonSerializerSettings
            {
                ReferenceLoopHandling = ReferenceLoopHandling.Ignore
            });


            //string st = JsonConvert.SerializeObject(spclList {
            //    ReferenceLoopHandling = ReferenceLoopHandling.Ignore;
            //});
            //js.MaxJsonLength = 2147483644;
            //var serializer = new JavaScriptSerializer() { MaxJsonLength = 500000000 };
            //var serializedResult = js.Serialize(myLists);
            return json;
            //var jsn = Json(spclList);
        }


        //public string GetSpecialOnDate(string Date)
        //{
        //    string d = Date;
        //    List<String> result = new List<string>();
        //    string[] DateFormatarray = d.Split("-");
        //    string datesv = DateFormatarray[1] + '/' + DateFormatarray[0] + '/' + DateFormatarray[2];
        //    DateTime date = DateTime.ParseExact(datesv, "M/d/yyyy", CultureInfo.InvariantCulture);

        //    if(date >= DateTime.Now)
        //    {
        //        result.Add("0");
        //    }
        //    else
        //    {
        //        result.Add("1");
        //    }


        //    //long spId = long.Parse(Id);





        //    List<IList> TtlList = new List<IList>();



        //    var specialMenu2 = _context.SpecialMenuOrder.Where(x => x.Id > 0).ToList();
        //    TtlList.Add(specialMenu2);
        //    var spclParent = _context.SpecialMenuParent.Where(x => x.OrderDate == date).ToList();
        //    TtlList.Add(spclParent);

        //    var storeOutItem = _context.StoreOutItem.Where(x => x.Id > 0).ToList();
        //    var User = _userManager.Users.Where(x => x.Id != null).ToList();
        //    var Office = _context.Office.Where(x => x.Id != null).ToList();
        //    TtlList.Add(storeOutItem);
        //    TtlList.Add(User);
        //    TtlList.Add(Office);
        //    TtlList.Add(result);










        //    string json = JsonConvert.SerializeObject(TtlList, Formatting.Indented, new JsonSerializerSettings
        //    {
        //        ReferenceLoopHandling = ReferenceLoopHandling.Ignore
        //    });


        //    return json;

        //}


        public async Task<string> SpecialPrint(string Id)
        {
            long prntId = long.Parse(Id);
            List<IList> TtlList = new List<IList>();
            List<String> action = new List<string>();

            string usrName = SessionExtensions.GetString(HttpContext.Session, "user");
            var admin = await _userManager.FindByNameAsync(usrName);
          //  var admin = await _userManager.GetUserAsync(userr);

            action.Add(admin.BUPFullName);
            action.Add(DateTime.Now.ToString());


            var spclParent = _context.SpecialMenuParent.Where(x => x.Id == prntId).ToList();
            var spclParentObj = _context.SpecialMenuParent.Where(x => x.Id == prntId).FirstOrDefault();


            var spclmenu = _context.SpecialMenuOrder.Where(x => x.SpecialMenuParentId == prntId).ToList();
            var storeOut = _context.StoreOutItem.Where(x => x.Id > 0).ToList();
            var user = _userManager.Users.Where(x => x.Id == spclParentObj.UserId).ToList();
            var office = _context.Office.Where(x => x.Id > 0).ToList();

            TtlList.Add(spclmenu);
            TtlList.Add(spclParent);

            TtlList.Add(storeOut);

            TtlList.Add(user);

            TtlList.Add(office);
            TtlList.Add(action);


            string json = JsonConvert.SerializeObject(TtlList, Formatting.Indented, new JsonSerializerSettings
            {
                ReferenceLoopHandling = ReferenceLoopHandling.Ignore
            });


            return json;





        }







        public string SaveSpecialOfficeMenuEdit(string Id, string Item, string Quantity)
        {


            //JsonConvert.DeserializeObject
            List<string> ErrorList = new List<string>();
            List<string> ErrorBit = new List<string>();
            List<IList> TtlList = new List<IList>();



            if (Item == null || Quantity == null || double.Parse(Quantity) <= 0)
            {
                ErrorList.Add("Input field is not valid");
                ErrorBit.Add("1");
                TtlList.Add(ErrorList);
                TtlList.Add(ErrorBit);
            }
            else
            {
                long spId = long.Parse(Id);

                var storeout = _context.StoreOutItem.Where(x => x.Name == Item).FirstOrDefault();

                var specialMenu = _context.SpecialMenuOrder.Where(x => x.Id == spId).FirstOrDefault();

                specialMenu.StoreOutItemId = storeout.Id;
                specialMenu.UnitOrdered = double.Parse(Quantity);

                _context.SpecialMenuOrder.Update(specialMenu);
                _context.SaveChanges();


                var spclParent = _context.SpecialMenuParent.Where(x => x.OrderDate >= DateTime.Now && x.MealTypeId == 10007).ToList();

                var specialMenu2 = _context.SpecialMenuOrder.Where(x => x.Id > 0).ToList();

                //            var distinctPersonDateList = specialMenu
                //.GroupBy(m => new { m.UserId, m.OrderDate })
                //.Select(group => group.First())  // instead of First you can also apply your logic here what you want to take, for example an OrderBy
                //.ToList();
                var storeOutItem = _context.StoreOutItem.Where(x => x.Id > 0).ToList();
                var Office = _context.Office.Where(x => x.Id > 0).ToList();



                TtlList.Add(specialMenu2);
                TtlList.Add(spclParent);

                TtlList.Add(storeOutItem);
                TtlList.Add(Office);
                ErrorBit.Add("0");
                TtlList.Add(ErrorBit);
            }



            string json = JsonConvert.SerializeObject(TtlList, Formatting.Indented, new JsonSerializerSettings
            {
                ReferenceLoopHandling = ReferenceLoopHandling.Ignore
            });


            //string st = JsonConvert.SerializeObject(spclList {
            //    ReferenceLoopHandling = ReferenceLoopHandling.Ignore;
            //});
            //js.MaxJsonLength = 2147483644;
            //var serializer = new JavaScriptSerializer() { MaxJsonLength = 500000000 };
            //var serializedResult = js.Serialize(myLists);
            return json;
            //var jsn = Json(spclList);
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

        public string DecodeServerName(string encodedServername)
        {
            return Encoding.UTF8.GetString(Convert.FromBase64String(encodedServername));
        }

       // public


        public class User
        {
            public string full_name { get; set; }
            public string email { get; set; }
            public string bup_id { get; set; }
            public string name { get; set; }
            public string user_type { get; set; }
        }

        public User GetUserInfoFromSSO(string tokentype, string accesstoken)
        {
        
            if (tokentype == null || accesstoken == null) return null;
            try
            {
                var userUrl = "https://webportal.bup.edu.bd" + "/connect/userinfo";
                var authorization = tokentype + " " + accesstoken;

                using (WebClient client = new WebClient())
                {
                    var values = new NameValueCollection();
                    client.Headers["Content-Type"] = "application/json";
                    client.Headers.Add("Authorization", authorization);

                    var response = client.DownloadString(userUrl);

                    var responseMessage = JsonConvert.DeserializeObject<User>(response);
                    if (responseMessage != null)
                    {
                        // SessionManager.SaveObjToSession<User>(responseMessage, Constants.COMMON_SSOUSER);
                        return responseMessage;
                    }
                    else
                    {
                        return null;
                    }
                   // return responseMessage;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                return null;
              //  ErrorRedirect("Error in SSO Get UserInfo : " + ex.Message + " > tokenType:" + tokentype + " > accessToken:" + accesstoken);
            }
          //  return null;
        }













    }
}

