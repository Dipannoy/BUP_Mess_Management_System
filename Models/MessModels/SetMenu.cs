using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Mess_Management_System_Alpha_V2.Models.MessModels
{
    public partial class SetMenu : BaseClass
    {
        public DateTime SetMenuDate { get; set; }
        public int ? Day { get; set; }
        public int Serial { get; set; }
        public bool IsAvailableAsExtra { get; set; }
        public decimal SetMenuPrice { get; set; }

        [ForeignKey("MealType")]
        public long MealTypeId { get; set; }
        public virtual MealType MealType { get; set; }

        public virtual ICollection<SetMenuDetails> SetMenuDetailsList { get; set; }

        public virtual ICollection<CustomerChoice> CustomerChoiceList { get; set; }
        public virtual ICollection<CustomerChoiceV2> CustomerChoiceV2List { get; set; }



        public virtual ICollection<ExtraItem> ExtraItemList { get; set; }

        public virtual ICollection<OrderHistory> OrderHistoryList { get; set; }
        public virtual ICollection<OrderHistoryVr2> OrderHistoryVr2List { get; set; }
        public virtual ICollection<DailySetMenu> DailySetMenuList { get; set; }



    }
}
