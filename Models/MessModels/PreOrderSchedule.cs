using Mess_Management_System_Alpha_V2.Data;
using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace Mess_Management_System_Alpha_V2.Models.MessModels
{
    public partial class PreOrderSchedule : BaseClass
    {

        public DateTime LastConfigurationUpdateDate { get; set; }
        public bool IsPreOrderSet { get; set; }


        [ForeignKey("MealType")]
        public long MealTypeId { get; set; }
        public virtual MealType MealType { get; set; }

        [ForeignKey("SetMenu")]
        public long ?  SetMenuId { get; set; }
        public virtual SetMenu SetMenu { get; set; }


        // add link to user
        [ForeignKey("ApplicationUser")]
        public string UserId { get; set; }
        public virtual ApplicationUser ApplicationUser { get; set; }
    }
}
