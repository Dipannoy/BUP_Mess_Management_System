using Mess_Management_System_Alpha_V2.Data;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Mess_Management_System_Alpha_V2.Models.MessModels
{
    public partial class OrderHistory : BaseClass
    {


        // add link to user
        [ForeignKey("ApplicationUser")]
        public string UserId { get; set; }
        public virtual ApplicationUser ApplicationUser { get; set; }

        [ForeignKey("SetMenu")]
        public long ? SetMenuId { get; set; }
        public virtual SetMenu SetMenu { get; set; }

        [ForeignKey("MealType")]
        public long MealTypeId { get; set; }
        public virtual MealType MealType { get; set; }

        [ForeignKey("StoreOutItem")]
        public long? StoreOutItemId { get; set; }
        public virtual StoreOutItem StoreOutItem { get; set; }

      

        public double UnitOrdered { get; set; }
        public double OrderAmount { get; set; }
        public bool IsPreOrder { get; set; }
        public DateTime OrderDate { get; set; }

        public  bool ? IsForwardedToOffice { get; set; }
        public string ForwardRemarks { get; set; }





    }
}
