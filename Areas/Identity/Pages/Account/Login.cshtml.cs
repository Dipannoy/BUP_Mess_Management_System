using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Mess_Management_System_Alpha_V2.Data;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;
using Mess_Management_System_Alpha_V2.Models.UCAMModel;
using Mess_Management_System_Alpha_V2.CommonUtilities;
using System.Data;
using System.Data.SqlClient;
using Mess_Management_System_Alpha_V2;
using Microsoft.AspNetCore.Http;
using System.Security.Claims;
using Microsoft.AspNetCore.Authentication.Cookies;

namespace BupMessManagement.Areas.Identity.Pages.Account
{
    [AllowAnonymous]
    public class LoginModel : PageModel
    {
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly ILogger<LoginModel> _logger;

        public LoginModel(SignInManager<ApplicationUser> signInManager, UserManager<ApplicationUser> userManager, ILogger<LoginModel> logger)
        {
            _signInManager = signInManager;
            _userManager = userManager;
            _logger = logger;
        }

        [BindProperty]
        public InputModel Input { get; set; }

        public IList<AuthenticationScheme> ExternalLogins { get; set; }

        public string ReturnUrl { get; set; }

        [TempData]
        public string ErrorMessage { get; set; }
         
        public class InputModel
        {
            [Required]
            [Display(Name = "User Name")]
            public string UserName { get; set; }

            [Required]
            [DataType(DataType.Password)]
            public string Password { get; set; }

            [Display(Name = "Remember me?")]
            public bool RememberMe { get; set; }
        }

        public async Task OnGetAsync(string returnUrl = null)
        {
            if (!string.IsNullOrEmpty(ErrorMessage))
            {
                ModelState.AddModelError(string.Empty, ErrorMessage);
            }

            returnUrl = returnUrl ?? Url.Content("~/");

            // Clear the existing external cookie to ensure a clean login process
            await HttpContext.SignOutAsync(IdentityConstants.ExternalScheme);

            ExternalLogins = (await _signInManager.GetExternalAuthenticationSchemesAsync()).ToList();

            ReturnUrl = returnUrl;
        }

        public async Task<IActionResult> OnPostAsync(string returnUrl = null)
        {
            returnUrl = returnUrl ?? Url.Content("~/");

            if (ModelState.IsValid)
            {
                string userName = Input.UserName;
                string userPassword = Input.Password;
                var result2 = await _signInManager.PasswordSignInAsync(Input.UserName, Input.Password, Input.RememberMe, lockoutOnFailure: true);
                var c = User.Claims.ToList().Count();
                var userNew = await _userManager.FindByNameAsync(userName);
                var claims = new List<Claim> {
                    new Claim(ClaimTypes.Name, "John Doe")
                };

                var identity = new ClaimsIdentity(CookieAuthenticationDefaults.AuthenticationScheme);
                identity.AddClaims(claims);
                ClaimsPrincipal principal = new ClaimsPrincipal(identity);

                await HttpContext.SignInAsync(
                    "ACE_AUTH",
                    principal,
                    new AuthenticationProperties
                    {
                        IsPersistent = true,
                        ExpiresUtc = DateTime.UtcNow.AddDays(7)
                    });

                var loggedInUser = HttpContext.User;



                var userr = await _userManager.GetUserAsync(HttpContext.User);



                UCAMUser uCAMUser = CheckUserIdPasswordFromUCAM(userName, userPassword);

                if (uCAMUser != null)
                {
                    if (uCAMUser.RoleID == 1 || uCAMUser.RoleID == 54)
                    {
                        //if (result.Succeeded)
                        //{
                        //_logger.LogInformation("User logged in.");
                        //return LocalRedirect(returnUrl);

                        var result = await _signInManager.PasswordSignInAsync(Input.UserName, Input.Password, Input.RememberMe, lockoutOnFailure: true);

                       // var user2 = await _userManager.GetUserAsync(User);

                       var user = await _userManager.FindByNameAsync(userName);


                        //var newPassword = _userManager.PasswordHasher.HashPassword(user, "escl.#esclbup##");
                        //user.PasswordHash = newPassword;
                        //var res = await _userManager.UpdateAsync(user);

                        //   string decriptPassword = Utilities.DecryptString(user.Pass);




                        string existingRole = _userManager.GetRolesAsync(user).Result.Single();


                            if (existingRole == "Admin" || existingRole == "MessAdmin")
                            {
                                //return LocalRedirect("~/Admin/IndexHome");
                                return LocalRedirect("~/AdminShowOrder/Dashboard");

                            }

                        //}

                        if (result.RequiresTwoFactor)
                        {
                            return RedirectToPage("./LoginWith2fa", new { ReturnUrl = returnUrl, RememberMe = Input.RememberMe });
                        }
                        if (result.IsLockedOut)
                        {
                            _logger.LogWarning("User account locked out.");
                            return RedirectToPage("./Lockout");
                        }
                        else
                        {
                            ModelState.AddModelError(string.Empty, "Invalid login attempt.");
                            return Page();
                        }


                    }
                    else
                    {
                        string defaultPassword = "Xxx1@gmail.com";

                        var result = await _signInManager.PasswordSignInAsync(Input.UserName, defaultPassword, Input.RememberMe, lockoutOnFailure: true);
                        if (result.Succeeded)
                        {
                            //_logger.LogInformation("User logged in.");
                            //return LocalRedirect(returnUrl);

                            var user = await _userManager.FindByNameAsync(userName);

                            string existingRole = _userManager.GetRolesAsync(user).Result.Single();



                            if (existingRole == "Consumer")
                            {
                                return LocalRedirect("~/Consumer/Index");
                            }

                        }
                        if (result.RequiresTwoFactor)
                        {
                            return RedirectToPage("./LoginWith2fa", new { ReturnUrl = returnUrl, RememberMe = Input.RememberMe });
                        }
                        if (result.IsLockedOut)
                        {
                            _logger.LogWarning("User account locked out.");
                            return RedirectToPage("./Lockout");
                        }
                        else
                        {
                            ModelState.AddModelError(string.Empty, "Invalid login attempt.");
                            return Page();
                        }




                    }
                }
                else
                {
                    ModelState.AddModelError(string.Empty, "Invalid login attempt.");
                    return Page();
                }


            }


            // If we got this far, something failed, redisplay form
            return Page();
        }




