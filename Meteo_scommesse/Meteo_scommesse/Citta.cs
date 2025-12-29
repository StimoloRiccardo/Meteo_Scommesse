using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Meteo_scommesse
{
    enum Meteo
    {
        Soleggiato, SoleConNuvole, Nuvoloso, Pioggia, Temporale, Neve
    }
    
    public class Citta
    {
        private string nome, immagine;
        private float temperatura, velocitaVento, umidita;
        private Meteo meteo;

        public Citta(string nome)
        {
            this.nome = nome;
            this.immagine = "";
            this.meteo = Meteo.Soleggiato;
            this.velocitaVento = 0;
            this.temperatura = 0;
            this.umidita = 0;
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

        public float getUmidita()
        {
            return umidita;
        }

        public float getVelocitaVento()
        {
            return velocitaVento;
        }

        public override string ToString()
        {
            return " " + nome + "    " + meteo + "    " + temperatura + "°";
        }
    }
}
