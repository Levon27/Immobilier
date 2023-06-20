using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Immobilier.Domain
{
    public class User
    {
        [Key]
        public ulong UserId { get; set; }
        [Column("NAME")]
        public string Name { get; set; }
        [Column("age")]
        public int Age { get; set; }
        //public IEnumerable<Property> Properties { get; set; }
    }
}

