using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StoreAuto.Models
{
    public class AvailabilityCar
    {
        public int Id { get; set; }

        public int? CarId { get; set; }
        public Car Car { get; set; }

        public Storage Storage { get; set; }
    }
}
