using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.Json.Serialization;

namespace Meteo_scommesse
{
    internal class WeatherModels
    {
        public class WeatherResponse
        {
            public Main main { get; set; }
            public Weather[] weather { get; set; }
            public Wind wind { get; set; }
            public string name { get; set; }
            public Rain rain { get; set; }
            public Snow snow { get; set; }
            public Coord coord { get; set; }
        }


        public class Main
        {
            public double temp { get; set; }
            public double temp_min { get; set; }
            public double temp_max { get; set; }
            public double humidity { get; set; }
            

        }

        public class Weather
        {
            public string main { get; set; }
        }

        public class Wind
        {
            public double speed { get; set; }
        }

        public class Rain
        {
            [JsonPropertyName("1h")]
            public double OneHour { get; set; }
        }

        public class Snow
        {
            [JsonPropertyName("1h")]
            public double OneHour { get; set; }
        }

        public class Coord
        {
            public double lat { get; set; }
            public double lon { get; set; }
        }
    }
}
