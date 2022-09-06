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

namespace ContinueLirycsOfSong
{
    /// <summary>
    /// Логика взаимодействия для settings.xaml
    /// </summary>
    public partial class settings : Window
    {
        public settings()
        {
            InitializeComponent();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            language_choice.SelectedItem = Setinn.language;
            if (Setinn.language == 1)
            {
                tb1.Text = "Мова інтерфейсу:";
                tb2.Text = "Розробник: avdikey.";
                settings_form.Title = "Налаштування";
            }
            else if (Setinn.language == 0)
            {
                tb1.Text = "Язык интерфейса:";
                tb2.Text = "Разработчик: avdikey.";
                settings_form.Title = "Настройки";
            }
        }

        private void language_choice_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Setinn.language = language_choice.SelectedIndex;
        }

        private void settings_form_Activated(object sender, EventArgs e)
        {
            language_choice.SelectedIndex = Setinn.language;
        }
    }
}
