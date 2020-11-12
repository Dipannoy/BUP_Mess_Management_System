using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Mess_Management_System_Alpha_V2.Models.ViewModel
{
    public class StoreInItemVM
    {
        public long CalendarId { get; set; }
        public long UnitId { get; set; }
        public long YearId { get; set; }
        public int Sequence { get; set; }
        public string Name { get; set; }
        public string Code { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
    }
}
