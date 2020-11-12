using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Mess_Management_System_Alpha_V2.Models.MessModels
{
    public partial class ExtraItem : BaseClass
    {
        public DateTime Date { get; set; }
        public DateTime MenuDate { get; set; }

        public int ? Day { get; set; }

        public double Price { get; set; }

        [ForeignKey("SetMenu")]
        public long ? SetMenuId { get; set; }
        public virtual SetMenu SetMenu { get; set; }

        [ForeignKey("MealType")]
        public long? MealTypeId { get; set; }
        public virtual MealType MealType { get; set; }


        [ForeignKey("StoreOutItem")]
        public long StoreOutItemId { get; set; }
        public virtual StoreOutItem StoreOutItem { get; set; }

        public virtual ICollection<CustomerChoice> CustomerChoiceList { get; set; }
        public virtual ICollection<CustomerChoiceV2> CustomerChoiceV2List { get; set; }
        public virtual ICollection<SetMenuDetails> SetMenuDetailList { get; set; }

        public virtual ICollection<MenuItem> MenuItemList { get; set; }



    }
}
