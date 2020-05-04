using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace fmiDatanHaku
{
    public class Temperature
    {
        public DateTime Timestamp { get; set; }

        public Double Values { get; set; }

        public Location Location { get; set; }


    }

    /// <summary>
    /// Location info
    /// </summary>
    public class Location
    {
        /// <summary>
        /// Parses WGS84 formatted string to location object.
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static Location ParseWGS84(string value)
        {
            Location l = new Location();

            string[] split = value.Split(null);

            l.Latitude = Convert.ToDouble( split[0].Replace(".", ","));

            l.Longitude = Convert.ToDouble(split[1].Replace(".", ","));



            return l;
        }

        public double Latitude { get; set; }
        public double Longitude { get; set; }



        /// <summary>
        /// Shows location as WGS84 formnatted string (lat lon)
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return String.Format("{0} {1}", Latitude, Longitude);
        }
    }
}
