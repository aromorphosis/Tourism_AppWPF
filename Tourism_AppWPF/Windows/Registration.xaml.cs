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
    /// Логика взаимодействия для Registration.xaml
    /// </summary>
    public partial class Registration : Window
    {
        public Registration()
        {
            InitializeComponent();
        }

        private void BtnVhod_Click(object sender, RoutedEventArgs e)
        {
            var AgentAuth = ClassHelper.context.Agent.ToList().
            Where(p => p.Login == this.TBLogin.Text && p.Password == this.TBPassword.Text).
            FirstOrDefault();

            if (AgentAuth != null)
            {
                this.Hide();
                MainMenu mainMenu = new MainMenu();
                mainMenu.Show();
                this.Close();
            }

            var AdminAuth = ClassHelper.context.Admin.ToList().
            Where(p => p.Login == this.TBLogin.Text && p.Password == this.TBPassword.Text).
            FirstOrDefault();

            if (AdminAuth != null)
            {
                this.Hide();
                MainWindowAdmin mainWindowAdmin = new MainWindowAdmin();
                mainWindowAdmin.Show();
                this.Close();
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

    }
}
