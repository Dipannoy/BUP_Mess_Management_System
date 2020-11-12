using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Mess_Management_System_Alpha_V2.Data;
using Mess_Management_System_Alpha_V2.Models.MessModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;

namespace Mess_Management_System_Alpha_V2.Controllers.Admin
{
    public class SetMenuController : Controller
    {

        private readonly ApplicationDbContext _context;
        private UserManager<ApplicationUser> _userManager;
        private RoleManager<IdentityRole> _roleManager;

        public SetMenuController(ApplicationDbContext context, UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager)
        {
            _context = context;
            _userManager = userManager;
            _roleManager = roleManager;
        }


        // GET: SetMenu
        public ActionResult Index()
        {
            var result = _context.SetMenu.Include(a => a.MealType).Include(x => x.SetMenuDetailsList).Include(x=> x.ExtraItemList).ToList().OrderByDescending(x => x.SetMenuDate);
            return View(result);
        }


        // GET: SetMenu/Create
        public ActionResult Create()
        {
            ViewBag.MealItemList = _context.StoreOutItem.ToList();
            ViewBag.ExtraItemList = _context.StoreOutItem.ToList();
            return View();
        }

        // POST: SetMenu/Create
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

        // GET: SetMenu/Edit/5
        public ActionResult Edit(int id)
        {
            SetMenu result = _context.SetMenu.Include(x => x.SetMenuDetailsList).Include(x => x.ExtraItemList).Where(x=> x.Id == id).FirstOrDefault();

            return View(result);
        }

        //// POST: SetMenu/Edit/5
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public ActionResult Edit(int id, IFormCollection collection)
        //{
        //    try
        //    {
        //        // TODO: Add update logic here

        //        return RedirectToAction(nameof(Index));
        //    }
        //    catch
        //    {
        //        return View();
        //    }
        //}

        //// GET: SetMenu/Delete/5
        //public ActionResult Delete(int id)
        //{
        //    return View();
        //}

        //// POST: SetMenu/Delete/5
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public ActionResult Delete(int id, IFormCollection collection)
        //{
        //    try
        //    {
        //        // TODO: Add delete logic here

        //        return RedirectToAction(nameof(Index));
        //    }
        //    catch
        //    {
        //        return View();
        //    }
        //}




        public class SetMenuIds
        {
            public string SetMenuItemId { set; get; }
        }
      
        public class ExtraItemsTemp
        {
            public string AddedItemId { set; get; }
            public string AddedItemPrice { set; get; }
        }

        [HttpPost]
        public async Task<IActionResult> SaveSetMenuAndAddedItemList(string SetMenuList, string ExtraItemList, string dateTimeT, string categoryIdValue, string SetMenuPrice)
        {

            long setMenuMasterId = -1;
            try
            {
                
                int count = 0;
                var user = await _userManager.GetUserAsync(User);
                var userID = user.Id;
                double TempSetMenuPrice = 0;

                List<SetMenuIds> tempSetMenuList = (List<SetMenuIds>)JsonConvert.DeserializeObject(SetMenuList, typeof(List<SetMenuIds>));

                List<ExtraItemsTemp> tempExtraItemList = (List<ExtraItemsTemp>)JsonConvert.DeserializeObject(ExtraItemList, typeof(List<ExtraItemsTemp>));

                

                //============ Check SetMenu and AddedItem is selected atlist one item ========================
                if ((tempSetMenuList.Count <= 0) && (tempExtraItemList.Count <= 0))
                {
                    return Json(new { success = false, responseText = "Please Check atlist one Item From Items or Extra Items..!!" });
                }



                #region Set Menu Master and Details Insert

                
                if (tempSetMenuList.Count > 0 && tempSetMenuList != null)
                {
                    #region Insert Set Menu Master

                    SetMenu tempData = new SetMenu();
                    TempSetMenuPrice = Convert.ToDouble(SetMenuPrice);

                    tempData.SetMenuDate = Convert.ToDateTime(dateTimeT);
                    tempData.Serial = Convert.ToInt32(categoryIdValue);
                    tempData.IsAvailableAsExtra = false;
                    tempData.SetMenuPrice = Convert.ToDecimal(SetMenuPrice);
                    tempData.MealTypeId = Convert.ToInt32(categoryIdValue);

                    tempData.CreatedBy = userID;
                    tempData.CreatedDate = DateTime.Now;
                    tempData.LastModifiedDate = DateTime.Now;

                    _context.SetMenu.Add(tempData);
                    _context.SaveChanges();
                    setMenuMasterId = tempData.Id;

                    #endregion

                    if (setMenuMasterId > 0)
                    {

                        #region Extra Item Insert

                        
                        
                        if (tempExtraItemList.Count > 0)
                        {
                            #region Insert Extra Items

                            foreach (var data in tempExtraItemList)
                            {
                                ExtraItem et = new ExtraItem();

                                et.Date = Convert.ToDateTime(dateTimeT);
                                et.MenuDate = Convert.ToDateTime(dateTimeT);
                                et.Price = Convert.ToDouble(data.AddedItemPrice);
                                et.SetMenuId = setMenuMasterId;
                                et.StoreOutItemId = Convert.ToInt64(data.AddedItemId);

                                et.CreatedBy = userID;
                                et.CreatedDate = DateTime.Now;
                                et.LastModifiedDate = DateTime.Now;


                                _context.ExtraItem.Add(et);
                                _context.SaveChanges();

                                count = count + 1;

                            }

                            #endregion
                        }

                        #endregion


                        #region Insert Set Menu Details

                        if (tempSetMenuList.Count > 0)
                        {
                            foreach (var data in tempSetMenuList)
                            {
                                SetMenuDetails setMenuDetails = new SetMenuDetails();

                                setMenuDetails.SetMenuId = setMenuMasterId;
                                setMenuDetails.StoreOutItemId = Convert.ToInt64(data.SetMenuItemId);

                                setMenuDetails.CreatedBy = userID;
                                setMenuDetails.CreatedDate = DateTime.Now;
                                setMenuDetails.LastModifiedDate = DateTime.Now;


                                _context.SetMenuDetails.Add(setMenuDetails);
                                _context.SaveChanges();

                                //----------------------------------------------------------------------
                                //Insert the items of setmenu in Extra Item List .It was missing in Previous Version.
                                ExtraItem et = new ExtraItem();

                                et.Date = Convert.ToDateTime(dateTimeT);
                                et.MenuDate = Convert.ToDateTime(dateTimeT);
                                et.Price = TempSetMenuPrice;
                                et.SetMenuId = setMenuMasterId;
                                et.StoreOutItemId = Convert.ToInt64(data.SetMenuItemId);

                                et.CreatedBy = userID;
                                et.CreatedDate = DateTime.Now;
                                et.LastModifiedDate = DateTime.Now;


                                _context.ExtraItem.Add(et);
                                _context.SaveChanges();

                            }
                        }
                        

                        #endregion
                    }


                }

                #endregion

                if (count > 0)
                {
                    return Json(new { success = true, responseText = "Set-Menu and Extra Items Insert Successful." });
                }
                else
                {
                    return Json(new { success = true, responseText = "Set-Menu Insert Successful but Extra Items FAILD to Insert" });
                }

                

            }
            catch (Exception ex)
            {

                //........... Remove ExtraItem list.................
                List<ExtraItem> eiL = _context.ExtraItem.Where(x => x.SetMenuId == setMenuMasterId).ToList();
                if (eiL.Count > 0)
                {
                    foreach(var ei in eiL)
                    {
                        _context.ExtraItem.Remove(ei);
                        _context.SaveChanges();
                    }
                }

                //........... Remove SetMenuDetails list.................
                List<SetMenuDetails> smdL = _context.SetMenuDetails.Where(x => x.SetMenuId == setMenuMasterId).ToList();
                if (smdL.Count > 0)
                {
                    foreach (var smd in smdL)
                    {
                        _context.SetMenuDetails.Remove(smd);
                        _context.SaveChanges();
                    }
                }

                //........... Remove SetMenu ................
                SetMenu sm = _context.SetMenu.Where(x => x.Id == setMenuMasterId).FirstOrDefault();
                _context.SetMenu.Remove(sm);
                _context.SaveChanges();

                return Json(new { success = false, responseText = "Data Insert Failed." + " " + ex.Message });
            }

        }







