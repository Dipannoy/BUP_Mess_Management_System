using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Mess_Management_System_Alpha_V2.Models.UCAMModel
{
    public class UCAMEmployee
    {
        public int EmployeeID { get; set; }
        public string Code { get; set; }
        public Nullable<int> DeptID { get; set; }
        public int CreatedBy { get; set; }
        public DateTime CreatedDate { get; set; }
        public Nullable<int> ModifiedBy { get; set; }
        public Nullable<DateTime> ModifiedDate { get; set; }
        public Nullable<int> SchoolId { get; set; }
        public string Remarks { get; set; }
        public string History { get; set; }
        public Nullable<int> PersonId { get; set; }
        public string Attribute1 { get; set; }
        public string Attribute2 { get; set; }
        public Nullable<DateTime> DOJ { get; set; }
        public Nullable<int> Status { get; set; }
        public string Program { get; set; }
        public string Designation { get; set; }
        public string LibraryCardNo { get; set; }
        public string MIUEmployeeID { get; set; }
        public Nullable<int> EmployeeTypeId { get; set; }
    }
}
