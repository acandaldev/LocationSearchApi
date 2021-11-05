using System;
using System.ComponentModel.DataAnnotations;

namespace LocationSearchApi.Model
{
    /// <summary>
    /// Filter for search
    /// </summary>
    public class SearchFilter
    {
        /// <summary>
        /// Latitude
        /// </summary>
        [Required]
        [Range(-90, 90)]
        public double Latitude { get; set; }

        /// <summary>
        /// Longitude
        /// </summary>
        [Required]
        [Range(-180, 180)]
        public double Longitude { get; set; }

        /// <summary>
        /// Max distance
        /// </summary>
        public int? MaxDistance { get; set; }

        /// <summary>
        /// Max results
        /// </summary>
        public int? MaxResults { get; set; }

        /// <summary>
        /// Constructor
        /// </summary>
        public SearchFilter()
        {
            MaxDistance = null;
            MaxResults = null;
        }

        /// <summary>
        /// Create a Location class from search filters
        /// </summary>
        /// <returns>Location class</returns>
        public Location getLocationFromFilter()
        {
            return new Location(Latitude, Longitude);
        }
    }
}
