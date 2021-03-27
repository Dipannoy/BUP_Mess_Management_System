using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Mess_Management_System_Alpha_V2.Models.MessModels
{
    public partial class StoreOutItem : BaseClass
    {
        public string Name { get; set; }
        public bool IsOpen { get; set; }
        public double MinimumProductionUnit { get; set; }
        public double MinimumProductionUnitMultiplier { get; set; }

        public int ? Day { get; set; }
        public int ? MealTypeId { get; set; }
        public double ? Price { get; set; }

        [ForeignKey("UnitType")]
        public long UnitTypeId { get; set; }
        public virtual UnitType UnitType { get; set; }

        [ForeignKey("StoreOutItemCategory")]
        public long StoreOutCategoryId { get; set; }
        public virtual StoreOutItemCategory StoreOutItemCategory { get; set; }

        public virtual ICollection<StoreOutItemRecipe> StoreOutItemRecipeList { get; set; }
        public virtual ICollection<SetMenuDetails> SetMenuDetailsList { get; set; }
        public virtual ICollection<OrderHistory> OrderHistoryList { get; set; }
        public virtual ICollection<OrderHistoryVr2> OrderHistoryVr2List { get; set; }

        public virtual ICollection<ExtraItem> ExtraItemList { get; set; }
        public virtual ICollection<MenuItem> MenuItemList { get; set; }

        public virtual ICollection<SpecialMenuOrder> SpecialMenuOrderList { get; set; }
        public virtual ICollection<ConsumerMealWiseExtrachit> ConsumerMealWiseExtrachitList { get; set; }

        public virtual ICollection<DailyOfferItem> DailyOfferItemList { get; set; }
        public virtual ICollection<CustomerChoiceV2> CustomerChoiceV2List { get; set; }
        public virtual ICollection<WarehouseStorage> WarehouseStorageList { get; set; }


    }
}
