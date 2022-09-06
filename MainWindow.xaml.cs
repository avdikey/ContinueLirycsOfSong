using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Microsoft.Win32;

namespace ContinueLirycsOfSong
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            Setinn.language = 0;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            if (Setinn.language == 0)
            {
                openFileDialog.Filter = "Музыкальный файл (*.mp3)|*.mp3";
            }
            else if (Setinn.language == 1)
            {
                openFileDialog.Filter = "Музичний файл (*.mp3)|*.mp3";
            }
            if (openFileDialog.ShowDialog() == true)
            {
                data.url_file = openFileDialog.FileName.ToString().Replace(".mp3", "_data.txt");
                data.url = new Uri(openFileDialog.FileName);
                FileStream fsfile = new FileStream(data.url_file, FileMode.Open);
                StreamReader srdata = new StreamReader(fsfile);
                data.name_song = srdata.ReadLine();
                for (int i = 0; i < 20; i++)
                    data.temptimecodes[i] = srdata.ReadLine();
                for (int i = 0; i < 10; i++)
                {
                    string temp1;
                    string temp2;
                    int temp3;
                    int temp4;
                    string varr = data.temptimecodes[i];
                    temp1 = varr[0].ToString() + varr[1].ToString();
                    temp2 = varr[3].ToString() + varr[4].ToString();
                    temp3 = Convert.ToInt32(temp1);
                    temp4 = Convert.ToInt32(temp2);
                    data.timecode[i] = new TimeSpan(0, temp3, temp4);
                }
                for (int i = 10; i < 20; i++)
                {
                    data.timeforcode[i - 10] = Convert.ToInt32(data.temptimecodes[i]);
                }
                for (int i = 19; i < 29; i++)
                    data.answers[i-19] = srdata.ReadLine();
                srdata.Close();

                game pre = new game();
                pre.Show();
                this.Close();
            }
        }
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            Gradient_Animation_Random_Play();
            sloganScale.BeginAnimation(ScaleTransform.ScaleXProperty, new DoubleAnimation
            {
                From = 1,
                To = 1.1,
                Duration = TimeSpan.FromSeconds(1),
                RepeatBehavior = RepeatBehavior.Forever,
                AutoReverse = true
            });
        }
        private Color gradientfirstcolor;
        private Color gradientsecondcolor;

        private void Gradient_Animation_Random()
        {
            var rnd = new Random();
            gradientfirstcolor = Color.FromArgb(255, (byte)rnd.Next(256), (byte)rnd.Next(256), (byte)rnd.Next(256));
            gradientsecondcolor = Color.FromArgb(255, (byte)rnd.Next(256), (byte)rnd.Next(256), (byte)rnd.Next(256));
        }

        private void Gradient_Animation_Random_Play()
        {
            Gradient_Animation_Random();
            gradientfirst.BeginAnimation(GradientStop.ColorProperty, new ColorAnimation
            {
                To = gradientfirstcolor,
                Duration = TimeSpan.FromSeconds(20)
            });
            ColorAnimation gradientsecondanimation;
            gradientsecondanimation = new ColorAnimation();
            gradientsecondanimation.To = gradientsecondcolor;
            gradientsecondanimation.Duration = TimeSpan.FromSeconds(20);
            gradientsecondanimation.Completed += Gradient_Animation_Random_Play_Completed;
            gradientsecond.BeginAnimation(GradientStop.ColorProperty, gradientsecondanimation);
        }

        private void Gradient_Animation_Random_Play_Completed(object sender, EventArgs e)
        {
            Gradient_Animation_Random_Play();
        }

        private void MenuItem_Click(object sender, RoutedEventArgs e)
        {
            settings set = new settings();
            set.Show();
        }

        private void Window_Activated(object sender, EventArgs e)
        {
            if (Setinn.language == 0)
            {
                slogan.Text = "игра «Продолжи слова песни»";
                slogan.ToolTip = "Чтобы начать игру, выбери файл в меню Игра > Начать игру";
                tbl1.Title = "Главная: Продолжи слова песни";
                tbl2.Header = "Игра";
                tbl3.Header = "Выбрать файл игры и начать игру";
                tbl4.Header = "Инструкции";
                tbl4_1.Header = "Как играть";
                tbl4_2.Header = "Как создать файл игры";
                tbl5.Header = "Выход";
                tbl6.Header = "Настройки";
            }
            else if (Setinn.language == 1)
            {
                slogan.Text = "гра «Продовжи слова пісні»";
                slogan.ToolTip = "Щоб розпочати гру, обери гральний файл в меню Гра > Почати гру";
                tbl1.Title = "Головна: Продовжи слова пісні";
                tbl2.Header = "Гра";
                tbl3.Header = "Обрати файл й почати гру";
                tbl4.Header = "Інструкції";
                tbl4_1.Header = "Як грати";
                tbl4_2.Header = "Як створити файл гри";
                tbl5.Header = "Вихід";
                tbl6.Header = "Налаштування";
            }
        }

        private void tbl2_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void tbl4_Click(object sender, RoutedEventArgs e)
        {
            if (Setinn.language == 0)
                MessageBox.Show("Вместе с программой у вас должны быть два файла игры: [название].mp3 и [название]_data.txt. Важно: имя файлов должно быть одинаково, различие в имени должно быть только в расширении (на предыдущем примере это то, что идет после [название]).\r\nЧтобы начать игру, нажмите \"Игра\" > \"Выбрать файл игры и начать игру\". После этого появится диалоговое окно, в котором вы должны выбрать музыкальный файл [название].mp3, второй файл выберется сам, если он в одной папке с музыкальным файлом. \r\nПосле выбора файла откроется новое окно. Нажмите кнопку \"Повторить\" для воспроизведения композиции (требуется только при запуске окна) и начинайте угадывать недостающие слова фрагмента. По завершению угадывания слов фрагмента, нажмите кнопку \"Следующий фрагмент\". \r\nХорошей игры!");
            else if (Setinn.language == 1)
                MessageBox.Show("Разом з програмою у вас має бути два файла гри: [назва].mp3 і [назва]_data.txt. Важливо: імена файлів мають бути однаковими, відмінність в іменах має бути тільки в розширенні файлу (на попередньому прикладі це те, що йде після [назва]). \r\nЩоб почати гру, натисніть \"Гра\" > \"Обрати файл і почати гру\". Після цього з'явиться діалогове вікно, в якому ви маєте обрати музичний файл [назва].mp3, другий файл обереться сам, якщо він в одній папці з музичним файлом. \r\nПісля вибору файлу відкриється нове вікно. Натисніть кнопку \"Повторити\" для відтворення композиції (потрібно тільки при запуску вікна) і починайте вгадувати відсутні слова фрагмента. По завершенню вгадування слів фрагмента, натисніть кнопку \"Наступний фрагмент\". \r\nГарної вам гри!");
        }
        private void tbl4_Click_2(object sender, RoutedEventArgs e)
        {
            if (Setinn.language == 0)
                MessageBox.Show("Подготовьте два файла: уже созданный (или скачанный) музыкальный файл [название].mp3 и новый текстовый документ [название]_data.txt. Важно: имя файлов должно быть одинаково, различие в имени должно быть только в расширении (на предыдущем примере это то, что идет после [название]).\r\nФормат заполнения нового текстового документа:\r\n1 строка - название песни. Не делайте слишком длинное названиe\r\n2-11 строки - время начала фрагмента 1-10 (формат - 01:28 (пять символов); 11 строка - для концовки композиции, где уже ничего угадывать не нужно будет).\r\n12-21 строки: длительность фрагмента 1-10. Используйте одну или две цифры без пробелов. \r\n22-31 строки - слова фрагмента. В 31 строке ничего угадывать уже не нужно, это для концовки. Для разрыва строки среди фрагмента используйте символ амперсанд (&). Для удобства можно использовать символ нижнего подчеркивания (_) на месте отсутствующих букв и слов фрагмента. За желанием можно добавить в строках 22-31 троеточие перед ответом на предыдущий фрагмент. \r\nЗа образец можно использовать любой файл игры _data.txt, который вы получили вместе с программой.");
            else if (Setinn.language == 1)
                MessageBox.Show("Підготуйте два файли: вже створений (або скачаний) музичний файл [назва].mp3 і новий текстовий документ [назва]_data.txt. Важливо: ім'я файлів повинне бути однаково, відмінність в імені має бути тільки в розширенні файлу (на попередньому прикладі це те, що йде після [назва]).\r\nФормат заповнення нового текстового документа:\r\n1 рядок - назва пісні. Не робіть занадто довгу назву.\r\n2-11 рядки - час початку фрагмента 1-10 (формат - 1:28 (п'ять символів); ​​11 рядок - для кінцівки композиції, де вже нічого вгадувати не потрібно буде). \r\n12-21 рядки: тривалість фрагмента 1-10. Використовуйте одну або дві цифри без пробілів.\r\n22-31 рядки - слова фрагмента. У 31 рядку нічого вгадувати вже не потрібно, це для кінцівки. Для розриву рядка серед фрагмента використовуйте символ амперсанд (&). Для зручності можна використовувати символ нижнього підкреслення (_) на місці відсутніх букв і слів фрагмента. За бажанням можна додати в рядках 23-31 три крапки перед відповіддю на попередній фрагмент. \r\nЗа зразок можна використовувати будь-який файл гри _data.txt, який ви отримали разом з програмою.");
        }
    }
    public static class Setinn
    {
        public static int language;
    }
}
