using Mess_Management_System_Alpha_V2.Models.MessModels;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Mess_Management_System_Alpha_V2.Data
{
    public class ApplicationUser : IdentityUser
    {

        public int BUPEmployeeID { get; set; }
        public string BUPFullName { get; set; }
        public string BUPEmployeTypeName { get; set; }
        public string BUPGender { get; set; }
        public string BUPPhone { get; set; }
        public string BUPEmail { get; set; }
        public int BUPUser_ID { get; set; }
        public string BUPLogInID { get; set; }
        public string BUPPassword { get; set; }
        public string BUPRoleName { get; set; }
        public int? EmployeeRank { get; set; }
        public string  BUPNumber { get; set; }
        public string OfficeName { get; set; }

        public virtual ICollection<PreOrderSchedule> PreOrderScheduleList { get; set; }
        public virtual ICollection<MaintenanceBillHistory> MaintenanceBillHistoryList { get; set; }

        public virtual ICollection<OrderHistory> OrderHistoryList { get; set; }
        public virtual ICollection<OrderHistoryVr2> OrderHistoryVr2List { get; set; }

        public virtual ICollection<CustomerChoice> CustomerChoiceList { get; set; }
        public virtual ICollection<CustomerChoiceV2> CustomerChoiceV2List { get; set; }


        public virtual ICollection<Period> PeriodList { get; set; }

        public virtual ICollection<ConsumerMonthlyBillRecord> ConsumerMonthlyBillRecordList { get; set; }

        public virtual ICollection<SpecialMenuParent> SpecialMenuParentList { get; set; }
        public virtual ICollection<CustomerDailyMenuChoice> CustomerDailyMenuChoiceList { get; set; }

        public virtual ICollection<UserDateChoiceMaster> UserDateChoiceMasterList { get; set; }
        //public virtual ICollection<ExtraChitParent> ExtraChitParentList { get; set; }

        public virtual ICollection<OnSpotParent> OnSpotParentList { get; set; }
        //public virtual ICollection<ConsumerMealWiseExtrachit> ConsumerMealWiseExtrachitList { get; set; }
        public virtual ICollection<ConsumerMealWiseExtraChitParent> ConsumerMealWiseExtraChitParentList { get; set; }

        public virtual ICollection<ConsumerBillParent> ConsumerBillParentList { get; set; }

    }
}
