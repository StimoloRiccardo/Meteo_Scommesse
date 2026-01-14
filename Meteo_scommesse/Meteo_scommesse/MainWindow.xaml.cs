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
using System.IO;

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
            CaricaCitta();
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
                SalvaCitta(nome);
                Citta nuovaCitta = new Citta(finestraCitta.getNomeCitta());
                listBox_citta.Items.Add(nuovaCitta);
            }
            
        }

        private void SalvaCitta(string citta)
        {
            string path = "citta.csv";

            if (!File.Exists(path))
            {
                using (StreamWriter sw = File.CreateText(path))
                {
                    sw.WriteLine("NomeCitta");
                }
            }
            else if (File.Exists(path))
            {
                File.AppendAllText(path, citta + Environment.NewLine);
            }
        }

        private void CaricaCitta()
        {
            string path = "citta.csv";

            if (File.Exists(path))
            {
                using (StreamReader sr = new StreamReader(path))
                {
                    // Salta la prima riga (intestazione)
                    sr.ReadLine();

                    while (!sr.EndOfStream)
                    {
                        string riga = sr.ReadLine();

                        if (!string.IsNullOrWhiteSpace(riga))
                        {
                            Citta nuovaCitta = new Citta(riga);
                            listBox_citta.Items.Add(nuovaCitta);
                        }
                    }
                }
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

        private void listBox_citta_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            if (listBox_citta.SelectedItem == null)
            {
                return;
            }
            FinestraCitta citta = new FinestraCitta((Citta)listBox_citta.SelectedItem);
            citta.ShowDialog();

            listBox_citta.SelectedItem = null;
        }


        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            listBox_citta.Items.Refresh();
        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            if (listBox_citta.SelectedItem == null)
            {
                MessageBox.Show("Seleziona una città prima di scommettere.");
                return;
            }

            Citta selezionata = (Citta)listBox_citta.SelectedItem;

            FinestraScommesse finestra = new FinestraScommesse(selezionata);
            finestra.ShowDialog();
        }

        private void listBox_citta_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }
    }
}
