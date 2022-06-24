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
    /// Логика взаимодействия для ChangeCountry.xaml
    /// </summary>
    public partial class ChangeCountry : Window
    {

        List<EF.Country> ListCountry = new List<EF.Country>();
        public ChangeCountry()
        {
            InitializeComponent();
            AllCountry.ItemsSource = ClassHelper.context.Country.ToList();
        }

        private void Filter()
        {
            ListCountry = ClassHelper.context.Country.ToList();
            ListCountry = ListCountry.
            Where(i => i.Name.Contains(TBSearch.Text)).ToList();

            switch (TBSearch.CaretIndex)
            {
                case 0:
                    ListCountry = ListCountry.OrderBy(i => i.ID).ToList();
                    break;

                case 1:
                    ListCountry = ListCountry.OrderBy(i => i.Name).ToList();
                    break;
            }

            if (ListCountry.Count == 0)
            {
                MessageBox.Show("Записи не найдены");
            }

            AllCountry.ItemsSource = ListCountry;

        }

        private void AllCountry_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Delete || e.Key == Key.Back)
            {
                var resClick = MessageBox.Show($"Удалить страну {(AllCountry.SelectedItem as EF.Agent).FirstName}", "Подтвержение", MessageBoxButton.YesNo, MessageBoxImage.Information);


                if (resClick == MessageBoxResult.Yes)
                {
                    EF.Country country = new EF.Country();
                    if (!(AllCountry.SelectedItem is EF.Country))
                    {
                        MessageBox.Show("Запись не выбраны");
                        return;
                    }
                    country = AllCountry.SelectedItem as EF.Country;

                    ClassHelper.context.Country.Remove(country);
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
            AddTour addTour = new AddTour();
            addTour.ShowDialog();
            this.Close();
        }
    }
}
