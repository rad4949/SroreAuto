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
        public DateTime DateOrder { get; set; }
        public DateTime Term { get; set; }

        public int InvoiceId { get; set; }
        public Invoice Invoice { get; set; }

        public List<CompleteSet> CompleteSets { get; set; } = new List<CompleteSet>();
    }
}
