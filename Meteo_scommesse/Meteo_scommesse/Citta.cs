using System;
using System.Collections.Generic;
using System.Linq;
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


    }
}
