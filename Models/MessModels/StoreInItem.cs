using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Mess_Management_System_Alpha_V2.Models.MessModels
{
    public partial class StoreInItem : BaseClass
    {
        public string Name { get; set; }
        public bool IsOpen { get; set; }

        [ForeignKey("UnitType")]
        public long UnitTypeId { get; set; }
        public virtual UnitType UnitType { get; set; }

        [ForeignKey("StoreInItemCategory")]
        public long StoreInCategoryId { get; set; }
        public virtual StoreInItemCategory StoreInItemCategory { get; set; }

        public virtual ICollection<StoreOutItemRecipe> StoreOutItemRecipeList { get; set; }
        public virtual ICollection<WarehouseStorage> WarehouseStorageList { get; set; }
        public virtual ICollection<RemainingBalanceAndWeightedPriceCalculation> RemainingBalanceAndWeightedPriceCalculationList { get; set; }
    }
}
