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
    /// Логика взаимодействия для Clients.xaml
    /// </summary>
    public partial class Clients : Window
    {

        List<EF.Client> ListClient = new List<EF.Client>();

        public Clients()
        {
            InitializeComponent();
            AllClients.ItemsSource = ClassHelper.context.Client.ToList();

        }

        private void Filter()
        {
            ListClient = ClassHelper.context.Client.ToList();
            ListClient = ListClient.
            Where(i => i.LastName.Contains(SearchTBCL.Text)
            || i.FirstName.Contains(SearchTBCL.Text)
            || i.Patronymic.Contains(SearchTBCL.Text)
            || i.NumberPhone.Contains(SearchTBCL.Text)).ToList();

            switch (SearchTBCL.CaretIndex)
            {
                case 0:
                    ListClient = ListClient.OrderBy(i => i.ID).ToList();
                    break;

                case 1:
                    ListClient = ListClient.OrderBy(i => i.LastName).ToList();
                    break;

                case 2:
                    ListClient = ListClient.OrderBy(i => i.FirstName).ToList();
                    break;

                case 3:
                    ListClient = ListClient.OrderBy(i => i.Patronymic).ToList();
                    break;

                default:
                    ListClient = ListClient.OrderBy(i => i.ID).ToList();
                    break;
            }

            if (ListClient.Count == 0)
            {
                MessageBox.Show("Записи не найдены");
            }

            AllClients.ItemsSource = ListClient;

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

        private void AddClients_Click(object sender, RoutedEventArgs e)
        {
            this.Hide();
            AddClients addClients = new AddClients();
            addClients.ShowDialog();
            this.Close();
        }

        private void AllClients_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Delete || e.Key == Key.Back)
            {
                var resClick = MessageBox.Show($"Удалить клиента {(AllClients.SelectedItem as EF.Client).FirstName}", "Подтвержение", MessageBoxButton.YesNo, MessageBoxImage.Information);


                if (resClick == MessageBoxResult.Yes)
                {
                    EF.Client client = new EF.Client();
                    if (!(AllClients.SelectedItem is EF.Client))
                    {
                        MessageBox.Show("Запись не выбраны");
                        return;
                    }
                    client = AllClients.SelectedItem as EF.Client;

                    ClassHelper.context.Client.Remove(client);
                    ClassHelper.context.SaveChanges();
                }
            }
            Filter();
        }
    }
}
