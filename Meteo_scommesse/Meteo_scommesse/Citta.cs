using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;

namespace Meteo_scommesse
{
    enum Meteo
    {
        Soleggiato, SoleConNuvole, Nuvoloso, Pioggia, Temporale, Neve
    }
    internal class Citta
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

        public override string ToString()
        {
            return " " + nome + "    " + meteo + "    " + temperatura + "°";
        }
    }
}
