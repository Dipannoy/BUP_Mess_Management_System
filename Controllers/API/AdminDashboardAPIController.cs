using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Mess_Management_System_Alpha_V2.Data;
using Mess_Management_System_Alpha_V2.Models;
using Mess_Management_System_Alpha_V2.Models.MessModels;
using Mess_Management_System_Alpha_V2.Models.ViewModel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.Web.CodeGeneration.Contracts.Messaging;

namespace Mess_Management_System_Alpha_V2.Controllers.API
{
    [Produces("application/json")]
    [Route("api/admin/dashboard/[action]")]
    [ApiController]
    public class AdminDashboardAPIController : ControllerBase
    {
        private readonly ApplicationDbContext _db;
        private UserManager<ApplicationUser> _userManager;
        private RoleManager<IdentityRole> _roleManager;

        public AdminDashboardAPIController(ApplicationDbContext db, UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager)
        {
            _db = db;
            _userManager = userManager;
            _roleManager = roleManager;
        }

        #region Test Post Method Example
        /// <summary>
        /// returns StoreInItem list
        /// </summary>
        /// <returns>A list of StoreInItem <see cref="StoreInItem"/></returns>
        [HttpGet]
        [ActionName("GetSoreInItemList")]
        public List<StoreInItem> GetSoreInItemList()
        {
            return _db.StoreInItem.ToList();
        }

        [Authorize]
        [HttpPost]
        [ActionName("Update")]
        public bool Update(long MealTypeId, bool value)
        {
            //User.Identity.Name
            return true;
        }

        /// <summary>
        /// Adds a new store in item
        /// </summary>
        /// <param name="model">a object of type <see cref="StoreInItemVM"/> StoreInItemVM</param>
        /// <returns>a api return object of type <see cref="APIResultReturnObject"/> APIResultReturnObject  </returns>
        [HttpPost]
        [ActionName("AddNewStoreInItem")]
        public IActionResult AddNewStoreInItem([FromBody] StoreInItemVM model)
        {
            var response = new APIResultReturnObject();
            try
            {
                if (!ModelState.IsValid)
                {
                    response.Status = APIResultStatus.error;
                    var keyErrList = ModelState
                        .ToDictionary(kvp => kvp.Key, kvp => kvp.Value.Errors.Select(e => e.ErrorMessage + " ," + e.Exception.Message).ToArray());
                    foreach (var keyErr in keyErrList.ToList())
                    {
                        response.ErrorList.Add(new APIResultError { Field = Char.ToLowerInvariant(keyErr.Key.ToString().Trim()[0]) + keyErr.Key.ToString().Trim().Substring(1), Message = string.Concat(keyErr.Value) });
                    }
                    return new OkObjectResult(response);
                }
                var duplicateCount = _db.StoreInItem
                    .Count();

                if (duplicateCount > 0)
                {
                    response.ErrorList.Add(new APIResultError { Field = "", Message = "Session exists on selected calendar, unit & year" });
                    response.Status = APIResultStatus.error;
                }
                var duplicateCount2 = _db.StoreInItem
                    .Count();

                if (duplicateCount2 > 0)
                {
                    response.ErrorList.Add(new APIResultError { Field = "code", Message = "Code Exists" });
                    response.Status = APIResultStatus.error;
                }

                var duplicateCount3 = _db.StoreInItem
                    .Count(a => a.Name.Trim().ToLowerInvariant() == model.Name.Trim().ToLowerInvariant());

                if (duplicateCount3 > 0)
                {
                    response.ErrorList.Add(new APIResultError { Field = "name", Message = "Name Exists" });
                    response.Status = APIResultStatus.error;
                }
                if (!response.HasError && !response.HasWarning)
                {
                    _db.StoreInItem
                        .Add(
                        new StoreInItem
                        {    Name = ""
                        });
                    _db.SaveChanges();
                    response.Status = APIResultStatus.success;
                }
            }
            catch (Exception ex)
            {
                response.Status = APIResultStatus.error;
                response.ErrorList.Add(new APIResultError { Field = "", Message = ex.Message });
                return new OkObjectResult(response);
            }
            return new OkObjectResult(response);
        }

        #endregion








        [HttpPost]
        [ActionName("SaveOrderHistory")]
        public async Task<IActionResult> SaveOrderHistory(DateTime SelectedDateT, string UserId, bool IsPreorder, long Mealtypeid, long SetMenuId, long? ExtraItemId, double UnitOrdered)
        {
            var response = new APIResultReturnObject();
            try
            {                
                long? extraStoreoutid = null;
                Double price = 0;
                if (ExtraItemId == null)
                {
                    var setMenu = _db.SetMenu.Where(x => x.Id == SetMenuId).FirstOrDefault();
                    price = Double.Parse(setMenu.SetMenuPrice.ToString()) * UnitOrdered;
                }
                else
                {
                    var soi = _db.ExtraItem.Find(ExtraItemId);
                    extraStoreoutid = soi.StoreOutItemId;
                    price = soi.Price * UnitOrdered;
                }

                var mdl = new Models.MessModels.OrderHistory();
                mdl.UserId = UserId;
                mdl.SetMenuId = SetMenuId;
                mdl.MealTypeId = Mealtypeid;
                mdl.StoreOutItemId = extraStoreoutid;
                mdl.UnitOrdered = UnitOrdered;
                mdl.OrderAmount = price;
                mdl.IsPreOrder = IsPreorder;
                mdl.OrderDate = SelectedDateT;
                mdl.CreatedBy = UserId;
                mdl.CreatedDate = DateTime.Now;
                mdl.LastModifiedDate = DateTime.Now;

                _db.OrderHistory.Add(mdl);
                
                var count = await _db.SaveChangesAsync();
                if (count == 1)
                {
                    response.Status = APIResultStatus.success;
                    return new OkObjectResult(response);
                }

                response.Status = APIResultStatus.error;
                response.ErrorList.Add(new APIResultError { Field = "", Message = count.ToString() });
                return new OkObjectResult(response);
            }
            catch (Exception ex)
            {
                response.Status = APIResultStatus.error;
                response.ErrorList.Add(new APIResultError { Field = "", Message = ex.Message });
                return new OkObjectResult(response);
            }
        }








    }
}