        public UCAMUser CheckUserIdPasswordFromUCAM(string userUserid, string userPassword)
        {


            #region Check UserID and Password From UCAM


            //string dPassword = Utilities.EncryptString(userPassword);


            List<SqlParameter> parameterListV3 = new List<SqlParameter>();
            parameterListV3.Add(new SqlParameter { ParameterName = "LoginId", SqlDbType = System.Data.SqlDbType.Text, Value = userUserid });


            string spNameV3 = "CheckUserIdPasswordFromUCAM";

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


            List<UCAMUser> uCAMUserList = new List<UCAMUser>();

            if (dtV3.Rows.Count > 0)
            {

                foreach (DataRow row in dtV3.Rows)
                {
                    UCAMUser tempData = new UCAMUser();

                    tempData.User_ID = Convert.ToInt32(row["User_ID"]);
                    tempData.LogInID = row["LogInID"].ToString();
                    tempData.Password = row["Password"].ToString();

                    tempData.RoleID = Convert.ToInt32(row["RoleID"]);
                    tempData.RoleExistStartDate = Convert.ToDateTime(row["RoleExistStartDate"]);
                    tempData.RoleExistEndDate = Convert.ToDateTime(row["RoleExistEndDate"]);
                    tempData.IsActive = Convert.ToBoolean(row["IsActive"]);
                    tempData.CreatedBy = Convert.ToInt32(row["CreatedBy"]);
                    tempData.CreatedDate = Convert.ToDateTime(row["CreatedDate"]);
                    tempData.ModifiedBy = Convert.ToInt32(row["ModifiedBy"]);
                    tempData.ModifiedDate = Convert.ToDateTime(row["ModifiedDate"]);

                    uCAMUserList.Add(tempData);
                }

            }

            #endregion

            UCAMUser uCAMUser = uCAMUserList.FirstOrDefault();

            UCAMUser uCAMUserReturnResult = new UCAMUser();

            if (uCAMUser != null)
            {
                string decriptPassword = Utilities.DecryptString(uCAMUser.Password);
                if (decriptPassword == userPassword)
                {
                    uCAMUserReturnResult = uCAMUser;
                }
                else
                {
                    uCAMUserReturnResult = null;
                }
            }
            else
            {
                uCAMUserReturnResult = null;
            }


            return uCAMUserReturnResult;
        }



    }
}
