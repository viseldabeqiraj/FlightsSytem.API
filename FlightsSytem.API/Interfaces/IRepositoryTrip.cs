using FlightSystem.API.Dtos.Trip;
using FlightSystem.Models;

namespace FlightSystem.API.Interfaces
{
    public interface IRepositoryTrip
    {
        public List<TripToAddDto> getAllTrips(string currentUserId, string role);
        public Task<Trip> CreateTrip(TripToAddDto trip, string currentUserId);
        public Task<bool> DeleteTrip(int id);
        public void UpdateTrip(TripToShowDto trip);
        public List<TripToShowDto> Filter(string reason, string status);
        public TripToShowDto GetTrip(int id);
        public bool SendApproval(int id);
        public bool ApproveTrip(int id);
    }
}
