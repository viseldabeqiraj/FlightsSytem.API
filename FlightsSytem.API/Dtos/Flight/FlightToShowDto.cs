using System.ComponentModel.DataAnnotations;

namespace FlightSystem.API.Dtos.Flight
{
    public class FlightToShowDto
    {
        [Required(ErrorMessage = "From is required.")]
        public string From { get; set; }

        [Required(ErrorMessage = "To is required.")]
        public string To { get; set; }

        [Required(ErrorMessage = "Deparment date is required.")]
        public DateTime Deparment_date { get; set; }

        [Required(ErrorMessage = "Arrival date is required.")]
        public DateTime Arrival_date { get; set; }
    }
}
