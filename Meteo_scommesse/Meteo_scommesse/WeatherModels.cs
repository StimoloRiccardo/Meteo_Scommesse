using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
        }


        public class Main
        {
            public double temp { get; set; }
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

    }
}
