using Mess_Management_System_Alpha_V2.Data;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Mess_Management_System_Alpha_V2.Models.MessModels
{
    public partial class SpecialMenuOrder : BaseClass
    {


        // add link to user
        //[ForeignKey("ApplicationUser")]
        //public string UserId { get; set; }
        //public virtual ApplicationUser ApplicationUser { get; set; }

    

        [ForeignKey("SpecialMenuParent")]
        public long SpecialMenuParentId { get; set; }
        public virtual SpecialMenuParent SpecialMenuParent { get; set; }

        [ForeignKey("StoreOutItem")]
        public long? StoreOutItemId { get; set; }
        public virtual StoreOutItem StoreOutItem { get; set; }


        public double UnitOrdered { get; set; }



        //[ForeignKey("Office")]
        //public long ? OfficeId { get; set; }
        //public virtual Office Office { get; set; }


        //public double UnitOrdered { get; set; }
        //public DateTime OrderDate { get; set; }







    }
}

