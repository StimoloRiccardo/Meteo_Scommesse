using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace Meteo_scommesse
{
    /// <summary>
    /// Logica di interazione per FinestraCitta.xaml
    /// </summary>
    public partial class FinestraCitta : Window
    {
        private Citta cittaSelezionata = new Citta("");
        public FinestraCitta(Citta citta)
        {
            InitializeComponent();
            cittaSelezionata = citta;
            label_nomeCitta.Content = citta.getNome();
            label_temperatura.Content = citta.getTemperatura();
        }
    }
}
