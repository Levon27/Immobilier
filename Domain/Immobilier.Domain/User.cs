using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Immobilier.Domain
{
    public class User
    {
        public User(string name, string password, string email, int age)
        {
            Name = name;
            Email = email;
            Age = age;
            Password = password;
        }

        [Key]
        public ulong Id { get; set; }

        [Column("NAME")]
        public string Name { get; set; }

        [Column("PASSWORD")]
        public string Password { get; set; }

        [Column("EMAIL")] 
        public string Email { get; set; }

        [Column("AGE")]
        public int Age { get; set; }

        public IEnumerable<Property>? Properties { get; set; }
    }
}

