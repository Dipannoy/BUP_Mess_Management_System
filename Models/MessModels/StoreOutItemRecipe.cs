using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Mess_Management_System_Alpha_V2.Models.MessModels
{
    public partial class StoreOutItemRecipe : BaseClass
    {
        [ForeignKey("StoreOutItem")]
        public long StoreOutItemId { get; set; }
        public virtual StoreOutItem StoreOutItem { get; set; }

        [ForeignKey("StoreInItem")]
        public long StoreInItemId { get; set; }
        public virtual StoreInItem StoreInItem { get; set; }

        public double MinimumStoreOutUnit { get; set; }
        public double RequiredStoreInUnit { get; set; }

    }
}
