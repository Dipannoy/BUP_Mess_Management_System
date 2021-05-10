using Mess_Management_System_Alpha_V2.Data;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Mess_Management_System_Alpha_V2.Models.MessModels
{
    public class OnSpotParent : BaseClass
    {

        [ForeignKey("ApplicationUser")]
        public string UserId { get; set; }
        public virtual ApplicationUser ApplicationUser { get; set; }

        [ForeignKey("Constants")]
        public long? MessageId  { get; set; }
        public virtual Constants Constants { get; set; }

        [ForeignKey("Office")]
        public long? OfficeId { get; set; }
        public virtual Office Office { get; set; }

        public string BearerId { get; set; }
        public DateTime Date { get; set; }

        public bool IsOfficeOrder { get; set; }
        public bool IsApproved { get; set; }

        public bool IsMessageSent { get; set; }

        public bool IsMessageSeen { get; set; }

        public virtual ICollection<CustomerChoiceV2> CustomerChoiceV2List { get; set; }

    }
}
