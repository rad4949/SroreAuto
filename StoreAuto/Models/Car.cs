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
        public bool IsCash { get; set; }

        public int CompleteSetId { get; set; }
        public CompleteSet CompleteSet { get; set; }

        public string ColorName { get; set; }
        public string ColorCode { get; set; }
        public Color Color { get; set; }

        public int AvailabilityCarId { get; set; }
        public AvailabilityCar AvailabilityCar { get; set; }

        public int InvoiceId { get; set; }
        public Invoice Invoice { get; set; }
    }
}
