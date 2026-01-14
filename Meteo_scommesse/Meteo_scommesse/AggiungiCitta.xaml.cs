using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
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
using System.Windows.Threading;


namespace Meteo_scommesse
{
    /// <summary>
    /// Logica di interazione per AggiungiCitta.xaml
    /// </summary>
    /// 
    public partial class AggiungiCitta : Window
    {
        public string nomeCitta;


        public string getNomeCitta()
        {
            return nomeCitta;
        }
        public AggiungiCitta()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if(txtBox_nomeCitta.Text == "scommessa")
            {
                FinestraScommesse finestraScommesse = new FinestraScommesse();
                finestraScommesse.ShowDialog();
            }
            nomeCitta = txtBox_nomeCitta.Text;
            DialogResult = true;
        }
    }
}
