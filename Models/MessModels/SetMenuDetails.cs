using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Mess_Management_System_Alpha_V2.Models.MessModels
{
    public partial class SetMenuDetails : BaseClass
    {

        [ForeignKey("SetMenu")]
        public long SetMenuId { get; set; }
        public virtual SetMenu SetMenu { get; set; }


        [ForeignKey("StoreOutItem")]
        public long StoreOutItemId { get; set; }
        public virtual StoreOutItem StoreOutItem { get; set; }


        [ForeignKey("ExtraItem")]
        public long? ExtraItemId { get; set; }
        public virtual ExtraItem ExtraItem { get; set; }

        //public string TestProperty { get; set; }


    }
}
