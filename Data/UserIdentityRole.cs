using Mess_Management_System_Alpha_V2.Models.MessModels;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Mess_Management_System_Alpha_V2.Data
{
    public class UserIdentityRole : IdentityRole
    {
        public virtual ICollection<RoleMenu> RoleMenuList { get; set; }

    }
}
