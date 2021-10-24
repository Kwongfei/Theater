using MaterialDesignThemes.Wpf;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Theater
{
    /// <summary>
    /// FilmsManagement_AddFilms.xaml 的交互逻辑
    /// </summary>
    public partial class FilmsManagement_AddFilms : UserControl
    {
        public delegate void AddFilm(string value1, string value2, string value3, string value4, string value5, string value6, string value7,int value8);
        public AddFilm addFilm;
        public Int64 num2;
        public FilmsManagement_AddFilms()
        {
            InitializeComponent();
        }
        private void AddFilm_Click(object sender, RoutedEventArgs e)
        {
            string movie_name = Textbox_Moviename.Text;
            string movie_director = Textbox_Director.Text;
            string movie_actors = Textbox_Actors.Text;
            string movie_type = Textbox_Type.Text;
            string movie_date = Textbox_MovieDate.Text;
            string movie_intro = Textbox_MovieIntro.Text;
            string movie_length = TextBox_Length.Text;
            
            string movie_image= Textbox_Image.Text;

            Moviename_Tip.Text = "";
            Director_Tip.Text = "";
            Actors_Tip.Text = "";
            Type_Tip.Text = "";
            Date_Tip.Text = "";
            Intro_Tip.Text = "";
            Image_Tip.Text = "";
            //用户名、导演、主演、类型、日期、简介、图片为空的显示
            if (Textbox_Moviename.Text == "")
            {
                Moviename_Tip.Text = "电影名为空，重新输入电影名！";
                return;
            }
            else if (Textbox_Director.Text  == "" )
            {
                Director_Tip.Text = "导演为空，重新输入导演！";
                return;
            }
            else if (Textbox_Actors.Text =="")
            {
                Actors_Tip.Text = "主演为空，重新输入主演！";
                return;
            }
            else if (Textbox_Type.Text == "")
            {
                Type_Tip.Text = "电影类型为空，重新输入电影类型！";
                return;
            }
            else if (Textbox_MovieDate.Text == "")
            {
                Date_Tip.Text = "上映日期为空，重新选择上映日期！";
                return;
            }
            else if (Textbox_MovieIntro.Text == "")
            {
                Intro_Tip.Text = "电影简介为空，重新输入电影简介！";
                return;
            }
            else if (Textbox_Image.Text == "")
            {
                Image_Tip.Text = "电影海报为空，重新选择电影海报！";
                return;
            }
            else if (Int64.TryParse(TextBox_Length.Text, out num2) == false)//判断输入的电影时长是否为整数
            {
                length_Tip.Text = "电影时长必须为正整数，重新输入时长！";
                TextBox_Length.Text = "";
                return;
            }
            else
            {
                Moviename_Tip.Text = "";
                Director_Tip.Text = "";
                Actors_Tip.Text = "";
                Type_Tip.Text = "";
                Date_Tip.Text = "";
                Intro_Tip.Text = "";
                Image_Tip.Text = "";
                int movie_length1 = Convert.ToInt32(movie_length);
                addFilm(movie_name ,movie_director, movie_actors, movie_type, movie_date, movie_intro, movie_image, movie_length1);
                DialogHost.CloseDialogCommand.Execute(null, null);
            }



        }

    }
}
