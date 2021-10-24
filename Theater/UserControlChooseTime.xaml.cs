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

namespace Theater
{
    /// <summary>
    /// UserControlChooseTime.xaml 的交互逻辑
    /// </summary>
    public partial class UserControlChooseTime : UserControl
    {
        public static string TagSendScreeningId;
        public static string TagSendingTicket_userId;
        public static string TagTicket_ScreeningId;
        public static string TagTicket_SeatNo;
        public UserControlChooseTime()
        {
            InitializeComponent();
            Showdata();
        }
        private string film_con = MainWindow.DataBasePath.dataBasePath;

        private void Showdata() //数据库连接、读入
        {
            SqlConnection con = new SqlConnection(film_con);
            con.Open();
            /*string sql = @"select* from movie_User";
            DataSet ds = new DataSet();
            SqlDataAdapter da = new SqlDataAdapter(sql, film_con);
            da.Fill(ds, "User");*/
            SqlCommand cmd = con.CreateCommand();
            string a=UserControlFilm.Tag4.ToString();
           
            cmd.CommandText = "select* from movie_Info,Cinema,Screening,Hall where movie_Info.Movie_Image='" + a + "' and  Screening.Screening_movieName=movie_Info.Movie_Name  and Cinema.Cinema_ID=Hall.Cinema_ID and Hall.Hall_ID=Screening.Screening_HallId";
            SqlDataReader dr = cmd.ExecuteReader();
            while (dr.Read())
            {
                ListFilmTime.Items.Add(new FilmsList
                {
                    Cinema_Name = dr["Cinema_Name"].ToString(),
                    Cinema_address = dr["Cinema_address"].ToString(),
                    Hall_Name = dr["Hall_Name"].ToString(),
                    Screening_Date = dr["Screening_Date"].ToString(),
                    Screening_StartTime = dr["Screening_StartTime"].ToString(),
                    Screening_EndTime = dr["Screening_EndTime"].ToString(),
                    Screening_Price = dr["Screening_Price"].ToString(),
                    Screening_Id = dr["Screening_Id"].ToString(),
                });
            }
            con.Close();
        }

        private void Cancel_Click(object sender, RoutedEventArgs e)
        {
            var w = Window.GetWindow(Cancel);
            var g = w.FindName("GridPrincipal") as Grid;
            g.Children.Clear();
            g.Children.Add(new UserControlFilm());
        }

        public class FilmsSeats
        {
            public string Ticket_userId { get; set; }
            public string Ticket_ScreeningId { get; set; }
            public string Ticket_SeatNo { get; set; }
        }

        public static List<FilmsSeats> ListFilmTimeofSeat = new List<FilmsSeats>();

        public static string ScrenningID = "";
        public static string UserID = "";

        private void ListFilmTime_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            int index = ListFilmTime.SelectedIndex;
            FilmsList s = (FilmsList)ListFilmTime.SelectedItem;
            TagSendScreeningId = s.Screening_Id.ToString();
            ScrenningID = TagSendScreeningId;
            UserID = Loginxaml.Tag1.ToString();
            SqlConnection con = new SqlConnection(film_con);
            con.Open();
            SqlCommand cmd = con.CreateCommand();
          
            cmd.CommandText = "select* from Ticket where Ticket.Ticket_ScreeningId='" + TagSendScreeningId + "' ";
            SqlDataReader dr = cmd.ExecuteReader();
            ListFilmTimeofSeat.Clear();
            while (dr.Read())
            {
                ListFilmTimeofSeat.Add(new FilmsSeats
                {
                    Ticket_userId = dr["Ticket_userId"].ToString(),
                    Ticket_ScreeningId = dr["Ticket_ScreeningId"].ToString(),
                    Ticket_SeatNo = dr["Ticket_SeatNo"].ToString(),
                });
            }
            con.Close();
            //int index1 = ListFilmTime.SelectedIndex;
            //FilmsList s1 = (FilmsList)ListFilmTime.SelectedItem;
            //TagSendingTicket_userId = s1.Ticket_userId.ToString();
            //TagTicket_ScreeningId = s1.Ticket_ScreeningId.ToString();
            //TagTicket_SeatNo = s1.Ticket_SeatNo.ToString();

            var w = Window.GetWindow(ListFilmTime);
            var g = w.FindName("GridPrincipal") as Grid;
            g.Children.Clear();
            g.Children.Add(new UserControlBuyTickets());
        }
    }
}
