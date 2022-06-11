using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using zadanie5.Models;
using zadanie5.Services;

namespace zadanie5.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TripsController : ControllerBase
    {
        private readonly IDbService _dbService;
        public TripsController(IDbService dbService)
        {
            _dbService = dbService;
        }
        [HttpGet]
        public async Task<IActionResult> GetTrips()
        {
            var trips = await _dbService.GetTrips();
            return Ok(trips);
        }

        [HttpPost("{idTrip}/clients")]
        public async Task<IActionResult> AddClientForTrip(Order order, int idTrip)
        {
            if (_dbService.AddClientForTrip(order))
            {
                return Ok("Zamówienie przyjęte");
            }else
            {

                return StatusCode(StatusCodes.Status500InternalServerError);
            }
          
        }
       
    }
}
