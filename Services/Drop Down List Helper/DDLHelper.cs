using Mess_Management_System_Alpha_V2.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;

namespace Mess_Management_System_Alpha_V2.Services.Drop_Down_List_Helper
{
    public partial class DDLHelper : IDDLHelper
    {
        private readonly ApplicationDbContext _db;
        //private UserManager<ApplicationUser> _userManager;
        //public DDLHelper(ApplicationDbContext db, UserManager<ApplicationUser> userManager)
        //{
        //    _db = db;
        //    //_userManager = userManager;
        //}
        public DDLHelper(ApplicationDbContext db)
        {
            _db = db;
            //_userManager = userManager;
        }
        /// <summary>
        /// Implemention of GetYears()
        /// </summary>
        /// <param name="a">the left side operand.</param> 
        /// <returns>returns all the years from year 2000 to current year and 2 advance years</returns>
        public Dictionary<string, string> GetYears()
        {
            var list = new Dictionary<string, string>();
            Enumerable.Range(2000, (DateTime.Now.Year - 2000 + 2))
                .OrderByDescending(a => a)
                .ToList()
                .ForEach(a => list.Add(a.ToString(), a.ToString()));
            return list;
        }




        /// <summary>
        /// Gettig all Sotre Unit Types
        /// Takes no parameters
        /// </summary>
        /// <returns>returns all UnitTypes</returns>
        public Dictionary<string, string> GetUnitTypes()
        {
            var list = new Dictionary<string, string>();
            _db.UnitType
                .ToList()
                .ForEach(a => list.Add(a.Id.ToString(), a.Name.ToString()));
            return list;
        }


        /// <summary>
        /// Gettig all StoreIn Item Category
        /// Takes no parameters
        /// </summary>
        /// <returns>returns all StoreInItemCategory</returns>
        public Dictionary<string, string> GetStoreInItemCategory()
        {
            var list = new Dictionary<string, string>();
            _db.StoreInItemCategory
                .ToList()
                .ForEach(a => list.Add(a.Id.ToString(), a.Name.ToString()));
            return list;
        }

        public Dictionary<string, string> GetStoreOutItemOnDay(DateTime date)
        {
            var ItemOrderHistoryOnDay = _db.OrderHistory.Where(x =>  x.StoreOutItemId != null && x.OrderDate.ToShortDateString() == date.ToShortDateString()).Select(x => x.StoreOutItemId).Distinct().ToList();
            var list = new Dictionary<string, string>();

            foreach (var item in ItemOrderHistoryOnDay)
            {
                var storeOutitm = _db.StoreOutItem.Where(x => x.Id == item).FirstOrDefault();
                list.Add(storeOutitm.Id.ToString(), storeOutitm.Name);
            }
      
            return list;
        }



        /// <summary>
        /// Gettig all StoreOut Item Category
        /// Takes no parameters
        /// </summary>
        /// <returns>returns all SStoreOutItemCategory</returns>
        public Dictionary<string, string> GetStoreOutItemCategory()
        {
            var list = new Dictionary<string, string>();
            _db.StoreOutItemCategory
                .ToList()
                .ForEach(a => list.Add(a.Id.ToString(), a.Name.ToString()));
            return list;
        }




        /// <summary>
        /// Gettig all StoreIn Item 
        /// Takes no parameters
        /// </summary>
        /// <returns>returns all StoreInItem</returns>
        public Dictionary<string, string> GetStoreInItem()
        {
            var list = new Dictionary<string, string>();
            _db.StoreInItem
                .Include(x => x.UnitType)
                .Include(x => x.StoreInItemCategory)
                .ToList()
                .ForEach(a => list.Add(a.Id.ToString(), a.Name.ToString()));
            return list;
        }



        /// <summary>
        /// Gettig all StoreOut Item 
        /// Takes no parameters
        /// </summary>
        /// <returns>returns all StoreOutItem</returns>
        public Dictionary<string, string> GetStoreOutItem()
        {
            var list = new Dictionary<string, string>();
            _db.StoreOutItem.Where(x=>x.IsOpen == true)
                .OrderBy(x=>x.Name)
                .Include(x=> x.UnitType)
                .Include(x => x.StoreOutItemCategory)
                .ToList()
                .ForEach(a => list.Add(a.Id.ToString(), a.Name.ToString()));

            //Array.Sort(list, (x, y) => String.Compare(x.Name, y.Name));
            //list.so
            return list;
        }


        /// <summary>
        /// Gettig all StoreOut Item 
        /// Takes no parameters
        /// </summary>
        /// <returns>returns all StoreOutItem</returns>
        public Dictionary<string, string> GetMealType()
        {
            var list = new Dictionary<string, string>();
            _db.MealType
                .OrderBy(x=> x.Serial)
                .ToList()
                .ForEach(a => list.Add(a.Id.ToString(), a.Name.ToString()));
            return list;
        }


        public Dictionary<string, string> GetExtraItemList(long SetMenuId, long MealTypeId)
        {
            var list = new Dictionary<string, string>();
            _db.ExtraItem.Where(a=>a.SetMenuId == SetMenuId && a.SetMenu.MealTypeId == MealTypeId)
                .Include(a=>a.StoreOutItem)
                .ToList()
                .ForEach(a => list.Add(a.Id.ToString(), a.StoreOutItem.Name.ToString() +"(" + a.Price + " Tk.)"));
            return list;
        }


        public Dictionary<string, string> GetAllUsersList()
        {
            var list = new Dictionary<string, string>();
            try
            {
              //var a =  _userManager.Users
              //      .OrderBy(x => x.BUPUser_ID)
              //      .ToList();

              //  foreach(var i in a)
              //  {
              //      list.Add(i.Id.ToString(), i.BUPFullName.ToString());
              //  }
                    //.ForEach(a => list.Add(a.Id.ToString(), a.BUPFullName.ToString()));
            }
            catch(Exception ex)
            {

            }
            return list;
        }

    }
}
