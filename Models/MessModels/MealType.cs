using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Mess_Management_System_Alpha_V2.Models.MessModels
{
    public partial class MealType : BaseClass
    {
        public int Serial { get; set; }
        public string Name { get; set; }
        public bool IsAvailableForPreOrder { get; set; }
        public DateTime PreOrderLastTime { get; set; }

        public virtual ICollection<PreOrderSchedule> PreOrderScheduleList { get; set; }
        public virtual ICollection<SetMenu> SetMenuList { get; set; }
        public virtual ICollection<OrderHistory> OrderHistoryList { get; set; }
        public virtual ICollection<OrderHistoryVr2> OrderHistoryVr2List { get; set; }


        public virtual ICollection<CustomerChoice> CustomerChoiceList { get; set; }
        public virtual ICollection<CustomerChoiceV2> CustomerChoiceV2List { get; set; }


        public virtual ICollection<MenuItem> MenuItemList { get; set; }


        public virtual ICollection<Period> PeriodList { get; set; }
        public virtual ICollection<DailySetMenu> DailySetMenuList { get; set; }

        public virtual ICollection<SpecialMenuParent> SpecialMenuParentList { get; set; }





    }
}
