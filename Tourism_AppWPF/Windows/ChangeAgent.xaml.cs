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
    /// Логика взаимодействия для ChangeAgent.xaml
    /// </summary>
    public partial class ChangeAgent : Window
    {

        List<EF.Agent> ListAgent = new List<EF.Agent>();

        public ChangeAgent()
        {
            InitializeComponent();
            AllAgents.ItemsSource = ClassHelper.context.Agent.ToList();
        }

        private void Filter()
        {
            ListAgent = ClassHelper.context.Agent.ToList();
            ListAgent = ListAgent.
            Where(i => i.LastName.Contains(TBSearch.Text)
            || i.FirstName.Contains(TBSearch.Text)
            || i.Patronymic.Contains(TBSearch.Text)).ToList();

            switch (TBSearch.CaretIndex)
            {
                case 0:
                    ListAgent = ListAgent.OrderBy(i => i.ID).ToList();
                    break;

                case 1:
                    ListAgent = ListAgent.OrderBy(i => i.LastName).ToList();
                    break;

                case 2:
                    ListAgent = ListAgent.OrderBy(i => i.FirstName).ToList();
                    break;

                case 3:
                    ListAgent = ListAgent.OrderBy(i => i.Patronymic).ToList();
                    break;

                default:
                    ListAgent = ListAgent.OrderBy(i => i.ID).ToList();
                    break;
            }

            if (ListAgent.Count == 0)
            {
                MessageBox.Show("Записи не найдены");
            }

            AllAgents.ItemsSource = ListAgent;

        }

        private void AllAgents_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Delete || e.Key == Key.Back)
            {
                var resClick = MessageBox.Show($"Удалить агента {(AllAgents.SelectedItem as EF.Agent).FirstName}", "Подтвержение", MessageBoxButton.YesNo, MessageBoxImage.Information);
                MessageBox.Show("Агент успешно удален.", "Выполнено!", MessageBoxButton.OK, MessageBoxImage.Information);


                if (resClick == MessageBoxResult.Yes)
                {
                    EF.Agent agent = new EF.Agent();
                    if (!(AllAgents.SelectedItem is EF.Agent))
                    {
                        MessageBox.Show("Запись не выбраны");
                        return;
                    }
                    agent = AllAgents.SelectedItem as EF.Agent;

                    ClassHelper.context.Agent.Remove(agent);
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
            AddAgents addAgents = new AddAgents();
            addAgents.ShowDialog();
            this.Close();
        }
    }
}
