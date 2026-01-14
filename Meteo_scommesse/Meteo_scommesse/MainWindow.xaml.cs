using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Threading;
using static Meteo_scommesse.WeatherModels;

namespace Meteo_scommesse
{
    /// <summary>
    /// Logica di interazione per MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            label_meteo.FontFamily = new FontFamily("Century Schoolbook");

        }
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            AggiungiCitta finestraCitta = new AggiungiCitta();

            if (finestraCitta.ShowDialog() == true)
            {
                string nome = finestraCitta.getNomeCitta();
                if (!cittaEsiste(nome))
                {
                    MessageBox.Show("La città inserita non esiste!");
                    return;
                }

                Citta nuovaCitta = new Citta(finestraCitta.getNomeCitta());
                listBox_citta.Items.Add(nuovaCitta);
            }
            
        }

        private bool cittaEsiste(string nomeCitta)
        {
            try
            {
                string ApiKey = "a918cdbddb30238b95abe66a89456147";
                using (HttpClient client = new HttpClient())
                {
                    //uso geo al posto di forcast o weather perche piu veloce
                    string url = $"http://api.openweathermap.org/geo/1.0/direct?q={nomeCitta}&limit=1&appid={ApiKey}";
                    string json = client.GetStringAsync(url).Result;

                    // Se la risposta è "[]", la città non esiste
                    return json.Length > 5;
                }
            }
            catch
            {
                return false;
            }
        }

        private void listBox_citta_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if(listBox_citta.SelectedItem == null)
            {
                return;
            }
            FinestraCitta citta = new FinestraCitta((Citta)listBox_citta.SelectedItem);
            citta.ShowDialog();

            listBox_citta.SelectedItem = null;
        }

        private void Button_Click_1(object sender, RoutedEventArgs e) // aggiorna
        {
            // forza aggiornamento grafico
            listBox_citta.Items.Refresh();
        }
    }
}
