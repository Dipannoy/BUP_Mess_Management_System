using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Mess_Management_System_Alpha_V2.Models.UCAMModel
{
    public class UCAMUser
    {
        public int User_ID { get; set; }
        public string LogInID { get; set; }
        public string Password { get; set; }
        public Nullable<int> RoleID { get; set; }
        public Nullable<DateTime> RoleExistStartDate { get; set; }
        public Nullable<DateTime> RoleExistEndDate { get; set; }
        public bool IsActive { get; set; }
        public int CreatedBy { get; set; }
        public DateTime CreatedDate { get; set; }
        public Nullable<int> ModifiedBy { get; set; }
        public Nullable<DateTime> ModifiedDate { get; set; }

    }
}
