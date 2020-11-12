using Mess_Management_System_Alpha_V2.Data;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Mess_Management_System_Alpha_V2.Models.MessModels
{
    public partial class ConsumerMonthlyBillRecord : BaseClass
    {



        [ForeignKey("ApplicationUser")]
        public string UserId { get; set; }
        public virtual ApplicationUser ApplicationUser { get; set; }

        public int Month { get; set; }

        public int Year { get; set; }


        public double TotalAmount { get; set; }

        public Boolean IsPaid { get; set; }





    }
}
