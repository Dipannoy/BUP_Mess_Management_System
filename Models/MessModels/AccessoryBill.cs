using Mess_Management_System_Alpha_V2.Data;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Mess_Management_System_Alpha_V2.Models.MessModels
{
    public partial class AccessoryBill : BaseClass
    {
    
        public string Name { get; set; }
        public Boolean Active { get; set; }

        public int ? MinMeal { get; set; }

        public double ? CostMinMeal { get; set; }

        public double DefaultCost { get; set; }

   
    }
}
