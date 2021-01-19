using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Teleport.BusinessLogic;
using Teleport.Services.Interfaces;

namespace Teleport.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class DistanceController : ControllerBase
    {
        private readonly ILogger<DistanceController> _logger;
        private readonly ILocationFetcher _locationFetcher;

        public DistanceController(ILogger<DistanceController> logger, ILocationFetcher locationFetcher)
        {
            _logger = logger;
            _locationFetcher = locationFetcher;
        }

        [HttpGet]
        public async Task<IActionResult> Get(string from, string to)
        {
            if (from == null || to == null)
                return BadRequest("Request parameters are not specified or insufficient");

            var fromLocation = await _locationFetcher.GetAirportLocation(from);
            if (fromLocation == null || fromLocation.Location == null)
                return BadRequest("Error getting departure location");

            var toLocation = await _locationFetcher.GetAirportLocation(to);
            if (toLocation == null || toLocation.Location == null)
                return BadRequest("Error getting destination location");

            var distance = DistanceAlgorithm.GetDistanceBetweenPlaces(fromLocation.Location.Lat, fromLocation.Location.Lon, toLocation.Location.Lat, toLocation.Location.Lon);

            return new JsonResult(distance);
        }
    }
}
