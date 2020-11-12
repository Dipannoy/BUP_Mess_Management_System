using System;
using System.Collections.Generic;
using System.Linq;
using Mess_Management_System_Alpha_V2.Data;
using Mess_Management_System_Alpha_V2.Models.MessModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

using Microsoft.EntityFrameworkCore;
using System.Data.SqlClient;
using System.Data;
using Newtonsoft.Json;

namespace Mess_Management_System_Alpha_V2.Controllers.Admin
{
    public class AdminOrderController : Controller
    {

        private readonly ApplicationDbContext _context;
        private UserManager<ApplicationUser> _userManager;
        private RoleManager<IdentityRole> _roleManager;

        public AdminOrderController(ApplicationDbContext context, UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager)
        {
            _context = context;
            _userManager = userManager;
            _roleManager = roleManager;
        }

        public ActionResult UserBill()
        {
            //var result = _context.WarehouseStorage.Include(x => x.StoreInItem).ToList();
            return View();
        }

        public IActionResult AdminOrderShow()
        {
            return View();
        }
        public ActionResult EditBill(DateTime? from = null, DateTime? to = null, String UserId = "")
        {
            var result = new List<OrderHistory>();

            if (UserId == "" || UserId == null)
            {
                result = _context.OrderHistory.FromSql(@"select * from OrderHistory 
                                                        where 
                                                        ((Convert(date,OrderDate,103) >= Convert(date,{1},103) or {1} is null)
                                                        and 
                                                        (Convert(date,OrderDate,103) <= Convert(date,{2},103) or {2} is null)) ", UserId, from, to)
                                                        .Include(x => x.ApplicationUser).Include(x => x.MealType).Include(x => x.StoreOutItem).ToList();

            }
            else
            {
                result = _context.OrderHistory.FromSql(@"select * from OrderHistory 
                                                        where 
                                                        (UserId = {0})
                                                        and 
                                                        ((Convert(date,OrderDate,103) >= Convert(date,{1},103) or {1} is null)
                                                        and 
                                                        (Convert(date,OrderDate,103) <= Convert(date,{2},103) or {2} is null)) ", UserId, from, to)
                                                        .Include(x => x.ApplicationUser).Include(x => x.MealType).Include(x => x.StoreOutItem).ToList();

            }

            return View(result);

            //var result = _context.WarehouseStorage.Include(x => x.StoreInItem).ToList();
            return View();
        }
        public ActionResult MaintenanceBillProcess()
        {
            //var result = _context.WarehouseStorage.Include(x => x.StoreInItem).ToList();
            return View();
        }
        public ActionResult BillProcess()
        {
            //var result = _context.WarehouseStorage.Include(x => x.StoreInItem).ToList();
            return View();
        }



        // GET: AdminOrder
        public ActionResult Index(DateTime? from = null, DateTime? to = null, String UserId = "")
        {
            var result = new List<OrderHistory>();

            if (UserId == "" || UserId == null)
            {
                result = _context.OrderHistory.FromSql(@"select * from OrderHistory 
                                                        where 
                                                        ((Convert(date,OrderDate,103) >= Convert(date,{1},103) or {1} is null)
                                                        and 
                                                        (Convert(date,OrderDate,103) <= Convert(date,{2},103) or {2} is null)) ", UserId, from, to)
                                                        .Include(x => x.ApplicationUser).Include(x => x.MealType).Include(x => x.StoreOutItem).ToList();

            } else
            {
                result = _context.OrderHistory.FromSql(@"select * from OrderHistory 
                                                        where 
                                                        (UserId = {0})
                                                        and 
                                                        ((Convert(date,OrderDate,103) >= Convert(date,{1},103) or {1} is null)
                                                        and 
                                                        (Convert(date,OrderDate,103) <= Convert(date,{2},103) or {2} is null)) ", UserId, from, to)
                                                        .Include(x => x.ApplicationUser).Include(x => x.MealType).Include(x => x.StoreOutItem).ToList();

            }
            
            return View(result);
        }


        public ActionResult OrderByAdmin(DateTime? LoadOrderDate)
        {
            List<SetMenu> result = _context.SetMenu.Include(x => x.SetMenuDetailsList).Include(x => x.ExtraItemList).Include(x => x.MealType).Where(x => x.SetMenuDate.ToShortDateString() == (LoadOrderDate ?? DateTime.Now).ToShortDateString()).ToList();
            ViewBag.Result = result;
            ViewBag.Date = LoadOrderDate ?? DateTime.Now;
            return View();
        }

        public ActionResult UserBillControl()
        {
            return RedirectToAction(nameof(UserBill));
        }
        public ActionResult ProcessBill()
        {
            return RedirectToAction(nameof(BillProcess));
        }
        public ActionResult BillEdit()
        {
            return RedirectToAction(nameof(EditBill));
        }
        public ActionResult ProcessMaintenanceBill()
        {
            return RedirectToAction(nameof(MaintenanceBillProcess));
        }

        [HttpPost]
        [AutoValidateAntiforgeryToken]
        public async Task<IActionResult> DateFetch()
        {
            var to = DateTime.Parse(Request.Form["to"][0]);
            var from = DateTime.Parse(Request.Form["from"][0]);

            ViewBag.To = to;
            ViewBag.From = from;
            return View("UserBill");


        }
        [HttpPost]
        [AutoValidateAntiforgeryToken]
        public async Task<IActionResult> DateFetch2()
        {
            var date = DateTime.Parse(Request.Form["date2"][0]);
            ViewBag.Date = date;
            return View("BillProcess");


        }
        [HttpPost]
        [AutoValidateAntiforgeryToken]
        public async Task<IActionResult> MaintenanceBillGenerate()
        {
            var year = Int64.Parse((Request.Form["year"]));
            var month = Int32.Parse(Request.Form["month"]);
            var cost = float.Parse(Request.Form["MCost"]);
            List<SqlParameter> parameterListV3 = new List<SqlParameter>();
            var user = await _userManager.GetUserAsync(User);
            var userID = user.Id;

            var existingEntry = _context.MaintenanceBillHistory.Where(a => a.Month == month && a.Year == year).ToList();
            if (existingEntry.Count == 0)
            {

                try
                {
                    parameterListV3.Add(new SqlParameter { ParameterName = "UserId", SqlDbType = System.Data.SqlDbType.NVarChar, Value = userID });
                    parameterListV3.Add(new SqlParameter { ParameterName = "Month", SqlDbType = System.Data.SqlDbType.Int, Value = month });
                    parameterListV3.Add(new SqlParameter { ParameterName = "Year", SqlDbType = System.Data.SqlDbType.BigInt, Value = year });
                    parameterListV3.Add(new SqlParameter { ParameterName = "BillAmount", SqlDbType = System.Data.SqlDbType.Float, Value = cost });




                    string spNameV3 = "CreateMaintenanceBillHistory";

                    string connStrV3 = Startup.ConnectionString;

                    DataTable dtV3 = new DataTable();


                    using (SqlConnection connection = new SqlConnection(connStrV3))
                    {
                        SqlCommand command = new SqlCommand(spNameV3, connection);
                        command.CommandType = System.Data.CommandType.StoredProcedure;
                        foreach (var item in parameterListV3)
                        {
                            command.Parameters.Add(item);
                        }

                        try
                        {
                            connection.Open();
                            SqlDataReader reader;
                            reader = command.ExecuteReader();
                            dtV3.Load(reader);
                        }
                        finally
                        {
                            connection.Close();
                        }
                    }
                    if (dtV3.Rows.Count > 0)
                    {


                        foreach (DataRow row in dtV3.Rows)
                        {
                            if (Convert.ToInt32(row["ROWCOUNT"]) > 0)

                            {
                                ViewBag.SuccessMessage = "Maintenance bill has been generated successfully for the particular month of the year";
                                ViewBag.Year = year;
                                ViewBag.Month = month;
                                return View("MaintenanceBillProcess");
                                //return Json(new { success = true, responseText = "Order has been generated successfully for this meal." });

                            }
                            else
                            {
                                return RedirectToAction(nameof(BillProcess));

                                //return Json(new { success = true, responseText = "Something is wrong.Order is not getting generated." });

                            }
                        }





                    }
                }
                catch (Exception ex)
                {
                    //return Json(new { success = true, responseText = ex.Message });

                }
            }
            else
            {
                ViewBag.DuplicateMessage = "Maintenance bill has already been generated for the particular month of the year";
                return View("MaintenanceBillProcess");


            }
            return RedirectToAction(nameof(MaintenanceBillProcess));

            //return View("BillProcess");


        }
        [HttpPost]
        public async Task<IActionResult> EditOrderHistoryList(string OrderList,string NotForwardedOrderList)
        {
            //var logJson = JsonConvert.SerializeObject(OrderList);
            //var parse = JsonConvert.DeserializeObject<OrderHistoryIds>(logJson);



            List<OrderHistoryIds> tempOrderList = (List<OrderHistoryIds>)JsonConvert.DeserializeObject(OrderList, typeof(List<OrderHistoryIds>));
            List<OrderHistoryIds> tempNotForwardedOrderList = (List<OrderHistoryIds>)JsonConvert.DeserializeObject(NotForwardedOrderList, typeof(List<OrderHistoryIds>));

            foreach (var item in tempOrderList)
            {
                int id = Int32.Parse(item.OrderId);
                var orderRecord = _context.OrderHistory.Where(x => x.Id == id).FirstOrDefault();
                orderRecord.IsForwardedToOffice = true;
                _context.Update(orderRecord);
                _context.SaveChanges();



            }
            foreach (var item in tempNotForwardedOrderList)
            {
                int id = Int32.Parse(item.OrderId);
                var orderRecord = _context.OrderHistory.Where(x => x.Id == id).FirstOrDefault();
                if (orderRecord.IsForwardedToOffice == true)
                {
                    orderRecord.IsForwardedToOffice = false;
                    _context.Update(orderRecord);
                    _context.SaveChanges();
                }
            }

                



            
            //string s = tempOrderList[0].OrderId;
            //string s2 = tempOrderList[1].OrderHistoryId;

            return Json(new { success = true, responseText = "Order history has been updated successfully . " });




            //return RedirectToAction(nameof(Index));

        }
        public async Task<ActionResult> CreatePreOrderSchedule()
        {
            var user = await _userManager.GetUserAsync(User);
            var userID = user.Id;
            List<SqlParameter> parameterListV3 = new List<SqlParameter>();


            try
            {
                parameterListV3.Add(new SqlParameter { ParameterName = "UserId", SqlDbType = System.Data.SqlDbType.NVarChar, Value = userID });
               




                string spNameV3 = "CreateTodaysPreOrderSchedule";

                string connStrV3 = Startup.ConnectionString;

                DataTable dtV3 = new DataTable();


                using (SqlConnection connection = new SqlConnection(connStrV3))
                {
                    SqlCommand command = new SqlCommand(spNameV3, connection);
                    command.CommandType = System.Data.CommandType.StoredProcedure;
                    foreach (var item in parameterListV3)
                    {
                        command.Parameters.Add(item);
                    }

                    try
                    {
                        connection.Open();
                        SqlDataReader reader;
                        reader = command.ExecuteReader();
                        dtV3.Load(reader);
                    }
                    finally
                    {
                        connection.Close();
                    }
                }
                if (dtV3.Rows.Count > 0)
                {


                    foreach (DataRow row in dtV3.Rows)
                    {
                        if (Convert.ToInt32(row["ROWCOUNT"]) > 0)

                        {
                            TempData["Message"] = "Preorder Schedule has been created successfully for tomorrow";
                            return LocalRedirect("~/Admin/IndexHome");
                            //return Json(new { success = true, responseText = "Order has been generated successfully for this meal." });

                        }
                        else
                        {
                            TempData["Message"] = "Preorder schedule has already been generated";


                            return LocalRedirect("~/Admin/IndexHome");

                            //return Json(new { success = true, responseText = "Something is wrong.Order is not getting generated." });

                        }
                    }





                }
            }
            catch (Exception ex)
            {
                TempData["Message"] = "Exception One";

                return LocalRedirect("~/Admin/IndexHome");

            }
            TempData["Message"] = "Last One";

            return LocalRedirect("~/Admin/IndexHome");



        }

        public class OrderHistoryIds
        {
            public string OrderId { get; set; }
        }





        //// GET: AdminOrder/Details/5
        //public ActionResult Details(int id)
        //{
        //    return View();
        //}

        //// GET: AdminOrder/Create
        //public ActionResult Create()
        //{
        //    return View();
        //}

        //// POST: AdminOrder/Create
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

        //// GET: AdminOrder/Edit/5
        //public ActionResult Edit(int id)
        //{
        //    return View();
        //}

        //// POST: AdminOrder/Edit/5
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

        //// GET: AdminOrder/Delete/5
        //public ActionResult Delete(int id)
        //{
        //    return View();
        //}

        //// POST: AdminOrder/Delete/5
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