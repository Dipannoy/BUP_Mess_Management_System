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
    //[Authorize(Roles = "Admin,MessAdmin")]

    public class StorageManagementController : Controller
    {

        private readonly ApplicationDbContext _context;
        private UserManager<ApplicationUser> _userManager;
        private RoleManager<UserIdentityRole> _roleManager;
        public static List<double> weightedPrice = new List<double>();
        public StorageManagementController(ApplicationDbContext context, UserManager<ApplicationUser> userManager, RoleManager<UserIdentityRole> roleManager)
        {
            _context = context;
            _userManager = userManager;
            _roleManager = roleManager;
        }


        // GET: StoreOut
        public ActionResult Index()
        {
            var result = _context.WarehouseStorage.Include(x => x.StoreInItem).ToList();
            return View(result);
        }

        // GET: StoreOut/Create
        public ActionResult Create()
        {
            return View();
        }

        public ActionResult UpdatedStorageManagement()
        {
            return View();
        }

        // POST: StoreOut/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(WarehouseStorage model)
        {
            try
            {

                if (ModelState.IsValid)
                {


                    string usrName = SessionExtensions.GetString(HttpContext.Session, "user");
                    var user = await _userManager.FindByNameAsync(usrName);
                  //  var user = await _userManager.GetUserAsync(User);
                    var userID = user.Id;



                    var result = _context.RemainingBalanceAndWeightedPriceCalculation.Where(x => x.StoreInItemId == model.StoreInItemId).FirstOrDefault();
                    if (result != null)
                    {
                        bool isstoreout = model.IsStoreOut;

                        if (isstoreout == true) //...Store Out. (-) the Amout from Remaining 
                        {
                            double availableamount = result.TotalAvailableAmount;
                            double amount = availableamount - model.Amount;

                            if (amount < 0)
                            {
                                ModelState.AddModelError("Amount", " Your amount is Exceeded " + " Remaining amount is : " + availableamount.ToString());
                                return View(model);
                            }
                            else
                            {
                                result.TotalAvailableAmount = amount;
                                result.WeightedPrice = 1;
                                result.StoreInItemId = model.StoreInItemId;

                                result.LastModifiedBy = userID;
                                result.LastModifiedDate = DateTime.Now;

                                _context.RemainingBalanceAndWeightedPriceCalculation.Update(result);
                                _context.SaveChanges();
                            }

                        }
                        else //...Store In. (+) the Amount with Remaining
                        {
                            double availableamount = result.TotalAvailableAmount;

                            result.TotalAvailableAmount = availableamount + model.Amount;
                            result.WeightedPrice = 1;
                            result.StoreInItemId = model.StoreInItemId;

                            result.LastModifiedBy = userID;
                            result.LastModifiedDate = DateTime.Now;

                            _context.RemainingBalanceAndWeightedPriceCalculation.Update(result);
                            _context.SaveChanges();
                        }

                    }
                    else
                    {
                        RemainingBalanceAndWeightedPriceCalculation rbwpc = new RemainingBalanceAndWeightedPriceCalculation
                        {
                            TotalAvailableAmount = model.Amount,
                            WeightedPrice = 1,
                            StoreInItemId = model.StoreInItemId,

                            CreatedBy = userID,
                            CreatedDate = DateTime.Now
                        };

                        _context.RemainingBalanceAndWeightedPriceCalculation.Add(rbwpc);
                        _context.SaveChanges();

                    }


                    model.CreatedBy = userID;
                    model.CreatedDate = DateTime.Now;

                    _context.WarehouseStorage.Add(model);
                    _context.SaveChanges();

                    return RedirectToAction(nameof(Index));
                }
                return View(model);
            }
            catch
            {
                return View();
            }
        }

        // GET: StoreOut/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        public ActionResult check()
        {
            //SaveUpdateWarehouseStorageIn();
            return RedirectToAction(nameof(WarehouseStorageIn));
        }

        // POST: StoreOut/Edit/5
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

        // GET: StoreOut/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: StoreOut/Delete/5
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



        [HttpGet]
        public JsonResult GetStoreItems()
        {
            #region Get All Store Item Catalog
            List<StoreInItem> storeItemList = _context.StoreInItem.ToList();
            #endregion

            List<SelectListItem> selectListItems = new List<SelectListItem>();
            foreach (var tempData in storeItemList)
            {
                SelectListItem selectListItem = new SelectListItem
                {
                    Text = tempData.Name,
                    Value = tempData.Id.ToString()
                };
                selectListItems.Add(selectListItem);
            }


            return Json(selectListItems);
        }



        // GET: StoreOut/Create
        public ActionResult SaveUpdateWareHouseStorage()
        {
            return View();
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddRawItem(string material)
        {
            string message = "";
            if (SessionExist())
            {
                if (await UserExistMess())
                {
                    if (String.IsNullOrEmpty(Request.Form["rawitm"]) || long.Parse(Request.Form["unit"]) == -1)
                    {
                        message = "Input field is not valid.";
                    }
                    else
                    {
                        var obj = _context.StoreInItem.Where(x => x.Name == Request.Form["rawitm"]).FirstOrDefault();
                        if (obj == null)
                        {

                            StoreInItem si = new StoreInItem();

                            string usrName = SessionExtensions.GetString(HttpContext.Session, "user");
                            var user = await _userManager.FindByNameAsync(usrName);
                            var userID = user.Id;
                            si.Name = Request.Form["rawitm"];
                            si.UnitTypeId = long.Parse(Request.Form["unit"]);
                            si.LastModifiedDate = DateTime.Now;

                            si.CreatedBy = userID;
                            si.CreatedDate = DateTime.Now;
                            si.IsOpen = true;
                            si.StoreInCategoryId = 2;
                            _context.StoreInItem.Add(si);
                            int addResult = _context.SaveChanges();
                            message = addResult > 0 ? "Successfully added" : "Addition failed";
                        }
                        else
                        {
                            message = "Item already exists";
                        }

                    }
                    ViewBag.FullName = await GetUserName();
                    ViewBag.StoreInMessage = message;
                    ViewBag.chkmat = material;
                    ViewBag.StoreId = 1;
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


                    return View("WarehouseStorageIn");
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
        //[Authorize]
        //public  string Token()
        //{
        //    string accessToken = await HttpContext.GetTokenAsync("access_token");
        //    return accessToken;
        //}
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ViewStoreOut()
        {
            if (SessionExist())
            {
                if (await UserExistMess())
                {
                    string d = Request.Form["stodate"];
                    string[] DateFormatarray = d.Split("-");
                    string datesv = DateFormatarray[1] + '/' + DateFormatarray[0] + '/' + DateFormatarray[2];
                    DateTime date = DateTime.ParseExact(datesv, "M/d/yyyy", CultureInfo.InvariantCulture);
                    ViewBag.StoDate = date;
                    ViewBag.StoreId = 2;
                    ViewBag.FullName = await GetUserName();
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

                    return View("WarehouseStorageIn");
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
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ViewStoreOut2()
        {
            if (SessionExist())
            {
                if (await UserExistMess())
                {
                    string d = Request.Form["stodate2"];
                    string[] DateFormatarray = d.Split("-");
                    string datesv = DateFormatarray[1] + '/' + DateFormatarray[0] + '/' + DateFormatarray[2];
                    DateTime date = DateTime.ParseExact(datesv, "M/d/yyyy", CultureInfo.InvariantCulture);
                    ViewBag.StoDate = date;
                    ViewBag.StoreId = 2;
                    ViewBag.FullName = await GetUserName();
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
                    return View("WarehouseStorageIn");
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
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddStoreOutItem()
        {
            var son = Request.Form["stroutitm2"].ToString();
           // son = son.Replace(" ", String.Empty); 
            var storeList = _context.StoreOutItem.Where(x => x.Id > 0).ToList();
            int exist = 0;

            if (SessionExist())
            {
                if (await UserExistMess())
                {

                    if (long.Parse(Request.Form["unit2"]) == -1 || long.Parse(Request.Form["category"]) == -1)
                    {
                        ViewBag.StoreId = 2;

                        ViewBag.AddStoreOutError = "Fill up all the fields.Your item has not been added.";
                        ViewBag.FullName = await GetUserName();
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

                        return View("WarehouseStorageIn");
                        //await WarehouseStorageNew();
                    }
                    //var accessToken = Request.Headers["Authorization"];



                    //var accessToken =   HttpContext.GetTokenAsync("access_token");
                    //var accessToken2 = HttpContext.Request.Headers["Authorization"];

                    //var scope = "openid BupUserProfile";
                    //var state = "OpenIdConnect.AuthenticationProperties=" + Guid.NewGuid().ToString("N");
                    //var nonce = Guid.NewGuid().ToString("N");

                    ////string baseUrl = "http://pokeapi.co/api/v2/pokemon/";
                    //// string baseUrl = "https://webportal.bup.edu.bd/connect/userinfo";
                    //string baseUrl = " https://webportal.bup.edu.bd/connect/authorize ? " + "client_id=" + "ims-in-bup" + "&redirect_uri=" + "http://ucamdemo.bup.edu.bd" +
                    //"&response_mode=form_post" + "&response_type=" + OpenIdConnectResponseType.CodeIdToken + "&scope=" + scope + "&state=" + state + "&nonce=" + nonce;


                    ////HttpResponse response = HttpContext.Current.Response;
                    ////response.Clear();

                    ////StringBuilder s = new StringBuilder();
                    ////s.Append("<html>");
                    ////s.AppendFormat("<body onload='document.forms[\"form\"].submit()'>");
                    ////s.AppendFormat("<form name='form' action='{0}' method='post'>", url);
                    ////foreach (string key in data)
                    ////{
                    ////    s.AppendFormat("<input type='hidden' name='{0}' value='{1}' />", key, data[key]);
                    ////}
                    ////s.Append("</form></body></html>");
                    ////response.Write(s.ToString());
                    ////response.End();
                    ////https://<ssobaseurl>/connect/userinfo
                    ////Have your using statements within a try/catch block
                    //try
                    //{
                    //    //We will now define your HttpClient with your first using statement which will use a IDisposable.
                    //    using (HttpClient client = new HttpClient())
                    //    {
                    //        //In the next using statement you will initiate the Get Request, use the await keyword so it will execute the using statement in order.
                    //        using (HttpResponseMessage res = await client.GetAsync(baseUrl))
                    //        {
                    //            //Then get the content from the response in the next using statement, then within it you will get the data, and convert it to a c# object.
                    //            using (HttpContent content = res.Content)
                    //            {
                    //                //Now assign your content to your data variable, by converting into a string using the await keyword.
                    //                var data = await content.ReadAsStringAsync();
                    //                //If the data isn't null return log convert the data using newtonsoft JObject Parse class method on the data.
                    //                if (data != null)
                    //                {
                    //                    //Now log your data in the console
                    //                    Console.WriteLine("data------------{0}", data);
                    //                }
                    //                else
                    //                {
                    //                    Console.WriteLine("NO Data----------");
                    //                }
                    //            }
                    //        }
                    //    }
                    //}
                    //catch (Exception exception)
                    //{
                    //    Console.WriteLine("Exception Hit------------");
                    //    Console.WriteLine(exception);
                    //}

                    foreach (var i in storeList)
                    {
                        var stnm = i.Name;
                        if (stnm.Equals(son))
                        {
                            exist = 1;
                            break;
                        }


                    }

                    //string userId = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier).Value;

                    // var StoreItemList = _context.StoreOutItem.Where(x => x.Name.Replace(" ",String.Empty) == son.Replace(" ", String.Empty)).ToList();
                    if (exist == 0)
                    {
                        StoreOutItem so = new StoreOutItem();
                        string usrName = SessionExtensions.GetString(HttpContext.Session, "user");
                        var user = await _userManager.FindByNameAsync(usrName);
                        var userID = user.Id;
                        so.Name = Request.Form["stroutitm2"];
                        so.UnitTypeId = long.Parse(Request.Form["unit2"]);
                        so.StoreOutCategoryId = Int32.Parse(Request.Form["category"]);
                        so.LastModifiedDate = DateTime.Now;

                        so.CreatedBy = userID;
                        so.CreatedDate = DateTime.Now;
                        so.IsOpen = true;
                        so.MinimumProductionUnit = 10;
                        so.Price = 10;
                        so.MinimumProductionUnitMultiplier = 5;
                        _context.StoreOutItem.Add(so);
                        _context.SaveChanges();
                        ViewBag.StoreId = 2;
                        ViewBag.AddStoreOutError = son + " has been added successfully.";

                        ViewBag.FullName = await GetUserName();
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
                        return View("WarehouseStorageIn");
                    }
                    else
                    {
                        ViewBag.StoreId = 2;

                        ViewBag.AddStoreOutError = son + " already exists";
                        ViewBag.FullName = await GetUserName();
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
                        return View("WarehouseStorageIn");

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



        public class TempWarehouseStorage
        {
            public string Date { get; set; }

            public string IsStoreOut { get; set; }

            public string StoreInItemId { get; set; }

            public string Amount { get; set; }
        }

        [HttpPost]
        public async Task<IActionResult> SaveUpdateWarehouseStorage(string StoreInList)
        {
            try
            {

                string usrName = SessionExtensions.GetString(HttpContext.Session, "user");
                var user = await _userManager.FindByNameAsync(usrName);
                var userID = user.Id;

                List<TempWarehouseStorage> tempStoreInList = (List<TempWarehouseStorage>)JsonConvert.DeserializeObject(StoreInList, typeof(List<TempWarehouseStorage>));

                //if (tempStoreInList.Count > 0)
                //{
                //    StoreIn si = new StoreIn();
                //    si.StoreInDate = Convert.ToDateTime(tempStoreInList[0].StoreInDate);
                //    si.ChallanNo = tempStoreInList[0].ChallanNo.ToString();
                //    si.StoreItemId = Convert.ToInt64(tempStoreInList[0].StoreItemId);
                //    si.UnitOrQuantity = Convert.ToDecimal(tempStoreInList[0].UnitOrQuantity);
                //    si.PerUnitOrQuantityPrice = Convert.ToDecimal(tempStoreInList[0].PerUnitOrQuantityPrice);
                //    si.TotalPrice = Convert.ToDecimal(tempStoreInList[0].TotalPrice);
                //    si.CreatedBy = userID;
                //    si.CreatedDate = DateTime.Now;

                //    _context.Add(si);
                //    _context.SaveChanges();
                //    long storeInId = si.Id;

                //    if (storeInId > 0)
                //    {
                //        return Json("True/False");
                //    }
                //    else
                //    {
                //        return Json("Error");
                //    }

                //}
                //else
                //{
                //    return Json("Error");
                //}

                return Json("True/False");


            }
            catch (Exception ex)
            {
                return Json("Error");
            }
        }






        // GET: StoreOut/Create
        public ActionResult WarehouseStorageIn()
        {
            return View();
        }
        public async Task<ActionResult> WarehouseStorageNew()
        {
            if(SessionExist())
            {
                if(await UserExistMess())
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

                    return View("WarehouseStorageIn");
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


        public async Task<List<NavigationMenu>> getMenuUserAsync()
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
            return filterMenuList;
            //ViewBag.FilterMenuList = filterMenuList;
        }

        [HttpPost]
        [AutoValidateAntiforgeryToken]
        public async Task<IActionResult> SaveUpdateWarehouseStorageIn(string itemname)

        {
            if (SessionExist())
            {
                if (await UserExistMess())
                {
                    try
                    {
                        var itemList = _context.StoreInItem.Where(x => x.Name.Contains(itemname)).ToList();
                        string d = Request.Form["datepicker"];
                        string[] DateFormatarray = d.Split("-");
                        string datesv = DateFormatarray[1] + '/' + DateFormatarray[0] + '/' + DateFormatarray[2];
                        string message = "";
                        string finalMessage = "";
                        DateTime date = DateTime.ParseExact(datesv, "M/d/yyyy", CultureInfo.InvariantCulture);

                        if (itemList.Count == 0)
                        {
                            itemList = _context.StoreInItem.ToList();

                        }


                        string usrName = SessionExtensions.GetString(HttpContext.Session, "user");
                        var user = await _userManager.FindByNameAsync(usrName);
                        var userID = user.Id;

                        bool isstoreout = false;

                        int count = 0;

                        try
                        {
                            if (itemList.Count > 0)
                            {
                                foreach (var i in itemList)
                                {
                                    Double amount = 0;
                                    Double totalPurchasePrice = 0;
                                    var storeInItem = _context.StoreInItem.Where(x => x.Id == i.Id).FirstOrDefault();


                                    var result = _context.RemainingBalanceAndWeightedPriceCalculation.Where(x => x.StoreInItemId == storeInItem.Id).LastOrDefault();
                                    //weightedPrice.Add(result.WeightedPrice);

                                    var tempResult = _context.RemainingBalanceAndWeightedPriceCalculation.Where(x => x.StoreInItemId == storeInItem.Id).LastOrDefault();

                                    Double.TryParse(Request.Form[i.Id.ToString()][0], out amount);
                                    Double.TryParse(Request.Form[i.Id.ToString() + "-tp"][0], out totalPurchasePrice);

                                    //double tp2 = totalPurchasePrice;

                                    if (amount > 0 && totalPurchasePrice > 0)
                                    {

                                        double lastWP = 1;
                                        double lastAmnt = 0;
                                        double checkWeightedPrice = 0;
                                        double checkWareHsStrPrice = 0;
                                        if (result != null)
                                        {
                                            lastAmnt = result.TotalAvailableAmount;
                                            lastWP = result.WeightedPrice;
                                        }
                                        else
                                        {
                                            lastAmnt = 0;
                                            lastWP = 0;
                                        }
                                        if (totalPurchasePrice == 0)
                                        {
                                            checkWeightedPrice = lastWP;

                                        }
                                        else
                                        {
                                            checkWeightedPrice = ((lastWP * lastAmnt) + Convert.ToDouble(totalPurchasePrice)) / (lastAmnt + Convert.ToDouble(amount));
                                            checkWareHsStrPrice = totalPurchasePrice;
                                        }


                                        RemainingBalanceAndWeightedPriceCalculation rbwpc = new RemainingBalanceAndWeightedPriceCalculation
                                        {
                                            TotalAvailableAmount = lastAmnt + Convert.ToDouble(amount),
                                            WeightedPrice = checkWeightedPrice,
                                            StoreInItemId = storeInItem.Id,
                                            Date = DateTime.Now,
                                            LastModifiedDate = DateTime.Now,

                                            CreatedBy = userID,
                                            CreatedDate = DateTime.Now
                                        };
                                        _context.RemainingBalanceAndWeightedPriceCalculation.Add(rbwpc);
                                        _context.SaveChanges();






                                        WarehouseStorage model = new WarehouseStorage
                                        {
                                            IsStoreOut = isstoreout,
                                            Amount = Convert.ToDouble(amount),
                                            TotalPurchasePrice = checkWareHsStrPrice,
                                            PurchaseRate = (checkWareHsStrPrice / Convert.ToDouble(amount)),
                                            Date = date,
                                            StoreInItemId = storeInItem.Id,
                                            LastModifiedDate = DateTime.Now,

                                            CreatedBy = userID,
                                            CreatedDate = DateTime.Now
                                        };
                                        _context.WarehouseStorage.Add(model);
                                        _context.SaveChanges();



                                        count = count + 1;

                                    }
                                    else if ((amount > 0 && totalPurchasePrice <= 0) || (amount <= 0 && totalPurchasePrice > 0))
                                    {
                                        message += storeInItem.Name + "  ";
                                    }
                                    else
                                    {

                                    }
                                }

                                if (count > 0)
                                {
                                    _context.SaveChanges();
                                }
                                if (message == "")
                                {
                                    finalMessage = "All items have been successfully updated";
                                }
                                else
                                {
                                    finalMessage = "Problem is found for " + message + "due to invalid input field. Please check input field.";
                                }

                                ViewBag.chkmat = itemname;
                                ViewBag.FullName = await GetUserName();
                                ViewBag.StoreInMessage = finalMessage;
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

                                return View("WarehouseStorageIn");
                                //return RedirectToAction("WarehouseStorageIn");





                            }
                        }
                        catch (Exception ex)
                        {

                            ViewBag.chkmat = itemname;
                            ViewBag.FullName = await GetUserName();
                            ViewBag.StoreInMessage = ex.Message;
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

                            return View("WarehouseStorageIn");
                        }


                    }
                    catch (Exception ex)
                    {
                        ViewBag.chkmat = itemname;
                        ViewBag.FullName = await GetUserName();
                        ViewBag.StoreInMessage = ex.Message;
                        var role3 = await GetLogInUserRoleObjectAsync();
                        var roleMenuList3 = _context.RoleMenu.Where(x => x.UserIdentityRoleId == role3.Id).ToList();
                        var nevMenuList3 = _context.NavigationMenu.Where(x => x.Id > 0).ToList();
                        var pr3 = from r in roleMenuList3
                                 join n in nevMenuList3
                                 on r.NavigationMenuId equals n.Id
                                 // where o.LastModifiedDate.ToShortDateString() == OD.AddDays(-1).ToShortDateString() && o.MealTypeId == 1
                                 select n;
                        var filterMenuList3 = pr3.ToList();

                        ViewBag.FilterMenuList = filterMenuList3;


                        return View("WarehouseStorageIn");
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

            ViewBag.chkmat = itemname;
            ViewBag.FullName = await GetUserName();
            var role4 = await GetLogInUserRoleObjectAsync();
            var roleMenuList4 = _context.RoleMenu.Where(x => x.UserIdentityRoleId == role4.Id).ToList();
            var nevMenuList4 = _context.NavigationMenu.Where(x => x.Id > 0).ToList();
            var pr4 = from r in roleMenuList4
                     join n in nevMenuList4
                     on r.NavigationMenuId equals n.Id
                     // where o.LastModifiedDate.ToShortDateString() == OD.AddDays(-1).ToShortDateString() && o.MealTypeId == 1
                     select n;
            var filterMenuList4 = pr4.ToList();

            ViewBag.FilterMenuList = filterMenuList4;

            return View("WarehouseStorageIn");
        }







        // GET: StoreOut/Create
        public ActionResult WarehouseStorageOut()
        {
            return View();
        }
        [HttpPost]
        [AutoValidateAntiforgeryToken]
        public async Task<IActionResult> DateFetch()
        {
            var date = DateTime.Parse(Request.Form["date2"][0]);
            ViewBag.Date = date;
            return View("WarehouseStorageOut");


        }



        [HttpPost]
        [AutoValidateAntiforgeryToken]
        public async Task<IActionResult> UpdtSTOPrice(string stomaterial)
        {
            string message = "";

            if (SessionExist())
            {
                if (await UserExistMess())
                {
                    var storeoutItemList = new List<StoreOutItem>();
                    try
                    {
                        if (stomaterial == null)
                        {
                            storeoutItemList = _context.StoreOutItem.Where(x => x.IsOpen == true).ToList();
                        }
                        else
                        {
                            storeoutItemList = _context.StoreOutItem.Where(x => x.Name.Contains(stomaterial)).ToList();
                        }
                        // var 
                        foreach (var item in storeoutItemList)
                        {
                            double price = 0;
                            if (Request.Form[item.Id.ToString()][0] != null)
                            {
                                Double.TryParse(Request.Form[item.Id.ToString()][0], out price);
                                item.Price = price;
                                _context.StoreOutItem.Update(item);
                            }
                            //else
                            //{
                            //    message +=  
                            //}
                            _context.SaveChanges();



                        }
                    }
                    catch (Exception ex)
                    {
                        message = ex.Message.ToString();
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
            if (message == "")
            {
                ViewBag.STOUpdateMsg = "Store Out Item price has been updated successfully";
            }
            else
            {
                ViewBag.STOUpdateMsg = "Store Out Item price update is not successful" + message;
            }

            ViewBag.StoreId = 2;
            ViewBag.chkstomat = stomaterial;
            ViewBag.viewSTOUpdate = 1;
            ViewBag.FullName = await GetUserName();
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


            return View("WarehouseStorageIn");
        }



        [HttpPost]
        [AutoValidateAntiforgeryToken]
        public async Task<IActionResult> SaveUpdateWarehouseStorageOut(string submit,string StoreOutDate,double avgPrice,long itemId,double quantity,double chkquantity, string rawmat)
        {
            var date = DateTime.Parse(StoreOutDate);
            var errorList = new Dictionary<string, string>();
            var userGivenAmount = new Dictionary<string, string>();

            var StoreoutReceipe = _context.StoreOutItemRecipe.Where(x => x.StoreOutItemId == itemId).ToList();
            double UpdtQnt = quantity;
            if(chkquantity != 0)
            {
                UpdtQnt = chkquantity;

            }
            if (SessionExist())
            {
                if (await UserExistMess())
                {
                    if (UpdtQnt == 0 && _context.StoreOutItem.Where(x=>x.Id == itemId).FirstOrDefault().Name != "Waste Material" )
                    {

                        ViewBag.ErrorMsg = "No order is found for" + _context.StoreOutItem.Where(x => x.Id == itemId).FirstOrDefault().Name;
                        ViewBag.StoreId = 2;
                        ViewBag.chkmat2 = rawmat;
                        ViewBag.FullName = await GetUserName();
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


                        return View("WarehouseStorageIn");
                    }
                    // List<StoreInItem> itemList = new List<StoreInItem>();
                    var itemList = _context.StoreInItem.Where(x => x.Name.Contains(rawmat)).Include(a => a.RemainingBalanceAndWeightedPriceCalculationList).ToList();

                    double UpdatedStoreOutItemPrice = 0;
                    double chkPrc = 0;
                    if (itemList.Count == 0)
                    {
                        itemList = _context.StoreInItem.Include(a => a.RemainingBalanceAndWeightedPriceCalculationList).ToList();

                    }
                    //if (StoreoutReceipe.Count > 0)
                    //{
                    //    foreach (var i in StoreoutReceipe)
                    //    {
                    //        itemList.Add(_context.StoreInItem.Where(x => x.Id == i.StoreInItemId).Include(a => a.RemainingBalanceAndWeightedPriceCalculationList).FirstOrDefault());
                    //    }
                    //}
                    //else
                    //{
                    //     itemList = _context.StoreInItem.Include(a=>a.RemainingBalanceAndWeightedPriceCalculationList).ToList();

                    //}
                    string usrName = SessionExtensions.GetString(HttpContext.Session, "user");
                    var user = await _userManager.FindByNameAsync(usrName);
                    //var user = await _userManager.GetUserAsync(User);
                    var userID = user.Id;
                    bool isstoreout = true;
                    var tempPriceList = new Dictionary<string, double>();
                    var tempQuantityList = new Dictionary<string, double>();

                    var storeOutSuccessMsg = new Dictionary<string, string>();
                    var RemBalInputList = new List<RemainingBalanceAndWeightedPriceCalculation>();
                    var RemBalUpdateList = new List<RemainingBalanceAndWeightedPriceCalculation>();
                    var ListofPriceOfTheDay = new List<RemainingBalanceAndWeightedPriceCalculation>();
                    var StorageItemInputList = new List<WarehouseStorage>();

                    try
                    {
                        if (itemList.Count > 0)
                        {
                            foreach (var i in itemList)
                            {
                                Double amount = 0;
                                if (Request.Form[i.Id.ToString()][0] != null)
                                {
                                    Double.TryParse(Request.Form[i.Id.ToString()][0], out amount);
                                    if (amount > 0) userGivenAmount.Add(i.Id.ToString(), amount.ToString());
                                    if (amount > 0 && errorList.Count() == 0)
                                    {
                                        if (i.RemainingBalanceAndWeightedPriceCalculationList.LastOrDefault() != null)
                                        {
                                            double availableamount = i.RemainingBalanceAndWeightedPriceCalculationList.LastOrDefault().TotalAvailableAmount;
                                            double amountT = availableamount - Convert.ToDouble(amount);
                                            if (amountT < 0)
                                            {
                                                errorList.Add(i.Id.ToString(), " Your amount is Exceeded " + "Remaining amount is : " + availableamount.ToString());

                                            }
                                            else
                                            {
                                                var otu = i.RemainingBalanceAndWeightedPriceCalculationList.LastOrDefault();
                                                var temp = i.RemainingBalanceAndWeightedPriceCalculationList.LastOrDefault();
                                                tempPriceList.Add(i.Id.ToString(), temp.WeightedPrice);
                                                tempQuantityList.Add(i.Id.ToString(), amount);
                                                UpdatedStoreOutItemPrice += amount * temp.WeightedPrice;

                                                if (submit == "StoreOut")
                                                {

                                                    otu.TotalAvailableAmount = Convert.ToDouble(amountT);
                                                    //otu.WeightedPrice = 1;
                                                    otu.LastModifiedBy = userID;
                                                    otu.LastModifiedDate = DateTime.Now;

                                                    RemBalUpdateList.Add(otu);

                                                    StorageItemInputList.Add(new WarehouseStorage
                                                    {
                                                        IsStoreOut = isstoreout,
                                                        Amount = Convert.ToDouble(amount),
                                                        Date = date,
                                                        StoreOutItemId = itemId,
                                                        StoreInItemId = i.Id,
                                                        CreatedBy = userID,
                                                        CreatedDate = DateTime.Now,
                                                        LastModifiedDate = DateTime.Now
                                                    }) ;
                                                }
                                            }
                                        }
                                        else
                                        {
                                            errorList.Add(i.Id.ToString(), "No Remaining Balance Found");
                                            if (submit == "StoreOut")
                                            {
                                                RemBalInputList.Add(new RemainingBalanceAndWeightedPriceCalculation
                                                {
                                                    CreatedBy = userID,
                                                    CreatedDate = DateTime.Now,
                                                    StoreInItemId = i.Id,
                                                    WeightedPrice = 0,
                                                    TotalAvailableAmount = 0,
                                                    LastModifiedDate = DateTime.Now

                                                });
                                            }
                                        }

                                    }
                                }
                            }

                            chkPrc = UpdatedStoreOutItemPrice;
                            if (errorList.Count() == 0)
                            {
                                ViewBag.PriceListForStorageOut = ListofPriceOfTheDay;

                                if (submit == "StoreOut")
                                {
                                    //StoreOutItem storeOutItem = _context.StoreOutItem.Where(x => x.Id == itemId).FirstOrDefault();
                                    //double StOPrc = chkPrc / UpdtQnt;
                                    //decimal stop = Math.Round(Convert.ToDecimal(StOPrc), 2);

                                    //if (storeOutItem.Price != Convert.ToDouble(stop))
                                    //{
                                    //    storeOutItem.Price = Convert.ToDouble(stop);
                                    //    _context.StoreOutItem.Update(storeOutItem);

                                    //}
                                    RemBalInputList.ForEach(a => _context.RemainingBalanceAndWeightedPriceCalculation.Add(a));
                                    StorageItemInputList.ForEach(a => _context.WarehouseStorage.Add(a));
                                    RemBalUpdateList.ForEach(a => _context.RemainingBalanceAndWeightedPriceCalculation.Update(a));
                                    _context.SaveChanges();
                                }
                                ViewBag.StoreOutSuccessMsg = storeOutSuccessMsg;
                                ViewBag.TempPriceList = tempPriceList;
                                ViewBag.TempQuantityList = tempQuantityList;
                                ViewBag.ItemId = itemId;
                                ViewBag.quantity = quantity;
                                ViewBag.chkquantity = chkquantity;
                                ViewBag.chkmat2 = rawmat;
                                ViewBag.FullName = await GetUserName();

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


                                ViewBag.StoreId = 2;

                                return View("WarehouseStorageIn");
                            }

                        }
                    }
                    catch (Exception ex)
                    {
                        errorList.Add("msg", ex.Message);
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

            ViewBag.ErrorList = errorList;
            ViewBag.UserGivenAmount = userGivenAmount;
            ViewBag.StoreId = 2;
            ViewBag.chkmat2 = rawmat;
            ViewBag.FullName = await GetUserName();
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


            return View("WarehouseStorageIn");
        }

        public async Task<ActionResult> Test()
        {
            var chkmat = Request.Form["material"].ToString();
            ViewBag.chkmat = chkmat;
            ViewBag.StoreId = 1;

            ViewBag.FullName = await GetUserName();
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

            return View("WarehouseStorageIn");

        }
        public async Task<ActionResult> Test2(string item, string ordDate, string quantity)
        {
            var chkmat = Request.Form["material2"].ToString();
            ViewBag.chkmat2 = chkmat;
            ViewBag.StoreId = 2;
            ViewBag.ItemId = long.Parse(item);
           // ViewBag.OrderDate = ordDate;

            ViewBag.quantity = double.Parse(quantity);

            ViewBag.FullName = await GetUserName();
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

            return View("WarehouseStorageIn");

        }
        public async Task<ActionResult> GetSearchedStoreOut()
        {
            if (SessionExist())
            {
                if (await UserExistMess())
                {
                    var chkstomat = Request.Form["stomaterial"].ToString();
                    ViewBag.chkstomat = chkstomat;
                    ViewBag.StoreId = 2;
                    ViewBag.viewSTOUpdate = 1;
                    ViewBag.FullName = await GetUserName();
                    ViewBag.FilterMenuList = getMenuUserAsync();
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
                    return View("WarehouseStorageIn");
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

        public JsonResult GetItemsOnDay(string Date)
        {
            //var PersonList = _userManager.Users.Where(x => x.BUPFullName.Contains(search)).ToList();

            string[] DateFormatarray = Date.Split("-");
            string datesv = DateFormatarray[1] + '/' + DateFormatarray[0] + '/' + DateFormatarray[2];
            DateTime date = DateTime.ParseExact(datesv, "M/d/yyyy", CultureInfo.InvariantCulture);
            int day = (int)date.DayOfWeek + 1;
            var MenuItemsOnDay = _context.MenuItem.Where(x => x.Day == day).ToList();
            var ItemOrderHistoryOnDay = _context.OrderHistory.Where(x => x.StoreOutItemId != null && x.OrderDate.ToShortDateString() == date.ToShortDateString()).Select(x => x.StoreOutItemId).Distinct().ToList();
            var list = new Dictionary<string, string>();
            var map = new Dictionary<long?, string>();
            var setmenuItemmap = new Dictionary<long?, double>();
            var storeOutItemDict = new Dictionary<long?, string>();
            foreach (var item in MenuItemsOnDay)
            {
                var ext = _context.ExtraItem.Where(x => x.Id == item.ExtraItemId).FirstOrDefault();
                var strName = _context.StoreOutItem.Where(x => x.Id == ext.StoreOutItemId).FirstOrDefault();
                var mealType = _context.MealType.Where(x => x.Id == ext.MealTypeId).FirstOrDefault();
                list.Add(item.ExtraItemId.ToString(), strName.Name + " [" + mealType.Name + "]");

                //if (!storeOutItemDict.ContainsKey(strName.Id))
                //{
                //    storeOutItemDict.Add(strName.Id, strName.Name);


                //}
            }

            //foreach (var item in ItemOrderHistoryOnDay)
            //{
            //    var storeOutitm = _context.StoreOutItem.Where(x => x.Id == item).FirstOrDefault();
            //    map.Add(storeOutitm.Id, storeOutitm.Name);
            //    list.Add(storeOutitm.Id.ToString(), storeOutitm.Name);
            //}

            //var SetMenuList = _context.PreOrderSchedule.Where(x =>  x.LastModifiedDate.ToShortDateString() == date.AddDays(-1).ToShortDateString()).ToList();
            //foreach (var sml in SetMenuList)
            //{
            //    var setmenudetail = _context.SetMenuDetails.Where(x => x.SetMenuId == sml.SetMenuId).ToList();
            //    foreach(var smd in setmenudetail)
            //    {
            //        var storeOutitm = _context.StoreOutItem.Where(x => x.Id == smd.StoreOutItemId).FirstOrDefault();

            //        if (!map.ContainsKey(smd.StoreOutItemId))
            //        {
            //            map.Add(storeOutitm.Id, storeOutitm.Name);
            //            list.Add(storeOutitm.Id.ToString(), storeOutitm.Name);
            //        }
            //        else
            //        {
                        
            //        }
            //    }
            //}
            return Json(list);
        }

        public JsonResult EditStoreIn(string SInId)
        {
            var strinItm = _context.StoreInItem.Where(x => x.Id == long.Parse(SInId)).FirstOrDefault();
            
            var list = new Dictionary<string, string>();
            list.Add(SInId, strinItm.Name);
            var RmBalStrIn = _context.RemainingBalanceAndWeightedPriceCalculation.Where(x => x.StoreInItemId == long.Parse(SInId)).LastOrDefault();
            list.Add(RmBalStrIn.Id.ToString(), RmBalStrIn.TotalAvailableAmount.ToString());
            list.Add("-1", RmBalStrIn.WeightedPrice.ToString());

            return Json(list);
        }

        public async Task<ActionResult> EditStrIn(int id, string material)
        {
            //StoreOutItemRecipe storeOutItemRecipe = _context.StoreOutItemRecipe.Include(x => x.StoreOutItem).Include(x => x.StoreInItem).Where(x => x.Id == id).FirstOrDefault();

            //if (storeOutItemRecipe == null)
            //{
            //    return NotFound();
            //}
            if (SessionExist())
            {
                if (await UserExistMess())
                {
                    ViewBag.EditableIdStrIn = id;
                    ViewBag.StoreId = 1;
                    ViewBag.chkmat = material;

                    ViewBag.FullName = await GetUserName();
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

                    return View("WarehouseStorageIn");
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
        public async Task<ActionResult> EditStrOutName(int id, string material)
        {
            if (SessionExist())
            {
                if (await UserExistMess())
                {
                    ViewBag.EditableIdStrOut = id;
                    ViewBag.StoreId = 2;
                    ViewBag.chkstomat = material;
                    //ViewBag.stomat = material;
                    ViewBag.viewSTOUpdate = 1;

                    ViewBag.FullName = await GetUserName();
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

                    return View("WarehouseStorageIn");
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
        [AutoValidateAntiforgeryToken]
        public async Task<IActionResult> SaveStoreOutName(int id, string material)
        {
            if (SessionExist())
            {
                if (await UserExistMess())
                {
                    var name = Request.Form["stoNameEd"][0];

                    StoreOutItem soi = _context.StoreOutItem.Where(x => x.Id == id).FirstOrDefault();
                    soi.Name = name;
                    _context.StoreOutItem.Update(soi);
                    _context.SaveChanges();
                    ViewBag.StoreId = 2;
                    ViewBag.chkstomat = material;

                    ViewBag.viewSTOUpdate = 1;

                    ViewBag.STOUpdateMsg = "Name has been updated successfully";
                    ViewBag.FullName = await GetUserName();
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

                    return View("WarehouseStorageIn");
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
        public async Task<ActionResult> ClearFilter()
        {
            if (SessionExist())
            {
                if (await UserExistMess())
                {
                    ViewBag.StoreId = 1;
                    ViewBag.chkmat = "";
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

            return View("WarehouseStorageIn");
        }
        public async Task<ActionResult> ClearFilter2(string item, string quantity)
        {
            if (SessionExist())
            {
                if (await UserExistMess())
                {

                    ViewBag.StoreId = 2;
                    ViewBag.chkmat2 = "";
                    ViewBag.ItemId = long.Parse(item);
                    ViewBag.quantity = double.Parse(quantity);
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

            return View("WarehouseStorageIn");
        }
        public async Task<ActionResult> ClearSTOFilter()
        {
            if (SessionExist())
            {
                if (await UserExistMess())
                {
                    ViewBag.chkstomat = "";

                    ViewBag.StoreId = 2;
                    ViewBag.viewSTOUpdate = 1;
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


            return View("WarehouseStorageIn");
        }

        [HttpPost]
        [AutoValidateAntiforgeryToken]
        public async Task<ActionResult> DeleteStrIn(int id, string material)
        {
            //StoreOutItemRecipe storeOutItemRecipe = _context.StoreOutItemRecipe.Include(x => x.StoreOutItem).Include(x => x.StoreInItem).Where(x => x.Id == id).FirstOrDefault();
            if (SessionExist())
            {
                if (await UserExistMess())
                {
                    StoreInItem sin = _context.StoreInItem.Where(x => x.Id == id).FirstOrDefault();

                    var RecipeList = _context.StoreOutItemRecipe.Where(x => x.StoreInItemId == id).ToList();
                    var WareHouseList = _context.WarehouseStorage.Where(x => x.StoreInItemId == id).ToList();
                    var RemBalList = _context.RemainingBalanceAndWeightedPriceCalculation.Where(x => x.StoreInItemId == id).ToList();
                    foreach (var item in RecipeList)
                    {
                        _context.StoreOutItemRecipe.Remove(item);
                    }
                    foreach (var item in WareHouseList)
                    {
                        _context.WarehouseStorage.Remove(item);
                    }
                    foreach (var item in RemBalList)
                    {
                        _context.RemainingBalanceAndWeightedPriceCalculation.Remove(item);
                    }

                    _context.StoreInItem.Remove(sin);
                    _context.SaveChanges();

                    //if (storeOutItemRecipe == null)
                    //{
                    //    return NotFound();
                    //}
                    ViewBag.StoreId = 1;
                    ViewBag.chkmat = material;
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



            return View("WarehouseStorageIn");
        }

        [HttpPost]
        [AutoValidateAntiforgeryToken]
        public async Task<ActionResult> DeleteStrOut(int id, string stomat)
        {
            //StoreOutItemRecipe storeOutItemRecipe = _context.StoreOutItemRecipe.Include(x => x.StoreOutItem).Include(x => x.StoreInItem).Where(x => x.Id == id).FirstOrDefault();

            if (SessionExist())
            {
                if (await UserExistMess())
                {
                    StoreOutItem so = _context.StoreOutItem.Where(x => x.Id == id).FirstOrDefault();

                    so.IsOpen = false;


                    _context.StoreOutItem.Update(so);

                    _context.SaveChanges();

                    //if (storeOutItemRecipe == null)
                    //{
                    //    return NotFound();
                    //}
                    ViewBag.StoreId = 2;
                    ViewBag.chkstomat = stomat;
                    ViewBag.viewSTOUpdate = 1;
                    ViewBag.STOUpdateMsg = "Menu item has been removed successfully";
                    ViewBag.FullName = await GetUserName();
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

            return View("WarehouseStorageIn");
        }

        [HttpPost]
        [AutoValidateAntiforgeryToken]
        public async Task<IActionResult> SaveEditedStrIn(string strId, string material)
        {
            string message = "";
            if (SessionExist())
            {
                if (await UserExistMess())
                {
                    try
                    {
                        string usrName = SessionExtensions.GetString(HttpContext.Session, "user");
                        var user = await _userManager.FindByNameAsync(usrName);
                        var userID = user.Id;
                        string Name = Request.Form["eri"];
                        string avlAmount = Request.Form["eriavl"];
                        string wPrice = Request.Form["eriwp"];
                        if (String.IsNullOrEmpty(Name) || String.IsNullOrEmpty(avlAmount) || String.IsNullOrEmpty(wPrice))
                        {
                            message = "Null field is not accepted";
                        }
                        //else if (double.Parse(avlAmount) < 0 || double.Parse(wPrice) < 0)
                        //{
                        //    message = ""
                        //}
                        else
                        {
                            var UnitTypeId = long.Parse(Request.Form["eriunit"]);
                            var am = double.Parse(avlAmount);
                            var wp = double.Parse(wPrice);

                            var LastModifiedDate = DateTime.Now;

                            StoreInItem sin = _context.StoreInItem.Where(x => x.Id == Int32.Parse(strId)).FirstOrDefault();
                            sin.Name = Name;
                            sin.UnitTypeId = UnitTypeId;
                            _context.Update(sin);

                            RemainingBalanceAndWeightedPriceCalculation rmstrin = _context.RemainingBalanceAndWeightedPriceCalculation.Where(x => x.StoreInItemId == Int32.Parse(strId)).LastOrDefault();
                            if (rmstrin == null)
                            {
                                RemainingBalanceAndWeightedPriceCalculation rbwpc = new RemainingBalanceAndWeightedPriceCalculation
                                {
                                    TotalAvailableAmount = am,
                                    WeightedPrice = wp,
                                    StoreInItemId = Int32.Parse(strId),

                                    CreatedBy = userID,
                                    CreatedDate = DateTime.Now
                                };
                                _context.RemainingBalanceAndWeightedPriceCalculation.Add(rbwpc);
                                _context.SaveChanges();
                            }
                            else
                            {
                                rmstrin.TotalAvailableAmount = am;
                                rmstrin.WeightedPrice = wp;
                                _context.Update(rmstrin);


                                int updtResult = _context.SaveChanges();
                                if (updtResult > 0)
                                {
                                    message = "Successfully Updated";
                                }
                                else
                                {
                                    message = "Update failed";
                                }
                            }

                        }

                    }
                    catch (Exception ex)
                    {
                        message = ex.Message;
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
            ViewBag.FullName = await GetUserName();
            ViewBag.chkmat = material;
            ViewBag.StoreInMessage = message;
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

            return View("WarehouseStorageIn");

        }

        public JsonResult GetItemQuantityOnDay(string Date,string ItemId)
        {
            long itemid = long.Parse(ItemId);
            double quantity = 0;
            var itmqnt = _context.OrderHistory.Where(x => x.StoreOutItemId != null && x.StoreOutItemId ==itemid && x.OrderDate.ToShortDateString() == DateTime.Parse(Date).ToShortDateString()).Select(x => x.UnitOrdered).Sum();
            quantity += itmqnt;
            var list = new Dictionary<string, string>();
            var map = new Dictionary<long?, string>();
            var setmenuItemmap = new Dictionary<long?, double>();
            var SetMenuList = _context.PreOrderSchedule.Where(x => x.LastModifiedDate.ToShortDateString() == DateTime.Parse(Date).AddDays(-1).ToShortDateString()).ToList();
            foreach (var sml in SetMenuList)
            {
                var setmenudetail = _context.SetMenuDetails.Where(x => x.SetMenuId == sml.SetMenuId).ToList();
                foreach (var smd in setmenudetail)
                {
                    if(smd.StoreOutItemId == itemid)
                    {
                        quantity += _context.OrderHistory.Where(x => x.SetMenuId == smd.SetMenuId).Select(x => x.UnitOrdered).Sum();
                    }
                    var storeOutitm = _context.StoreOutItem.Where(x => x.Id == smd.StoreOutItemId).FirstOrDefault();

                   
                }
            }
            return Json(quantity);
        }
        public async Task<ActionResult> GetRawMaterial(string datepicker2, string Item)
        {
            if (SessionExist())
            {
                if (await UserExistMess())
                {
                    try
                    {
                        int index = datepicker2.IndexOf("-");
                        string[] DateFormatarray = index > -1 ? datepicker2.Split("-") : datepicker2.Split("/");
                        string datesv = DateFormatarray[1] + '/' + DateFormatarray[0] + '/' + DateFormatarray[2];
                        DateTime orderdate = DateTime.ParseExact(datesv, "M/d/yyyy", CultureInfo.InvariantCulture);

                        if (Item == "-1")
                        {
                            ViewBag.OrderDate = orderdate;

                            //ViewBag.quantity = quantity;
                            ViewBag.StoreId = 2;
                            ViewBag.ErrorMsg = "Empty field is not allowed";
                            ViewBag.FullName = await GetUserName();
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

                            return View("WarehouseStorageIn");
                        }
                        else if (orderdate > DateTime.Now.Date || orderdate < DateTime.Now.Date)
                        {
                            ViewBag.OrderDate = orderdate;

                            //ViewBag.quantity = quantity;
                            ViewBag.StoreId = 2;
                            ViewBag.ErrorMsg = "Only today's date is allowed";
                            ViewBag.FullName = await GetUserName();
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

                            return View("WarehouseStorageIn");
                        }
                        else
                        {
                            var storeOutObj = _context.StoreOutItem.Where(x => x.Id == long.Parse(Item)).FirstOrDefault();
                            long strId = 0;
                            if(storeOutObj.Name == "Waste Material")
                            {
                                strId = storeOutObj.Id;
                            }
                            else
                            {
                                 strId = _context.ExtraItem.Where(x => x.Id == long.Parse(Item)).FirstOrDefault().StoreOutItemId;

                            }
                            var RawMaterialForReciepe = _context.StoreOutItemRecipe.Where(x => x.StoreOutItemId == strId).ToList();
                            long itemid = long.Parse(Item);
                            double quantity = 0;
                            //var itmqnt = _context.OrderHistory.Where(x => x.StoreOutItemId != null && x.StoreOutItemId == itemid && x.OrderDate.ToShortDateString() == orderdate.ToShortDateString()).Select(x => x.UnitOrdered).Sum();
                            var itmqnt = _context.CustomerChoiceV2.Where(x => x.ExtraItemId == long.Parse(Item) && x.Date.ToShortDateString() == orderdate.ToShortDateString()).Select(x => x.quantity).Sum();

                            quantity += itmqnt;
                            var list = new Dictionary<string, string>();
                            var map = new Dictionary<long?, string>();
                            var setmenuItemmap = new Dictionary<long?, double>();
                            //var SetMenuList = _context.PreOrderSchedule.Where(x => x.LastModifiedDate.ToShortDateString() == orderdate.AddDays(-1).ToShortDateString()).ToList();
                            //foreach (var sml in SetMenuList)
                            //{
                            //    var setmenudetail = _context.SetMenuDetails.Where(x => x.SetMenuId == sml.SetMenuId).ToList();
                            //    foreach (var smd in setmenudetail)
                            //    {
                            //        if (smd.StoreOutItemId == itemid)
                            //        {
                            //            quantity += _context.OrderHistory.Where(x => x.SetMenuId == smd.SetMenuId).Select(x => x.UnitOrdered).Sum();
                            //        }
                            //        var storeOutitm = _context.StoreOutItem.Where(x => x.Id == smd.StoreOutItemId).FirstOrDefault();


                            //    }
                            //}
                            var StoreoutReceipe = _context.StoreOutItemRecipe.Where(x => x.StoreOutItemId == strId).ToList();
                            List<StoreInItem> itemList = new List<StoreInItem>();
                            if (StoreoutReceipe.Count > 0)
                            {
                                foreach (var i in StoreoutReceipe)
                                {
                                    itemList.Add(_context.StoreInItem.Where(x => x.Id == i.StoreInItemId).Include(a => a.RemainingBalanceAndWeightedPriceCalculationList).FirstOrDefault());
                                }
                            }
                            else
                            {
                                itemList = _context.StoreInItem.Include(a => a.RemainingBalanceAndWeightedPriceCalculationList).ToList();

                            }
                            var errorList = new Dictionary<string, string>();
                            var tempPriceList = new Dictionary<string, double>();
                            var tempQuantityList = new Dictionary<string, double>();
                            try
                            {
                                if (itemList.Count > 0)
                                {
                                    foreach (var i in itemList)
                                    {
                                        Double amount = 0;
                                        if (StoreoutReceipe.Count == 0)
                                        {
                                            amount = 0;
                                        }
                                        else
                                        {
                                            var str = _context.StoreOutItemRecipe.Where(x => x.StoreOutItemId == strId && x.StoreInItemId == i.Id).FirstOrDefault();
                                            amount = str.RequiredStoreInUnit;
                                        }

                                        if (amount > 0 && errorList.Count() == 0)
                                        {
                                            if (i.RemainingBalanceAndWeightedPriceCalculationList.LastOrDefault() != null)
                                            {
                                                double availableamount = i.RemainingBalanceAndWeightedPriceCalculationList.LastOrDefault().TotalAvailableAmount;
                                                double amountT = availableamount - Convert.ToDouble(amount);
                                                if (amountT < 0)
                                                {
                                                    errorList.Add(i.Id.ToString(), " Your amount is Exceeded " + "Remaining amount is : " + availableamount.ToString());

                                                }
                                                else
                                                {
                                                    var otu = i.RemainingBalanceAndWeightedPriceCalculationList.LastOrDefault();
                                                    var temp = i.RemainingBalanceAndWeightedPriceCalculationList.LastOrDefault();
                                                    double minProduction = _context.StoreOutItem.Where(x => x.Id == strId).FirstOrDefault().MinimumProductionUnit;
                                                    double minRawUnit = amount;
                                                    double suggestedUnit = (minRawUnit * quantity) / minProduction;
                                                    tempPriceList.Add(i.Id.ToString(), temp.WeightedPrice);
                                                    tempQuantityList.Add(i.Id.ToString(), suggestedUnit);


                                                }
                                            }
                                            else
                                            {
                                                errorList.Add(i.Id.ToString(), "No Remaining Balance Found");

                                            }

                                        }

                                    }

                                    if (errorList.Count() == 0)
                                    {


                                        ViewBag.TempPriceList = tempPriceList;
                                        ViewBag.TempQuantityList = tempQuantityList;
                                        ViewBag.ItemId = strId;
                                        ViewBag.OrderDate = orderdate;

                                        ViewBag.quantity = quantity;
                                        ViewBag.StoreId = 2;
                                        ViewBag.FullName = await GetUserName();

                                        //ViewBag.chkquantity = chkquantity;


                                        var role3 = await GetLogInUserRoleObjectAsync();
                                        var roleMenuList3 = _context.RoleMenu.Where(x => x.UserIdentityRoleId == role3.Id).ToList();
                                        var nevMenuList3 = _context.NavigationMenu.Where(x => x.Id > 0).ToList();
                                        var pr3 = from r in roleMenuList3
                                                 join n in nevMenuList3
                                                 on r.NavigationMenuId equals n.Id
                                                 // where o.LastModifiedDate.ToShortDateString() == OD.AddDays(-1).ToShortDateString() && o.MealTypeId == 1
                                                 select n;
                                        var filterMenuList3 = pr3.ToList();

                                        ViewBag.FilterMenuList = filterMenuList3;


                                        //return View("WarehouseStorageOut");
                                        return View("WarehouseStorageIn");

                                    }

                                    else
                                    {
                                        ViewBag.ItemId = strId;
                                        ViewBag.OrderDate = orderdate;

                                        ViewBag.quantity = quantity;
                                        ViewBag.StoreId = 2;
                                        ViewBag.ErrorMsg = "No sufficient raw item for this menu";
                                        ViewBag.FullName = await GetUserName();
                                        var role4 = await GetLogInUserRoleObjectAsync();
                                        var roleMenuList4 = _context.RoleMenu.Where(x => x.UserIdentityRoleId == role4.Id).ToList();
                                        var nevMenuList4 = _context.NavigationMenu.Where(x => x.Id > 0).ToList();
                                        var pr4 = from r in roleMenuList4
                                                 join n in nevMenuList4
                                                 on r.NavigationMenuId equals n.Id
                                                 // where o.LastModifiedDate.ToShortDateString() == OD.AddDays(-1).ToShortDateString() && o.MealTypeId == 1
                                                 select n;
                                        var filterMenuList4 = pr4.ToList();

                                        ViewBag.FilterMenuList = filterMenuList4;

                                        return View("WarehouseStorageIn");
                                    }

                                }
                            }
                            catch (Exception ex)
                            {
                                errorList.Add("msg", ex.Message);
                            }
            ;
                            ViewBag.ItemId = strId;
                            ViewBag.quantity = quantity;
                            ViewBag.OrderDate = orderdate;
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

                            //var mealtype = _context.MealType.Where(x => x.Id == Int32.Parse(meal)).FirstOrDefault();
                            //ViewBag.OrderDate = from;
                            //ViewBag.mealId = mealtype.Id;
                            return View("WarehouseStorageOut");
                        }



                    }
                    catch (Exception ex)
                    {
                        ViewBag.OrderDate = DateTime.Now;

                        //ViewBag.quantity = quantity;
                        ViewBag.StoreId = 2;
                        ViewBag.ErrorMsg = "Date field is invalid";
                        ViewBag.FullName = await GetUserName();
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

                        return View("WarehouseStorageIn");
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
        [HttpPost]
        [AutoValidateAntiforgeryToken]
        public async Task<ActionResult> TestQuantityAsync( string StoreOutDate, long itemId, double quantity)
        {
            if (SessionExist())
            {
                if (await UserExistMess())
                {
                    var chkqnt = Request.Form["chkquantity"].ToString();
                    ViewBag.quantity = quantity;
                    ViewBag.chkquantity = double.Parse(chkqnt);
                    ViewBag.check = true;
                    ViewBag.ItemId = itemId;
                    ViewBag.FilterMenuList = getMenuUserAsync();

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

            return View("WarehouseStorageOut");


        }
        [HttpPost]
        [AutoValidateAntiforgeryToken]
        public async Task<IActionResult> Testing(string StoreOutDate, long itemId, double quantity)
        {
            var chkqnt = Request.Form["chkquantity"].ToString();
            //ViewBag.quantity = quantity;
             double chkquantity = double.Parse(chkqnt);
            //ViewBag.check = true;
            //ViewBag.ItemId = itemId;

            var StoreoutReceipe = _context.StoreOutItemRecipe.Where(x => x.StoreOutItemId == itemId).ToList();
            List<StoreInItem> itemList = new List<StoreInItem>();
            if (StoreoutReceipe.Count > 0)
            {
                foreach (var i in StoreoutReceipe)
                {
                    itemList.Add(_context.StoreInItem.Where(x => x.Id == i.StoreInItemId).Include(a => a.RemainingBalanceAndWeightedPriceCalculationList).FirstOrDefault());
                }
            }
            else
            {
                itemList = _context.StoreInItem.Include(a => a.RemainingBalanceAndWeightedPriceCalculationList).ToList();

            }
            var errorList = new Dictionary<string, string>();
            var tempPriceList = new Dictionary<string, double>();
            var tempQuantityList = new Dictionary<string, double>();
            try
            {
                if (itemList.Count > 0)
                {
                    foreach (var i in itemList)
                    {
                        Double amount = 0;
                        if (StoreoutReceipe.Count == 0)
                        {
                            amount = 0;
                        }
                        else
                        {
                            var str = _context.StoreOutItemRecipe.Where(x => x.StoreOutItemId == itemId && x.StoreInItemId == i.Id).FirstOrDefault();
                            amount = str.RequiredStoreInUnit;
                        }

                        if (amount > 0 && errorList.Count() == 0)
                        {
                            if (i.RemainingBalanceAndWeightedPriceCalculationList.LastOrDefault() != null)
                            {
                                double availableamount = i.RemainingBalanceAndWeightedPriceCalculationList.LastOrDefault().TotalAvailableAmount;
                                double amountT = availableamount - Convert.ToDouble(amount);
                                if (amountT < 0)
                                {
                                    errorList.Add(i.Id.ToString(), " Your amount is Exceeded " + "Remaining amount is : " + availableamount.ToString());

                                }
                                else
                                {
                                    var otu = i.RemainingBalanceAndWeightedPriceCalculationList.LastOrDefault();
                                    var temp = i.RemainingBalanceAndWeightedPriceCalculationList.LastOrDefault();
                                    double minProduction = _context.StoreOutItem.Where(x => x.Id == itemId).FirstOrDefault().MinimumProductionUnit;
                                    double minRawUnit = amount;
                                    double suggestedUnit = (minRawUnit * chkquantity) / minProduction;
                                    tempPriceList.Add(i.Id.ToString(), temp.WeightedPrice);
                                    tempQuantityList.Add(i.Id.ToString(), suggestedUnit);


                                }
                            }
                            else
                            {
                                errorList.Add(i.Id.ToString(), "No Remaining Balance Found");

                            }

                        }

                    }

                    if (errorList.Count() == 0)
                    {


                        ViewBag.TempPriceList = tempPriceList;
                        ViewBag.TempQuantityList = tempQuantityList;
                        ViewBag.ItemId = itemId;
                        ViewBag.quantity = quantity;
                        ViewBag.chkquantity = double.Parse(chkqnt);
                        ViewBag.StoreId = 2;

                        ViewBag.FullName = await GetUserName();

                        return View("WarehouseStorageIn");
                    }

                }
            }
            catch (Exception ex)
            {
                errorList.Add("msg", ex.Message);
            }

            ViewBag.ItemId = itemId;
            ViewBag.quantity = quantity;
            ViewBag.OrderDate = DateTime.Parse(StoreOutDate);
            ViewBag.StoreId = 2;
            ViewBag.FullName = await GetUserName();


            return View("WarehouseStorageIn");



        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateStoreOutRecipe()
        {
            StoreOutItemRecipe storeOutItemRecipe = new StoreOutItemRecipe();
            storeOutItemRecipe.CreatedDate = DateTime.Now;

            string usrName = SessionExtensions.GetString(HttpContext.Session, "user");
            var user = await _userManager.FindByNameAsync(usrName);
           // var user = await _userManager.GetUserAsync(User);
            var userID = user.Id;
            storeOutItemRecipe.CreatedBy = userID;
            storeOutItemRecipe.LastModifiedDate = DateTime.Now;
            storeOutItemRecipe.MinimumStoreOutUnit = 5;
            storeOutItemRecipe.RequiredStoreInUnit = double.Parse(Request.Form["q"]);
            storeOutItemRecipe.StoreInItemId = Int32.Parse(Request.Form["si"]);
            storeOutItemRecipe.StoreOutItemId = Int32.Parse(Request.Form["so"]);
            _context.Add(storeOutItemRecipe);
            _context.SaveChanges();


            ViewBag.StoreId = 3;
            ViewBag.FullName = await GetUserName();

            return View("WarehouseStorageIn");

        }

        public async Task<ActionResult> EditMinProduction(int id)
        {
            //if (storeOutItemRecipe == null)
            //{
            //    return NotFound();
            //}
            ViewBag.EditableMinProId = id;
            ViewBag.StoreId = 3;

            ViewBag.FullName = await GetUserName();

            return View("WarehouseStorageIn");
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditMinProdSave(int id)
        {
            double NewMinProd = double.Parse(Request.Form["minprod"]);
            var editableItem = _context.StoreOutItem.Where(x=>x.Id == id).FirstOrDefault();
            editableItem.MinimumProductionUnit = NewMinProd;
            _context.StoreOutItem.Update(editableItem);
            _context.SaveChanges();

           

            ViewBag.StoreId = 3;
            ViewBag.FullName = await GetUserName();


            return View("WarehouseStorageIn");
        }
        public async Task<ActionResult> EditRecipe(int id)
        {
            StoreOutItemRecipe storeOutItemRecipe = _context.StoreOutItemRecipe.Include(x => x.StoreOutItem).Include(x => x.StoreInItem).Where(x => x.Id == id).FirstOrDefault();
            if (storeOutItemRecipe == null)
            {
                return NotFound();
            }
            ViewBag.EditableId = id;
            ViewBag.StoreId = 3;
            ViewBag.FullName = await GetUserName();


            return View("WarehouseStorageIn");
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditSave(int id)
        {
            int StoreInItemId = Int32.Parse(Request.Form["Sin"]);
            double RequiredStoreInUnit = double.Parse(Request.Form["qnt"]);

            string usrName = SessionExtensions.GetString(HttpContext.Session, "user");
            var user = await _userManager.FindByNameAsync(usrName);
           // var user = await _userManager.GetUserAsync(User);
            var userID = user.Id;

            StoreOutItemRecipe storeOutItemRecipe = _context.StoreOutItemRecipe.Where(x => x.Id == id).FirstOrDefault();

            //storeOutItemRecipe.StoreOutItemId = model.StoreOutItemId;
            storeOutItemRecipe.StoreInItemId = StoreInItemId;
            storeOutItemRecipe.RequiredStoreInUnit = RequiredStoreInUnit;

            storeOutItemRecipe.LastModifiedBy = userID;
            storeOutItemRecipe.LastModifiedDate = DateTime.Now;
            
            _context.Update(storeOutItemRecipe);
            _context.SaveChanges();
            ViewBag.StoreId = 3;
            ViewBag.FullName = await GetUserName();


            return View("WarehouseStorageIn");

        }
        public async Task<ActionResult> DeleteRecipe(int id)
        {
            try
            {
                StoreOutItemRecipe storeOutItemRecipe = _context.StoreOutItemRecipe.Where(x => x.Id == id).FirstOrDefault();

                _context.Remove(storeOutItemRecipe);
                _context.SaveChanges();
                ViewBag.StoreId = 3;
                ViewBag.FullName = await GetUserName();

                return View("WarehouseStorageIn");

                //return RedirectToAction(nameof(Index));
            }
            catch
            {
                ViewBag.FullName = await GetUserName();

                return View("WarehouseStorageIn");
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
