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
using static Tourism_AppWPF.EF.ClassHelper;

namespace Tourism_AppWPF.Windows
{
    /// <summary>
    /// Логика взаимодействия для AddHotel.xaml
    /// </summary>
    public partial class AddHotel : Window
    {

        public AddHotel()
        {
            InitializeComponent();
            CBCountry.ItemsSource = context.Country.ToList();
            CBCountry.DisplayMemberPath = "Name";
            CBCountry.SelectedIndex = 0;
        }

        private void TBName_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (sender is TextBox textBox)
            {
                textBox.Text = new string
                    (
                         textBox.Text.Where(ch => (ch >= 'а' && ch <= 'я') || (ch >= ' ' && ch <= '-') || (ch >= 'А' && ch <= 'Я') || (ch >= '-' && ch <= ' ') || ch == 'ё' || ch == 'Ё' || (ch >= 'A' && ch <= 'Z') || (ch >= 'a' && ch <= 'z')).ToArray()
                    );
            }
        }

        private void TBCountStars_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (sender is TextBox textBox)
            {
                textBox.Text = new string
                    (
                         textBox.Text.Where(ch => ch >= '1' && ch <= '9').ToArray()
                    );
            }
        }

        private void TBPrice_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (sender is TextBox textBox)
            {
                textBox.Text = new string
                    (
                         textBox.Text.Where(ch => ch >= '1' && ch <= '9').ToArray()
                    );
            }
        }

        private void BtnBack_Click(object sender, RoutedEventArgs e)
        {
            this.Hide();
            ChangeHotels changeHotels = new ChangeHotels();
            changeHotels.ShowDialog();
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
                if (string.IsNullOrWhiteSpace(TBCountStars.Text))
                {
                    MessageBox.Show("Заполните поле колличество звезд!", "Ошибка!", MessageBoxButton.OK);
                    return;
                }
                if (string.IsNullOrWhiteSpace(TBPrice.Text))
                {
                    MessageBox.Show("Заполните поле цена!", "Ошибка!", MessageBoxButton.OK);
                    return;
                }
                if (string.IsNullOrWhiteSpace(CBCountry.Text))
                {
                    MessageBox.Show("Выберите одну из стран!", "Ошибка!", MessageBoxButton.OK);
                    return;
                }

                try
                {
                    var resClick = MessageBox.Show("Добавить отель?", "Подтверждение.", MessageBoxButton.YesNo, MessageBoxImage.Question);
                    if (resClick == MessageBoxResult.Yes)
                    {
                        EF.Hotel hotel = new EF.Hotel();
                        hotel.Name = TBName.Text;
                        hotel.CountStars = TBCountStars.Text;
                        hotel.Price = Convert.ToDecimal(TBPrice.Text);
                        hotel.IDCountry = Convert.ToInt32(CBCountry.Text);

                        ClassHelper.context.Hotel.Add(hotel);
                        ClassHelper.context.SaveChanges();

                        MessageBox.Show("Отель успешно добавлен.", "Выполнено!", MessageBoxButton.OK, MessageBoxImage.Information);
                        this.Hide();
                        ChangeHotels changeHotels = new ChangeHotels();
                        changeHotels.ShowDialog();
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
