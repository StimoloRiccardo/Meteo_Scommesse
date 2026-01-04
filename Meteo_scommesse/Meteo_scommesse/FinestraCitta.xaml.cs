using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
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
        public FinestraCitta(Citta citta)
        {
            InitializeComponent();
            Citta cittaSelezionata = citta;
            label_nomeCitta.Content = citta.getNome();
            label_temperatura.Content = citta.getTemperatura();
            label_umidita.Content += citta.getUmidita();
            label_vento.Content += citta.getVelocitaVento();

            String pathDellEseguibile = AppDomain.CurrentDomain.BaseDirectory;
            String path = System.IO.Path.Combine(pathDellEseguibile, citta.getImmagine());
            immagine_meteo.Source = new BitmapImage(new Uri(path, UriKind.Absolute));
        }
    }
}
