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

namespace Tourism_AppWPF.Windows
{
    /// <summary>
    /// Логика взаимодействия для ChangeHotels.xaml
    /// </summary>
    public partial class ChangeHotels : Window
    {
        List<EF.Hotel> ListHotel = new List<EF.Hotel>();

        public ChangeHotels()
        {
            InitializeComponent();
            AllHotel.ItemsSource = ClassHelper.context.Hotel.ToList();
        }

        private void Filter()
        {
            ListHotel = ClassHelper.context.Hotel.ToList();
            ListHotel = ListHotel.
            Where(i => i.Name.Contains(TBSearch.Text)
            || i.CountStars.Contains(TBSearch.Text)
            || i.IDCountry.Equals(TBSearch.Text)).ToList();

            switch (TBSearch.CaretIndex)
            {
                case 0:
                    ListHotel = ListHotel.OrderBy(i => i.ID).ToList();
                    break;

                case 1:
                    ListHotel = ListHotel.OrderBy(i => i.Name).ToList();
                    break;

                case 2:
                    ListHotel = ListHotel.OrderBy(i => i.CountStars).ToList();
                    break;

                case 3:
                    ListHotel = ListHotel.OrderBy(i => i.IDCountry).ToList();
                    break;

                default:
                    ListHotel = ListHotel.OrderBy(i => i.ID).ToList();
                    break;
            }

            if (ListHotel.Count == 0)
            {
                MessageBox.Show("Записи не найдены");
            }

            AllHotel.ItemsSource = ListHotel;

        }

        private void AllHotel_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Delete || e.Key == Key.Back)
            {
                var resClick = MessageBox.Show($"Удалить отель {(AllHotel.SelectedItem as EF.Hotel).Name}", "Подтвержение", MessageBoxButton.YesNo, MessageBoxImage.Information);


                if (resClick == MessageBoxResult.Yes)
                {
                    EF.Hotel hotel = new EF.Hotel();
                    if (!(AllHotel.SelectedItem is EF.Hotel))
                    {
                        MessageBox.Show("Запись не выбраны");
                        return;
                    }
                    hotel = AllHotel.SelectedItem as EF.Hotel;

                    ClassHelper.context.Hotel.Remove(hotel);
                    ClassHelper.context.SaveChanges();
                }
            }
            Filter();
        }

        private void BtnBack_Click(object sender, RoutedEventArgs e)
        {
            this.Hide();
            AdminSettings adminSettings = new AdminSettings();
            adminSettings.ShowDialog();
            this.Close();
        }

        private void TBSearch_TextChanged(object sender, TextChangedEventArgs e)
        {
            Filter();
        }

        private void BtnAdd_Click(object sender, RoutedEventArgs e)
        {
            this.Hide();
            AddHotel addHotel = new AddHotel();
            addHotel.ShowDialog();
            this.Close();
        }
    }
}
