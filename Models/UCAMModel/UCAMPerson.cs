using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Mess_Management_System_Alpha_V2.Models.UCAMModel
{
    public class UCAMPerson
    {
        public int PersonID { get; set; }
        public string FullName { get; set; }
        public Nullable<DateTime> DOB { get; set; }
        public string Gender { get; set; }
        public string MatrialStatus { get; set; }
        public string BloodGroup { get; set; }
        public string Religion { get; set; }
        public string Nationality { get; set; }
        public string FatherName { get; set; }
        public string FatherProfession { get; set; }
        public string MotherName { get; set; }
        public string MotherProfession { get; set; }
        public string GuardianName { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
        public string PhotoPath { get; set; }
        public string Remarks { get; set; }
        public Nullable<int> TypeId { get; set; }
        public Nullable<bool> IsActive { get; set; }
        public Nullable<bool> IsDeleted { get; set; }
        public string SMSContactSelf { get; set; }
        public string SMSContactGuardian { get; set; }
        public string GuardianEmail { get; set; }
        public Nullable<int> QuotaId { get; set; }
        public int CreatedBy { get; set; }
        public DateTime CreatedDate { get; set; }
        public Nullable<int> ModifiedBy { get; set; }
        public Nullable<DateTime> ModifiedDate { get; set; }

    }
}
