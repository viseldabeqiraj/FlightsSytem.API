using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace FlightSystem.Models
{
    //[Table("Role")]
    public class Role
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public virtual ICollection<User> Users { get; set; }
        public const string Admin = "Admin";
        public const string User = "User";
    }
}
