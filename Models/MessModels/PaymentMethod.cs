using Mess_Management_System_Alpha_V2.Data;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Mess_Management_System_Alpha_V2.Models.MessModels
{ 
    public class PaymentMethod : BaseClass
    {
        public string MethodName { get; set; }


        public virtual ICollection<ConsumerPaymentInfo> ConsumerPaymentInfoList { get; set; }


    }
}
