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
    /// Логика взаимодействия для AddClients.xaml
    /// </summary>
    public partial class AddClients : Window
    {
        public AddClients()
        {
            InitializeComponent();
        }

        private void BtnBack_Click(object sender, RoutedEventArgs e)
        {
            this.Hide();
            Clients clients = new Clients();
            clients.ShowDialog();
            this.Close();
        }

        private void BtnAdd_Click(object sender, RoutedEventArgs e)
        {
            {
                if (string.IsNullOrWhiteSpace(TBName.Text))
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
                if (string.IsNullOrWhiteSpace(TBNumberPhone.Text))
                {
                    MessageBox.Show("Заполните поле номер телефона!", "Ошибка!", MessageBoxButton.OK);
                    return;
                }
              
                try
                {
                    var resClick = MessageBox.Show("Добавить пользователя?", "Подтверждение.", MessageBoxButton.YesNo, MessageBoxImage.Question);
                    if (resClick == MessageBoxResult.Yes)
                    {
                        EF.Client addClient = new EF.Client();
                        addClient.FirstName = TBName.Text;
                        addClient.LastName = TBLastName.Text;
                        addClient.Patronymic = TBPatronymic.Text;
                        addClient.NumberPhone = TBNumberPhone.Text;

                        ClassHelper.context.Client.Add(addClient);
                        ClassHelper.context.SaveChanges();

                        MessageBox.Show("Пользователь успешно добавлен.", "Выполнено!", MessageBoxButton.OK, MessageBoxImage.Information);
                        this.Hide();
                        Clients clients = new Clients();
                        clients.ShowDialog();
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

        private void TBName_TextChanged(object sender, TextChangedEventArgs e)
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

       

        private void TBNumberPhone_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (sender is TextBox textBox)
            {
                textBox.Text = new string
                    (
                         textBox.Text.Where(ch => ch == '+' || (ch >= '0' && ch <= '9') || ch == '(' || ch == ')').ToArray()
                    );
            }
        }

        private void TBPatronymic_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (sender is TextBox textBox)
            {
                textBox.Text = new string
                    (
                         textBox.Text.Where(ch => (ch >= 'а' && ch <= 'я') || (ch >= 'А' && ch <= 'Я') || ch == 'ё' || ch == 'Ё' || (ch >= 'A' && ch <= 'Z') || (ch >= 'a' && ch <= 'z')).ToArray()
                    );
            }
        }

    }
}
