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
    /// Логика взаимодействия для ChangeAirlines.xaml
    /// </summary>
    public partial class ChangeAirlines : Window
    {

        List<EF.Airline> ListAirline = new List<EF.Airline>();

        public ChangeAirlines()
        {
            InitializeComponent();
            AllAirlines.ItemsSource = ClassHelper.context.Airline.ToList();
        }

        private void Filter()
        {
            ListAirline = ClassHelper.context.Airline.ToList();
            ListAirline = ListAirline.
            Where(i => i.Name.Contains(TBSearch.Text)).ToList();

            switch (TBSearch.CaretIndex)
            {
                case 0:
                    ListAirline = ListAirline.OrderBy(i => i.ID).ToList();
                    break;

                case 1:
                    ListAirline = ListAirline.OrderBy(i => i.Name).ToList();
                    break;

                default:
                    ListAirline = ListAirline.OrderBy(i => i.ID).ToList();
                    break;
            }

            if (ListAirline.Count == 0)
            {
                MessageBox.Show("Записи не найдены");
            }

            AllAirlines.ItemsSource = ListAirline;

        }

        private void AllAirlines_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Delete || e.Key == Key.Back)
            {
                var resClick = MessageBox.Show($"Удалить авиакомпанию {(AllAirlines.SelectedItem as EF.Airline).Name}", "Подтвержение", MessageBoxButton.YesNo, MessageBoxImage.Information);


                if (resClick == MessageBoxResult.Yes)
                {
                    EF.Airline airline = new EF.Airline();
                    if (!(AllAirlines.SelectedItem is EF.Airline))
                    {
                        MessageBox.Show("Запись не выбраны");
                        return;
                    }
                    airline = AllAirlines.SelectedItem as EF.Airline;

                    ClassHelper.context.Airline.Remove(airline);
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
            AddAirline addAirline = new AddAirline();
            addAirline.ShowDialog();
            this.Close();
        }
    }
}
