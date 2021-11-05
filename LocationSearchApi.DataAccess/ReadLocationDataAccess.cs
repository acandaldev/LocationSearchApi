using LazyCache;
using LocationSearchApi.Model;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Sylvan.Data.Csv;
using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Threading.Tasks;

namespace LocationSearchApi.DataAccess
{
    /// <summary>
    /// Data Access component
    /// </summary>
    public class ReadLocationDataAccess : IReadLocationDataAccess
    {
        private ILogger<ReadLocationDataAccess> _logger;
        private GlobalSettings _globalSettings;
        private IAppCache _appCache;

        /// <summary>
        /// DI Constructor
        /// </summary>
        /// <param name="logger"></param>
        /// <param name="globalSettings"></param>
        /// <param name="appCache"></param>
        public ReadLocationDataAccess(ILogger<ReadLocationDataAccess> logger, IOptions<GlobalSettings> globalSettings, IAppCache appCache)
        {
            _logger = logger;
            _globalSettings = globalSettings.Value;
            _appCache = appCache;
        }
        /// <summary>     
        /// Load Localizations from cache or from file
        /// Using LAZY CACHE : https://github.com/alastairtree/LazyCache/
        /// </summary>
        /// <returns>List of localizations/returns>
        public async Task<List<Location>> LoadLocationsAsync()
        {
            Func<Task<List<Location>>> cacheableAsyncFunc = () => ReadCsvLocationsAsync();
            var cachedList = await _appCache.GetOrAddAsync(_globalSettings.CacheKeyName, cacheableAsyncFunc, TimeSpan.FromMinutes(_globalSettings.CacheLoadIntervalMinutes));
            return cachedList;
        }

        /// <summary>
        /// Read CSV file with CSV library
        /// Fastest CSV Reader : https://www.joelverhagen.com/blog/2020/12/fastest-net-csv-parsers
        /// </summary>
        /// <returns>List of csv items</returns>
        private async Task<List<Location>> ReadCsvLocationsAsync()
        {
            List<Location> returnList = new List<Location>();
            using var csv = await CsvDataReader.CreateAsync(Path.Combine(Path.GetDirectoryName(Assembly.GetEntryAssembly().Location), _globalSettings.CsvPath));
            while (await csv.ReadAsync())
                returnList.Add(new Location(csv.GetDouble(1), csv.GetDouble(2), csv.GetString(0)));
            return returnList;
        }


    }
}
