using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Mess_Management_System_Alpha_V2.Data;
using Mess_Management_System_Alpha_V2.Models.ViewModel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Mess_Management_System_Alpha_V2.Controllers
{
    [Authorize(Roles = "Admin")]
    public class AdminController : Controller
    {


        private readonly ApplicationDbContext _context;
        private UserManager<ApplicationUser> _userManager;
        private RoleManager<IdentityRole> _roleManager;

        public AdminController(ApplicationDbContext context, UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager)
        {
            _context = context;
            _userManager = userManager;
            _roleManager = roleManager;
        }




        // GET: Admin
        public IActionResult Index()
        {

            List<UserViewModel> userList = new List<UserViewModel>();
            var allUser = _userManager.Users.ToList();
            foreach (var tempdata in allUser)
            {
                UserViewModel uvm = new UserViewModel();
                uvm.Id = tempdata.Id;
                uvm.UserName = tempdata.UserName;
                uvm.NormalizedUserName = tempdata.NormalizedUserName;
                uvm.EmployeeID = tempdata.BUPEmployeeID;
                uvm.FullName = tempdata.BUPFullName;
                uvm.EmployeTypeName = tempdata.BUPEmployeTypeName;
                uvm.Gender = tempdata.BUPGender;
                uvm.Phone = tempdata.BUPPhone;
                uvm.Email = tempdata.BUPEmail;
                uvm.User_ID = tempdata.BUPUser_ID;
                uvm.LogInID = tempdata.BUPLogInID;
                uvm.RoleName = tempdata.BUPRoleName;
                

                userList.Add(uvm);

            }
            
            if (userList != null && userList.Count > 0)
            {
                ViewBag.AllUsers = userList.OrderBy(x=> x.User_ID);
            }
            else
            {
                ViewBag.AllUsers = null;
            }
            

            return View();
        }




        public IActionResult IndexHome()
        {
            return View();
        }










    }
}