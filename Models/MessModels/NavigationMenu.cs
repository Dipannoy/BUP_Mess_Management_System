using Mess_Management_System_Alpha_V2.Data;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Mess_Management_System_Alpha_V2.Models.MessModels
{
    public class NavigationMenu : BaseClass
    {
        public string Controller { get; set; }
        public string Action { get; set; }
        public string Name { get; set; }
        public bool IsActive { get; set; }
        public long ParentId { get; set; }
        public string RouteVariable { get; set;}

        public string RouteVariableValue { get; set; }

        public virtual ICollection<RoleMenu> RoleMenuList { get; set; }


    }
}
