using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Mess_Management_System_Alpha_V2.Models;
//using Microsoft.AspNetCore.Identity;
using Mess_Management_System_Alpha_V2.Data;
using Mess_Management_System_Alpha_V2.Models.MessModels;
using Newtonsoft.Json;
using System.Web;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Authorization;


namespace Mess_Management_System_Alpha_V2.Controllers
{

   // [Produces("application/json")]
    public class HomeController : Controller
    {

        //private readonly ApplicationDbContext _context;
        //private UserManager<ApplicationUser> _userManager;
        //private RoleManager<IdentityRole> _roleManager;

        //public HomeController(ApplicationDbContext context, UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager)
        //{
        //    //_context = context;
        //    //_userManager = userManager;
        //    //_roleManager = roleManager;
        //}
        //E:\Codes\BupMessManagement\Views\AdminShowOrder\Dashboard.cshtml


       
        public IActionResult Index()
        {
            //return View("~/AdminShowOrder/Dashboard");
            // return LocalRedirect("~/AdminShowOrder/Dashboard");
          var frm = HttpContext.Request;
         //   string page = HttpContext.Request.Query["code"].ToString();


     //           MyAuthorize();

            //var pathBase =  String.IsNullOrEmpty(HttpContext.Request.Form["Code"]) &&
            //               String.IsNullOrEmpty(HttpContext.Request.Form["Id_token"]);

            return View("Dashboard");

        }


        public  bool MyAuthorize()
        {
            //return View("~/AdminShowOrder/Dashboard");
            // return LocalRedirect("~/AdminShowOrder/Dashboard");
            var frm = HttpContext.Request;

            //var pathBase =  String.IsNullOrEmpty(HttpContext.Request.Form["Code"]) &&
            //               String.IsNullOrEmpty(HttpContext.Request.Form["Id_token"]);

            return true;

        }

        //public static bool IsRedirectedFromSSO()
        //{
        //    ////var request = IHttpContextAccessor.Htt

        //    ////HttpContext currentContext = HttpContext.Request;
        //    //var result = HttpContext.Request.Form["code"] != null &&
        //    //                    HttpContext.Request.Form["Id_token"] != null &&
        //    //                    HttpContext.Current.Request.Form["Scope"] != null &&
        //    //                    HttpContext.Current.Request.Form["state"] != null;
        //    ////if (result)
        //    ////{
        //    ////    SessionSGD.SaveObjToSession<string>(HttpContext.Current.Request.Form["code"], SessionName.Common_code);
        //    ////    SessionSGD.SaveObjToSession<string>(HttpContext.Current.Request.Form["Id_token"], SessionName.Common_Id_token);
        //    ////    SessionSGD.SaveObjToSession<string>(HttpContext.Current.Request.Form["Scope"], SessionName.Common_Scope);
        //    ////    SessionSGD.SaveObjToSession<string>(HttpContext.Current.Request.Form["state"], SessionName.Common_State);
        //    ////}
        //    //return result;
        //}

        public IActionResult About()
        {
            ViewData["Message"] = "Your application description page.";

            return View();
        }

        public IActionResult Contact()
        {
            ViewData["Message"] = "Your contact page.";

            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        public IActionResult TestLoginDesign()
        {
            return View();
        }



    }
}
