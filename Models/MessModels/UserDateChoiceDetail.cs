using Mess_Management_System_Alpha_V2.Data;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Mess_Management_System_Alpha_V2.Models.MessModels
{
    public class UserDateChoiceDetail : BaseClass
    {
        [ForeignKey("UserDateChoiceMaster")]
        public long? UserDateChoiceMasterId { get; set; }
        public virtual UserDateChoiceMaster UserDateChoiceMaster { get; set; }
        public DateTime Date { get; set; }
        public bool IsOrderSet { get; set; }

    }
}
