using Mess_Management_System_Alpha_V2.Data;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Mess_Management_System_Alpha_V2.Models.MessModels
{
    public class ExtraChitParent : BaseClass
    {
        public virtual ICollection<CustomerChoiceV2> CustomerChoiceV2List { get; set; }

    }
}
