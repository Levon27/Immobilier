using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Immobilier.Domain
{
    public class User
    {
        public User(string name, string password, string email)
        {
            Name = name;
            Email = email;
            Password = password;
        }

        [Key]
        public uint Id { get; set; }

        [Column("NAME")]
        public string Name { get; set; }

        [Column("PASSWORD")]
        public string Password { get; set; }

        [Column("EMAIL")] 
        public string Email { get; set; }

        public IEnumerable<Property>? Properties { get; set; }
    }
}

