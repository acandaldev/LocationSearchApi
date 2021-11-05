using LocationSearchApi.Model;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace LocationSearchApi.Services
{
    /// <summary>
    /// Location Search Service Interface
    /// </summary>
    public interface ILocationSearchService
    {
        /// <summary>
        /// GetLocations from Data Store
        /// </summary>
        /// <param name="filter">filters for Search</param>
        /// <returns>List of Locations</returns>
        public Task<List<Location>> GetLocationsAsync(SearchFilter filter);
    }
}
