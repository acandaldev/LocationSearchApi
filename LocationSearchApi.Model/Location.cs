using System;

namespace LocationSearchApi
{
    public class Location
    {
        /// <summary>
        /// Latitude
        /// </summary>
        public double Latitude { get; set; }
        /// <summary>
        /// Longitude
        /// </summary>
        public double Longitude { get; set; }
        /// <summary>
        /// Location direction
        /// </summary>
        public string Address { get; set; }
        /// <summary>
        /// Location distance
        /// </summary>
        public double Distance { get; set; }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="latitude"></param>
        /// <param name="longitude"></param>
        public Location(double latitude, double longitude)
        {
            Latitude = latitude;
            Longitude = longitude;
            Address = String.Empty; ;
            Distance = 0;
        }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="latitude"></param>
        /// <param name="longitude"></param>
        /// <param name="address"></param>
        public Location(double latitude, double longitude,string address)
        {
            Latitude = latitude;
            Longitude = longitude;
            Address = address;
        }

        /// <summary>
        /// Creates a new location that is <paramref name="offsetLat"/>, <paramref name="offsetLon"/> meters from this location.
        /// </summary>
        public Location Add(double offsetLat, double offsetLon)
        {
            double latitude = Latitude + (offsetLat / 111111d);
            double longitude = Longitude + (offsetLon / (111111d * Math.Cos(latitude)));

            return new Location(latitude, longitude);
        }

        /// <summary>
        /// Calculates the distance between this location and another one, in meters.
        /// </summary>
        public double CalculateDistance(Location location)
        {
            var rlat1 = Math.PI * Latitude / 180;
            var rlat2 = Math.PI * location.Latitude / 180;
            var rlon1 = Math.PI * Longitude / 180;
            var rlon2 = Math.PI * location.Longitude / 180;
            var theta = Longitude - location.Longitude;
            var rtheta = Math.PI * theta / 180;
            var dist = Math.Sin(rlat1) * Math.Sin(rlat2) + Math.Cos(rlat1) * Math.Cos(rlat2) * Math.Cos(rtheta);
            dist = Math.Acos(dist);
            dist = dist * 180 / Math.PI;
            dist = dist * 60 * 1.1515;

            return dist * 1609.344;
        }

        public override string ToString()
        {
            return Latitude + ", " + Longitude;
        }
    }
}
