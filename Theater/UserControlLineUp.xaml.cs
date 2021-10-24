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
    /// UserControlLineUp.xaml 的交互逻辑
    /// </summary>
    public partial class UserControlLineUp : UserControl
    {
        public UserControlLineUp()
        {
            InitializeComponent();
            ShowScreening();


        }
        //public string user_con = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=E:\Java\DBS\重构\Movie8.17.2\Theater\Theater\movie.mdf;Integrated Security=True";
        private string user_con = MainWindow.DataBasePath.dataBasePath;

        /// <summary>
        /// 展现数据库（放映信息）数据
        /// </summary>
        private void ShowScreening()
        {
            datagrid_Screening.DataContext = null;
            SqlConnection con = new SqlConnection(user_con);
            con.Open();
            string sql = @"select* from Screening";
            DataSet ds = new DataSet();
            SqlDataAdapter da = new SqlDataAdapter(sql, user_con);
            da.Fill(ds, "Cinema");
            datagrid_Screening.DataContext = ds.Tables["Cinema"];
            con.Close();
        }
        /// <summary>
        /// 查询放映信息
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void SearchScreening_Click(object sender, RoutedEventArgs e)
        {
            datagrid_Screening.DataContext = null;
            SqlConnection sqlconn = new SqlConnection(user_con);
            sqlconn.Open();
            string sqlsearch = "select* from Screening where concat(Screening_Id,Screening_movieName,Screening_HallId,Screening_HallId,Screening_Date,Screening_StartTime)like '%" + search_name.Text + "%'";
            DataSet ds = new DataSet();
            SqlDataAdapter da = new SqlDataAdapter(sqlsearch, user_con);
            da.Fill(ds, "Screening");
            datagrid_Screening.DataContext = ds.Tables["Screening"];
            if (datagrid_Screening.Items.Count == 0)
            {
                MessageBox.Show("信息输入错误，该放映场次不存在");
                ShowScreening();
            }
            sqlconn.Close();
            search_name.Text = "";
        }
        private void showall_Click(object sender, RoutedEventArgs e)
        {
            ShowScreening();
        }
        /// <summary>
        /// 添加房放映信息弹窗设置
        /// </summary>
        public void AddScreeningFinish(string value1, string value2, string value3, string value4, string value6)
        {
            if (value1 != null && value2 != null && value3 != null && value4 != null && value6 != null)
            {
                SqlConnection Conn = new SqlConnection(user_con);
                Conn.Open();
                //实现编写放映id功能（影院ID_序号）
                
                DateTime dateselected = Convert.ToDateTime(value3);
                string dateselected1=dateselected.ToString("yyyy - MM - dd");
                string datenow1 = dateselected1.Replace("-", "").Replace(" ", "").Trim();
                string datenow2 = datenow1.Substring(2,6);//获取选择的日期（例：210820）
                string cinema_Id = value2.Substring(0, 4);//获取（形如A001）
                string cmd1 = "select Screening_Id from Screening where Screening_HallId like '%" + cinema_Id + "%' and  Screening_Id like '%"+ datenow2+ "%'";
                //下面这段是防止序号错乱（当你删除掉一条放映信息后！！！）
                DataSet ds = new DataSet();
                SqlDataAdapter da = new SqlDataAdapter(cmd1, user_con);
                da.Fill(ds, "Screening_HallId");
                DataTable dtgroup = ds.Tables["Screening_HallId"];
                if (dtgroup.Rows.Count==0)
                {
                    string Screeningnum11 = "0";
                    int Screeningnum1 = Convert.ToInt32(Screeningnum11);
                    int Screeningnum1new = Screeningnum1 + 1;
                    string num = Screeningnum1new.ToString().PadLeft(3, '0');
                    string screeningId = datenow2 + cinema_Id + "_" + num;
                    //找出对应电影的时长
                    SqlCommand cmd2 = Conn.CreateCommand();
                    cmd2.CommandText = "select movie_time from movie_Info where movie_Name='" + value1.ToString()+"'";
                    string result1 = cmd2.ExecuteScalar().ToString();
                    int result2 = Convert.ToInt32(result1);
                    //实现编写结束时间的功能（通过给出开始时间和时长）
                    string end_time = Convert.ToDateTime(value4).AddSeconds(result2 * 60).ToString();
                    string end_time1 = end_time.Substring(10);
                    string SQL = "insert into Screening(Screening_Id,Screening_movieName,Screening_HallId,Screening_Date,Screening_StartTime,Screening_Price,Screening_EndTime) values ('" + screeningId + "','" + value1.ToString() + "','" + value2.ToString() + "','" + value3.ToString() + "','" + value4.ToString() + "','" + value6.ToString() + "','" + end_time1.ToString() + "')";
                    SqlCommand cmd3 = new SqlCommand(SQL, Conn);
                    cmd3.ExecuteNonQuery();
                    ShowScreening();
                    Conn.Close();

                }
                else
                {
                    string Screeningnum = dtgroup.Rows[dtgroup.Rows.Count - 1]["Screening_Id"].ToString();
                    string Screeningnum11 = Screeningnum.Substring(13, 1);
                    int Screeningnum1 = Convert.ToInt32(Screeningnum11);
                    int Screeningnum1new = Screeningnum1 + 1;
                    string num = Screeningnum1new.ToString().PadLeft(3, '0');
                    string screeningId = datenow2 + cinema_Id + "_" + num;
                    //找出对应电影的时长
                    SqlCommand cmd2 = Conn.CreateCommand();
                    cmd2.CommandText = "select movie_time from movie_Info where movie_Name='" + value1.ToString()+"'";
                    string result1 = cmd2.ExecuteScalar().ToString();
                    int result2 = Convert.ToInt32(result1);
                    //实现编写结束时间的功能（通过给出开始时间和时长）
                    string end_time = Convert.ToDateTime(value4).AddSeconds(result2 * 60).ToString();
                    string end_time1 = end_time.Substring(10);
                    string SQL = "insert into Screening(Screening_Id,Screening_movieName,Screening_HallId,Screening_Date,Screening_StartTime,Screening_Price,Screening_EndTime) values ('" + screeningId + "','" + value1.ToString() + "','" + value2.ToString() + "','" + value3.ToString() + "','" + value4.ToString() + "','" + value6.ToString() + "','" + end_time1.ToString() + "')";
                    SqlCommand cmd3 = new SqlCommand(SQL, Conn);
                    cmd3.ExecuteNonQuery();
                    ShowScreening();
                    Conn.Close();

                }

            }
        }
        private void AdminAddScreening_Click(object sender, RoutedEventArgs e)
        {
            LineUp_ADD Screening_ADD = new LineUp_ADD();
            Screening_ADD.addScreening = AddScreeningFinish;
            DialogHost.Show(Screening_ADD, "RootDialog");  
        }
        /// <summary>
        /// 删除用户
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void DeleteScreeningFinish(int value)
        {
            if (value == 1)
            {
                SqlConnection con = new SqlConnection(user_con);
                con.Open();
                DataRowView dwView = (datagrid_Screening.SelectedItem) as DataRowView;//当前选中列
                string selected_id = dwView[0].ToString();//获取选择行的序列号
                if (dwView != null)
                {
                    dwView.Delete();//删除选中列 
                }
                SqlCommand cmd = con.CreateCommand();
                string sqlsearch1 = String.Format("delete from Screening where Screening_Id='{0}' ", selected_id);
                cmd.CommandText = sqlsearch1;
                cmd.ExecuteNonQuery();
                con.Close();
            }
        }
        private void ScreeningDelete_Click(object sender, RoutedEventArgs e)
        {
            LineUp_Delete deleteScreeningConfer = new LineUp_Delete();
            deleteScreeningConfer.screeningdelete = DeleteScreeningFinish;
            DialogHost.Show(deleteScreeningConfer, "RootDialog");
        }
        /// <summary>
        /// 管理员编辑电影厅信息界面弹出
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        /// 
        public void EditScreeningFinish(string value1, string value2, string value3, string value4, string value6)
        {
            if (value1 != null && value2 != null && value3 != null && value4 != null && value6 != null)
            {
                SqlConnection Conn = new SqlConnection(user_con);
                Conn.Open();
                DataRowView dwView = (datagrid_Screening.SelectedItem) as DataRowView;//当前选中列
                string selected_id = dwView[0].ToString();//获取选择行的序列号
                SqlCommand cmd2 = Conn.CreateCommand();
                cmd2.CommandText = "select movie_time from movie_Info where movie_Name= '" + value1.ToString()+"'";
                string result1 = cmd2.ExecuteScalar().ToString();
                int result2 = Convert.ToInt32(result1);
                string end_time = Convert.ToDateTime(value4).AddSeconds(result2 * 60).ToString();
                string end_time1 = end_time.Substring(10);//及时更新结束时间
                string SQL = string.Format("update Screening set Screening_movieName='{0}', Screening_HallId='{1}',Screening_Date='{2}'," +
                    "Screening_StartTime='{3}',Screening_Price='{4}',Screening_EndTime='{5}' where Screening_Id='{6}'", 
                    value1.ToString(), value2.ToString(), value3.ToString(), value4.ToString(),  value6.ToString(), end_time1.ToString(), selected_id);
                SqlCommand cmd3 = new SqlCommand(SQL, Conn);
                cmd3.ExecuteNonQuery();
                ShowScreening();
                Conn.Close();
            }
        }

        private void AdminEditScreening_Click(object sender, RoutedEventArgs e)
        {

            SqlConnection con = new SqlConnection(user_con);
            //获取选中放映ID的信息
            con.Open();
            DataRowView dwView = (datagrid_Screening.SelectedItem) as DataRowView;//当前选中列
            string selected_id = dwView[0].ToString();//获取选择行的电影厅序列号
            string sql = string.Format("select* from Screening where Screening_Id='{0}' ", selected_id);
            DataSet ds = new DataSet();
            SqlDataAdapter da = new SqlDataAdapter(sql, user_con);
            da.Fill(ds, "Screening");

            string editScreening_ID= ds.Tables["Screening"].Rows[0].ItemArray[0].ToString();
            string editScreening_movieName = ds.Tables["Screening"].Rows[0].ItemArray[1].ToString();
            string editScreening_HallId = ds.Tables["Screening"].Rows[0].ItemArray[2].ToString();
            string editScreening_Date = ds.Tables["Screening"].Rows[0].ItemArray[3].ToString();
            string editScreening_StartTime = ds.Tables["Screening"].Rows[0].ItemArray[4].ToString();
            string editScreening_EndTime = ds.Tables["Screening"].Rows[0].ItemArray[5].ToString();
            string editScreening_Price = ds.Tables["Screening"].Rows[0].ItemArray[6].ToString();
            
            //获取电影名下拉列表的index
            SqlCommand cmd1 = con.CreateCommand();
            cmd1.CommandText = string.Format("select rowNum from (select row_number() over (order by movie_Name) as rowNum,* from movie_Info) as t where movie_Name='{0}' ", editScreening_movieName);
            string result1 = cmd1.ExecuteScalar().ToString();
            int index1 = Convert.ToInt32(result1);
            //获取电影厅id下拉列表的index
            SqlCommand cmd2 = con.CreateCommand();
            cmd2.CommandText = string.Format("select rowNum from (select row_number() over (order by Hall_Id) as rowNum,* from Hall) as t where Hall_Id='{0}' ", editScreening_HallId);
            string result2 = cmd2.ExecuteScalar().ToString();
            int index2 = Convert.ToInt32(result2)-1;
            con.Close();

            //在子窗口显示
            LineUp_Edit Screening_edit = new LineUp_Edit();
            Screening_edit.combobox_movieName.SelectedIndex = index1 - 1;
            Screening_edit.combobox_HallId.SelectedIndex = index2 ;
            Screening_edit.combobox_date.Text = editScreening_Date;
            Screening_edit.combobox_Time.Text = editScreening_StartTime;
            Screening_edit.Textbox_Price.Text = editScreening_Price;
            Screening_edit.Textbox_ID.Text = editScreening_ID;
            Screening_edit.editScreening = EditScreeningFinish;
            
            DialogHost.Show(Screening_edit, "RootDialog");
        }

    }
}

