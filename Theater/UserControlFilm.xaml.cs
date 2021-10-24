
using System.Windows;
using System.Windows.Controls;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.IO;



namespace Theater
{
    public class FilmsList
    {
        public string Movie_Name { get; set; }
        public string Movie_Image { get; set; }
        public string Movie_Actors { get; set; }
        public string Movie_RelDate { get; set; }
        public string Movie_Director { get; set; }
        public string Movie_Intro { get; set; }
        public string Movie_Type { get; set; }
        public string Cinema_Name { get; set; }
        public string Cinema_address { get; set; }
        public string Hall_Name { get; set; }
        public string Screening_Date { get; set; }
        public string Screening_StartTime { get; set; }
        public string Screening_EndTime { get; set; }
        public string Screening_Price { get; set; }
        public string Screening_Id { get; set; }
        public string Ticket_userId { get; set; }
        public string Ticket_ScreeningId { get; set; }
        public string Ticket_SeatNo { get; set; }
    }
        /// <summary>
        /// UserControlFilm.xaml 的交互逻辑
        /// </summary>
        public partial class UserControlFilm : UserControl
    {
        public static string Tag2;
        public static string Tag3;
        public static string Tag4;
        public static FilmsList chooseddFilm;
        public UserControlFilm()
        {
            InitializeComponent();
            Showdata();
        }
        private void FilmsMore_Click(object sender, RoutedEventArgs e)
        {
            UserControlFilmsMore filmsMore = new UserControlFilmsMore();

        }
        private string film_con = MainWindow.DataBasePath.dataBasePath;

        private void Showdata() //数据库连接、读入
        {
            SqlConnection con = new SqlConnection(film_con);
            con.Open();
            SqlCommand cmd = con.CreateCommand();
            cmd.CommandText = "select* from movie_info";
            SqlDataReader dr = cmd.ExecuteReader();
            while (dr.Read())
            {
                lib.Items.Add(new FilmsList
                {
                    Movie_Name = dr["movie_Name"].ToString(),
                    Movie_Image = dr["movie_Image"].ToString(),
                    Movie_Actors = dr["movie_Actors"].ToString(),
                    Movie_RelDate = dr["movie_RelDate"].ToString(),
                    Movie_Director = dr["movie_Director"].ToString(),
                    Movie_Intro    = dr["movie_Intro"].ToString(),
                    Movie_Type = dr["movie_Type"].ToString(),
                });
            }
            con.Close();
        }
        /// <summary>
        /// 查询操作
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void SearchFilm_Click(object sender, RoutedEventArgs e)
        {
            
            SqlConnection sqlconn = new SqlConnection(film_con);
            sqlconn.Open();
            string sqlsearch = "select* from movie_Info where concat(movie_Name,movie_Director,movie_Actors,movie_Type,movie_RelDate) like '%" + search_name.Text + "%'";
            SqlCommand sqlcmd = new SqlCommand(sqlsearch, sqlconn);
            SqlDataReader dr1 = sqlcmd.ExecuteReader();
            //读取数据
            lib.Items.Clear();
            while (dr1.Read())
            {
                lib.Items.Add(new FilmsList
                {
                    Movie_Name = dr1["movie_Name"].ToString(),
                    Movie_Image = dr1["movie_Image"].ToString(),
                    Movie_Actors = dr1["movie_Actors"].ToString(),
                    Movie_RelDate = dr1["movie_RelDate"].ToString(),
                    Movie_Director = dr1["movie_Director"].ToString(),
                    Movie_Intro = dr1["movie_Intro"].ToString(),
                    Movie_Type = dr1["movie_Type"].ToString(),
                });
            }
            if (lib.Items.Count == 0)
            {
                MessageBox.Show("电影信息输入错误，该电影不存在");
            }
            search_name.Text = "";
            sqlconn.Close();
        }
        

        private void Lib_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            int index = lib.SelectedIndex;
            FilmsList s = (FilmsList)lib.SelectedItem;
            Tag2 = s.Movie_Name.ToString();
            Tag3 = s.Movie_Intro.ToString();
            Tag4 = s.Movie_Image.ToString();
            chooseddFilm = s;

            var w = Window.GetWindow(lib);
            var g = w.FindName("GridPrincipal") as Grid;
            g.Children.Clear();
            g.Children.Add(new UserControlFilmsMore());
        }

    }




}