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

using System.Globalization;
using Microsoft.AspNetCore.Authorization;
using System.Collections;


// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Mess_Management_System_Alpha_V2.Controllers.Admin
{
    //[Authorize(Roles = "Admin,MessAdmin")]


    public class AdminBillGenerateController : Controller
    {
        // GET: /<controller>/

        private readonly ApplicationDbContext _context;
        private UserManager<ApplicationUser> _userManager;
        private RoleManager<IdentityRole> _roleManager;

        public AdminBillGenerateController(ApplicationDbContext context, UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager)
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

        public async Task<IActionResult> AdminOrderShow()
        {

            ViewBag.FullName = await GetUserName();

            return View();
        }
        public async Task<IActionResult> home()
        {
            ViewBag.FullName = await GetUserName();

            return View();
        }
        public IActionResult Dashboard()
        {
            return View();
        }

        public async Task<IActionResult> Schedule()
        {
            ViewBag.FullName = await GetUserName();

            return View();
        }
        public async Task<IActionResult> ItemBase()
        {
            ViewBag.FullName = await GetUserName();

            return View();
        }


        public JsonResult GetPersonBill(string PersonName,string Month,string Year)
        {
            List<IList> BillList = new List<IList>();
            List<AccessoryBill> ACBill = new List<AccessoryBill>();
            List<Double> ArrearBill = new List<Double>();
            var prsndtl = _userManager.Users.Where(x => x.BUPFullName == PersonName).FirstOrDefault();
            int year = DateTime.Now.Year;


            var pmbr = _context.ConsumerMonthlyBillRecord.Where(x => x.UserId == prsndtl.Id && x.Month == Int32.Parse(Month) && x.Year == year).FirstOrDefault();
            if (pmbr != null)
            {
                _context.ConsumerMonthlyBillRecord.Remove(pmbr);
                _context.SaveChanges();
            }


            double maintenanceBill = 0;
            var AccsBillList = _context.AccessoryBill.Where(x => x.Id > 0).ToList();
            ACBill = AccsBillList;

            maintenanceBill = _context.AccessoryBill.Where(x => x.Id > 0).Sum(x => x.DefaultCost);

            //foreach(var abl in AccsBillList )
            //{
            //    ACBill.Add(abl.DefaultCost);
            //    maintenanceBill += abl.DefaultCost;
            //}


            var PersonBillMonth = _context.OrderHistoryVr2.Where(x => x.OrderDate.Month == Int32.Parse(Month) && x.OrderDate.Year == Int32.Parse(Year) && x.UserId==prsndtl.Id).ToList();

            double PrsnTtlMnthBill = PersonBillMonth.Sum(x => x.OrderAmount) + maintenanceBill;



            //var cMont = _context.ConsumerMonthlyBillRecord.Where(x=> x.Month == Month && x.UserId == prsndtl.Id).F
            double duebill = _context.ConsumerMonthlyBillRecord.Where(x => x.IsPaid == false && x.UserId == prsndtl.Id ).Sum(x => x.TotalAmount);
            ArrearBill.Add(duebill);

            ConsumerMonthlyBillRecord cmbr = new ConsumerMonthlyBillRecord();
            cmbr.CreatedDate = DateTime.Now;
            cmbr.LastModifiedDate = DateTime.Now;
            cmbr.UserId = prsndtl.Id;
            cmbr.Month = Int32.Parse(Month);
            cmbr.Year = year;
            cmbr.TotalAmount = PrsnTtlMnthBill;
            _context.ConsumerMonthlyBillRecord.Add(cmbr);
            _context.SaveChanges();

            PersonBillMonth.OrderBy(x => x.OrderDate);

            var TotalBillOnDateMeal =
    from person in PersonBillMonth
    group person by new { person.MealTypeId,person.OrderDate.Date }  into PersonGroup
    select new
    {
        Team = PersonGroup.Key,
        TotalBill = PersonGroup.Sum(x => x.OrderAmount),
    };
            var TB = TotalBillOnDateMeal.ToList();

            BillList.Add(ACBill);
            BillList.Add(TB);
            BillList.Add(ArrearBill);
         
            return Json(BillList);

            //var PersonBillData = _userManager.Users.Where(x => x.BUPFullName.Contains(search)).ToList();

            //return Json(PersonList);
        }


        [HttpPost]

        public async Task<IActionResult> GetBillStatus()
        {
            string user = Request.Form["stperson"];
            var prsndtl = _userManager.Users.Where(x => x.BUPFullName == user).FirstOrDefault();

            int month =  Int32.Parse(Request.Form["stmonth"]);
            int year = Int32.Parse(Request.Form["styear"]);

            ViewBag.StatusUser = "-1";
            if (prsndtl != null)
            {
                ViewBag.StatusUser = prsndtl.Id;
            }
            ViewBag.Mnth = month;
            ViewBag.Yr = year;
            ViewBag.FullName = await GetUserName();


            return View("Index");

        }

        [HttpPost]

        public async Task<IActionResult> GetBillStatus2()
        {
            string user = Request.Form["stperson2"];
            var prsndtl = _userManager.Users.Where(x => x.BUPFullName == user).FirstOrDefault();
            int month = Int32.Parse(Request.Form["stmonth2"]);
            int year = Int32.Parse(Request.Form["styear2"]);

            ViewBag.StatusUser = "-1";

            if (prsndtl != null)
            {
                ViewBag.StatusUser = prsndtl.Id;
            }

            ViewBag.Mnth = month;
            ViewBag.Yr = year;

            ViewBag.FullName = await GetUserName();


            return View("Index");

        }

        [HttpPost]

        public async Task<IActionResult> ViewBill()
        {
            var from = Request.Form["from"].ToString();
            var to = Request.Form["to"].ToString();
            DateTime fromdate = DateTime.Now;
            DateTime Todate = DateTime.Now;

            if (from != "")
            {
                string frm = Request.Form["from"];
                string[] DateFormatarray = frm.Split("-");
                string datef = DateFormatarray[1] + '/' + DateFormatarray[0] + '/' + DateFormatarray[2];
                fromdate = DateTime.ParseExact(datef, "M/d/yyyy", CultureInfo.InvariantCulture);
            }
            if(to != "")
            {
                string too = Request.Form["to"];
                string[] DateFormatarray = too.Split("-");
                string dateto = DateFormatarray[1] + '/' + DateFormatarray[0] + '/' + DateFormatarray[2];
                 Todate = DateTime.ParseExact(dateto, "M/d/yyyy", CultureInfo.InvariantCulture);
            }
            //var user = Request.Form["person2"].ToString();

            string usr = Request.Form["person2"];
            var prsndtl = _userManager.Users.Where(x => x.BUPFullName == usr).FirstOrDefault();

            string user = "-1";
            if(prsndtl != null)
            {
                user = prsndtl.Id;
            }

            if (from != "" && to != "" && user != "-1")
            {
                ViewBag.Form = fromdate;
                ViewBag.To = Todate;
                ViewBag.UserId = user;
                ViewBag.FullName = await GetUserName();

                return View("Index");

            }
            else 
            {
               
                ViewBag.Form = fromdate;
                ViewBag.To = Todate;

                ViewBag.FullName = await GetUserName();

                return View("Index");




            }

          //  return View("Index");
        }

        [HttpPost]

        public async Task<IActionResult> CreateAccessoryBill()
        {
            string name = Request.Form["AcsName"].ToString();
            int minmeal = 0; double minCost = 0;

            if (Request.Form["MinMeal"] != "")
            {
                minmeal = Int32.Parse(Request.Form["MinMeal"]);
            }
            if (Request.Form["MinMealCost"] != "")
            {
                minCost = double.Parse(Request.Form["MinMealCost"]);
            }

            
            double defaultCost = double.Parse(Request.Form["DefaultCost"]);

            AccessoryBill ab = new AccessoryBill();
            ab.Name = name;
            ab.Active = true;
            ab.CreatedDate = DateTime.Now;
            ab.LastModifiedDate = DateTime.Now;
            if(minmeal != 0)
            {
                ab.MinMeal = minmeal;

            }
            if(minCost != 0)
            {
                ab.CostMinMeal = minCost;
            }
            ab.DefaultCost = defaultCost;
            _context.AccessoryBill.Add(ab);
            _context.SaveChanges();

            ViewBag.ACBill = 1;
            ViewBag.FullName = await GetUserName();

            return View("Index");

        }
        [HttpPost]

        public async Task<IActionResult> GetEditACId(int Id)
        {
            ViewBag.ACBill = 1;
            ViewBag.ACId = Id;
            ViewBag.FullName = await GetUserName();

            return View("Index");
        }

        [HttpPost]

        public async Task<IActionResult> DeleteACBill(int Id)
        {
            var ab = _context.AccessoryBill.Where(x => x.Id == Id).FirstOrDefault();
            _context.AccessoryBill.Remove(ab);
            _context.SaveChanges();
            ViewBag.ACBill = 1;
            ViewBag.FullName = await GetUserName();

            return View("Index");
        }

        [HttpPost]

        public async Task<IActionResult> SaveEditACId(int Id)
        {

            string name = Request.Form["EAcsName"].ToString();
            int minmeal = 0; double minCost = 0;

        
                minmeal = Int32.Parse(Request.Form["EMinMeal"].ToString());
            
          
                minCost = double.Parse(Request.Form["EMinMealCost"].ToString());
            


            double defaultCost = double.Parse(Request.Form["EDefaultCost"]);

            var ab = _context.AccessoryBill.Where(x => x.Id == Id).FirstOrDefault();

            ab.Name = name;
            //ab.Active = true;
            ab.LastModifiedDate = DateTime.Now;
            if (minmeal != 0)
            {
                ab.MinMeal = minmeal;

            }
            if (minCost != 0)
            {
                ab.CostMinMeal = minCost;
            }
            ab.DefaultCost = defaultCost;
            _context.AccessoryBill.Update(ab);
            _context.SaveChanges();

            ViewBag.ACBill = 1;
            ViewBag.FullName = await GetUserName();

            return View("Index");
           
        }

        [HttpPost]

        public async Task<IActionResult> ViewBill2()
        {
            var from = Request.Form["from2"].ToString();
            var to = Request.Form["to2"].ToString();
            DateTime fromdate = DateTime.Now;
            DateTime Todate = DateTime.Now;

           

            if (from != "")
            {
                string frm = Request.Form["from2"];
                string[] DateFormatarray = frm.Split("-");
                string datef = DateFormatarray[1] + '/' + DateFormatarray[0] + '/' + DateFormatarray[2];
                fromdate = DateTime.ParseExact(datef, "M/d/yyyy", CultureInfo.InvariantCulture);
            }
            if (to != "")
            {
                string too = Request.Form["to2"];
                string[] DateFormatarray = too.Split("-");
                string dateto = DateFormatarray[1] + '/' + DateFormatarray[0] + '/' + DateFormatarray[2];
                Todate = DateTime.ParseExact(dateto, "M/d/yyyy", CultureInfo.InvariantCulture);
            }
            string usr = Request.Form["person3"];
            var prsndtl = _userManager.Users.Where(x => x.BUPFullName == usr).FirstOrDefault();

            string user = "-1";
            if (prsndtl != null)
            {
                user = prsndtl.Id;
            }
            if (from != "" && to != "" && user != "-1")
            {
                ViewBag.Form = fromdate;
                ViewBag.To = Todate;
                ViewBag.UserId = user;
                ViewBag.FullName = await GetUserName();

                return View("Index");

            }
            else
            {

                ViewBag.Form = fromdate;
                ViewBag.To = Todate;

                ViewBag.FullName = await GetUserName();

                return View("Index");




            }

            //return View("Index");
        }

        [HttpPost]
        public async Task<JsonResult> PaidStatusModify(string MonthRecordId, string Checked)
        {

            int MRId = Int32.Parse(MonthRecordId);
          
            var cmb = _context.ConsumerMonthlyBillRecord.Where(x => x.Id == MRId).FirstOrDefault();
            if (Checked == "true")
            {
                cmb.IsPaid = true;
            }
            else
            {
                cmb.IsPaid = false;
            }

            _context.ConsumerMonthlyBillRecord.Update(cmb);
            _context.SaveChanges();
             
            return Json(new { success = true, responseText = "" });

        }


        public JsonResult GetSearchPerson(string search)
        {
            var PersonList = _userManager.Users.Where(x => x.BUPFullName.Contains(search)).ToList();

            return Json(PersonList);
        }



        [HttpPost]
        //[ValidateAntiForgeryToken]

        public async Task<IActionResult> CreateDailyBill()
        {
            List<SqlParameter> parameterListV3 = new List<SqlParameter>();
            //int mealTypeId = Convert.ToInt32(MealTypeId);
            string usrName = SessionExtensions.GetString(HttpContext.Session, "user");
            var user = await _userManager.FindByNameAsync(usrName);
           // var user = await _userManager.GetUserAsync(User);
            var userID = user.Id;
            var msg = "";
            //var preOrderScheduleList = _context.PreOrderSchedule.Include(x => x.MealType).Include(x => x.ApplicationUser).ToList();
            //var setMenuList = _context.SetMenu.Include(x => x.SetMenuDetailsList).Include(a => a.MealType).Where(x => x.SetMenuDate.ToShortDateString() == DateTime.Now.ToShortDateString()).ToList();

            //var setMenuOnMeal = setMenuList.Where(x => x.MealTypeId == mealTypeId).FirstOrDefault();
            //foreach (var itm in preOrderScheduleList.Select(a => a.MealType).Distinct().OrderBy(a => a.Serial).ToList())
            //{
            //    foreach (var a in itm.PreOrderScheduleList) {

            //if (setMenuOnMeal != null)
            //{

                //var existingEntry = _context.OrderHistory.Where(a => a.CreatedBy == userID && a.CreatedDate == DateTime.Now && a.MealTypeId == mealTypeId && a.SetMenuId == setMenuOnMeal.Id).FirstOrDefault();
                var existingEntry = _context.OrderHistoryVr2.Where(a => a.OrderDate.ToShortDateString() == DateTime.Now.ToShortDateString()).ToList();

                foreach(var i in existingEntry)
                {
                _context.OrderHistoryVr2.Remove(i);
                _context.SaveChanges();
                }
                //if (existingEntry.Count == 0)
                //{
                    try
                    {
                        //parameterListV3.Add(new SqlParameter { ParameterName = "MealTypeId", SqlDbType = System.Data.SqlDbType.BigInt, Value = mealTypeId });
                        //parameterListV3.Add(new SqlParameter { ParameterName = "UserId", SqlDbType = System.Data.SqlDbType.NVarChar, Value = userID });


                        string spNameV3 = "DailyBillGenerate";

                        string connStrV3 = Startup.ConnectionString;

                        DataTable dtV3 = new DataTable();
                        using (SqlConnection connection = new SqlConnection(connStrV3))
                        {
                            SqlCommand command = new SqlCommand(spNameV3, connection);
                            command.CommandType = System.Data.CommandType.StoredProcedure;
                            //foreach (var item in parameterListV3)
                            //{
                            //    command.Parameters.Add(item);
                            //}

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
                                msg = "Today's bill has been generated successfully";
                                    //return Json(new { success = true, responseText = "Order has been generated successfully for this meal." });

                                }
                                else
                                {
                                msg = "Something is wrong.Bill is not getting generated.";
                                   // return Json(new { success = true, responseText = "Something is wrong.Order is not getting generated." });

                                }
                            }





                        }
                    }
                    catch (Exception ex)
                    {
                        return Json(new { success = true, responseText = ex.Message });

                    }
                
                //else
                //{
                //    msg = "Bill has already been generated for today.";
                //   // return Json(new { success = true, responseText = "Order has already been generated for this meal" });

                //}
            //}
            //else
            //{
            //    return Json(new { success = true, responseText = "Today SetMenu is not defined for this meal" });

            //}

            ViewBag.msg = msg;
            ViewBag.FullName = await GetUserName();

            return View("Index");
           // return RedirectToAction(nameof(Index));




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

    }
}

