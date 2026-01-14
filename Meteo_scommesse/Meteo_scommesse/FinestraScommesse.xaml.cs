using System;
using System.Collections.Generic;
using System.Threading;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Interop;
using System.Windows.Media.Imaging;
using static Meteo_scommesse.WeatherModels;

namespace Meteo_scommesse
{
    public partial class FinestraScommesse : Window
    {
        Citta cittaScommessa;
        public FinestraScommesse(Citta c)
        {
            InitializeComponent();
            cittaScommessa = c;

            comboBox_vMeteo.Items.Add("Sole");
            comboBox_vMeteo.Items.Add("Nuvole");
            comboBox_vMeteo.Items.Add("Pioggia");
            comboBox_vMeteo.Items.Add("Temporale");
            comboBox_vMeteo.Items.Add("Neve");
            comboBox_vMeteo.Items.Add("Nebbia");
            comboBox_vMeteo.Items.Add("Disastro Naturale");
        }

        private void button_conferma_Click(object sender, RoutedEventArgs e)
        {
            if (datePicker_data.SelectedDate == null)
            {
                MessageBox.Show("Seleziona una data.");
                return;
            }

            string citta = cittaScommessa.getNome();
            DateTime data = datePicker_data.SelectedDate.Value;

            List<double> valori = new List<double>();
            string tipo = "";

            try
            {
                switch (tabControl_tipoValori.SelectedIndex)
                {
                    case 0:
                        tipo = "Temperatura";
                        valori.Add(double.Parse(textBox_vTemp.Text));
                        valori.Add(double.Parse(textBox_vTempPercepita.Text));
                        valori.Add(double.Parse(textBox_vTempMin.Text));
                        valori.Add(double.Parse(textBox_vTempMax.Text));
                        break;

                    case 1:
                        tipo = "Meteo";
                        if (comboBox_vMeteo.SelectedIndex < 0)
                        {
                            MessageBox.Show("Seleziona un tipo di meteo.");
                            return;
                        }
                        valori.Add(comboBox_vMeteo.SelectedIndex);
                        break;

                    case 2:
                        tipo = "Altro";
                        valori.Add(double.Parse(textBox_vUmidita.Text));
                        valori.Add(double.Parse(textBox_vVelVento.Text));
                        valori.Add(double.Parse(textBox_vPrecipitazioni.Text));
                        break;

                    default:
                        MessageBox.Show("Seleziona un tipo di scommessa.");
                        return;
                }
            }
            catch
            {
                MessageBox.Show("Compila correttamente tutti i campi numerici.");
                return;
            }

            Scommessa scommessa = new Scommessa(citta, data, tipo, valori);

            bool esito = cittaScommessa.ControllaScommessa(scommessa);
            scommessa.Esito = esito;
            label_Stato.Content = scommessa.ToString();

            int soldi = int.Parse(label_soldi.Content.ToString().Replace("€", ""));
            if (esito == true)
            { 
                soldi += 100;
            }
            else if (esito == false && soldi >= 20)
            {
                soldi -= 20;
            }
            label_soldi.Content = soldi + "€";

            this.Tag = scommessa;
        }

        public void impostaImmagine()
        {
            if (comboBox_vMeteo.SelectedItem == null)
                return;

            string selezione = comboBox_vMeteo.SelectedItem.ToString();
            string img = "images/1.png"; // default

            switch (selezione)
            {
                case "Sole":
                    img = "images/1.png";
                    break;

                case "Nuvole":
                    img = "images/2.png";
                    break;

                case "Pioggia":
                    img = "images/3.png";
                    break;

                case "Temporale":
                    img = "images/5.png";
                    break;

                case "Neve":
                    img = "images/6.png";
                    break;

                case "Nebbia":
                    img = "images/7.png";
                    break;

                case "Disastro Naturale":
                    img = "images/8.png";
                    break;
            }

            // Carica l'immagine
            String pathDellEseguibile = AppDomain.CurrentDomain.BaseDirectory;
            string path = System.IO.Path.Combine(pathDellEseguibile, img);
            img_vMeteo.Source = new BitmapImage(new Uri(path, UriKind.Absolute));
        }

        private void tabControl_tipoValori_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void comboBox_vMeteo_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            impostaImmagine();
        }
    }
}
