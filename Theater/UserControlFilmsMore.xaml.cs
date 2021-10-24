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
using System.Data.SqlClient;
using System.Data;
namespace Theater
{
    /// <summary>
    /// UserControlFilmsMore.xaml 的交互逻辑
    /// </summary>
    public partial class UserControlFilmsMore : UserControl
    {
        public UserControlFilmsMore()
        {
            InitializeComponent();
            Showdata();
            //Title.Text = UserControlFilm.Tag2.ToString();
            Intro.Text = "      " + UserControlFilm.Tag3.ToString();
        }
        //public string film_con = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=E:\Java\DBS\重构\Theater8.20.1\Theater\Theater\movie.mdf;Integrated Security=True";
        private string film_con = MainWindow.DataBasePath.dataBasePath;
       
        private void Showdata()
        {
            Film_title.Text = UserControlFilm.chooseddFilm.Movie_Name;
            Film_Type.Text = UserControlFilm.chooseddFilm.Movie_Type;
            Film_Director.Text = UserControlFilm.chooseddFilm.Movie_Director;
            Film_Actors.Text = UserControlFilm.chooseddFilm.Movie_Actors;
            Film_RelDate.Text = UserControlFilm.chooseddFilm.Movie_RelDate;
            string s = UserControlFilm.chooseddFilm.Movie_Image;
            Film_Image.Source = new BitmapImage(new Uri(s, UriKind.Relative));
            //SqlConnection con = new SqlConnection(film_con);
            //con.Open();
            //string sql = @"select* from movie_Info";
            //DataSet ds = new DataSet();
            //SqlDataAdapter da = new SqlDataAdapter(sql, film_con);
            //da.Fill(ds, "Info");

            //con.Close();
        }
        private void BuyTickets_Click(object sender, RoutedEventArgs e)
        {
            var w = Window.GetWindow(BuyTickets);
            var g = w.FindName("GridPrincipal") as Grid;
            g.Children.Clear();
            g.Children.Add(new UserControlChooseTime());
        }

        private void Cancel_Click(object sender, RoutedEventArgs e)
        {
            var w = Window.GetWindow(Cancel);
            var g = w.FindName("GridPrincipal") as Grid;
            g.Children.Clear();
            g.Children.Add(new UserControlFilm());
        }
    }
}
