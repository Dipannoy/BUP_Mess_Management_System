using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Mess_Management_System_Alpha_V2.Models.MessModels
{
    public partial class RemainingBalanceAndWeightedPriceCalculation : BaseClass
    {
        public double TotalAvailableAmount { get; set; }

        public double WeightedPrice { get; set; }

        public DateTime Date { get; set; }

        [ForeignKey("StoreInItem")]
        public long StoreInItemId { get; set; }
        public virtual StoreInItem StoreInItem { get; set; }
    }
}
