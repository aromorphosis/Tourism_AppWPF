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
    /// Логика взаимодействия для ChangeTypeTour.xaml
    /// </summary>
    public partial class ChangeTypeTour : Window
    {

        List<EF.TypeTour> ListTypeTour = new List<EF.TypeTour>();

        public ChangeTypeTour()
        {
            InitializeComponent();
            AllTypeTours.ItemsSource = ClassHelper.context.TypeTour.ToList();
        }

        private void Filter()
        {
            ListTypeTour = ClassHelper.context.TypeTour.ToList();
            ListTypeTour = ListTypeTour.
            Where(i => i.Name.Contains(TBSearch.Text)).ToList();

            switch (TBSearch.CaretIndex)
            {
                case 0:
                    ListTypeTour = ListTypeTour.OrderBy(i => i.ID).ToList();
                    break;

                case 1:
                    ListTypeTour = ListTypeTour.OrderBy(i => i.Name).ToList();
                    break;

                default:
                    ListTypeTour = ListTypeTour.OrderBy(i => i.ID).ToList();
                    break;
            }

            if (ListTypeTour.Count == 0)
            {
                MessageBox.Show("Записи не найдены");
            }

            AllTypeTours.ItemsSource = ListTypeTour;

        }

        private void AllTypeTours_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Delete || e.Key == Key.Back)
            {
                var resClick = MessageBox.Show($"Удалить тип тура {(AllTypeTours.SelectedItem as EF.TypeTour).Name}", "Подтвержение", MessageBoxButton.YesNo, MessageBoxImage.Information);


                if (resClick == MessageBoxResult.Yes)
                {
                    EF.TypeTour typeTour = new EF.TypeTour();
                    if (!(AllTypeTours.SelectedItem is EF.TypeTour))
                    {
                        MessageBox.Show("Запись не выбраны");
                        return;
                    }
                    typeTour = AllTypeTours.SelectedItem as EF.TypeTour;

                    ClassHelper.context.TypeTour.Remove(typeTour);
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

        private void TBSearch_SelectionChanged(object sender, RoutedEventArgs e)
        {
            Filter();
        }

        private void BtnAdd_Click(object sender, RoutedEventArgs e)
        {
            this.Hide();
            AddTypeTour addTypeTour = new AddTypeTour();
            addTypeTour.ShowDialog();
            this.Close();
        }
    }
}
