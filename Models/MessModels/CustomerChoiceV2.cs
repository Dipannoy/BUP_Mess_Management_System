using Mess_Management_System_Alpha_V2.Data;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Mess_Management_System_Alpha_V2.Models.MessModels
{
    public partial class CustomerChoiceV2 : BaseClass
    {
        public DateTime Date { get; set; }

        [ForeignKey("SetMenu")]
        public long? SetMenuId { get; set; }

        public virtual SetMenu SetMenu { get; set; }


        [ForeignKey("ExtraItem")]
        public long? ExtraItemId { get; set; }
        public virtual ExtraItem ExtraItem { get; set; }


        [ForeignKey("OrederType")]
        public long OrderTypeId { get; set; }

        public virtual OrderType OrderType { get; set; }



        [ForeignKey("ApplicationUser")]
        public string UserId { get; set; }
        public virtual ApplicationUser ApplicationUser { get; set; }


        [ForeignKey("MealType")]
        public long MealTypeId { get; set; }
        public virtual MealType MealType { get; set; }

        public double quantity { get; set; }




    }
}
