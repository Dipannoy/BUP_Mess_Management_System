using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Mess_Management_System_Alpha_V2.Models.MessModels
{
    public partial class DailySetMenu : BaseClass
    {
        public DateTime ? SetMenuDate { get; set; }
        public int Day { get; set; }

        [ForeignKey("MealType")]
        public long MealTypeId { get; set; }
        public virtual MealType MealType { get; set; }


        [ForeignKey("SetMenu")]
        public long SetMenuId { get; set; }
        public virtual SetMenu SetMenu { get; set; }

     


    }
}
