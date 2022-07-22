using FlightSystem.API.Dtos.Flight;
using FlightSystem.API.Dtos.Trip;
using FlightSystem.API.Interfaces;
using FlightSystem.Data;
using FlightSystem.Models;

namespace FlightSystem.API.Repositories
{
    public class RepositoryTrip : IRepositoryTrip //<T> where T : class
    {
        protected AppDbContext _dbContext;
        public RepositoryTrip(AppDbContext _ApplicationContext)
        {
            _dbContext = _ApplicationContext;
        }

      
        public List<TripToAddDto> getAllTrips(string currentUserId, string role)
        {
            try
            {
                //var role = _dbContext.Users.Where
                //    (u => u.Id.ToString() == currentUserId).Select
                //    (u => u.RoleId).FirstOrDefault();
                var allTrips = new List<TripToAddDto>();

                if (role == "admin") //if admin
                {
                    allTrips = _dbContext.Trips.Where
                      (u => u.Status == "Waiting for approval").Select
                      (t => new TripToAddDto()
                      {
                          To = t.To,
                          From = t.From,
                          Deparment_date = t.Deparment_date,
                          Description = t.Description,
                          Arrival_date = t.Arrival_date,
                          Reason = t.Reason
                      })
                      .ToList();
                }
                else if (role == "user") //if user
                {
                    allTrips = _dbContext.Trips.Where
                        (u => u.UserId == currentUserId).Select
                        (t => new TripToAddDto()
                        {
                            To = t.To,
                            From = t.From,
                            Deparment_date = t.Deparment_date,
                            Description = t.Description,
                            Arrival_date = t.Arrival_date,
                            Reason = t.Reason
                        })
                        .ToList();
                }
               
                return allTrips;
            }
            catch (Exception ex)
            {

            }
            return null;
        }

        public async Task<Trip> CreateTrip(TripToAddDto trip, string currentUserId)
        {
            try
            {
                //currentUserId = "6";
                var newTrip = new Trip();

                newTrip.From = trip.From;
                newTrip.Arrival_date = trip.Arrival_date;
                newTrip.UserId = currentUserId;
                newTrip.Deparment_date = trip.Deparment_date;
                newTrip.Status = "Created";
                newTrip.Description = trip.Description;
                newTrip.To = trip.To;
                newTrip.Reason = trip.Reason;

                _dbContext.Trips.Add(newTrip);
                _dbContext.SaveChanges();
                return newTrip;
            }
            catch(Exception ex)
            {
                
            }
            return null;
        }
        public async Task<bool> DeleteTrip(int id)
        {
            try
            {
                var tripToDelete = _dbContext.Trips.Where
                    (t =>t.Id == id).FirstOrDefault();
                if (tripToDelete != null)
                {
                    _dbContext.Remove(tripToDelete);
                    _dbContext.SaveChanges();
                    return true;
                }
                return false;
            }
            catch(Exception ex)
            {

            }
            return false;
        }

        public void UpdateTrip(TripToShowDto trip)
        {
            try
            {
                var tripToUpdate = _dbContext.Trips.Where
                   (t => t.Id == trip.Id).FirstOrDefault();

                if (tripToUpdate != null)
                {
                    tripToUpdate.Reason = trip.Reason;
                    tripToUpdate.Deparment_date = trip.Deparment_date;
                    tripToUpdate.From = trip.From;
                    tripToUpdate.To = trip.To;
                    tripToUpdate.Arrival_date = trip.Arrival_date;
                    tripToUpdate.Description = trip.Description;
                }       

                _dbContext.SaveChanges();
            }
            catch(Exception ex)
            {

            }
        }
        public TripToShowDto GetTrip(int id)
        {
            try
            {
                var trip = _dbContext.Trips.Find(id);
                TripToShowDto TripToShow = null;

                if (trip != null)
                {
                    TripToShow = new TripToShowDto
                    {
                        Reason = trip.Reason,
                        Deparment_date = trip.Deparment_date,
                        From = trip.From,
                        To = trip.To,
                        Arrival_date = trip.Arrival_date,
                        Description = trip.Description,
                        Status = trip.Status,
                        Id=trip.Id
                    };
                }
              
                return TripToShow;
            }
            catch (Exception ex)
            {

            }
            return null;
        }

        public void addFlight(FlightToShowDto flight, int tripId )
        {
            try
            {
                var newFlight = new Flight
                {
                    To = flight.To,
                    From = flight.From,
                    Deparment_date = flight.Deparment_date,
                    Arrival_date= flight.Arrival_date,
                    TripId = tripId
                };

                _dbContext.Flights.Add(newFlight);
                _dbContext.SaveChanges();
            }
            catch (Exception ex)
            {

            }
        }

        public List<TripToShowDto> Filter(string reason, string status)
        {
            try
            {
                var trips = new List<TripToShowDto>();

                if (reason != null && status != null)
                {
                    trips = _dbContext.Trips.Where
                        (t => t.Status == status && t.Reason == reason)
                       .Select
                    (x => new TripToShowDto()
                    {
                        To = x.To,
                        From = x.From,
                        Deparment_date = x.Deparment_date,
                        Description = x.Description,
                        Arrival_date = x.Arrival_date,
                        Reason = x.Reason,
                        Status = x.Status
                    })
                    .ToList();

                }
                else if (reason == null)
                {
                    trips = _dbContext.Trips.Where
                       (t => t.Status == status).Select
                    (x => new TripToShowDto()
                    {
                        To = x.To,
                        From = x.From,
                        Deparment_date = x.Deparment_date,
                        Description = x.Description,
                        Arrival_date = x.Arrival_date,
                        Reason = x.Reason,
                        Status = x.Status
                    })
                    .ToList();
                }
                else if (status == null)
                {
                    trips = _dbContext.Trips.Where
                       (t => t.Reason == reason).Select
                    (x => new TripToShowDto()
                    {
                        To = x.To,
                        From = x.From,
                        Deparment_date = x.Deparment_date,
                        Description = x.Description,
                        Arrival_date = x.Arrival_date,
                        Reason = x.Reason,
                        Status = x.Status
                    })
                    .ToList();
                }              

                return trips;
            }
            catch(Exception ex)
            {

            }
            return null;
        }

        public bool SendApproval(int id)
        {
            try
            {
                var tripToApprove = _dbContext.Trips.Where
                  (t => t.Id == id).FirstOrDefault();
                if (tripToApprove != null)
                {
                    tripToApprove.Status = "Waiting for approval";
                    _dbContext.SaveChanges();
                    return true;
                }
                return false;
            }
            catch (Exception ex)
            {

            }
            return false;
        }

        public bool ApproveTrip(int id)
        {
            try
            {
                var tripToApprove = _dbContext.Trips.Where
                  (t => t.Id == id).FirstOrDefault();

                if (tripToApprove != null)
                {
                    tripToApprove.Status = "Approved";
                    _dbContext.SaveChanges();
                    return true;
                }
                return false;
            }
            catch (Exception ex)
            {

            }
            return false;
        }


    }
}
