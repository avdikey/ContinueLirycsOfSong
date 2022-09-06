using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ContinueLirycsOfSong
{
    class data
    {
        public static string name_song { get; set; }//название песни
        public static TimeSpan[] timecode { get; set; } = new TimeSpan[10];//данные о том где останавливать программе песню
        public static string[] temptimecodes { get; set; } = new string[21];
        public static int[] timeforcode { get; set; } = new int[10];
        public static string[] answers { get; set; } = new string[10];//текст массив для слов песни и ответов
        public static int quest { get; set; }//номер отрывка который сейчас идет
        public static Uri url { get; set; }//ссылка на песню
        public static string url_file { get; set; }//ссылка на песню
    }
}
