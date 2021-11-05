using System.Collections.Generic;
using System.Threading.Tasks;

namespace LocationSearchApi.DataAccess
{
    /// <summary>
    /// Data Access component interface
    /// </summary>
    public interface IReadLocationDataAccess
    {
        /// <summary>     
        /// Load Localizations from cache or from file
        /// Using LAZY CACHE : https://github.com/alastairtree/LazyCache/
        /// </summary>
        /// <returns>List of localizations/returns>
        public Task<List<Location>> LoadLocationsAsync();
    }
}
