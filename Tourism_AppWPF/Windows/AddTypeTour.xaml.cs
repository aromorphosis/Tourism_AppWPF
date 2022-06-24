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
    /// Логика взаимодействия для AddTypeTour.xaml
    /// </summary>
    public partial class AddTypeTour : Window
    {
        public AddTypeTour()
        {
            InitializeComponent();
        }

        private void TBName_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (sender is TextBox textBox)
            {
                textBox.Text = new string
                    (
                         textBox.Text.Where(ch => (ch >= 'а' && ch <= 'я') || (ch >= ' ' && ch <= '-') || (ch >= 'А' && ch <= 'Я')  || ch == 'ё' || ch == 'Ё' || (ch >= 'A' && ch <= 'Z') || (ch >= 'a' && ch <= 'z')).ToArray()
                    );
            }
        }

        private void TBPrice_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (sender is TextBox textBox)
            {
                textBox.Text = new string
                    (
                         textBox.Text.Where(ch => ch >= '0' && ch <= '9').ToArray()
                    );
            }
        }

        private void BtnBack_Click(object sender, RoutedEventArgs e)
        {
            this.Hide();
            ChangeTypeTour changeTypeTour = new ChangeTypeTour();
            changeTypeTour.ShowDialog();
            this.Close();
        }

        private void BtnAdd_Click(object sender, RoutedEventArgs e)
        {
            {
                if (string.IsNullOrWhiteSpace(TBName.Text))
                {
                    MessageBox.Show("Заполните поле название!", "Ошибка!", MessageBoxButton.OK);
                    return;
                }
                if (string.IsNullOrWhiteSpace(TBPrice.Text))
                {
                    MessageBox.Show("Заполните поле цена!", "Ошибка!", MessageBoxButton.OK);
                    return;
                }

                try
                {
                    var resClick = MessageBox.Show("Добавить тип тура?", "Подтверждение.", MessageBoxButton.YesNo, MessageBoxImage.Question);
                    if (resClick == MessageBoxResult.Yes)
                    {
                        EF.TypeTour addTypeTour = new EF.TypeTour();
                        addTypeTour.Name = TBName.Text;
                        addTypeTour.Price = Convert.ToDecimal(TBPrice.Text);

                        ClassHelper.context.TypeTour.Add(addTypeTour);
                        ClassHelper.context.SaveChanges();

                        MessageBox.Show("Тип тура успешно добавлен.", "Выполнено!", MessageBoxButton.OK, MessageBoxImage.Information);
                        this.Hide();
                        ChangeTypeTour changeTypeTour = new ChangeTypeTour();
                        changeTypeTour.ShowDialog();
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
