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
using Microsoft.AspNetCore.Hosting;

using System.Globalization;
using Microsoft.AspNetCore.Authorization;
using System.Collections;
using System.IO;
using BupMessManagement.Email;


// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Mess_Management_System_Alpha_V2.Controllers.Admin
{
    //[Authorize(Roles = "Admin,MessAdmin")]


    public class AdminBillGenerateController : Controller
    {
        // GET: /<controller>/

        private readonly ApplicationDbContext _context;
        private UserManager<ApplicationUser> _userManager;
        private RoleManager<UserIdentityRole> _roleManager;
        private IHostingEnvironment _env;
        private readonly IEmailService _Mailer;


        public AdminBillGenerateController(ApplicationDbContext context, UserManager<ApplicationUser> userManager, 
            RoleManager<UserIdentityRole> roleManager, IHostingEnvironment env, IEmailService Mailer)
        {
            _context = context;
            _userManager = userManager;
            _roleManager = roleManager;
            _env = env;
            _Mailer = Mailer;
        }


        public async Task<IActionResult> Index()
        {
            if (SessionExist())
            {
                if (await UserExistMess())
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

        public async Task<IActionResult> BillPayment()
        {
            if (SessionExist())
            {
                if (await UserExistMess())
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


        public async Task<IActionResult> ConsumerPaymentSlip()
        {
            if (SessionExist())
            {
                if (await UserExistMess())
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

        public async Task<IActionResult> ConsumerPaymentHistory()
        {
            if (SessionExist())
            {
                if (await UserExistMess())
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


        [HttpPost]
        public async Task<JsonResult> ConfirmPayment(string BillParent, string Method, string Mobile, string TransId,
                                                       string Bank, string Account, string Receiver, string Amount, string Due,
                                                       string File, string FileExtension, string LinkUrl, string PaymentDate)
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

             
                if(method == 1 || method == 2)
                {
                    string[] DateFormatarray = PaymentDate.Split("-");

                    string datesv = DateFormatarray[1] + '/' + DateFormatarray[0] + '/' + DateFormatarray[2];
                    pmntDate = DateTime.ParseExact(datesv, "M/d/yyyy", CultureInfo.InvariantCulture);

                }
                var billParentObj = _context.ConsumerBillParent.Where(x => x.Id == long.Parse(BillParent)).FirstOrDefault();
                long paymentInfoId = 0;
                string dateString = DateTime.Now.Month.ToString() +"_"+ DateTime.Now.Year.ToString();
                string fileName = billParentObj.Id.ToString()+ "_" + dateString + "." + FileExtension;

                UploadFile("~/Uploads/", fileName, File);

                if (method == 1)
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
                else if (method == 2)
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
                    ConsumerPaymentInfo cpi = new ConsumerPaymentInfo();
                    cpi.CreatedDate = DateTime.Now;
                    cpi.LastModifiedDate = DateTime.Now;
                    cpi.CreatedBy = UserId;
                    cpi.LastModifiedBy = UserId;
                    cpi.PaymentMethodId = method;

                    cpi.ConsumerBillParentId = billParentObj.Id;
                    cpi.IsActive = true;

                    _context.ConsumerPaymentInfo.Add(cpi);
                    _context.SaveChanges();
                    paymentInfoId = cpi.Id;
                }

                //var billHistoryObj = _context.ConsumerBillHistory.Where(x => x.ConsumerBillParentId == billParentObj.Id).LastOrDefault();
                //if(billHistoryObj == null)
                //{
                    ConsumerBillHistory cbh = new ConsumerBillHistory();
                    cbh.ConsumerBillParentId = billParentObj.Id;
                    cbh.ConsumerPaymentInfoId = paymentInfoId;
                    cbh.PaymentDate = pmntDate;
                    cbh.Attachment = fileName;
                    cbh.PaymentAmount = double.Parse(Amount);
                    cbh.Due = double.Parse(Due) - double.Parse(Amount);
                    cbh.IsPartial = double.Parse(Due) - double.Parse(Amount) > 0 ? true : false;
                    cbh.ReceivedBy = String.IsNullOrEmpty(Receiver) ? null : Receiver;

                    _context.ConsumerBillHistory.Add(cbh);
                    _context.SaveChanges();

                var consm = _userManager.Users.Where(x => x.Id == billParentObj.UserId).FirstOrDefault();

                //}
                EmailAddress From = new EmailAddress();
                From.Name = "DIPANNOY DAS GUPTA";
                From.Address = "dipannoydip@gmail.com";
                EmailAddress To = new EmailAddress();
                To.Name = consm.BUPFullName;
                To.Address = "tuhin@bup.edu.bd";
                EmailMessage em = new EmailMessage();
                em.FromAddresses.Add(From);
                em.ToAddresses.Add(To);
                em.Subject = "Mess Payment";
                em.Content = "You have paid " + Amount + " Tk. Current Due-" + (double.Parse(Due) - double.Parse(Amount)).ToString() + " Tk";
                _Mailer.Send(em);

            }
            catch(Exception ex)
            {
                return Json(new { success = false, responseText = ex.Message });

            }
            return Json(new { success = true, responseText = "Payment is successful" });

        }


        public  void UploadFile(string fullDirectoryPath, string fullname, string base64BinaryString)
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


        public string GetPaymentInfoEdit(string ParentId, string ChildId)
        {
            var billParentObj = _context.ConsumerBillParent.Where(x => x.Id == long.Parse(ParentId)).ToList();
            var childObj = _context.ConsumerBillHistory.Where(x => x.Id == long.Parse(ChildId)).ToList();
            var paymentObj = _context.ConsumerPaymentInfo.Where(x => x.Id == childObj.ElementAt(0).ConsumerPaymentInfoId).ToList();
            var methodObj = _context.PaymentMethod.Where(x => x.Id == paymentObj.ElementAt(0).PaymentMethodId).ToList();
            List<IList> paymentList = new List<IList>();
            
            paymentList.Add(billParentObj);
            paymentList.Add(childObj);
            paymentList.Add(paymentObj);
            paymentList.Add(methodObj);
            string json = JsonConvert.SerializeObject(paymentList, Formatting.Indented, new JsonSerializerSettings
            {
                ReferenceLoopHandling = ReferenceLoopHandling.Ignore
            });
            //return Json(paymentList);
            return json;


        }

        [HttpPost]
        public async Task<JsonResult> ConfirmEdit(string BillHistory, string Method, string Mobile, string TransId,
                                                  string Bank, string Account, string Receiver, string Amount,
                                                  string PaymentDate)
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
                double newAmount = double.Parse(Amount);
                double adjustAmount = 0;
                DateTime pmntDate = DateTime.Now;


                var historyObj = _context.ConsumerBillHistory.Where(x => x.Id == long.Parse(BillHistory)).FirstOrDefault();
                var paymentInfoObj = _context.ConsumerPaymentInfo.Where(x => x.Id == historyObj.ConsumerPaymentInfoId).FirstOrDefault();  
                string[] DateFormatarray = PaymentDate.Split("-");

                    string datesv = DateFormatarray[1] + '/' + DateFormatarray[0] + '/' + DateFormatarray[2];
                    pmntDate = DateTime.ParseExact(datesv, "M/d/yyyy", CultureInfo.InvariantCulture);

                if(historyObj.PaymentAmount >= newAmount)
                {
                    newAmount = historyObj.PaymentAmount - newAmount;
                }
                else
                {
                    newAmount = -(newAmount- historyObj.PaymentAmount);
                }
                //var billParentObj = _context.ConsumerBillParent.Where(x => x.Id == long.Parse(BillParent)).FirstOrDefault();
                //long paymentInfoId = 0;
                //string dateString = DateTime.Now.Month.ToString() + "_" + DateTime.Now.Year.ToString();
                //string fileName = billParentObj.Id.ToString() + "_" + dateString + "." + FileExtension;

                //UploadFile("~/Uploads/", fileName, File);

                if (method == 1)
                {

                    paymentInfoObj.MobileNumber = Mobile;
                    paymentInfoObj.TransactionID = TransId;
                    paymentInfoObj.LastModifiedBy = UserId;
                    paymentInfoObj.LastModifiedDate = DateTime.Now;
                    _context.ConsumerPaymentInfo.Update(paymentInfoObj);
                    _context.SaveChanges();
                }
                else if (method == 2)
                {

                    paymentInfoObj.BankName = Bank;
                    paymentInfoObj.AccountNumber = Account;
                    paymentInfoObj.LastModifiedBy = UserId;
                    paymentInfoObj.LastModifiedDate = DateTime.Now;
                    _context.ConsumerPaymentInfo.Update(paymentInfoObj);
                    _context.SaveChanges();
                }
                else
                {
                   
                }
                historyObj.PaymentAmount = double.Parse(Amount);
                historyObj.PaymentDate = pmntDate;
                historyObj.LastModifiedBy = UserId;
                historyObj.LastModifiedDate = DateTime.Now;
                historyObj.Due = historyObj.Due + adjustAmount;
                historyObj.IsPartial = historyObj.Due + adjustAmount > 0 ? true : false;
                historyObj.ReceivedBy = Receiver;
                _context.ConsumerBillHistory.Update(historyObj);
                _context.SaveChanges();
                var paymentList = _context.ConsumerBillHistory.Where(x => x.ConsumerBillParentId == historyObj.ConsumerBillParentId && x.Id > historyObj.Id).ToList();
                foreach(var i in paymentList)
                {
                    i.Due = i.Due + adjustAmount;
                    i.IsPartial = i.Due + adjustAmount > 0 ? true : false;
                    _context.ConsumerBillHistory.Update(i);
                    _context.SaveChanges();
                }
           
                //}

            }
            catch (Exception ex)
            {
                return Json(new { success = false, responseText = ex.Message });

            }
            return Json(new { success = true, responseText = "Update is successful" });

        }




        public JsonResult GetPersonBill(string PersonName,string Month,string Year)
        {
            List<IList> BillList = new List<IList>();
            List<AccessoryBill> ACBill = new List<AccessoryBill>();
            List<Double> ArrearBill = new List<Double>();
            var personArray = PersonName.Split("-");
            var prsndtl = _userManager.Users.Where(x => x.BUPNumber == personArray[1]).FirstOrDefault();
            //var prsndtl = _userManager.Users.Where(x => x.BUPFullName == PersonName).FirstOrDefault();
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
            cmbr.Year = Int32.Parse(Year);
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

        public string GetPaymentInfo(string ParentId)
        {
            List<ConsumerPaymentInfo> paymentInfoList = new List<ConsumerPaymentInfo>();
            List<ConsumerBillParent> billParentList = new List<ConsumerBillParent>();
            List<IList> full = new List<IList>();

            List<PaymentMethod> paymentMethodList = _context.PaymentMethod.Where(x => x.Id > 0).ToList();
            var billParentObj = _context.ConsumerBillParent.Where(x => x.Id == long.Parse(ParentId)).FirstOrDefault();
            List<ApplicationUser> userObj = _userManager.Users.Where(x => x.Id == billParentObj.UserId).ToList();

            if (billParentObj != null)
            {
                billParentList.Add(billParentObj);
                paymentInfoList = _context.ConsumerPaymentInfo.Where(x => x.ConsumerBillParentId == billParentObj.Id).ToList();
            }

            full.Add(paymentMethodList);
            full.Add(billParentList);
            full.Add(paymentInfoList);
            full.Add(userObj);

            string json = JsonConvert.SerializeObject(full, Formatting.Indented, new JsonSerializerSettings
            {
                ReferenceLoopHandling = ReferenceLoopHandling.Ignore
            });
            return json;
        }


        [HttpPost]

        public async Task<IActionResult> GetBillStatus()
        {
            string user = Request.Form["stperson"];
            var personArray = user.Split("-");
            var prsndtl = _userManager.Users.Where(x => x.BUPNumber == personArray[1]).FirstOrDefault();

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


            return View("Index");

        }

        [HttpPost]

        public async Task<IActionResult> GetBillStatus2()
        {
            string user = Request.Form["stperson2"];

            var personArray = user.Split("-");
            var prsndtl = _userManager.Users.Where(x => x.BUPNumber == personArray[1]).FirstOrDefault();
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


            return View("Index");

        }

        [HttpPost]

        public async Task<IActionResult> ViewBill()
        {
            if (SessionExist())
            {
                if (await UserExistMess())
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
                    if (to != "")
                    {
                        string too = Request.Form["to"];
                        string[] DateFormatarray = too.Split("-");
                        string dateto = DateFormatarray[1] + '/' + DateFormatarray[0] + '/' + DateFormatarray[2];
                        Todate = DateTime.ParseExact(dateto, "M/d/yyyy", CultureInfo.InvariantCulture);
                    }
                    //var user = Request.Form["person2"].ToString();

                    string usr = Request.Form["person2"];
                    var personArray = usr.Split("-");
                    var prsndtl = _userManager.Users.Where(x => x.BUPNumber == personArray[1]).FirstOrDefault();
                    //var prsndtl = _userManager.Users.Where(x => x.BUPFullName == usr).FirstOrDefault();

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


                        return View("Index");

                    }
                    else
                    {

                        ViewBag.Form = fromdate;
                        ViewBag.To = Todate;

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



                        return View("Index");

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
            



            

          //  return View("Index");
        }

        public async Task<string> GetOrderDetails(string Day, string Month, string Year, string Type, string BUPNo)
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
                var consumer = _userManager.Users.Where(x => x.BUPNumber == BUPNo).FirstOrDefault();
                ordList = _context.OrderHistoryVr2.Where(x => x.UserId == consumer.Id && x.OrderDate.Date == ordDate.Date && x.MealTypeId == Int32.Parse(Type)).ToList();
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
            if (SessionExist())
            {
                if (await UserExistMess())
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
                    var personArray = usr.Split("-");
                    var prsndtl = _userManager.Users.Where(x => x.BUPNumber == personArray[1]).FirstOrDefault();

                    //var prsndtl = _userManager.Users.Where(x => x.BUPFullName == usr).FirstOrDefault();

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

                        return View("Index");

                    }
                    else
                    {

                        ViewBag.Form = fromdate;
                        ViewBag.To = Todate;

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

                        return View("Index");




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
            var PersonList = _userManager.Users.Where(x => x.BUPFullName.Contains(search) || x.OfficeName.Contains(search) || x.BUPNumber.Contains(search)).ToList();

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
                //var existingEntry = _context.OrderHistoryVr2.Where(a => a.OrderDate.ToShortDateString() == DateTime.Now.ToShortDateString()).ToList();

                //foreach(var i in existingEntry)
                //{
                //_context.OrderHistoryVr2.Remove(i);
                //_context.SaveChanges();
                //}
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
                                if (Convert.ToInt32(row["New"]) > 0 && Convert.ToInt32(row["Modified"]) > 0)

                                {
                                msg = "Today's bill has been generated and updated successfully";
                                    //return Json(new { success = true, responseText = "Order has been generated successfully for this meal." });

                                }
                                else if(Convert.ToInt32(row["New"]) > 0)
                                {
                                    msg = "Today's bill has been generated successfully";

                                }
                                else if (Convert.ToInt32(row["Modified"]) > 0)
                                {
                                    msg = "Today's bill has been updated successfully";

                                }
                                 else
                                {
                                msg = "No bill is generated and updated";
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

        public async Task<UserIdentityRole> GetLogInUserRoleObjectAsync()
        {
            string usrName = SessionExtensions.GetString(HttpContext.Session, "user");
            var user = await _userManager.FindByNameAsync(usrName);
            var userrole = _context.UserRoles.Where(x => x.UserId == user.Id).FirstOrDefault();

            var role = await _roleManager.FindByIdAsync(userrole.RoleId);

            return role;
        }
    }
}

