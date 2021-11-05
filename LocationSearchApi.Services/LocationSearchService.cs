using LocationSearchApi.DataAccess;
using LocationSearchApi.Model;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LocationSearchApi.Services
{
    /// <summary>
    /// Location Search Service
    /// </summary>
    public class LocationSearchService : ILocationSearchService
    {
        private ILogger<LocationSearchService> _logger;
        private IReadLocationDataAccess _readLocationDataAccess;

        /// <summary>
        /// Constructor DI
        /// </summary>
        /// <param name="logger"></param>
        /// <param name="readLocationsDataAccess"></param>
        public LocationSearchService(ILogger<LocationSearchService> logger, IReadLocationDataAccess readLocationsDataAccess)
        {
            _logger = logger;
            _readLocationDataAccess = readLocationsDataAccess;
        }

        /// <summary>
        /// GetLocations from Data Store
        /// </summary>
        /// <param name="filter">filters for Search</param>
        /// <returns>List of Locations</returns>
        public async Task<List<Location>> GetLocationsAsync(SearchFilter filter)
        {
            _logger.LogDebug($"Before reading Locations");
            List<Location> lista = await _readLocationDataAccess.LoadLocationsAsync();
            _logger.LogDebug($"Readed {lista.Count} locations.");
            // Calculate distances
            IEnumerable<Location> ielista = lista.Select(t =>
            {
                t.Distance = t.CalculateDistance(filter.getLocationFromFilter());
                return filter.MaxDistance.HasValue ? (filter.MaxDistance.Value >= t.Distance ? t : null) : t;
            });
            // Filter values and order
            ielista = ielista.Where(c => c is not null).OrderBy(h => h.Distance);
            // Take n fist results
            if (filter.MaxResults.HasValue)
                ielista = ielista.Take(filter.MaxResults.Value);
            return ielista.ToList();
        }
    }
}
