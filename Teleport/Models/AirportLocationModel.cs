using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.AccessControl;
using System.Threading.Tasks;

namespace Teleport.Models
{
    public class AirportLocationModel
    {
        public string country { get; set; }

        public string City_iata { get; set; }

        public string Iata { get; set; }

        public string City { get; set; }

        public string Timezone_region_name { get; set; }

        public string Country_iata { get; set; }

        public int Rating { get; set; }

        public string Name { get; set; }

        public LocationCoord Location { get; set; }

        public string Type { get; set; }

        public int Hubs { get; set; }
    }

    public class LocationCoord
    {
        public double Lat { get; set; }    

        public double Lon { get; set; }    
    }
}
