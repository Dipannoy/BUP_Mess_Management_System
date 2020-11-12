using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Mess_Management_System_Alpha_V2.Models.ViewModel
{
    public class UserViewModel
    {
        public string Id { get; set; }
        public string UserName { get; set; }
        public string NormalizedUserName { get; set; }


        public int EmployeeID { get; set; }
        public string FullName { get; set; }
        public string EmployeTypeName { get; set; }
        public string Gender { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
        public int User_ID { get; set; }
        public string LogInID { get; set; }
        public string RoleName { get; set; }

    }
}
