using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Mess_Management_System_Alpha_V2.Models.MessModels
{
    public partial class WarehouseStorage : BaseClass
    {

        public bool IsStoreOut { get; set; }

        public DateTime Date { get; set; }

        public double Amount { get; set; }        
        public double TotalPurchasePrice { get; set; }
        public double PurchaseRate { get; set; }

        [ForeignKey("StoreInItem")]
        public long StoreInItemId { get; set; }
        public virtual StoreInItem StoreInItem { get; set; }

        [ForeignKey("StoreOutItem")]
        public long? StoreOutItemId { get; set; }
        public virtual StoreOutItem StoreOutItem { get; set; }
    }
}
