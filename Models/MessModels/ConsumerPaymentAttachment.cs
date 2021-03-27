using Mess_Management_System_Alpha_V2.Data;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Mess_Management_System_Alpha_V2.Models.MessModels
{
    public class ConsumerPaymentAttachment : BaseClass
    {
        [ForeignKey("ConsumerBillParent")]
        public long ConsumerBillParentId { get; set; }
        public virtual ConsumerBillParent ConsumerBillParent { get; set; }

        [ForeignKey("ConsumerPaymentInfo")]
        public long ConsumerPaymentInfoId { get; set; }
        public virtual ConsumerPaymentInfo ConsumerPaymentInfo { get; set; }

        public DateTime UploadDate { get; set; }
        public string Remarks { get; set; }
        public string Attachment { get; set; }

        public double? Amount { get; set; }

        public bool EntryDone { get; set; }

        public string Attribute1 { get; set; }
        public string Attribute2 { get; set; }
    }
}
