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
    /// UserControlMyTickets.xaml 的交互逻辑
    /// </summary>
    public partial class UserControlMyTickets : UserControl
    {
        public class MyTicketList
        {
            public string Cinema_name { get; set; }
            public string Film_name { get; set; }
            public string Film_Date { get; set; }
            public string Film_price { get; set; }
            public string Seat_number { get; set; }
            public string Hall_Name { get; set; }
            public string Movie_Image { get; set; }
            public string Ticket_ID { get; set; }
        }
        public UserControlMyTickets()
        {
            InitializeComponent();
            Showdata();
        }
        private string film_con = MainWindow.DataBasePath.dataBasePath;

        private void Showdata() //数据库连接、读入
        {
            SqlConnection con = new SqlConnection(film_con);
            con.Open();
            SqlCommand cmd = con.CreateCommand();
            string user_ID = Loginxaml.Tag1;

            cmd.CommandText = "select Ticket_ID, Cinema_Name, Hall_Name, Screening_movieName, Screening_Date, Screening_Price, Movie_Image, Ticket_SeatNo " +
                "from Ticket,Screening, Hall, Cinema, movie_Info " +
                "where Screening_movieName = movie_Name And Ticket_ScreeningId = Screening_Id " +
                "And Screening_HallId = Hall_Id And Cinema.Cinema_ID = Hall.Cinema_ID " +
                "And Ticket_userId = '" + user_ID + "';";
            SqlDataReader dr = cmd.ExecuteReader();
            while (dr.Read())
            {
                lib.Items.Add(new MyTicketList
                {
                    Cinema_name = dr["Cinema_Name"].ToString(),
                    Film_name = dr["Screening_movieName"].ToString(),
                    Film_Date = dr["Screening_Date"].ToString(),
                    Film_price = dr["Screening_Price"].ToString(),
                    Seat_number = dr["Ticket_SeatNo"].ToString(),
                    Hall_Name = dr["Hall_Name"].ToString(),
                    Ticket_ID = dr["Ticket_ID"].ToString(),
                    Movie_Image = dr["Movie_Image"].ToString(),
                });
            }
            con.Close();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            //SqlConnection con = new SqlConnection(film_con);
            //con.Open();
            //SqlCommand cmd = con.CreateCommand();
            //string ticket_Id = btn.Ticket_ID.ToString();

            //cmd.CommandText = "delete from Ticket Where Ticket_ID = '" + ticket_Id + "';";
            //SqlDataReader dr = cmd.ExecuteReader();
            //con.Close();
            //Showdata();
        }

        private void BackToAccount_Click(object sender, RoutedEventArgs e)
        {
            var w = Window.GetWindow(BackToAccount);
            var g = w.FindName("GridPrincipal") as Grid;
            g.Children.Clear();
            g.Children.Add(new UserAccount());
        }
    }
}
