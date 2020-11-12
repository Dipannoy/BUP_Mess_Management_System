using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using Mess_Management_System_Alpha_V2.CommonUtilities;
using Mess_Management_System_Alpha_V2.Data;
using Mess_Management_System_Alpha_V2.Models.UCAMModel.UCAMViewModel;
using Mess_Management_System_Alpha_V2.Models.ViewModel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Mess_Management_System_Alpha_V2.Controllers
{

    [Authorize(Roles = "Admin")]
    public class UpdateUserFromUCAMController : Controller
    {


        private readonly ApplicationDbContext _context;
        private UserManager<ApplicationUser> _userManager;
        private RoleManager<IdentityRole> _roleManager;

        public UpdateUserFromUCAMController(ApplicationDbContext context, UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager)
        {
            _context = context;
            _userManager = userManager;
            _roleManager = roleManager;
        }



        // GET: UpdateUserFromUCAM
        //async Task<IActionResult>
        public IActionResult Index()
        {
            
            List<UCAMUserViewModel> uCAMUserViewModelList = GetListOfUsers();
            ViewBag.TotalUserCount = uCAMUserViewModelList.Count.ToString();


            //var applicationUser = await _context.Users.ToListAsync();
            //List<UserViewModel> userViewModel = new List<UserViewModel>();

            //foreach (var tempdata in applicationUser)
            //{
            //    UserViewModel uvm = new UserViewModel();
            //    uvm.Id = tempdata.Id;
            //    uvm.UserName = tempdata.UserName;
            //    uvm.NormalizedUserName = tempdata.NormalizedUserName;
            //    uvm.EmployeeID = tempdata.BUPEmployeeID;
            //    uvm.FullName = tempdata.BUPFullName;
            //    uvm.EmployeTypeName = tempdata.BUPEmployeTypeName;
            //    uvm.Gender = tempdata.BUPGender;
            //    uvm.Phone = tempdata.BUPPhone;
            //    uvm.Email = tempdata.BUPEmail;
            //    uvm.User_ID = tempdata.BUPUser_ID;
            //    uvm.LogInID = tempdata.BUPLogInID;
            //    uvm.RoleName = tempdata.BUPRoleName;
                
            //    userViewModel.Add(uvm);

            //}

            //List<UCAMUserViewModel> uCAMUserViewModelList2 = uCAMUserViewModelList.Where(x => !userViewModel.Any(y => y.User_ID == x.User_ID)).ToList();

            return View(uCAMUserViewModelList);
        }






        public IActionResult MigrateUsersFromUCAM()
        {

            List<UCAMUserViewModel> uCAMUserViewModelList = GetListOfUsers();

            foreach(var tempdata in uCAMUserViewModelList)
            {

                string dPassword = Utilities.DecryptString(tempdata.Password);
                string defaultPassword = "Xxx1@gmail.com";

                Task<ApplicationUser> checkAppUser = _userManager.FindByNameAsync(tempdata.LogInID);
                checkAppUser.Wait();

                ApplicationUser appUser = checkAppUser.Result;

                if (checkAppUser.Result == null)
                {
                    ApplicationUser newAppUser = new ApplicationUser
                    {
                        Email = tempdata.Email,
                        UserName = tempdata.LogInID,
                        BUPEmployeeID = tempdata.EmployeeID,
                        BUPFullName = tempdata.FullName,
                        BUPEmployeTypeName = tempdata.EmployeTypeName,
                        BUPGender = tempdata.Gender,
                        BUPPhone = tempdata.Phone,
                        BUPEmail = tempdata.Email,
                        BUPUser_ID = tempdata.User_ID,
                        BUPLogInID = tempdata.LogInID,
                        BUPPassword = dPassword,
                        BUPRoleName = tempdata.RoleName,
                        

                    };

                    Task<IdentityResult> taskCreateAppUser = _userManager.CreateAsync(newAppUser, defaultPassword);
                    taskCreateAppUser.Wait();

                    if (taskCreateAppUser.Result.Succeeded)
                    {
                        appUser = newAppUser;


                        Task<IdentityResult> newUserRole = _userManager.AddToRoleAsync(appUser, "Consumer");
                        newUserRole.Wait();

                    }
                    
                }
                
            }

            ViewBag.SuccessMassage = "Success";


            return RedirectToAction(nameof(Index));
        }



        public List<UCAMUserViewModel> GetListOfUsers()
        {
            #region Get All User From UCAM
            string spName = "GetAllUserFromUCAM";

            string connStr = Startup.ConnectionString;

            DataTable dt = new DataTable();
            using (SqlConnection connection = new SqlConnection(connStr))
            {
                SqlCommand command = new SqlCommand(spName, connection);
                command.CommandType = System.Data.CommandType.StoredProcedure;

                try
                {
                    connection.Open();
                    SqlDataReader reader;
                    reader = command.ExecuteReader();
                    dt.Load(reader);
                }
                finally
                {
                    connection.Close();
                }
            }

            List<UCAMUserViewModel> uCAMUserViewModelList = new List<UCAMUserViewModel>();

            if (dt.Rows.Count > 0)
            {

                foreach (DataRow row in dt.Rows)
                {
                    UCAMUserViewModel tempData = new UCAMUserViewModel();

                    tempData.EmployeeID = Convert.ToInt32(row["EmployeeID"]);
                    tempData.FullName = row["FullName"].ToString();
                    tempData.EmployeTypeName = row["EmployeTypeName"].ToString();
                    tempData.Gender = row["Gender"].ToString();
                    tempData.Phone = row["Phone"].ToString();
                    tempData.Email = row["Email"].ToString();
                    tempData.User_ID = Convert.ToInt32(row["User_ID"]);
                    tempData.LogInID = row["LogInID"].ToString();
                    tempData.Password = row["Password"].ToString();
                    tempData.RoleName = row["RoleName"].ToString();

                    uCAMUserViewModelList.Add(tempData);
                }

            }
            #endregion


            return uCAMUserViewModelList;
        }

















        // GET: UpdateUserFromUCAM/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: UpdateUserFromUCAM/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: UpdateUserFromUCAM/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(IFormCollection collection)
        {
            try
            {
                // TODO: Add insert logic here

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: UpdateUserFromUCAM/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: UpdateUserFromUCAM/Edit/5
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

        // GET: UpdateUserFromUCAM/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: UpdateUserFromUCAM/Delete/5
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
    }
}