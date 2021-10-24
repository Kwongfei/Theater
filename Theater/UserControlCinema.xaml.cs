using MaterialDesignThemes.Wpf;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.IO;
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
    /// UserControlCinema.xaml 的交互逻辑
    /// </summary>
    public partial class UserControlCinema : UserControl
    {
        public UserControlCinema()
        {
            InitializeComponent();
            ShowCinema();


        }
        //public string user_con = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=E:\Java\DBS\重构\Movie8.17.2\Theater\Theater\movie.mdf;Integrated Security=True";
        private string user_con = MainWindow.DataBasePath.dataBasePath;

        /// <summary>
        /// 展现数据库（电影院）数据
        /// </summary>
        private void ShowCinema()
        {
            datagrid_Cinema.DataContext = null;
            SqlConnection con = new SqlConnection(user_con);
            con.Open();
            string sql = @"select* from Cinema";
            DataSet ds = new DataSet();
            SqlDataAdapter da = new SqlDataAdapter(sql, user_con);
            da.Fill(ds, "Cinema");
            datagrid_Cinema.DataContext = ds.Tables["Cinema"];
            con.Close();
        }
        /// <summary>
        /// 查询电影院
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void SearchCinema_Click(object sender, RoutedEventArgs e)
        {
            datagrid_Cinema.DataContext = null;
            SqlConnection sqlconn = new SqlConnection(user_con);
            sqlconn.Open();
            string sqlsearch = "select* from Cinema where concat(Cinema_ID,Cinema_Name,Cinema_address) like '%" + search_name.Text + "%'";
            DataSet ds = new DataSet();
            SqlDataAdapter da = new SqlDataAdapter(sqlsearch, user_con);
            da.Fill(ds, "Cinema");
            datagrid_Cinema.DataContext = ds.Tables["Cinema"];
            if (datagrid_Cinema.Items.Count == 0)
            {
                MessageBox.Show("信息输入错误，该影院不存在");
                ShowCinema();
            }
            sqlconn.Close();
            search_name.Text = "";
        }
        private void showall_Click(object sender, RoutedEventArgs e)
        {
            ShowCinema();
        }
        /// <summary>
        /// 删除用户
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void DeleteCinemaFinish(int value)
        {
            if (value == 1)
            {
                SqlConnection con = new SqlConnection(user_con);
                con.Open();
                DataRowView dwView = (datagrid_Cinema.SelectedItem) as DataRowView;//当前选中列
                string selected_id = dwView[0].ToString();//获取选择行的序列号
                if (dwView != null)
                {
                    dwView.Delete();//删除选中列 
                }
                SqlCommand cmd = con.CreateCommand();
                string sqlsearch1 = String.Format("delete from Cinema where Cinema_ID='{0}' " ,selected_id);
                cmd.CommandText = sqlsearch1;
                cmd.ExecuteNonQuery();
                con.Close();
            }
        }
        private void CinemaDelete_Click(object sender, RoutedEventArgs e)
        {
            Cinema_Delete deleteCinemaConfer = new Cinema_Delete();
            deleteCinemaConfer.cinemadelete = DeleteCinemaFinish;
            DialogHost.Show(deleteCinemaConfer, "RootDialog");
        }
        /// <summary>
        /// 添加用户弹窗设置
        /// </summary>
        public void AddCinemaFinish(string value1, string value2, string value3, string value4, string value5, string value6)
        {
            if (value1 != null && value2 != null && value3 != null && value4 != null && value5 != null && value6 != null)
            {
                SqlConnection Conn = new SqlConnection(user_con);
                Conn.Open();
                if (value6 == "广州市")
                { 
                    string CId1 = "A";
                    //实现编写用户id功能（大写字母加序号）
                    string cmd1 = "select Cinema_ID from Cinema where Cinema_address like '%" + value6.ToString() + "%'" ;
                    DataSet ds = new DataSet();
                    SqlDataAdapter da = new SqlDataAdapter(cmd1, user_con);
                    da.Fill(ds, "Cinema_ID");
                    DataTable dtgroup = ds.Tables["Cinema_ID"];
                    string Screeningnum = dtgroup.Rows[dtgroup.Rows.Count - 1]["Cinema_ID"].ToString();
                    string Screeningnum11 = Screeningnum.Substring(3, 1);
                    int Screeningnum1 = Convert.ToInt32(Screeningnum11);
                    int Screeningnum1new = Screeningnum1 + 1;
                    string CId2 = Screeningnum1new.ToString().PadLeft(3, '0');
                    string CId = CId1 + CId2;
                    string address = value5 + value6 + value4;
                    string SQL = "insert into Cinema values ('" + CId + "','" + value1.ToString() + "','" + value2.ToString() + "','" + value3.ToString() + "','" + address + "')";
                    SqlCommand cmd2 = new SqlCommand(SQL, Conn);
                    cmd2.ExecuteNonQuery();
                }
                else if (value6 == "中山市")
                {
                    string CId1 = "T";
                    //实现编写用户id功能（大写字母加序号）
                    string cmd1 = "select Cinema_ID from Cinema where Cinema_address like '%" + value6.ToString() + "%'";
                    DataSet ds = new DataSet();
                    SqlDataAdapter da = new SqlDataAdapter(cmd1, user_con);
                    da.Fill(ds, "Cinema_ID");
                    DataTable dtgroup = ds.Tables["Cinema_ID"];
                    string Screeningnum = dtgroup.Rows[dtgroup.Rows.Count - 1]["Cinema_ID"].ToString();
                    string Screeningnum11 = Screeningnum.Substring(3, 1);
                    int Screeningnum1 = Convert.ToInt32(Screeningnum11);
                    int Screeningnum1new = Screeningnum1 + 1;
                    string CId2 = Screeningnum1new.ToString().PadLeft(3, '0');
                    string CId = CId1 + CId2;
                    string address = value5 + value6 + value4;
                    string SQL = "insert into Cinema values ('" + CId + "','" + value1.ToString() + "','" + value2.ToString() + "','" + value3.ToString() + "','" + address + "')";
                    SqlCommand cmd2 = new SqlCommand(SQL, Conn);
                    cmd2.ExecuteNonQuery();
                }
                ShowCinema();
                Conn.Close();
            }
        }
        private void AdminAddCinema_Click(object sender, RoutedEventArgs e)
        {
            Cinema_ADD cinema_ADD= new Cinema_ADD();
            cinema_ADD.addCinema = AddCinemaFinish;
            DialogHost.Show(cinema_ADD, "RootDialog");
        }
        /// <summary>
        /// 管理员编辑电影信息界面弹出
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        /// 
        public void EditCinemaFinish(string value1, string value2, string value3, string value4, string value5, string value6)
        {
            if (value1 != null && value2 != null && value3 != null && value4 != null && value5 != null && value6 != null )
            {
                SqlConnection Conn = new SqlConnection(user_con);
                Conn.Open();
                string address = value5 + value6 + value4;
                DataRowView dwView = (datagrid_Cinema.SelectedItem) as DataRowView;//当前选中列
                string selected_id = dwView[0].ToString();//获取选择行的序列号
                string SQL = string.Format("update Cinema set Cinema_Name='{0}', Cinema_Hallnum='{1}', Cinema_phone='{2}',  Cinema_address='{3}' where Cinema_ID='{4}'", value1.ToString(),value2.ToString(), value3.ToString(),address,selected_id );
                SqlCommand cmd2 = new SqlCommand(SQL, Conn);
                cmd2.ExecuteNonQuery();
                ShowCinema();
                Conn.Close();
            }
        }

        private void AdminEditFilm_Click(object sender, RoutedEventArgs e)
        {
            Cinema_Edit cinema_Edit = new Cinema_Edit();
            SqlConnection con = new SqlConnection(user_con);
            //获取选中用户的信息
            con.Open();
            DataRowView dwView = (datagrid_Cinema.SelectedItem) as DataRowView;//当前选中列
            string selected_id = dwView[0].ToString();//获取选择行的序列号
            string sql = string.Format("select* from Cinema where Cinema_ID='{0}' ", selected_id);
            DataSet ds = new DataSet();
            SqlDataAdapter da = new SqlDataAdapter(sql, user_con);
            da.Fill(ds, "Film");
            
            string editCinema_Name = ds.Tables["Film"].Rows[0].ItemArray[1].ToString();
            string editCinema_Hallnum = ds.Tables["Film"].Rows[0].ItemArray[2].ToString();
            string editCinema_phone = ds.Tables["Film"].Rows[0].ItemArray[3].ToString();
            string editCinema_address = ds.Tables["Film"].Rows[0].ItemArray[4].ToString();
            string editCinema_Province = editCinema_address.Substring(0, 3);
            string editCinema_City = editCinema_address.Substring(3, 3);
            string editCinema_addressnew = editCinema_address.Substring(6);
            if (editCinema_Province == "广东省")
            {
                cinema_Edit.SelectProvince.SelectedIndex = 0;
            }
            if (editCinema_City == "广州市")
            {
                cinema_Edit.SelectCity.SelectedIndex = 0;
            }
            else
            {
                cinema_Edit.SelectCity.SelectedIndex = 1;
            }


            con.Close();
            //在子窗口显示
            cinema_Edit.TextboxCinema_Name.Text = editCinema_Name;
            cinema_Edit.TextboxCinema_Hallnum.Text = editCinema_Hallnum;
            cinema_Edit.TextboxCinema_Phone.Text = editCinema_phone;
            cinema_Edit.TextboxCinema_Address.Text = editCinema_addressnew;
            cinema_Edit.editCinema = EditCinemaFinish;
            DialogHost.Show(cinema_Edit, "RootDialog");
        }

    }
}
