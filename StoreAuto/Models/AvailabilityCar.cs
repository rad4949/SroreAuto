using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StoreAuto.Models
{
    internal class AvailabilityCar
    {
        public int? CarId { get; set; }
        public Car Car { get; set; }

        public int? StorageId { get; set; }
        public Storage Storage { get; set; }
    }
}
