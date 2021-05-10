using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Mess_Management_System_Alpha_V2.Models.MessModels
{
    public class Constants : BaseClass
    {
        public string Name { get; set; }
        public string Value { get; set; }

        public bool IsMessage { get; set; }

        public virtual ICollection<OnSpotParent> OnSpotParentList { get; set; }

    }
}
