using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
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
    /// UserControlBuyTickets.xaml 的交互逻辑
    /// </summary>
    public partial class UserControlBuyTickets : UserControl
    {
        private string user_con = MainWindow.DataBasePath.dataBasePath;

        public UserControlBuyTickets()
        {
            InitializeComponent();
            InitialSeats();
        }

        private void InitialSeats()
        {
            string s = "";
            for (int i = 1; i <= 16; i++)
            {
                s = "Seat" + i.ToString();
                var Seat = this.FindName(s) as ToggleButton;
                Seat.IsEnabled = true;
                Seat.IsChecked = false;
                Seat.Background = Brushes.LightGreen;
            }

            s = "";
            int num = UserControlChooseTime.ListFilmTimeofSeat.Count();
            for (int i = 0; i < num; i++)
            {
                int z = Convert.ToInt32(UserControlChooseTime.ListFilmTimeofSeat[i].Ticket_SeatNo);
                s = "Seat" + z.ToString();
                var Seat = this.FindName(s) as ToggleButton;
                Seat.IsEnabled = false;
                Seat.IsChecked = false;
                Seat.Background = Brushes.Red;
            }
        }

        private void Cancel_Click(object sender, RoutedEventArgs e)
        {
            var w = Window.GetWindow(Cancel);
            var g = w.FindName("GridPrincipal") as Grid;
            g.Children.Clear();
            g.Children.Add(new UserControlFilm());
        }

        private void BuySuccessfully_Click(object sender, RoutedEventArgs e)
        {
            SqlConnection Conn = new SqlConnection(user_con);
            Conn.Open();
            byte[] buffer = Guid.NewGuid().ToByteArray();//生成字节数组
            int iRoot = BitConverter.ToInt32(buffer, 0);//利用BitConvert方法把字节数组转换为整数
            Random rd = new Random(iRoot);//以这个生成的整数为种子
            for (int i = 1; i <= 16; i++)
            {
                string s = "Seat" + i.ToString();
                string ticket_ID = "";
                for (int j = 1; j  <= 10; j++)
                {
                    int z = rd.Next(1, 10);
                    ticket_ID += z.ToString();
                }
                
                var Seat = this.FindName(s) as ToggleButton;
                if (Seat.IsEnabled == false || Seat.IsChecked == false) continue;
                string SQL = "insert into Ticket(Ticket_ID, Ticket_userId, Ticket_ScreeningId, Ticket_SeatNo) values ('" + ticket_ID + "','" + UserControlChooseTime.UserID + "','" + UserControlChooseTime.ScrenningID + "'," + i.ToString() + ")";
                SqlCommand cmd2 = new SqlCommand(SQL, Conn);
                cmd2.ExecuteNonQuery();
            }
            Conn.Close();
            var w = Window.GetWindow(BuySuccessfully);
            var g = w.FindName("GridPrincipal") as Grid;
            g.Children.Clear();
            g.Children.Add(new UserControlPayment());
        }
    }
}
