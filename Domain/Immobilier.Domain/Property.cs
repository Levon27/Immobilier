using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Immobilier.Domain
{
    public class Property
    {
        [Key]
        public ulong PropertyId { get; set; }
        [Column("NAME")]
        public string Name { get; set; }
        [Column("ADDRESS")]
        public string Address { get; set; }
        //public User Owner { get; set; }
    }
}
