using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace FlightSystem.Models
{
    [Table("Flight")]
    public class Flight
    {
        public int Id { get; set; }
        public string From { get; set; }
        public string To { get; set; }
        public DateTime Deparment_date { get; set; }
        public DateTime Arrival_date { get; set; }
        public int TripId { get; set; }
        public virtual Trip Trip { get; set; }
    }
}
