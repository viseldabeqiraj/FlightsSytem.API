using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations.Schema;

namespace FlightSystem.Models
{
    //[Table("User")]
    public class User
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public string RoleId { get; set; }
        [ForeignKey("RoleId")]
        public virtual IdentityRole Role { get; set; }
        public virtual ICollection<Trip> Trips { get; set; }

    }
}
