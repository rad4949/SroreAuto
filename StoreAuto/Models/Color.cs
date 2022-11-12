using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StoreAuto.Models
{
    public class Color
    {
        //public int Id { get; set; }
        public string ColorName { get; set; }
        public string ColorCode { get; set; }

        public List<Car> Cars { get; set; } = new List<Car>();

    }
}
