using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Mess_Management_System_Alpha_V2.Data;
using Mess_Management_System_Alpha_V2.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System.Data.SqlClient;
using System.Data;
using Newtonsoft.Json;
using Mess_Management_System_Alpha_V2.Controllers.SessionChecker;
using BupMessManagement.Models;
using Microsoft.AspNetCore.Hosting;
using Mess_Management_System_Alpha_V2;

namespace BupMessManagement.Controllers.Admin
{
    public interface IConsumerOrderService
    {
        Task<List<ConsumerOrder>> GetPaginatedResult(int currentPage, int pageSize = 10);
        Task<int> GetCount();
    }
    public class ConsumerOrderController : Controller, IConsumerOrderService
    {
        private readonly ApplicationDbContext _context;
        private readonly IHostingEnvironment _hostingEnvironment;

        private UserManager<ApplicationUser> _userManager;
        private RoleManager<IdentityRole> _roleManager;

        private SignInManager<ApplicationUser> _signInManager;
        private int currentPage;
        private int pageSize;
        private int totalPages;
        private string userNamePartial;
        public ConsumerOrderController(ApplicationDbContext context, 
            UserManager<ApplicationUser> userManager,
            RoleManager<IdentityRole> roleManager,
            SignInManager<ApplicationUser> signInManager, 
            IHostingEnvironment hostingEnvironment
           )
        {
            _context = context;
            _userManager = userManager;
            _roleManager = roleManager;
            _signInManager = signInManager;
            _hostingEnvironment = hostingEnvironment;
            currentPage = 1;
            pageSize = 10;
            totalPages = 0;
            userNamePartial = null;

        }


        public async Task<IActionResult> Index()
        {
            List<ConsumerOrder> coList = await GetPaginatedResult(currentPage);
             totalPages = await GetTotalPages();
            ViewBag.TotalPages = totalPages;
            ViewBag.FullName = await GetUserName();

            return View(coList);
        }

        public async Task<IActionResult> GetPageResult(string currentpage, string searchconsumer)
        {
            currentPage = Int32.Parse(currentpage);
            if(searchconsumer != "")
            {
                userNamePartial = searchconsumer;
            }

            List<ConsumerOrder> coList = await GetPaginatedResult(currentPage);
            totalPages = await GetTotalPages();
            ViewBag.TotalPages = totalPages;

            ViewBag.SearchedConsumer = searchconsumer;
            return View("Index",coList);
        }



        public async Task<List<ConsumerOrder>> GetPaginatedResult(int currentPage, int pageSize = 10)
        {
            var data = await GetData();
            return data.OrderBy(d => d.Id).Skip((currentPage - 1) * pageSize).Take(pageSize).ToList();
        }

        public async Task<int> GetCount()
        {
            var data = await GetData();
            return data.Count;
        }

        public async Task<int> GetTotalPages()
        {
            var Count = await GetCount();
            int totalPages = (int)Math.Ceiling(decimal.Divide(Count, pageSize));
            return totalPages;
        }

        //private async Task<List<ConsumerOrder>> GetData()
        //{
           

        //    var userList = _context.Users.Where(i => i.Id != null).ToList();
        //    var preOrderScheduleList = _context.PreOrderSchedule.Where(x => x.LastModifiedDate.ToShortDateString() == DateTime.Now.AddDays(-1).ToShortDateString()).ToList();

        //    var query = from c in userList
        //                join k in preOrderScheduleList on c.Id equals k.UserId into cc
        //                from colct in cc.DefaultIfEmpty()
        //                orderby c.Id descending
        //                select new ConsumerOrder
        //                {
        //                    Id = c.Id,
        //                    BUPFullName =  c.BUPFullName,
        //                    MealStatus = colct == null ? false : colct.IsPreOrderSet,
        //                    MealtypeId = colct == null ? 0 : colct.MealTypeId

        //                };
        //    //var json = await File.ReadAllTextAsync(Path.Combine(_hostingEnvironment.ContentRootPath, "Data", "paging.json"));
        //    var list = query.ToList();
        //    //return JsonConvert.DeserializeObject<List<ConsumerOrder>>(query);
        //    return list;
        //}


        private async Task<List<ConsumerOrder>> GetData()
        {
            List<SqlParameter> parameterListV3 = new List<SqlParameter>();

            parameterListV3.Add(new SqlParameter { ParameterName = "Id", SqlDbType = System.Data.SqlDbType.Int, Value = 1 });
            parameterListV3.Add(new SqlParameter { ParameterName = "UserNamePartial", SqlDbType = System.Data.SqlDbType.VarChar, Value = userNamePartial});

            string spNameV3 = "GetAllPreOrderSchedule";

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


            List<ConsumerOrder> consumerOrderList = new List<ConsumerOrder>();

            if (dtV3.Rows.Count > 0)
            {

                foreach (DataRow row in dtV3.Rows)
                {
                    ConsumerOrder tempData = new ConsumerOrder();

                    tempData.Id = row["UserId"].ToString();
                    tempData.BUPFullName = row["BUPFullName"].ToString();
                    tempData.UserName = row["UserName"].ToString();
                    tempData.Breakfast = Convert.ToInt32(row["Breakfast"]);
                    tempData.Lunch = Convert.ToInt32(row["Lunch"]);
                    tempData.Teabreak = Convert.ToInt32(row["Tea Break"]);
               

                    consumerOrderList.Add(tempData);
                }

            }

            return consumerOrderList;
        }


        [HttpPost]
        public async Task<JsonResult> OnOffModify(string Id, string Checked, string UserName)
        {
            string usrName = SessionExtensions.GetString(HttpContext.Session, "user");
            int rowCount = 0;
            string response = "";
            var user = await _userManager.FindByNameAsync(usrName);
            var idArray = Id.Split("-");
            int mealId = Int32.Parse(idArray[1]);
            string consumerName = idArray[0].ToString();
            var consumer = await _userManager.FindByNameAsync(UserName);
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
            var pos = _context.PreOrderSchedule.Where(x => x.UserId == consumer.Id && x.MealTypeId == mealId).FirstOrDefault();
            if (pos != null)
            {
                pos.IsPreOrderSet = preorderset;
                pos.LastModifiedBy = UserId;
                //pos.LastModifiedDate = 
                //_context.PreOrderSchedule.Remove(pos);
                _context.PreOrderSchedule.Update(pos);
                rowCount =  _context.SaveChanges();
            }
            if(rowCount > 0)
            {
                response = "On/Off has been modified successfully.";
            }
            else
            {
                response = "On/Off modification is not successful.";
            }




            return Json(new { success = true, responseText = response });

        }


        public async Task<ActionResult> FetchConsumer()
        {
            userNamePartial = Request.Form["consumer"].ToString();
            List<ConsumerOrder> coList = await GetPaginatedResult(currentPage);
            totalPages = await GetTotalPages();
            ViewBag.TotalPages = totalPages;
            ViewBag.SearchedConsumer = userNamePartial;

            ViewBag.FullName = await GetUserName();

            return View("Index", coList);
           
        }
        public async Task<string> GetUserName()
        {
            string usrName = SessionExtensions.GetString(HttpContext.Session, "user");
            var user = await _userManager.FindByNameAsync(usrName);


            return user.BUPFullName;
        }


    }
}
