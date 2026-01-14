using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;

namespace Meteo_scommesse
{
    public partial class FinestraScommesse : Window
    {
        public FinestraScommesse()
        {
            InitializeComponent();

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
            // --- VALIDAZIONI BASE ---
            if (string.IsNullOrWhiteSpace(textBox_Citta.Text))
            {
                MessageBox.Show("Inserisci una città.");
                return;
            }

            if (datePicker_data.SelectedDate == null)
            {
                MessageBox.Show("Seleziona una data.");
                return;
            }

            string citta = textBox_Citta.Text;
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
            this.Tag = scommessa;
            this.DialogResult = true;
        }

        private void tabControl_tipoValori_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }
    }
}
