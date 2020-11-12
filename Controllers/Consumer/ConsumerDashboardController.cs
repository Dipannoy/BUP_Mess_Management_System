using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Mess_Management_System_Alpha_V2.Data;
using Mess_Management_System_Alpha_V2.Models.MessModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;

namespace Mess_Management_System_Alpha_V2.Controllers.Consumer
{
    public class ConsumerDashboardController : Controller
    {
        private readonly ApplicationDbContext _context;
        private UserManager<ApplicationUser> _userManager;
        private RoleManager<IdentityRole> _roleManager;

        public ConsumerDashboardController(ApplicationDbContext context, UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager)
        {
            _context = context;
            _userManager = userManager;
            _roleManager = roleManager;
        }


        public IActionResult Index(string massage)
        {
            ViewBag.msg = massage;

            return View();
        }




        public class FormExtraItems
        {
            public long SetMenuId { get; set; }
            public long MealTypeId { get; set; }
            public long ExtraItemId { get; set; }
            public int ExtraItemOrderQuantity { get; set; }
            public int ExtraSetMenuOrderQuantity { get; set; }

        }

        [HttpPost]
        [ValidateAntiForgeryToken]

        public async Task<IActionResult> SaveExtraSetMenu(int SetMenuId,int MealTypeId,string date)
        {
            var Date = DateTime.Parse(date);
            var user = await _userManager.GetUserAsync(User);
            var userID = user.Id;
            OrderHistory oh = new OrderHistory();
            oh.UserId = userID;
            oh.SetMenuId = SetMenuId;
            oh.MealTypeId = MealTypeId;
      
            oh.IsPreOrder = true;
            oh.OrderDate = Date;

            oh.CreatedBy = userID;
            oh.CreatedDate = DateTime.Now;
            oh.LastModifiedDate = DateTime.Now;


            _context.OrderHistory.Add(oh);
            _context.SaveChanges();
            long orderHistoryId = oh.Id;

            if (orderHistoryId < 0)
            {
                return RedirectToAction("Index", new { massage = "Failed" });
            }

            return RedirectToAction(nameof(Index));

        }
        // POST: Menu/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> SaveExtraOrderItems(FormExtraItems model)
        {
            try
            {

                if (!(model.ExtraItemId > 0))
                {
                    return RedirectToAction(nameof(Index));
                }


                var user = await _userManager.GetUserAsync(User);
                var userID = user.Id;
                
                ExtraItem ei = _context.ExtraItem.Include(x => x.StoreOutItem).Where(x => x.Id == model.ExtraItemId).FirstOrDefault();
                SetMenu sm = _context.SetMenu.Find(model.SetMenuId);


                OrderHistory oh = new OrderHistory();
                oh.UserId = userID;
                oh.SetMenuId = model.SetMenuId;
                oh.MealTypeId = model.MealTypeId;
                oh.StoreOutItemId = ei.StoreOutItemId;
                oh.UnitOrdered = model.ExtraItemOrderQuantity;
                oh.OrderAmount = Convert.ToDouble(ei.Price * model.ExtraItemOrderQuantity);
                oh.IsPreOrder = true;
                oh.OrderDate = sm.SetMenuDate;

                oh.CreatedBy = userID;
                oh.CreatedDate = DateTime.Now;
                oh.LastModifiedDate = DateTime.Now;


                _context.OrderHistory.Add(oh);
                _context.SaveChanges();
                long orderHistoryId = oh.Id;

                if (orderHistoryId < 0)
                {
                    return RedirectToAction("Index", new { massage = "Failed" });
                }

                return RedirectToAction(nameof(Index));

            }
            catch
            {
                return View();
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> SaveExtraOrderSetMenu(FormExtraItems model)
        {
            try
            {

                //if (!(model.ExtraItemId > 0))
                //{
                //    return RedirectToAction(nameof(Index));
                //}


                var user = await _userManager.GetUserAsync(User);
                var userID = user.Id;

                ExtraItem ei = _context.ExtraItem.Include(x => x.StoreOutItem).Where(x => x.Id == model.ExtraItemId).FirstOrDefault();
                SetMenu sm = _context.SetMenu.Find(model.SetMenuId);


                OrderHistory oh = new OrderHistory();
                oh.UserId = userID;
                oh.SetMenuId = model.SetMenuId;
                oh.MealTypeId = model.MealTypeId;
                //oh.StoreOutItemId = ei.StoreOutItemId;
                oh.UnitOrdered = model.ExtraSetMenuOrderQuantity;
                oh.OrderAmount = Convert.ToDouble(sm.SetMenuPrice * model.ExtraSetMenuOrderQuantity);
                oh.IsPreOrder = true;
                oh.OrderDate = sm.SetMenuDate;

                oh.CreatedBy = userID;
                oh.CreatedDate = DateTime.Now;
                oh.LastModifiedDate = DateTime.Now;


                _context.OrderHistory.Add(oh);
                _context.SaveChanges();
                long orderHistoryId = oh.Id;

                if (orderHistoryId < 0)
                {
                    return RedirectToAction("Index", new { massage = "Failed" });
                }

                return RedirectToAction(nameof(Index));

            }
            catch
            {
                return View();
            }
        }



        public class FormDeleteOrderHistory
        {
            public long OrderHistoryId { get; set; }
        }

        // POST: StoreInItemCategory/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteOrderHistory(FormDeleteOrderHistory model)
        {
            try
            {
                OrderHistory oh = _context.OrderHistory.Where(x => x.Id == model.OrderHistoryId).FirstOrDefault();

                _context.OrderHistory.Remove(oh);
                _context.SaveChanges();

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }




    }
}