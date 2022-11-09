using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StoreAuto.Models
{
    public class Storage
    {
        public int Id { get; set; }
        public string Address { get; set; }

        List<AvailabilityCar> AvailabilityCars { get; set; }
    }
}
