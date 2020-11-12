using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Mess_Management_System_Alpha_V2.Models.MessModels
{
    public partial class StoreOutItemCategory : BaseClass
    {
        public string Name { get; set; }


        public virtual ICollection<StoreOutItem> StoreOutItemList { get; set; }
    }
}
