using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Reflection;
using System.Security.Principal;
using System.Text;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;
using System.Timers;
using System.Windows;
using System.Windows.Threading;
using static Meteo_scommesse.WeatherModels;

namespace Meteo_scommesse
{   
    public class Citta
    {
        private string nome, immagine;
        private double temperatura, velocitaVento, umidita, tempMax, tempMin, precipitazioni, lon, lat, tempPercepita;
        private string meteo;
        private DateTime giornoCorrente;

        private readonly DispatcherTimer _timer;
        public event Action MeteoAggiornato;


        public Citta(string nome)
        {
            this.nome = nome;
            this.giornoCorrente = DateTime.Now;
            LoadWeather();
            _timer = new DispatcherTimer
            {
                Interval = TimeSpan.FromMinutes(1)
            };
            _timer.Tick += (s, e) => LoadWeather();
            _timer.Start();
        }

        public void setTemperatura(float temperatura)
        {
            this.temperatura = temperatura;
        }

        public void setImmagine(string immagine)
        {
            this.immagine = immagine;
        }

        public void setVelocitaVento(float velocita)
        {
            this.velocitaVento = velocita;
        }

        public void setUmidita(float umidita)
        {
            this.umidita = umidita;
        }

        public string getNome()
        {
            return nome;
        }
        
        public string getTemperatura()
        {
            return temperatura + "°";
        }

        public string getUmidita()
        {
            return umidita + "%";
        }

        public string getVelocitaVento()
        {
            return velocitaVento + "km/h";
        }

        public string getImmagine()
        {
            return immagine;
        }

        public string getTempMax()
        {
            return tempMax + "°";
        }

        public string getTempMin()
        {
            return tempMin + "°";
        }

        public string getPrecipitazioni()
        {
            return precipitazioni + "mm";
        }

        public double getLongitudine()
        {
            return lon;
        }

        public double getLatitudine()
        {
            return lat;
        }
        
        public string getTempPercepita()
        {
            return tempPercepita + "°";
        }

        public DateTime getGiornoCorrente()
        {
            return giornoCorrente;
        }

        public void setGiornoCorrente(DateTime nuovaData)
        {
            giornoCorrente = nuovaData;
        }

        public override string ToString()
        {
            return " " + nome + "    " + meteo + "    " + temperatura + "°";
        }

        public void impostaImmagine()
        {
            switch (meteo)
            {
                case "Clear":
                    immagine = "images/1.png";
                    break;

                case "Clouds":
                    immagine = "images/2.png";
                    break;

                case "Drizzle":
                    immagine = "images/3.png";
                    break;

                case "Rain":
                    immagine = "images/3.png";
                    break;

                case "Thunderstorm":
                    immagine = "images/5.png";
                    break;

                case "Snow":
                    immagine = "images/6.png";
                    break;

                case "Mist":
                    immagine = "images/7.png";
                    break;

                case "Extreme":
                    immagine = "images/8.png";
                    break;

                default:
                    immagine = "images/1.png";
                    break;
            }
        }



        public async void LoadWeather()
        {
            try
            {
                string ApiKey = "7f76e18c6271aff9c2dce1751d2c2b12";
                HttpClient client = new HttpClient();
                string url = $"https://api.openweathermap.org/data/2.5/forecast?q={nome}&units=metric&appid={ApiKey}\r\n";

                var json = await client.GetStringAsync(url);
                var forecast = JsonSerializer.Deserialize<ForecastResponse>(json);

                var previsioneDelGiorno = forecast.list.FirstOrDefault(f => DateTime.Parse(f.dt_txt).Date == giornoCorrente.Date && DateTime.Parse(f.dt_txt).Hour == 12);
                if (previsioneDelGiorno == null)
                {
                    previsioneDelGiorno = forecast.list.FirstOrDefault(f => DateTime.Parse(f.dt_txt).Date == giornoCorrente.Date);
                }
                if (previsioneDelGiorno == null)
                    return;

                lat = forecast.city.coord.lat;
                lon = forecast.city.coord.lon;
                nome = forecast.city.name;
                temperatura = previsioneDelGiorno.main.temp;
                umidita = previsioneDelGiorno.main.humidity;
                velocitaVento = previsioneDelGiorno.wind.speed;
                meteo = previsioneDelGiorno.weather[0].main;
                tempMax = previsioneDelGiorno.main.temp_max;
                tempMin = previsioneDelGiorno.main.temp_min;
                tempPercepita = previsioneDelGiorno.main.feels_like;


                precipitazioni = 0;
                if (previsioneDelGiorno.rain != null)
                    precipitazioni = previsioneDelGiorno.rain.OneHour;

                if (previsioneDelGiorno.snow != null)
                    precipitazioni = previsioneDelGiorno.snow.OneHour;

                tempPercepita = previsioneDelGiorno.main.feels_like;

                impostaImmagine();
                MeteoAggiornato?.Invoke();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Errore nel recupero dati meteo\n" + ex.Message);
            }
        }

    }
}