        [HttpPost]
        public async Task<IActionResult> UpdateSetMenuAndAddedItemList(string SetMenuList, string ExtraItemList, string SetMenuId,string SetMenuPrice)
        {
            try
            {
                int count = 0;
                var user = await _userManager.GetUserAsync(User);
                var userID = user.Id;

                long setMenuIdT = Convert.ToInt64(SetMenuId);
                decimal price = Convert.ToDecimal(SetMenuPrice);

                List<SetMenuIds> tempSetMenuList = (List<SetMenuIds>)JsonConvert.DeserializeObject(SetMenuList, typeof(List<SetMenuIds>));
                List<ExtraItemsTemp> tempExtraItemList = (List<ExtraItemsTemp>)JsonConvert.DeserializeObject(ExtraItemList, typeof(List<ExtraItemsTemp>));

                var existSetMenu = _context.SetMenu.Where(x=> x.Id == setMenuIdT).FirstOrDefault();
                var existSetMenuDetails = _context.SetMenuDetails.Where(x=> x.SetMenuId == existSetMenu.Id).ToList();
                var existExtraItem = _context.ExtraItem.Where(x=> x.SetMenuId == setMenuIdT && x.Date.ToShortDateString() == existSetMenu.SetMenuDate.ToShortDateString()).ToList();
                var existOrderHistorySetMenu = _context.OrderHistory.Where(x => x.SetMenuId == setMenuIdT).ToList();

                if(existOrderHistorySetMenu != null)
                {
                    foreach(var item in existOrderHistorySetMenu)
                    {
                        if(item.UnitOrdered == 1)
                        {
                            item.OrderAmount = (double)price;
                            _context.Update(item);
                            _context.SaveChanges();
                        }
                        else
                        {
                            item.OrderAmount = (double)price * item.UnitOrdered;
                            _context.Update(item);
                            _context.SaveChanges();
                        }
                    }
                }


                if (existSetMenu != null)
                {
                    existSetMenu.SetMenuPrice = Convert.ToDecimal(SetMenuPrice);

                    _context.Update(existSetMenu);
                    _context.SaveChanges();


                    //return true;
                }
                foreach (var dataSetMenuDetails in existSetMenuDetails)
                {
                    var tempData = tempSetMenuList.Where(x => x.SetMenuItemId == dataSetMenuDetails.StoreOutItemId.ToString()).FirstOrDefault();
                    if (tempData == null)
                    {

                    }
                }




                return Json(new { success = true, responseText = "Data has been updated successfully" });

            }
            catch (Exception ex)
            {

                

                return Json(new { success = false, responseText = "Data Update Failed." + " " + ex.Message });
            }

        }






    }
}