using Mess_Management_System_Alpha_V2.Data;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Mess_Management_System_Alpha_V2.Models.MessModels
{
    public class ConsumerBillHistory : BaseClass
    {
        [ForeignKey("ConsumerBillParent")]
        public long ConsumerBillParentId { get; set; }
        public virtual ConsumerBillParent ConsumerBillParent { get; set; }

        [ForeignKey("ConsumerPaymentInfo")]
        public long ConsumerPaymentInfoId { get; set; }
        public virtual ConsumerPaymentInfo ConsumerPaymentInfo { get; set; }

        public DateTime PaymentDate { get; set; }

        public double PaymentAmount { get; set; }

        public double Due { get; set; }

        public bool IsPartial { get; set; }

        //public string TransactionID { get; set; }
        public string ReceivedBy { get; set; }

        public string Attachment { get; set; }


        public string Attribute1 { get; set; }
        public string Attribute2 { get; set; }
    }
}
