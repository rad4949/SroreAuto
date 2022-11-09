using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StoreAuto.Models
{
    public class Car
    {
        public int Id { get; set; }

        public int InvoiceId { get; set; }
        public Invoice Invoice { get; set; }

        public int ColorId { get; set; }
        public Color Color { get; set; }

        public int CompleteSetId { get; set; }
        public CompleteSet CompleteSet { get; set; }

        public int StorageId { get; set; }
        public Storage Storage { get; set; }

        public bool IsCash { get; set; }
    }
}
