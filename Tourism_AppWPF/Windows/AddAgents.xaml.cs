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
    /// Логика взаимодействия для AddAgents.xaml
    /// </summary>
    public partial class AddAgents : Window
    {
        public AddAgents()
        {
            InitializeComponent();
        }

        private void TBFirstName_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (sender is TextBox textBox)
            {
                textBox.Text = new string
                    (
                         textBox.Text.Where(ch => (ch >= 'а' && ch <= 'я') || (ch >= 'А' && ch <= 'Я') || (ch >= '-' && ch <= ' ') || ch == 'ё' || ch == 'Ё' || (ch >= 'A' && ch <= 'Z') || (ch >= 'a' && ch <= 'z')).ToArray()
                    );
            }
        }

        private void TBLastName_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (sender is TextBox textBox)
            {
                textBox.Text = new string
                    (
                         textBox.Text.Where(ch => (ch >= 'а' && ch <= 'я') || (ch >= 'А' && ch <= 'Я') || (ch >= '-' && ch <= ' ') || ch == 'ё' || ch == 'Ё' || (ch >= 'A' && ch <= 'Z') || (ch >= 'a' && ch <= 'z')).ToArray()
                    );
            }
        }

        private void TBPatronymic_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (sender is TextBox textBox)
            {
                textBox.Text = new string
                    (
                         textBox.Text.Where(ch => (ch >= 'а' && ch <= 'я') || (ch >= 'А' && ch <= 'Я') || (ch >= '-' && ch <= ' ') || ch == 'ё' || ch == 'Ё' || (ch >= 'A' && ch <= 'Z') || (ch >= 'a' && ch <= 'z')).ToArray()
                    );
            }
        }

        private void TBLogin_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (sender is TextBox textBox)
            {
                textBox.Text = new string
                    (
                         textBox.Text.Where(ch => (ch >= 'A' && ch <= 'Z') || (ch >= 'a' && ch <= 'z') || (ch >= '0' && ch <= '9')).ToArray()
                    );
            }
        }

        private void TBPassword_TextChanged(object sender, TextChangedEventArgs e)
        {
           if (sender is TextBox textBox)
            {
                textBox.Text = new string
                    (
                         textBox.Text.Where(ch => (ch >= 'A' && ch <= 'Z') || (ch >= 'a' && ch <= 'z') || (ch >= '0' && ch <= '9')).ToArray()
                     );
            }
        }

        private void BtnBack_Click(object sender, RoutedEventArgs e)
        {
            this.Hide();
            ChangeAgent changeAgent = new ChangeAgent();
            changeAgent.ShowDialog();
            this.Close();
        }

        private void BtnAdd_Click(object sender, RoutedEventArgs e)
        {
            {
                if (string.IsNullOrWhiteSpace(TBFirstName.Text))
                {
                    MessageBox.Show("Заполните поле имя!", "Ошибка!", MessageBoxButton.OK);
                    return;
                }
                if (string.IsNullOrWhiteSpace(TBLastName.Text))
                {
                    MessageBox.Show("Заполните поле фамилия!", "Ошибка!", MessageBoxButton.OK);
                    return;
                }
                if (string.IsNullOrWhiteSpace(TBPatronymic.Text))
                {
                    MessageBox.Show("Заполните поле отчество!", "Ошибка!", MessageBoxButton.OK);
                    return;
                }
                if (string.IsNullOrWhiteSpace(TBLogin.Text))
                {
                    MessageBox.Show("Заполните поле логин!", "Ошибка!", MessageBoxButton.OK);
                    return;
                }
                if (string.IsNullOrWhiteSpace(TBPassword.Text))
                {
                    MessageBox.Show("Заполните поле пароль!", "Ошибка!", MessageBoxButton.OK);
                    return;
                }

                try
                {
                    var resClick = MessageBox.Show("Добавить агента?", "Подтверждение.", MessageBoxButton.YesNo, MessageBoxImage.Question);
                    if (resClick == MessageBoxResult.Yes)
                    {
                        EF.Agent addAgent = new EF.Agent();
                        addAgent.FirstName = TBFirstName.Text;
                        addAgent.LastName = TBLastName.Text;
                        addAgent.Patronymic = TBPatronymic.Text;
                        addAgent.Login = TBLogin.Text;
                        addAgent.Password = TBPassword.Text;

                        ClassHelper.context.Agent.Add(addAgent);
                        ClassHelper.context.SaveChanges();

                        MessageBox.Show("Агент успешно добавлен.", "Выполнено!", MessageBoxButton.OK, MessageBoxImage.Information);
                        this.Hide();
                        ChangeAgent changeAgent = new ChangeAgent();
                        changeAgent.ShowDialog();
                        this.Close();
                    }

                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message.ToString());
                    throw;
                }
            }
        }
    }
}
