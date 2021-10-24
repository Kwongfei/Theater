using MaterialDesignThemes.Wpf;
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
    /// UserControlTickets.xaml 的交互逻辑
    /// </summary>
    public partial class UserControlTickets : UserControl
    {
        public UserControlTickets()
        {
            InitializeComponent();
            ShowTicket();
        }
       
        private string user_con = MainWindow.DataBasePath.dataBasePath;

        private void ShowTicket()
        {
            datagrid_Ticket.DataContext = null;
            SqlConnection con = new SqlConnection(user_con);
            con.Open();
            string sql = @"select* from Ticket";
            DataSet ds = new DataSet();
            SqlDataAdapter da = new SqlDataAdapter(sql, user_con);
            da.Fill(ds, "Info");
            datagrid_Ticket.DataContext = ds.Tables["Info"];
            con.Close();
        }
        /// <summary>
        /// 显示全部信息操作
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void showall_Click(object sender, RoutedEventArgs e)
        {
            ShowTicket();
        }
        /// <summary>
        /// 查询操作
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void SearchTicket_Click(object sender, RoutedEventArgs e)
        {
            datagrid_Ticket.DataContext = null;
            SqlConnection sqlconn = new SqlConnection(user_con);
            sqlconn.Open();
            string sqlsearch = "select* from Ticket where concat(Ticket_ID,Ticket_userId,Ticket_ScreeningId,Ticket_SeatNo) like '%" + search_name.Text + "%'";
            DataSet ds = new DataSet();
            SqlDataAdapter da = new SqlDataAdapter(sqlsearch, user_con);
            da.Fill(ds, "Ticket");
            datagrid_Ticket.DataContext = ds.Tables["Ticket"];
            if (datagrid_Ticket.Items.Count == 0)
            {
                MessageBox.Show("信息输入错误，该订单不存在");
                ShowTicket();
            }
            sqlconn.Close();
            search_name.Text = "";
        }
        /// <summary>
        /// 删除订单
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void DeleteTicketFinish(int value)
        {
            if (value == 1)
            {
                SqlConnection con = new SqlConnection(user_con);
                con.Open();
                DataRowView dwView = (datagrid_Ticket.SelectedItem) as DataRowView;//当前选中列
                string selected_id = dwView[0].ToString();//获取选择行的序列号
                if (dwView != null)
                {
                    dwView.Delete();//删除选中列 
                }
                SqlCommand cmd = con.CreateCommand();
                string sqlsearch1 = String.Format("delete from Ticket where Ticket_ID='{0}' ", selected_id);
                cmd.CommandText = sqlsearch1;
                cmd.ExecuteNonQuery();
                con.Close();
            }
        }
        private void TicketDelete_Click(object sender, RoutedEventArgs e)
        {
            Tickets_Delete deleteTicketConfer = new Tickets_Delete();
            deleteTicketConfer.ticketdelete = DeleteTicketFinish;
            DialogHost.Show(deleteTicketConfer, "RootDialog");
        }
    }
}