using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Mess_Management_System_Alpha_V2.Models
{
    public class Menu
    {

        public long Id { get; set; }
        public string MenuName { get; set; }



        public DateTime CreatedDate { get; set; }
        public string CreatedBy { get; set; }
        public Nullable<DateTime> ModifiedDate { get; set; }
        public string ModifiedBy { get; set; }



        public virtual List<MenuSub> SubMenuList { get; set; }


    }
}
