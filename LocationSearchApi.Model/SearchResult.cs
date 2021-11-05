using System;
using System.Collections.Generic;

namespace LocationSearchApi.Model
{
    /// <summary>
    /// return class of search
    /// </summary>
    public class SearchResult
    {
        /// <summary>
        /// List of Locations
        /// </summary>
        public List<Location> Locations { get; set; }

        /// <summary>
        /// Number of locations
        /// </summary>
        public int LocationsCount { get; set; }

        /// <summary>
        /// Original location from search
        /// </summary>
        public Location OriginLocation { get; set; }

        /// <summary>
        /// Init time of search
        /// </summary>
        public DateTime InitTime { get; set; }

        /// <summary>
        /// End time of search
        /// </summary>
        public DateTime EndTime { get; set; }

        /// <summary>
        /// Duration in MS of search
        /// </summary>
        public double TotalSecondsDuration { get; set; }
    }
}
