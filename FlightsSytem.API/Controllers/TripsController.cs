using FlightSystem.API.Dtos.Trip;
using FlightSystem.API.Interfaces;
using FlightSystem.API.Repositories;
using FlightSystem.Data;
using FlightSystem.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;
using System.Security.Claims;


namespace FlightSystemAPI.Controllers
{

    [Route("api/[controller]")]
    [ApiController]
    public class TripsController : ControllerBase
    {
        private readonly IRepositoryTrip _repository;
        private readonly UserManager<IdentityUser> _userManager;
        public TripsController( IRepositoryTrip repository, UserManager<IdentityUser> userManager)
        {
            _repository = repository;
            _userManager = userManager;
        }
              
        // DELETE api/<TripsController>/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTrip(int id)
        {
            try
            {
                var result= await _repository.DeleteTrip(id);

                if (result)
                    return Ok(200);
                else return NotFound();
            }
            catch(Exception ex)
            {

            }
            return BadRequest();
        }
        private async Task<User> GetCurrentUser()
        {
            var user = await _userManager.FindByNameAsync(User.Identity.Name);
            var role = await _userManager.GetRolesAsync(user);
            return new User
            {
                Id=user.Id,
                RoleId=role.FirstOrDefault().ToString()
            };            
        }

        [Authorize]
        [HttpGet("/api/[controller]/[action]")]
        public async Task<IActionResult> getTrips()
        {
            try
            {
                User user = await GetCurrentUser();
                var currentUserId = user.Id;
                var currentUserRole = user.RoleId;
                var allTrips =  _repository.getAllTrips(currentUserId, currentUserRole);

                if (allTrips.Count == 0)
                {
                    return NotFound();
                }
                return Ok(allTrips);
            }
            catch(Exception ex)
            {
               
            }
            return BadRequest("Could not get trips.");
        }

        [HttpPost("/api/[controller]/[action]")]
        public async Task<IActionResult> createTrip([FromBody] TripToAddDto trip)
        {
            try
            {
                if (!ModelState.IsValid)
                    return BadRequest("Invalid data.");

                User user = await GetCurrentUser();
                var currentUserId = user.Id;
                var result = await _repository.CreateTrip(trip, currentUserId);
                if (result != null)
                    return Ok(result);

                else return BadRequest("Could not be created.");
            }
            catch(Exception ex)
            {

            }
            return BadRequest();
        }
        [HttpPut("/api/[controller]/[action]")]

        public IActionResult UpdateTrip([FromBody] TripToShowDto trip)
        {
            try
            {
                if (!ModelState.IsValid)
                    return BadRequest("Invalid data.");

                _repository.UpdateTrip(trip);
                return Ok(200);
            }
            catch (Exception ex)
            {

            }
            return NotFound("Could not update trip");
        }
        [HttpGet("{reason},{status}")]
        public IActionResult Filter(string reason, string status)
        {
            try
            {
                var result = _repository.Filter(reason, status);
                if (result.Count == 0)
                {
                    return NotFound();
                }

                return Ok(result);
            }
            catch (Exception ex)
            {

            }
            return BadRequest("Could not filter trip");
        }
        [HttpGet("{id}")]
        public IActionResult GetTrip( int id)
        {
            try
            {
                var result = _repository.GetTrip(id);
                if (result.Status == "Approved")
                {
                    //display add flight button
                }

                return Ok(result);
            }
            catch (Exception ex)
            {

            }
            return BadRequest("Could not get trip data.");
        }

        [HttpPut("{id}")]
        public IActionResult SendApproval(int id)
        {
            try
            {
                var result =_repository.SendApproval(id);

                if(result)
                return Ok(200);
                else BadRequest("Could not send trip approval");
            }
            catch (Exception ex)
            {

            }
            return BadRequest();
        }
        [Authorize(Roles = "admin")]
        [HttpPost("{id}")]
        public IActionResult ApproveTrip(int id)
        {
            try
            {
                var result = _repository.ApproveTrip(id);

                if (result)
                    return Ok(200);
                else BadRequest("Could not approve trip.");
            }
            catch (Exception ex)
            {

            }
            return BadRequest();
        }
    }
}
