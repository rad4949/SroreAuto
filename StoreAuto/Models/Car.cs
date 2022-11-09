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
        public int ColorId { get; set; }
        public int CompleteSetId { get; set; }
        public bool IsCash { get; set; }
    }
}
