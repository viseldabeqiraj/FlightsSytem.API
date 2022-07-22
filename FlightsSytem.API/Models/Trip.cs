using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace FlightSystem.Models
{
    [Table("Trip")]
    public class Trip
    {
        public int Id { get; set; }     
        public string Reason { get; set; }
        public string Description { get; set; }
        public string From { get; set; }
        public string To { get; set; }
        public DateTime Deparment_date { get; set; }
        public DateTime Arrival_date { get; set; }
        public string Status { get; set; }
        public string UserId { get; set; }
        public virtual ICollection<Flight> Flights { get; set; }
        [ForeignKey("UserId")]
        public virtual IdentityUser User { get; set; }
    }
}
