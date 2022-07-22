using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;
using System.Text.Json.Serialization;

namespace FlightSystem.API.Dtos.Trip
{
    public class TripToShowDto
    {
        //[IgnoreDataMember]
        //[JsonIgnore]
        public int Id { get; set; }
        //[Required(ErrorMessage = "Reason is required.")]
        public string Reason { get; set; }

        [Required(ErrorMessage = "Description is required.")]
        public string Description { get; set; }

        [Required(ErrorMessage = "From is required.")]
        public string From { get; set; }

        [Required(ErrorMessage = "To is required.")]
        public string To { get; set; }

        [Required(ErrorMessage = "Deparment date is required.")]
        public DateTime Deparment_date { get; set; }

        [Required(ErrorMessage = "Arrival date is required.")]
        public DateTime Arrival_date { get; set; }
        public string Status { get; set; }
    }
}
