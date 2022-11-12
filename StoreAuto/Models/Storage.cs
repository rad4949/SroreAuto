using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StoreAuto.Models
{
    [Table("StorageAuto")]
    public class Storage
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [MaxLength(100)]
        [MinLength(5)]
        [Column("ClientAddress")]
        public string Address { get; set; }

        public List<AvailabilityCar> AvailabilityCars { get; set; } = new List<AvailabilityCar>();
    }
}


