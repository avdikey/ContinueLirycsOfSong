using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Threading;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace ContinueLirycsOfSong
{
    /// <summary>
    /// Логика взаимодействия для game.xaml
    /// </summary>
    public partial class game : Window
    {
        public game()
        {
            InitializeComponent();
        }

        private void button_next_Click(object sender, RoutedEventArgs e)
        {
            data.quest += 1;
            textbox_lyrics.Clear();
            ChangeButtonNumber();
            LirycsRN();
            playmusic();
            if (data.quest == 9)
            {
                button_next.IsEnabled = false;
                button9.IsEnabled = false;
            }
        }
        private void LirycsRN()
        {
            string vs = data.answers[data.quest];
            char temp;
            for (int i = 0; i < vs.Length; i++)
            {
                temp = vs[i];
                if (temp == '&')
                {
                    textbox_lyrics.Text += "\r\n";
                }
                else
                {
                    textbox_lyrics.Text += temp;
                }
            }
        }

        private void ChangeButtonNumber()
        {
            if (data.quest !=9)
            {
                Button[] T = { button1, button2, button3, button4, button5, button6, button7, button8, button9 };
                T[data.quest - 1].IsEnabled = false;
                T[data.quest].IsEnabled = true;
            }
        }

        private void title_window_Activated(object sender, EventArgs e)
        {
            if (Setinn.language == 0)
            {
                pause.ToolTip = "Стоп";
                repeat.ToolTip = "Повторить";
                title_window.Title = "Мини-игра: Продолжи слова песни";
                button_next.Content = "Следующий фрагмент";
            }
            else if (Setinn.language == 1)
            {
                pause.ToolTip = "Стоп";
                repeat.ToolTip = "Повторити";
                title_window.Title = "Міні-гра: Продовжи слова пісні";
                button_next.Content = "Наступний фрагмент";
            }

        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            Gradient_Animation_Random_Play();
            header.Text = data.name_song;
            player.Source = data.url;
            data.quest = 0;
            LirycsRN();
            timer = new DispatcherTimer();
            timer.Interval = TimeSpan.FromSeconds(1);
            timer.Tick += timer_Tick;
        }

        int templton = 0;

        void timer_Tick(object sender, EventArgs e)
        {
            templton += 1;
            if (templton == data.timeforcode[data.quest])
            {
                player.Stop();
                player.Position = data.timecode[data.quest];
                timer.Stop();
            }
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

        private void pause_Click(object sender, RoutedEventArgs e)
        {
            player.Pause();
            timer.Stop();
        }

        private void repeat_Click(object sender, RoutedEventArgs e)
        {
            playmusic();
        }

        public DispatcherTimer timer;

        private void player_MediaOpened(object sender, RoutedEventArgs e)
        {
            playmusic();
        }
        private void playmusic ()
        {
            player.Position = data.timecode[data.quest];
            player.Play();
            templton = 0;
            timer.Start();
        }
    }
}

