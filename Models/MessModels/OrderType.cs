using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Mess_Management_System_Alpha_V2.Models.MessModels
{
    public partial class OrderType : BaseClass
    {
        public string Name { get; set; }

        public virtual ICollection<CustomerChoice> CustomerChoiceList { get; set; }
        public virtual ICollection<OrderHistoryVr2> OrderHistoryVr2List { get; set; }
        public virtual ICollection<CustomerChoiceV2> CustomerChoiceV2List { get; set; }

    }
}
