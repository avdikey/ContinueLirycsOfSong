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
using System.Windows.Shapes;
using Microsoft.Win32;

namespace ContinueLirycsOfSong
{
    /// <summary>
    /// Логика взаимодействия для pregame.xaml
    /// </summary>
    public partial class pregame : Window
    {
        public pregame()
        {
            InitializeComponent();
        }
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            Gradient_Animation_Random_Play();
        }
        private Color gradientfirstcolor;
        private Color gradientsecondcolor;
        private bool check1 = false;
        private bool check2 = false;
        
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

        private void title_window_Activated(object sender, EventArgs e)
        {
            if (Setinn.language == 0)
            {
                header.Text = "Чтобы начать игру, выберите три файла:";
                title_window.Title = "Загрузка файла: Продолжи слова песни";
                button_load_song.Content = "Выбрать";
                button_load_timecodes.Content = "Выбрать";
                label1.Text = "Песня";
                label2.Text = "Сведения о словах и фрагментах в песне";
            }
            else if (Setinn.language == 1)
            {
                header.Text = "Щоб розпочати гру, оберіть три файла:";
                title_window.Title = "Завантаження файла: Продовжи слова пісні";
                button_load_song.Content = "Обрати";
                button_load_timecodes.Content = "Обрати";
                label1.Text = "Пісня";
                label2.Text = "Відомості про слова та фрагменти в пісні";
            }
        }

        private void button_load_song_Click(object sender, RoutedEventArgs e)
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
                check1 = true;
                file1.Text = openFileDialog.SafeFileName;
                data.url = new Uri (openFileDialog.FileName);
                button_load_song.IsEnabled = false;
            } 
        }

        private void button_load_timecodes_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            if (Setinn.language == 0)
            {
                openFileDialog.Filter = "Текстовой файл для отметок (*.txt)|*.txt";
            }
            else if (Setinn.language == 1)
            {
                openFileDialog.Filter = "Текстовий файл для позначок (*.txt)|*.txt";
            }
            if (openFileDialog.ShowDialog() == true)
            {
                check2 = true;
                file2.Text = openFileDialog.SafeFileName;
                FileStream fsfile = new FileStream(openFileDialog.FileName, FileMode.Open);
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
                srdata.Close();
                button_load_timecodes.IsEnabled = false;
            }
            //if (check1 == check2 == check3 == true)
            //{
                button_start_game.Visibility = Visibility.Visible;
            //}

        }

        private void button_load_answers_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            if (Setinn.language == 0)
            {
                openFileDialog.Filter = "Текстовой файл для ответов (*.txt)|*.txt";
            }
            else if (Setinn.language == 1)
            {
                openFileDialog.Filter = "Текстовий файл для відповідей (*.txt)|*.txt";
            }
            if (openFileDialog.ShowDialog() == true)
            {
            //    check3 = true;
            //    file3.Text = openFileDialog.SafeFileName;
                FileStream fsfile = new FileStream(openFileDialog.FileName, FileMode.Open);
                StreamReader srdata = new StreamReader(fsfile);
                for (int i = 0; i < 10; i++)
                    data.answers[i] = srdata.ReadLine();
                srdata.Close();
            //    button_load_answers.IsEnabled = false;
            }
            //if (check1==check2==check3==true)
            //{
                button_start_game.Visibility = Visibility.Visible;
            //}
        }
        
        private void button_start_game_Click(object sender, RoutedEventArgs e)
        {
            game gamewindow = new game();
            gamewindow.Show();
            this.Close();
        }
    }
}
