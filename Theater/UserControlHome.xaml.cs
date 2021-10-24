using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Imaging;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Data.SqlClient;
using System.Data;

namespace Theater
{
    public class Moive_Information
    {
        public string Movie_Name { get; set; }
        public string Movie_Image { get; set; }
        public string Movie_Actors { get; set; }
        public string Movie_RelDate { get; set; }
        public string Movie_Director { get; set; }
        public string Movie_Intro { get; set; }
        public string Movie_Type { get; set; }
    }
    /// <summary>
    /// UserControlHome.xaml 的交互逻辑
    /// </summary>
    public partial class UserControlHome : UserControl
    {
        private string film_con = MainWindow.DataBasePath.dataBasePath;
        private static List<Moive_Information> movie_information = new List<Moive_Information>();
        private int movie_Num = 0;

        public UserControlHome()
        {
            InitializeComponent();
            SqlConnection con = new SqlConnection(film_con);
            con.Open();
            string sqlsearch = @"select* from movie_Info";
            DataSet ds = new DataSet();
            SqlCommand sqlcmd = new SqlCommand(sqlsearch, con);
            SqlDataReader dr = sqlcmd.ExecuteReader();
            //读取数据
            while (dr.Read())
            {
                movie_information.Add(new Moive_Information
                {
                    Movie_Name = dr["movie_Name"].ToString(),
                    Movie_Image = dr["movie_Image"].ToString(),
                    Movie_Actors = dr["movie_Actors"].ToString(),
                    Movie_RelDate = dr["movie_RelDate"].ToString(),
                    Movie_Director = dr["movie_Director"].ToString(),
                    Movie_Intro = dr["movie_Intro"].ToString(),
                    Movie_Type = dr["movie_Type"].ToString(),
                });
            }
            con.Close();
            Film_title.Text = movie_information[movie_Num].Movie_Name;
            Film_Type.Text = movie_information[movie_Num].Movie_Type;
            Film_Director.Text = movie_information[movie_Num].Movie_Director;
            Film_Actors.Text = movie_information[movie_Num].Movie_Actors;
            Film_RelDate.Text = movie_information[movie_Num].Movie_RelDate;
            Intro.Text = movie_information[movie_Num].Movie_Intro;
            string s = movie_information[movie_Num].Movie_Image;
            Film_Image.Source = new BitmapImage(new Uri(s, UriKind.Relative));
        }

        private void Next_Movie_Click(object sender, RoutedEventArgs e)
        {
            int movie_len = movie_information.ToArray().Length;
            movie_Num++;
            movie_Num %= movie_len;
            Film_title.Text = movie_information[movie_Num].Movie_Name;
            Film_Type.Text = movie_information[movie_Num].Movie_Type;
            Film_Director.Text = movie_information[movie_Num].Movie_Director;
            Film_Actors.Text = movie_information[movie_Num].Movie_Actors;
            Film_RelDate.Text = movie_information[movie_Num].Movie_RelDate;
            Intro.Text = movie_information[movie_Num].Movie_Intro;
            string s = movie_information[movie_Num].Movie_Image;
            Film_Image.Source = new BitmapImage(new Uri(s, UriKind.Relative));
        }
    }
}
