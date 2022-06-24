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
    /// Логика взаимодействия для AddTour.xaml
    /// </summary>
    public partial class AddTour : Window
    {
        public AddTour()
        {
            InitializeComponent();
        }

        private void BtnBack_Click(object sender, RoutedEventArgs e)
        {
            this.Hide();
            ChangeCountry changeCountry = new ChangeCountry();
            changeCountry.ShowDialog();
            this.Close();
        }

        private void BtnAdd_Click(object sender, RoutedEventArgs e)
        {
            {
                if (string.IsNullOrWhiteSpace(TBTourName.Text))
                {
                    MessageBox.Show("Заполните поле название!", "Ошибка!", MessageBoxButton.OK);
                    return;
                }

                try
                {
                    var resClick = MessageBox.Show("Добавить страну?", "Подтверждение.", MessageBoxButton.YesNo, MessageBoxImage.Question);
                    if (resClick == MessageBoxResult.Yes)
                    {
                        EF.Country addCounty = new EF.Country();
                        addCounty.Name = TBTourName.Text;

                        ClassHelper.context.Country.Add(addCounty);
                        ClassHelper.context.SaveChanges();

                        MessageBox.Show("Страна успешно добавлен.", "Выполнено!", MessageBoxButton.OK, MessageBoxImage.Information);
                        this.Hide();
                        ChangeCountry changeCountry = new ChangeCountry();
                        changeCountry.ShowDialog();
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

        private void TBTourName_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (sender is TextBox textBox)
            {
                textBox.Text = new string
                    (
                         textBox.Text.Where(ch => (ch >= 'а' && ch <= 'я') || (ch >= 'А' && ch <= 'Я') || (ch >= '-' && ch <= ' ') || ch == 'ё' || ch == 'Ё' || (ch >= 'A' && ch <= 'Z') || (ch >= 'a' && ch <= 'z')).ToArray()
                    );
            }
        }
    }
}
