using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Mess_Management_System_Alpha_V2.Models.MessModels
{
    public partial class UnitType : BaseClass
    {
        public string Name { get; set; }
        public string ShortName { get; set; }

        public virtual ICollection<StoreInItem> StoreInItemList { get; set; }
    }
}
