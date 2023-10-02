using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Immobilier.Domain
{
    public class Property
    {
        public Property(string name, string address, uint idOwner)
        {
            Name = name;
            Address = address;
            IdOwner = idOwner;
        }

        [Key]
        public uint Id { get; set; }

        [Column("NAME")]
        public string Name { get; set; }

        [Column("ADDRESS")]
        public string Address { get; set; }

        [Column("ID_OWNER")]
        public uint IdOwner { get; set; }

        [ForeignKey(nameof(IdOwner))]
        public User? Owner { get; set; }
    }
}
