using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BupMessManagement.Models
{
    public class ConsumerOrder
    {
        public string Id { get; set; }
        public string BUPFullName { get; set; }
        public string UserName { get; set; }
        public int Breakfast { get; set; }
        public int Lunch { get; set; }
        public int Teabreak { get; set; }

    }
}
