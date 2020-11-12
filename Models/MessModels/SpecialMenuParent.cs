using Mess_Management_System_Alpha_V2.Data;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Mess_Management_System_Alpha_V2.Models.MessModels
{
    public partial class SpecialMenuParent : BaseClass
    {


        // add link to user
        [ForeignKey("ApplicationUser")]
        public string UserId { get; set; }
        public virtual ApplicationUser ApplicationUser { get; set; }



        [ForeignKey("MealType")]
        public long MealTypeId { get; set; }
        public virtual MealType MealType { get; set; }

       


        [ForeignKey("Office")]
        public long? OfficeId { get; set; }
        public virtual Office Office { get; set; }


        public DateTime OrderDate { get; set; }

        public virtual ICollection<SpecialMenuOrder> SpecialMenuOrderList { get; set; }










    }
}

