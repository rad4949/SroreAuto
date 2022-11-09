using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StoreAuto.Models
{
    public class Model
    {
        public int Id { get; set; }
        public int BrandId { get; set; }
        public string ModelName { get; set; }
        public string BodyType { get; set; }
    }
}
