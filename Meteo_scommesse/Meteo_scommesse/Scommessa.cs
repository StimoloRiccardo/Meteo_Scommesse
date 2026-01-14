using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Meteo_scommesse
{
    public class Scommessa
    {
        public string PCitta { get; set; }
        public DateTime Giorno { get; set; }
        public string Tipo { get; set; }
        public List<double> Valori { get; set; }
        public bool? Esito { get; set; }

        public Scommessa(string pCitta, DateTime giorno, string tipo, List<double> valori)
        {
            PCitta = pCitta;
            Giorno = giorno;
            Tipo = tipo;
            Valori = valori;
            Esito = null;
        }
        public override string ToString()
        {
            string stato = "";
            if (Esito == null)
            {
                stato = "In attesa...";
            }
            else if (Esito == true)
            {
                stato = "VINTA";
            }
            else if (Esito == false)
            {
                stato = "PERSA";
            }
            return stato;
        }
    }
}
