using Mess_Management_System_Alpha_V2.Data;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Mess_Management_System_Alpha_V2.Models.MessModels
{
    public class ConsumerBillParent : BaseClass
    {

        [ForeignKey("ApplicationUser")]
        public string UserId { get; set; }
        public virtual ApplicationUser ApplicationUser { get; set; }


        public string Attribute1 { get; set; }
        public string Attribute2 { get; set; }

        public string Attribute3 { get; set; }

        public virtual ICollection<ConsumerBillHistory> ConsumerBillHistoryList { get; set; }

        public virtual ICollection<ConsumerPaymentInfo> ConsumerPaymentInfoList { get; set; }
        public virtual ICollection<ConsumerPaymentAttachment> ConsumerPaymentAttachmentList { get; set; }


    }
}
