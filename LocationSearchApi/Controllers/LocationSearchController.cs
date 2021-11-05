using LocationSearchApi.Model;
using LocationSearchApi.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace LocationSearchApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    [Produces("application/json")]
    [Consumes("application/json")]

    public class LocationSearchController : ControllerBase
    {
        private ILogger<LocationSearchController> _logger;
        private ILocationSearchService _locationSearchService;

        public LocationSearchController(ILogger<LocationSearchController> logger, ILocationSearchService locationSearchService)
        {
            _logger = logger;
            _locationSearchService = locationSearchService;
        }

        [HttpGet]
        [ProducesResponseType(typeof(SearchResult),StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ValidationProblemDetails), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<SearchResult>> GetLocationsAsync([FromQuery] SearchFilter filter)
        {        
            try
            {
                SearchResult sr = new SearchResult();
                sr.InitTime = DateTime.UtcNow;
                sr.Locations = await _locationSearchService.GetLocationsAsync(filter);
                sr.OriginLocation = filter.getLocationFromFilter();
                sr.EndTime = DateTime.UtcNow;
                sr.LocationsCount = sr.Locations.Count();
                sr.TotalSecondsDuration = (sr.EndTime - sr.InitTime).TotalSeconds;           
                return Ok(sr);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                return Problem(ex.Message,string.Empty, 500, "General Error");
            }
        }
    }
}
