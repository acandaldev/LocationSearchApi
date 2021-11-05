namespace LocationSearchApi.Model
{
    /// <summary>
    /// Aplication Settings  in appsettings.json
    /// </summary>
    public class GlobalSettings
    {
        /// <summary>
        /// Relativa path of csv file
        /// </summary>
        public string CsvPath { get; set; }

        /// <summary>
        /// Cache duration in minutes (to refresh csv)
        /// </summary>
        public int CacheLoadIntervalMinutes { get; set; }

        /// <summary>
        /// Cache key name
        /// </summary>
        public string CacheKeyName { get; set; }
    }
}
