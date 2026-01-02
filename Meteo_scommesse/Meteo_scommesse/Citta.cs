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
        private double temperatura, velocitaVento, umidita;
        private string meteo;

        private readonly DispatcherTimer _timer;
        public event Action MeteoAggiornato;


        public Citta(string nome)
        {
            this.nome = nome;
            LoadWeather();
            _timer = new DispatcherTimer
            {
                Interval = TimeSpan.FromMinutes(1)
            };
            _timer.Tick += (s, e) => LoadWeather();
            _timer.Start();
            immagine = "images/1.png";
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

        public double getUmidita()
        {
            return umidita;
        }

        public double getVelocitaVento()
        {
            return velocitaVento;
        }

        public string getImmagine()
        {
            return immagine;
        }

        public override string ToString()
        {
            return " " + nome + "    " + meteo + "    " + temperatura + "°";
        }

        public void impostaImmagine()
        {
            if (meteo == "soleggiato")
            {
                immagine = "images/1.png";
            }
            else if (meteo == "poco nuvoloso")
            {
                immagine = "images/2.png";
            }
            else if (meteo == "nuvoloso")
            {
                immagine = "images/3.png";
            }
            else if (meteo == "pioggia leggera" || meteo == "pioggia moderata")
            {
                immagine = "images/4.png";
            }
            else if (meteo == "pioggia intensa" || meteo == "temporale")
            {
                immagine = "images/5.png";
            }
            else if (meteo == "neve leggera" || meteo == "neve moderata" || meteo == "neve intensa")
            {
                immagine = "images/6.png";
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
                        meteo = weather.weather[0].description;
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Errore nel recupero dati meteo\n" + ex.Message);
                    }
                }
    }
}
