using Mess_Management_System_Alpha_V2.Data;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Mess_Management_System_Alpha_V2.Models.MessModels
{
    public partial class Office : BaseClass
    {


        // add link to user
       

            public string Name { get; set; }


        public virtual ICollection<SpecialMenuParent> SpecialMenuParentList { get; set; }










    }
}

