using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
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
    /// UserControlComments.xaml 的交互逻辑
    /// </summary>
    public partial class UserControlComments : UserControl
    {
        public UserControlComments()
        {
            InitializeComponent();
            Showaccount();
        }
        private string user_con = MainWindow.DataBasePath.dataBasePath;
        private void Showaccount()
        {
            datagrid_user.DataContext = null;
            SqlConnection con = new SqlConnection(user_con);
            con.Open();
            string sql = @"select Screening_Date, Hall.Cinema_ID, count(*) as Ticket_Num,sum(Screening_Price) as Ticket_Sum from Hall, Screening, Ticket where Hall.Hall_Id=Screening.Screening_HallId and Screening.Screening_Id=Ticket.Ticket_ScreeningId
            group by Screening_Date, Hall.Cinema_ID";
            DataSet ds = new DataSet();
            SqlDataAdapter da = new SqlDataAdapter(sql, user_con);
            da.Fill(ds, "Tickets");
            datagrid_user.DataContext = ds.Tables["Tickets"];
            con.Close();
        }

        private void Search_Click(object sender, RoutedEventArgs e)
        {
            datagrid_user.DataContext = null;
            SqlConnection sqlconn = new SqlConnection(user_con);
            sqlconn.Open();
            string sqlsearch = "select Screening_Date, Hall.Cinema_ID, count(*) as Ticket_Num,sum(Screening_Price) as Ticket_Sum from Hall, Screening, Ticket where Hall.Hall_Id=Screening.Screening_HallId and Screening.Screening_Id=Ticket.Ticket_ScreeningId and"+
           " concat(Screening_Date,Hall.Cinema_ID) like '%" + search_name.Text + "%' group by Screening_Date, Hall.Cinema_ID";
            DataSet ds = new DataSet();
            SqlDataAdapter da = new SqlDataAdapter(sqlsearch, user_con);
            da.Fill(ds, "Tickets");
            datagrid_user.DataContext = ds.Tables["Tickets"];
            if (datagrid_user.Items.Count == 0)
            {
                MessageBox.Show("信息输入错误，该销售记录不存在");
                Showaccount();
            }
            sqlconn.Close();
            search_name.Text = "";
        }
    }
}
