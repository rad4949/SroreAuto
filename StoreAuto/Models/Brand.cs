using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StoreAuto.Models
{
    public class Brand
    {
        public int Id { get; set; }
        public string BrandName { get; set; }
        public string Country { get; set; }


        public List<Model> Models { get; set; } = new List<Model>();

    }
}
