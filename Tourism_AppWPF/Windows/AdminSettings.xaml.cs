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

namespace Tourism_AppWPF.Windows
{
    /// <summary>
    /// Логика взаимодействия для AdminSettings.xaml
    /// </summary>
    public partial class AdminSettings : Window
    {
        public AdminSettings()
        {
            InitializeComponent();
        }

        private void BtnBack_Click(object sender, RoutedEventArgs e)
        {
            this.Hide();
            MainWindowAdmin mainWindowAdmin = new MainWindowAdmin();
            mainWindowAdmin.ShowDialog();
            this.Close();
        }

        private void BtnChangeAgent_Click(object sender, RoutedEventArgs e)
        {
            this.Hide();
            ChangeAgent changeAgent = new ChangeAgent();
            changeAgent.ShowDialog();
            this.Close();
        }

        private void BtnChangeTypeTour_Click(object sender, RoutedEventArgs e)
        {
            this.Hide();
            ChangeTypeTour changeTypeTour = new ChangeTypeTour();
            changeTypeTour.ShowDialog();
            this.Close();
        }

        private void BtnChangeCountry_Click(object sender, RoutedEventArgs e)
        {
            this.Hide();
            ChangeCountry changeCountry = new ChangeCountry();
            changeCountry.ShowDialog();
            this.Close();
        }

        private void BtnChangeAirlines_Click(object sender, RoutedEventArgs e)
        {
            this.Hide();
            ChangeAirlines changeAirlines = new ChangeAirlines();
            changeAirlines.ShowDialog();
            this.Close();
        }

        private void BtnChangeHotels_Click(object sender, RoutedEventArgs e)
        {
            
        }
    }
}
