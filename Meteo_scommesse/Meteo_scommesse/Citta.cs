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

        public override string ToString()
        {
            return " " + nome + "    " + meteo + "    " + temperatura + "°";
        }

        public void impostaImmagine()
        {
            switch (meteo)
            {
                case "Clear":
                    immagine = "images/1.jpg";
                    break;

                case "Clouds":
                    immagine = "images/2.jpg";
                    break;

                case "Drizzle":
                    immagine = "images/3.jpg";
                    break;

                case "Rain":
                    immagine = "images/3.jpg";
                    break;

                case "Thunderstorm":
                    immagine = "images/5.jpg";
                    break;

                case "Snow":
                    immagine = "images/6.jpg";
                    break;

                case "Mist":
                    immagine = "images/7.jpg";
                    break;

                case "Extreme":
                    immagine = "images/8.jpg";
                    break;

                default:
                    immagine = "images/1.jpg";
                    break;
            }
        }



        private async void LoadWeather()
        {
            try
            {
                string ApiKey = "7f76e18c6271aff9c2dce1751d2c2b12";
                HttpClient client = new HttpClient();
                string url = $"https://api.openweathermap.org/data/2.5/weather?q={nome}&appid={ApiKey}&units=metric&lang=it";

                var json = await client.GetStringAsync(url);
                var weather = JsonSerializer.Deserialize<WeatherResponse>(json);

                nome = weather.name;
                temperatura = weather.main.temp;
                umidita = weather.main.humidity;
                velocitaVento = weather.wind.speed;
                meteo = weather.weather[0].main;
                tempMax = weather.main.temp_max;
                tempMin = weather.main.temp_min;
                if (meteo== "Drizzle" || meteo == "Rain" || meteo == "Thunderstorm" )
                {
                    precipitazioni = weather.rain.OneHour;
                }
                else if (meteo == "Snow")
                {
                    precipitazioni = weather.snow.OneHour;
                }
                lat = weather.coord.lat;
                lon = weather.coord.lon;
                tempPercepita = weather.main.feels_like;

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
