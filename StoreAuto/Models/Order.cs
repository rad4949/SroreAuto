using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StoreAuto.Models
{
    public class Order
    {
        public int Id { get; set; }
        public int CompleteSetId { get; set; }
        DateTime DateOrder { get; set; }
        DateTime Term { get; set; }
    }
}
