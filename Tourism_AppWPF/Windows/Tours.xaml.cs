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
using Tourism_AppWPF.EF;
using static Tourism_AppWPF.EF.ClassHelper;

namespace Tourism_AppWPF.Windows
{
    /// <summary>
    /// Логика взаимодействия для Tours.xaml
    /// </summary>
    public partial class Tours : Window
    {
        List<EF.Tour> ListClient = new List<EF.Tour>();

        public Tours()
        {
            InitializeComponent();
            AllTours.ItemsSource = context.Tour.ToList();
            
        }

        private void Filter()
        {
            ListClient =context.Tour.ToList();
            ListClient = ListClient.
            Where(i => !(!i.IDClient.Equals(SearchTBCL.Text)
                       && !i.IDAgent.Equals(SearchTBCL.Text)
                       && !i.IDTypeTour.Equals(SearchTBCL.Text)
                       && !i.IDCountry.Equals(SearchTBCL.Text)
                       && !i.DepartureDate.Equals(SearchTBCL.Text)
                       && !i.ArrivalDate.Equals(SearchTBCL.Text)
                       && !i.IDAirline.Equals(SearchTBCL.Text)
                       && !i.IDHotel.Equals(SearchTBCL.Text)
                       && !i.IDCountTourist.Equals(SearchTBCL.Text))
            ).ToList();

            switch (SearchTBCL.CaretIndex)
            {
                case 0:
                    ListClient = ListClient.OrderBy(i => i.ID).ToList();
                    break;

                case 1:
                    ListClient = ListClient.OrderBy(i => i.IDClient).ToList();
                    break;

                case 2:
                    ListClient = ListClient.OrderBy(i => i.IDAgent).ToList();
                    break;

                case 3:
                    ListClient = ListClient.OrderBy(i => i.IDTypeTour).ToList();
                    break;

                case 4:
                    ListClient = ListClient.OrderBy(i => i.IDCountry).ToList();
                    break;

                case 5:
                    ListClient = ListClient.OrderBy(i => i.DepartureDate).ToList();
                    break;

                case 6:
                    ListClient = ListClient.OrderBy(i => i.ArrivalDate).ToList();
                    break;

                case 7:
                    ListClient = ListClient.OrderBy(i => i.IDAirline).ToList();
                    break;

                case 8:
                    ListClient = ListClient.OrderBy(i => i.IDHotel).ToList();
                    break;

                case 9:
                    ListClient = ListClient.OrderBy(i => i.IDCountTourist).ToList();
                    break;

                default:
                    ListClient = ListClient.OrderBy(i => i.ID).ToList();
                    break;
            }

            if (ListClient.Count == 0)
            {
                MessageBox.Show("Записи не найдены");
            }

            AllTours.ItemsSource = ListClient;

        }

        private void BtnBack_Click(object sender, RoutedEventArgs e)
        {
            this.Hide();
            MainMenu mainMenu = new MainMenu();
            mainMenu.ShowDialog();
            this.Close();
        }

        private void SearchTBCL_SelectionChanged(object sender, RoutedEventArgs e)
        {
            Filter();
        }
    }
}
