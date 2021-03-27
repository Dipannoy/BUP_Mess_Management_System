using Mess_Management_System_Alpha_V2.Data;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Mess_Management_System_Alpha_V2.Models.MessModels
{
    public class ConsumerMealWiseExtraChitParent : BaseClass
    {
        [ForeignKey("OrederType")]
        public long OrderTypeId { get; set; }

        public virtual OrderType OrderType { get; set; }



        [ForeignKey("ApplicationUser")]
        public string UserId { get; set; }
        public virtual ApplicationUser ApplicationUser { get; set; }


        [ForeignKey("MealType")]
        public long MealTypeId { get; set; }
        public virtual MealType MealType { get; set; }

        public string Remarks { get; set; }

        public string Attribute1 { get; set; }
        public string Attribute2 { get; set; }
        public string Attribute3 { get; set; }

        public virtual ICollection<ConsumerMealWiseExtrachit> ConsumerMealWiseExtrachitList { get; set; }

    }

}
