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
            finestraCitta.ShowDialog();
            listBox_citta.Items.Add(new Citta(finestraCitta.getNomeCitta()));
        }

        private void listBox_citta_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            FinestraCitta citta = new FinestraCitta((Citta)listBox_citta.SelectedItem);
            citta.ShowDialog();
        }
    }
}
