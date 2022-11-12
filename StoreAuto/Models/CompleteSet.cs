using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StoreAuto.Models
{
    public class CompleteSet
    {
        public int Id { get; set; }
        public int EngineVolume { get; set; }
        public string FuelType { get; set; }
        public int ModelYear { get; set; }
        public decimal Price { get; set; }

        public int ModelId { get; set; }
        public Model Model { get; set; }
        public int OrderId { get; set; }
        public Order Order { get; set; }
        public List<Car> Cars { get; set; } = new List<Car>();
    }
}
