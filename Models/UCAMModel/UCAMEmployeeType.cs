using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Mess_Management_System_Alpha_V2.Models.UCAMModel
{
    public class UCAMEmployeeType
    {
        public int EmployeeTypeId { get; set; }
        public string EmployeTypeName { get; set; }
        public string EmployeTypeNameBEN { get; set; }
        public Nullable<int> IndexValue { get; set; }
        public Nullable<bool> IsActive { get; set; }
        public Nullable<int> CivilNoCivilTypeID { get; set; }
        public Nullable<int> CreatedBy { get; set; }
        public Nullable<DateTime> CreatedDate { get; set; }
        public Nullable<int> ModifiedBy { get; set; }
        public Nullable<DateTime> ModifiedDate { get; set; }

    }
}
