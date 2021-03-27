using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Mess_Management_System_Alpha_V2.Models.MessModels
{
    public class ConsumerPaymentInfo : BaseClass
    {
        [ForeignKey("PaymentMethod")]
        public long PaymentMethodId { get; set; }
        public virtual PaymentMethod PaymentMethod { get; set; }

        [ForeignKey("ConsumerBillParent")]
        public long ConsumerBillParentId { get; set; }
        public virtual ConsumerBillParent ConsumerBillParent { get; set; }

        public string BankName { get; set; }
        public string AccountNumber { get; set; }

        public string MobileNumber { get; set; }
        public bool IsActive { get; set; }

        public string TransactionID { get; set; }


        public string Attribute1 { get; set; }
        public string Attribute2 { get; set; }

        public string Attribute3 { get; set; }

        public virtual ICollection<ConsumerBillHistory> ConsumerBillHistoryList { get; set; }
        public virtual ICollection<ConsumerPaymentAttachment> ConsumerPaymentAttachmentList { get; set; }


    }
}
