using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StoreAuto.Models
{
    public class Invoice
    {
        public int Id { get; set; }
        public int ClientId { get; set; }
        public int CarId { get; set; }
        public int OrderId { get; set; }
        public DateTime Date { get; set; }

    }
}
