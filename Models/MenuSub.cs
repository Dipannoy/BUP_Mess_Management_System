using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Mess_Management_System_Alpha_V2.Models
{
    public class MenuSub
    {

        public long Id { get; set; }
        public string SubMenuName { get; set; }
        public string ControllerName { get; set; }
        public string ActionName { get; set; }


        [ForeignKey("Menu")]
        public long MenuId { get; set; }
        public virtual Menu Menu { get; set; }

        public bool IsActive { get; set; }

        public DateTime CreatedDate { get; set; }
        public string CreatedBy { get; set; }
        public Nullable<DateTime> ModifiedDate { get; set; }
        public string ModifiedBy { get; set; }

    }
}
