using Mess_Management_System_Alpha_V2.Data;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;

namespace Mess_Management_System_Alpha_V2.Models.MessModels
{
    public class RoleMenu : BaseClass
    {
        [ForeignKey("UserIdentityRole")]
        public string UserIdentityRoleId { get; set; }
        public virtual UserIdentityRole UserIdentityRole { get; set; }


        //[ForeignKey("ApplicationUser")]
        //public string UserId { get; set; }
        //public virtual ApplicationUser ApplicationUser { get; set; }


        [ForeignKey("NavigationMenu")]
        public long NavigationMenuId { get; set; }
        public virtual NavigationMenu NavigationMenu { get; set; }

    }
}
