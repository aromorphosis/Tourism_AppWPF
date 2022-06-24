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
    /// Логика взаимодействия для AddAirline.xaml
    /// </summary>
    public partial class AddAirline : Window
    {
        public AddAirline()
        {
            InitializeComponent();
        }

        private void BtnBack_Click(object sender, RoutedEventArgs e)
        {
            this.Hide();
            ChangeAirlines changeAirlines = new ChangeAirlines();
            changeAirlines.ShowDialog();
            this.Close();
        }

        private void BtnAdd_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(TBName.Text))
            {
                MessageBox.Show("Заполните поле название авиакомпании!", "Ошибка!", MessageBoxButton.OK);
                return;
            }
            if (string.IsNullOrWhiteSpace(TBPrice.Text))
            {
                MessageBox.Show("Заполните поле цена!", "Ошибка!", MessageBoxButton.OK);
                return;
            }

            try
            {
                var resClick = MessageBox.Show("Добавить авиакомпанию?", "Подтверждение.", MessageBoxButton.YesNo, MessageBoxImage.Question);
                if (resClick == MessageBoxResult.Yes)
                {
                    EF.Airline airline = new EF.Airline();
                    airline.Name = TBName.Text;
                    airline.Price = Convert.ToDecimal(TBPrice.Text);

                    ClassHelper.context.Airline.Add(airline);
                    ClassHelper.context.SaveChanges();

                    MessageBox.Show("Авиакомпания успешно добавлена.", "Выполнено!", MessageBoxButton.OK, MessageBoxImage.Information);
                    this.Hide();
                    ChangeAirlines changeAirlines = new ChangeAirlines();
                    changeAirlines.ShowDialog();
                    this.Close();
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString());
                throw;
            }
        }

        private void TBName_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (sender is TextBox textBox)
            {
                textBox.Text = new string
                    (
                         textBox.Text.Where(ch => (ch >= 'A' && ch <= 'Z') || (ch >= ' ' && ch <= '-') || (ch >= 'a' && ch <= 'z') || (ch >= 'а' && ch <= 'я') || (ch >= 'А' && ch <= 'Я')).ToArray()
                     );
            }
        }

        private void TBPrice_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (sender is TextBox textBox)
            {
                textBox.Text = new string
                    (
                         textBox.Text.Where(ch =>ch >= '0' && ch <= '9').ToArray()
                     );
            }
        }
    }
}


