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
    /// UserControlTheater.xaml 的交互逻辑
    /// </summary>
    public partial class UserControlTheater : UserControl
    {
        public UserControlTheater()
        {
            InitializeComponent();
            ShowHall();


        }
        //public string user_con = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=E:\Java\DBS\重构\Movie8.17.2\Theater\Theater\movie.mdf;Integrated Security=True";
        private string user_con = MainWindow.DataBasePath.dataBasePath;

        /// <summary>
        /// 展现数据库（影厅）数据
        /// </summary>
        private void ShowHall()
        {
            datagrid_Hall.DataContext = null;
            SqlConnection con = new SqlConnection(user_con);
            con.Open();
            string sql = @"select* from Hall";
            DataSet ds = new DataSet();
            SqlDataAdapter da = new SqlDataAdapter(sql, user_con);
            da.Fill(ds, "Cinema");
            datagrid_Hall.DataContext = ds.Tables["Cinema"];
            con.Close();
        }
        /// <summary>
        /// 查询电影厅
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void SearchCinema_Click(object sender, RoutedEventArgs e)
        {
            datagrid_Hall.DataContext = null;
            SqlConnection sqlconn = new SqlConnection(user_con);
            sqlconn.Open();
            string sqlsearch = "select* from Hall where concat(Hall_Id,Hall_Name,Hall_VIP) like '%" + search_name.Text + "%'";
            DataSet ds = new DataSet();
            SqlDataAdapter da = new SqlDataAdapter(sqlsearch, user_con);
            da.Fill(ds, "Cinema");
            datagrid_Hall.DataContext = ds.Tables["Cinema"];
            if (datagrid_Hall.Items.Count == 0)
            {
                MessageBox.Show("信息输入错误，该影厅不存在");
                ShowHall();
            }
            sqlconn.Close();
            search_name.Text = "";
        }
        private void showall_Click(object sender, RoutedEventArgs e)
        {
            ShowHall();
        }
        /// <summary>
        /// 删除电影厅
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void DeleteHallFinish(int value)
        {
            if (value == 1)
            {
                SqlConnection con = new SqlConnection(user_con);
                con.Open();
                DataRowView dwView = (datagrid_Hall.SelectedItem) as DataRowView;//当前选中列
                string selected_id = dwView[0].ToString();//获取选择行的序列号
                if (dwView != null)
                {
                    dwView.Delete();//删除选中列 
                }
                SqlCommand cmd = con.CreateCommand();
                string sqlsearch1 = string.Format("delete from Hall where Hall_Id='{0}'",selected_id);
                cmd.CommandText = sqlsearch1;
                cmd.ExecuteNonQuery();
                con.Close();
            }
        }
        private void HallDelete_Click(object sender, RoutedEventArgs e)
        {
            //DialogHost.Show(new DeleteConfer { }, "RootDialog");
            Theater_Delete deleteTheater = new Theater_Delete();
            deleteTheater.halldelete = DeleteHallFinish;
            DialogHost.Show(deleteTheater, "RootDialog");
        }
        /// <summary>
        /// 添加用户弹窗设置
        /// </summary>
        public void AddHallFinish(string value1, string value2, string value3, string value4)
        {
            if (value1 != null && value2 != null && value3 != null && value4 != null )
            {
                SqlConnection Conn = new SqlConnection(user_con);
                Conn.Open();
                    //实现编写电影厅id功能（影院ID_序号）
                string cmd1 = "select Hall_Id from Hall where Hall_Id like '%" + value2 + "%'";
                DataSet ds = new DataSet();
                SqlDataAdapter da = new SqlDataAdapter(cmd1, user_con);
                da.Fill(ds, "Hall_Id");
                DataTable dtgroup = ds.Tables["Hall_Id"];
                if(dtgroup.Rows.Count==0)
                {
                    string Screeningnum11 = "0";
                    int Screeningnum1 = Convert.ToInt32(Screeningnum11);
                    int Screeningnum1new = Screeningnum1 + 1;
                    string num = Screeningnum1new.ToString();
                    string cinemaId = value2 + "_" + num;
                    string SQL = "insert into Hall values ('" + cinemaId + "','" + value1.ToString() + "','" + value2.ToString() + "','" + value3.ToString() + "','" + value4.ToString() + "')";
                    SqlCommand cmd2 = new SqlCommand(SQL, Conn);
                    cmd2.ExecuteNonQuery();
                    ShowHall();
                    Conn.Close();
                }
                else
                {
                    string Screeningnum = dtgroup.Rows[dtgroup.Rows.Count - 1]["Hall_Id"].ToString();
                    string Screeningnum11 = Screeningnum.Substring(5,1);
                    int Screeningnum1 = Convert.ToInt32(Screeningnum11);
                    int Screeningnum1new = Screeningnum1 + 1;
                    string num = Screeningnum1new.ToString();
                    string cinemaId = value2 + "_" + num;
                    string SQL = "insert into Hall values ('" + cinemaId + "','" + value1.ToString() + "','" + value2.ToString() + "','" + value3.ToString() + "','" + value4.ToString() + "')";
                    SqlCommand cmd2 = new SqlCommand(SQL, Conn);
                    cmd2.ExecuteNonQuery();
                    ShowHall();
                    Conn.Close();
                }
                
                
            }
        }
        private void AdminAddHall_Click(object sender, RoutedEventArgs e)
        {
            Theater_ADD hall_ADD = new Theater_ADD();
            hall_ADD.addHall = AddHallFinish;
            DialogHost.Show(hall_ADD, "RootDialog");
        }
        /// <summary>
        /// 管理员编辑电影厅信息界面弹出
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        /// 
        public void EditHallFinish(string value1, string value2, string value3, string value4)
        {
            if (value1 != null && value2 != null && value3 != null && value4 != null )
            {
                SqlConnection Conn = new SqlConnection(user_con);
                Conn.Open();
                DataRowView dwView = (datagrid_Hall.SelectedItem) as DataRowView;//当前选中列
                string selected_id = dwView[0].ToString();//获取选择行的序列号
                string SQL = string.Format("update Hall set Hall_Name='{0}',  Hall_Seats='{1}',Hall_VIP='{2}' where Hall_Id='{3}'", value1.ToString(), value3.ToString(), value4.ToString(), selected_id);
                SqlCommand cmd2 = new SqlCommand(SQL, Conn);
                cmd2.ExecuteNonQuery();
                ShowHall();
                Conn.Close();
            }
        }

        private void AdminEditHall_Click(object sender, RoutedEventArgs e)
        {
            
            SqlConnection con = new SqlConnection(user_con);
            //获取选中电影厅的信息
            con.Open();
            DataRowView dwView = (datagrid_Hall.SelectedItem) as DataRowView;//当前选中列
            string selected_id = dwView[0].ToString();//获取选择行的电影厅序列号
            string sql = string.Format("select* from Hall where Hall_Id='{0}' ", selected_id);
            DataSet ds = new DataSet();
            SqlDataAdapter da = new SqlDataAdapter(sql, user_con);
            da.Fill(ds, "Hall");
            string editHall_Name = ds.Tables["Hall"].Rows[0].ItemArray[1].ToString();
            string editHall_CinemaId = ds.Tables["Hall"].Rows[0].ItemArray[2].ToString();
            string editHall_SeatNum = ds.Tables["Hall"].Rows[0].ItemArray[3].ToString();
            string editHall_level = ds.Tables["Hall"].Rows[0].ItemArray[4].ToString();
            string editHall_level1 = editHall_level.Replace(" ", "");
            SqlCommand cmd1 = con.CreateCommand();
            cmd1.CommandText = string.Format("select rowNum from (select row_number() over (order by Cinema_ID) as rowNum,* from Cinema) as t where Cinema_ID='{0}' ", editHall_CinemaId);
            string result = cmd1.ExecuteScalar().ToString();
            int index = Convert.ToInt32(result);//获取下拉框index
            con.Close();
            //在子窗口显示
            Theater_Edit hall_Edit = new Theater_Edit();
            hall_Edit.TextboxHall_Name.Text = editHall_Name;
            hall_Edit.TextboxHall_CinemaId.SelectedIndex =index-1 ;
            hall_Edit.TextboxHall_SeatNum.Text = editHall_SeatNum;
            if (editHall_level1=="普通")
            {
                hall_Edit.TextboxHall_level.SelectedIndex = 0;
            }
            else if(editHall_level1 == "VIP")
            {
                hall_Edit.TextboxHall_level.SelectedIndex = 1;
            }
            else 
            {
                hall_Edit.TextboxHall_level.SelectedIndex = 2;
            }
            hall_Edit.editHall = EditHallFinish;
            DialogHost.Show(hall_Edit, "RootDialog");
        }
    }
}
