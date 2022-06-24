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
using System.Windows.Navigation;
using System.Windows.Shapes;
using Tourism_AppWPF.EF;
using Tourism_AppWPF.Windows;
using static Tourism_AppWPF.EF.ClassHelper;

namespace Tourism_AppWPF
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {

        public MainWindow()
        {
            InitializeComponent();
            CmbClient.ItemsSource = context.Client.ToList();
            CmbClient.DisplayMemberPath = "CLIENT";
            CmbClient.SelectedIndex = 0;

            CmbAgent.ItemsSource = context.Agent.ToList();
            CmbAgent.DisplayMemberPath = "AGENT";
            CmbAgent.SelectedIndex = 0;

            CmbTypeTour.ItemsSource = context.TypeTour.ToList();
            CmbTypeTour.DisplayMemberPath = "Name";
            CmbTypeTour.SelectedIndex = 0;

            CmbCountry.ItemsSource = context.Country.ToList();
            CmbCountry.DisplayMemberPath = "Name";
            CmbCountry.SelectedIndex = 0;

            CmbAirline.ItemsSource = context.Airline.ToList();
            CmbAirline.DisplayMemberPath = "Name";
            CmbAirline.SelectedIndex = 0;

            CmbHotel.ItemsSource = context.Hotel.ToList();
            CmbHotel.DisplayMemberPath = "Name";
            CmbClient.SelectedIndex = 0;

            CmbCountTourist.ItemsSource = context.CountTourist.ToList();
            CmbCountTourist.DisplayMemberPath = "Count";
            CmbCountTourist.SelectedIndex = 0;
        }

        private void BtnBack_Click(object sender, RoutedEventArgs e)
        {
            this.Hide();
            MainMenu mainMenu = new MainMenu();
            mainMenu.ShowDialog();
            this.Close();
        }

        private void BtnTours_Click(object sender, RoutedEventArgs e)
        {
            this.Hide();
            Tours tours = new Tours();
            tours.ShowDialog();
            this.Close();
        }

        private void AddTour_Click(object sender, RoutedEventArgs e)
        {
            var resClick = MessageBox.Show("Добавить запись на услугу?", "Подтверждение.", MessageBoxButton.YesNo, MessageBoxImage.Question);
            if (resClick == MessageBoxResult.Yes)
            {
                EF.Tour addRecording = new EF.Tour();
                addRecording.IDClient = ((EF.Client)CmbClient.SelectedItem).ID;
                addRecording.IDAgent = CmbAgent.SelectedIndex + 1;
                addRecording.IDTypeTour = CmbTypeTour.SelectedIndex + 1;
                addRecording.IDCountry = CmbCountry.SelectedIndex + 1;
                addRecording.DepartureDate = (DateTime)DPDepartureDate.SelectedDate;
                addRecording.DepartureDate = (DateTime)DPArrivalDate.SelectedDate;
                addRecording.IDAirline = CmbAirline.SelectedIndex + 1;
                addRecording.IDHotel = CmbHotel.SelectedIndex + 1;
                addRecording.IDCountTourist = CmbCountTourist.SelectedIndex + 1;

                ClassHelper.context.Tour.Add(addRecording);
                ClassHelper.context.SaveChanges();

                MessageBox.Show("Тур успешно сформирован.", "Выполнено!", MessageBoxButton.OK, MessageBoxImage.Information);
                this.Hide();
                Tours tours = new Tours();
                tours.ShowDialog();
                this.Close();
            }
        }

        private void BtnRachet_Click(object sender, RoutedEventArgs e)
        {
            var TypeTour = (CmbTypeTour.SelectedItem as TypeTour);
            var Airline = (CmbAirline.SelectedItem as Airline);
            var Hotel = (CmbHotel.SelectedItem as Hotel);

            var TypeTourCost = context.TypeTour.ToList().Where(i => i.Price == TypeTour.Price).FirstOrDefault();
            var AirlineCost = context.Airline.ToList().Where(i => i.Price == Airline.Price).FirstOrDefault();
            var HotelCost = context.Hotel.ToList().Where(i => i.Price == Hotel.Price).FirstOrDefault();


            double resCost = Convert.ToDouble(TypeTourCost.Price) + Convert.ToDouble(AirlineCost.Price) + Convert.ToDouble(HotelCost.Price);

            MessageBox.Show("Примерная цена: " + resCost);

        }

        private void CmbCountTourist_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (CmbCountTourist.SelectedIndex != 0)
            {

            }
        }
    }
}
