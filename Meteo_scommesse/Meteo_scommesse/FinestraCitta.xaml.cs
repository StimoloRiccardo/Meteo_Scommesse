using System;
using System.Collections.Generic;
using System.ComponentModel;
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
        Citta citta;
        public FinestraCitta(Citta citta)
        {
            InitializeComponent();
            this.citta = citta;

            citta.MeteoAggiornato += aggiorna;
            citta.LoadWeather();
        }


        public void aggiorna()
        {
            if (citta.getImmagine() == null)
                return;
            label_nomeCitta.Content = citta.getNome();
            label_temperatura.Content = citta.getTemperatura();
            label_umidita.Content = "Umidità: " + citta.getUmidita();
            label_vento.Content = "Velocità del Vento: " + citta.getVelocitaVento();
            label_tempMax.Content = "Temp Massima: " + citta.getTempMax();
            label_tempMin.Content = "Tempp Minima: " + citta.getTempMin();
            label_precipitazioni.Content = "Precipitazioni: " + citta.getPrecipitazioni();
            label_latitudine.Content = "Latitudine: " + citta.getLatitudine().ToString();
            label_longitudine.Content = "Longitudine: " + citta.getLongitudine().ToString();
            label_tempPercepita.Content = "Temp Percepita: " + citta.getTempPercepita();
            label_data.Content = citta.getGiornoCorrente().ToShortDateString();

            String pathDellEseguibile = AppDomain.CurrentDomain.BaseDirectory;
            String path = System.IO.Path.Combine(pathDellEseguibile, citta.getImmagine());
            immagine_meteo.Source = new BitmapImage(new Uri(path, UriKind.Absolute));
        }

        private void button_avanti_Click(object sender, RoutedEventArgs e)
        {
            citta.setGiornoCorrente(citta.getGiornoCorrente().AddDays(1));
            citta.LoadWeather();
        }


        private void button_indietro_Click(object sender, RoutedEventArgs e)
        {
            citta.setGiornoCorrente(citta.getGiornoCorrente().AddDays(-1));
            citta.LoadWeather();
        }
    }
}
