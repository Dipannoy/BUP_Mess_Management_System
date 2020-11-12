using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Mess_Management_System_Alpha_V2.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Mess_Management_System_Alpha_V2.Controllers.API
{
    [Authorize]
    [Produces("application/json")]
    [Route("api/consumer/dashboard/[action]")]
    public class PreOrderScheduleAPIController : ControllerBase
    {


        private readonly ApplicationDbContext _db;
        private UserManager<ApplicationUser> _userManager;
        private RoleManager<IdentityRole> _roleManager;

        public PreOrderScheduleAPIController(ApplicationDbContext context, UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager)
        {
            _db = context;
            _userManager = userManager;
            _roleManager = roleManager;
        }



        [HttpPost]
        [ActionName("PreOrderConfigUpdate")]
        public async Task<bool> PreOrderConfigUpdate(long MealTypeId, bool value)
        {
            try
            {
                var user = await _userManager.GetUserAsync(User);
                var userID = user.Id;
                //var setMenuTomorrow = _db.SetMenu.Where(x => x.SetMenuDate.ToShortDateString() == DateTime.Now.AddDays(1).ToShortDateString() && x.MealTypeId == MealTypeId);

                var existingEntry = _db.PreOrderSchedule.Where(a => a.UserId == userID && a.LastConfigurationUpdateDate.ToShortDateString()== DateTime.Now.ToShortDateString() && a.MealTypeId == MealTypeId).FirstOrDefault();
                if (existingEntry != null)
                {
                    existingEntry.IsPreOrderSet = value;
                    existingEntry.LastConfigurationUpdateDate = DateTime.Now;
                    existingEntry.LastModifiedDate = DateTime.Now;
                    existingEntry.LastModifiedBy = userID;
                    //existingEntry.SetMenuId = setMenuTomorrow.FirstOrDefault().Id;
                    _db.Update(existingEntry);
                    _db.SaveChanges();
                    return true;
                }
                else
                {

                    _db.PreOrderSchedule.Add(new Models.MessModels.PreOrderSchedule
                    {
                        UserId = userID,
                        IsPreOrderSet = value,
                        MealTypeId = MealTypeId,
                        LastConfigurationUpdateDate = DateTime.Now,
                        CreatedDate = DateTime.Now,
                        LastModifiedDate = DateTime.Now,
                        CreatedBy = userID,
                        LastModifiedBy = userID,
                        //SetMenuId = setMenuTomorrow.FirstOrDefault().Id

                });

                    _db.SaveChanges();
                    return true;
                }
            } catch(Exception ex)
            {
                return false;
            }
        }


        
    }
}
