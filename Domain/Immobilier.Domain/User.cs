using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Immobilier.Domain
{
    public class User
    {
        public User(string name, string email, int age)
        {
            Name = name;
            Email = email;
            Age = age;
        }

        [Key]
        public ulong Id { get; set; }

        [Column("NAME")]
        public string Name { get; set; }

        [Column("EMAIL")] 
        public string Email { get; set; }

        [Column("AGE")]
        public int Age { get; set; }

        public IEnumerable<Property>? Properties { get; set; }
    }
}

