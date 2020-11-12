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


// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Mess_Management_System_Alpha_V2.Controllers.Admin
{
    public class UserBillController : Controller
    {
        // GET: /<controller>/

        private readonly ApplicationDbContext _context;
        private UserManager<ApplicationUser> _userManager;
        private RoleManager<IdentityRole> _roleManager;

        public UserBillController(ApplicationDbContext context, UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager)
        {
            _context = context;
            _userManager = userManager;
            _roleManager = roleManager;
        }


        public IActionResult Index()
        {
            return View();
        }
        public ActionResult BillProcess()
        {
            //var result = _context.WarehouseStorage.Include(x => x.StoreInItem).ToList();
            return View();
        }

        [HttpPost]
        [AutoValidateAntiforgeryToken]
        public async Task<IActionResult> DateFetch()
        {
            var date = DateTime.Parse(Request.Form["date2"][0]);
            ViewBag.Date = date;
            return RedirectToAction(nameof(Index));


        }

      
        [HttpPost]
        //[ValidateAntiForgeryToken]

        public async Task<IActionResult> CreateOrderHistoryOfSetMenu(string MealTypeId)
        {
            List<SqlParameter> parameterListV3 = new List<SqlParameter>();
            int mealTypeId = Convert.ToInt32(MealTypeId);
            var user = await _userManager.GetUserAsync(User);
            var userID = user.Id;
            //var preOrderScheduleList = _context.PreOrderSchedule.Include(x => x.MealType).Include(x => x.ApplicationUser).ToList();
            var setMenuList = _context.SetMenu.Include(x => x.SetMenuDetailsList).Include(a => a.MealType).Where(x => x.SetMenuDate.ToShortDateString() == DateTime.Now.ToShortDateString()).ToList();

            var setMenuOnMeal = setMenuList.Where(x => x.MealTypeId == mealTypeId).FirstOrDefault();
            //foreach (var itm in preOrderScheduleList.Select(a => a.MealType).Distinct().OrderBy(a => a.Serial).ToList())
            //{
            //    foreach (var a in itm.PreOrderScheduleList) {

            if (setMenuOnMeal != null)
            {

                //var existingEntry = _context.OrderHistory.Where(a => a.CreatedBy == userID && a.CreatedDate == DateTime.Now && a.MealTypeId == mealTypeId && a.SetMenuId == setMenuOnMeal.Id).FirstOrDefault();
                var existingEntry = _context.OrderHistory.Where(a => a.CreatedBy == userID  && a.CreatedDate.ToShortDateString() == DateTime.Now.ToShortDateString() &&  a.MealTypeId == mealTypeId && a.SetMenuId == setMenuOnMeal.Id).ToList();

                if (existingEntry.Count == 0)
                {
                    try
                    {
                        parameterListV3.Add(new SqlParameter { ParameterName = "MealTypeId", SqlDbType = System.Data.SqlDbType.BigInt, Value = mealTypeId });
                        parameterListV3.Add(new SqlParameter { ParameterName = "UserId", SqlDbType = System.Data.SqlDbType.NVarChar, Value = userID });


                        string spNameV3 = "CreateOrderHistoryOfSetMenu";

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
                                    return Json(new { success = true, responseText = "Order has been generated successfully for this meal." });

                                }
                                else
                                {
                                    return Json(new { success = true, responseText = "Something is wrong.Order is not getting generated." });

                                }
                            }





                        }
                    }
                    catch(Exception ex)
                    {
                        return Json(new { success = true, responseText = ex.Message });

                    }
                }
                else
                {
                    return Json(new { success = true, responseText = "Order has already been generated for this meal"  });

                }
            }
            else
            {
                return Json(new { success = true, responseText = "Today SetMenu is not defined for this meal" });

            }
            return RedirectToAction(nameof(Index));




        }

    }
    }
