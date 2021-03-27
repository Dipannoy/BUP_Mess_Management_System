using Mess_Management_System_Alpha_V2.Data;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Mess_Management_System_Alpha_V2.Models.MessModels
{
    public class DailyOfferItem : BaseClass
    {
        [ForeignKey("StoreOutItem")]
        public long StoreOutItemId { get; set; }
        public virtual StoreOutItem StoreOutItem { get; set; }

        public DateTime Date {get;set;}
        public long? OrderLimit { get; set;}
        public bool IsActive { get; set;}

    }

}
