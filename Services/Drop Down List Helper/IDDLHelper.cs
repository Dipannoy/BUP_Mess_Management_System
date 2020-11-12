using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Mess_Management_System_Alpha_V2.Services.Drop_Down_List_Helper
{
    public partial interface IDDLHelper
    {
        /// <summary>
        /// Get Current Year List
        /// Takes no parameters
        /// </summary>
        /// <returns>List of Recent Years</returns>
        Dictionary<string, string> GetYears();


        /// <summary>
        /// Gettig all Sotre Unit Types
        /// Takes no parameters
        /// </summary>
        /// <returns>List of Store Unit Types</returns>
        Dictionary<string, string> GetUnitTypes();



        /// <summary>
        /// Gettig all StoreIn Item Category
        /// Takes no parameters
        /// </summary>
        /// <returns>List of StoreIn Item Category</returns>
        Dictionary<string, string> GetStoreInItemCategory();


        /// <summary>
        /// Gettig all StoreOut Item Category
        /// Takes no parameters
        /// </summary>
        /// <returns>List of StoreOut Item Category</returns>
        Dictionary<string, string> GetStoreOutItemCategory();




        /// <summary>
        /// Gettig all StoreIn Item 
        /// Takes no parameters
        /// </summary>
        /// <returns>List of StoreIn Item </returns>
        Dictionary<string, string> GetStoreInItem();


        /// <summary>
        /// Gettig all StoreOut Item 
        /// Takes no parameters
        /// </summary>
        /// <returns>List of StoreOut Item </returns>
        Dictionary<string, string> GetStoreOutItem();



        /// <summary>
        /// Gettig all Meal Type
        /// Takes no parameters
        /// </summary>
        /// <returns>List of Meal Type</returns>
        Dictionary<string, string> GetMealType();

        /// <summary>
        /// 
        /// </summary>
        /// <param name="SetMenuId"></param>
        /// <param name="MealTypeId"></param>
        /// <returns></returns>
        Dictionary<string, string> GetExtraItemList(long SetMenuId, long MealTypeId);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="SetMenuId"></param>
        /// <param name="MealTypeId"></param>
        /// <returns></returns>
        Dictionary<string, string> GetAllUsersList();

    }
}
