using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using zadanie5.Services;

namespace zadanie5.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ClientsController : ControllerBase
    {
        private readonly IDbService _dbService;
        public ClientsController(IDbService dbService)
        {
            _dbService = dbService;
        }

        [HttpDelete("{idClient}")]
        public async Task<IActionResult> DeleteClient(int idClient)
        {
            if (_dbService.DeleteClient(idClient))
            {
               
                return Ok("Usuniety");
            }
            else
            {
              
               return StatusCode(StatusCodes.Status500InternalServerError);
            }

            
        }


        [HttpGet]
        public async Task<IActionResult> GetTrips()
        {
            var trips = await _dbService.GetTrips();
            return Ok(trips);
        }
    }
}
