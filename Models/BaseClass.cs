using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Mess_Management_System_Alpha_V2.Models
{
    public partial class BaseClass
    {
        [ScaffoldColumn(false)]
        public long Id { get; set; }


        [ScaffoldColumn(false)]
        public DateTime CreatedDate { get; set; }
        [ScaffoldColumn(false)]
        public string CreatedBy { get; set; }
        [ScaffoldColumn(false)]
        public DateTime LastModifiedDate { get; set; }
        [ScaffoldColumn(false)]
        public string LastModifiedBy { get; set; }
        
        [Timestamp]
        [ScaffoldColumn(false)]
        public byte[] RowVersion { get; set; }
    }
}
