using System.Diagnostics;
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
    /// UserControlAccount.xaml 的交互逻辑
    /// </summary>
    public partial class UserControlAccount : UserControl
    {
        public UserControlAccount()
        {
            InitializeComponent();
            Showdata();
        }
        //public string user_con = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=E:\Java\DBS\重构\Movie8.17.2\Theater\Theater\movie.mdf;Integrated Security=True";
        private string user_con = MainWindow.DataBasePath.dataBasePath;

        private void Showdata()
        {
            datagrid_user.DataContext = null;
            SqlConnection con = new SqlConnection(user_con);
            con.Open();
            string sql = @"select* from movie_User";
            DataSet ds = new DataSet();
            SqlDataAdapter da = new SqlDataAdapter(sql, user_con);
            da.Fill(ds, "User");
            datagrid_user.DataContext = ds.Tables["User"];
            con.Close();
        }

        /// <summary>
        /// 删除用户
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void DeleteFinish(int value)
        {
            if(value==1)
            {
                SqlConnection con = new SqlConnection(user_con);
                con.Open();
                DataRowView dwView = (datagrid_user.SelectedItem) as DataRowView;//当前选中列
                string selected_id = dwView[0].ToString();//获取选择行的序列号
                if (dwView != null)
                {
                    dwView.Delete();//删除选中列 
                }
                SqlCommand cmd = con.CreateCommand();
                string sqlsearch1 = "delete from movie_User where user_id=" + selected_id;
                cmd.CommandText = sqlsearch1;
                cmd.ExecuteNonQuery();
                /*string sqlsearch2 = @"select* from movie_User";
                 DataSet ds = new DataSet();
                 SqlDataAdapter da = new SqlDataAdapter(sqlsearch2, user_con);
                 da.Fill(ds, "User");
                 datagrid_user.DataContext = ds.Tables["User"]; */
                con.Close();
            }
        }
        private void AccountDelete_Click(object sender, RoutedEventArgs e)
        {
            //DialogHost.Show(new DeleteConfer { }, "RootDialog");
            DeleteConfer deleteConfer = new DeleteConfer();
            deleteConfer.delete = DeleteFinish ;
            DialogHost.Show( deleteConfer, "RootDialog");
        }


        /// <summary>
        /// 管理员编辑用户信息界面弹出
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        /// 
        public void EditAccountFinish(string value1, string value2, string value3, string value4, string value5)
        {
            if (value1 != null && value2 != null && value3 != null && value4 != null && value5 != null)
            {
                SqlConnection Conn = new SqlConnection(user_con);
                Conn.Open();
                DataRowView dwView = (datagrid_user.SelectedItem) as DataRowView;//当前选中列
                string selected_id = dwView[0].ToString();//获取选择行的序列号
                string SQL = string.Format("update movie_User set user_name='{0}', user_password='{1}', user_phone='{2}', user_sex='{3}', user_birthday='{4}'where user_id='{5}'", value1.ToString() ,value2.ToString() ,value3.ToString() , value4.ToString() ,value5.ToString() ,selected_id);
                SqlCommand cmd2 = new SqlCommand(SQL, Conn);
                cmd2.ExecuteNonQuery();
                Showdata();
                Conn.Close();
            }
        }
        
        private void AdminEditAccount_Click(object sender, RoutedEventArgs e)
        {
            AdminEditAccount adminEditAccount = new AdminEditAccount();
            SqlConnection con = new SqlConnection(user_con);
            //获取选中用户的信息
            con.Open();
            DataRowView dwView = (datagrid_user.SelectedItem) as DataRowView;//当前选中列
            string selected_id = dwView[0].ToString();//获取选择行的序列号

            string sql = @"select* from movie_User where user_id=" + selected_id;
            DataSet ds = new DataSet();
            SqlDataAdapter da = new SqlDataAdapter(sql, user_con);
            da.Fill(ds, "User");
            string editUser_name=ds.Tables["User"].Rows[0].ItemArray[1].ToString();
            string editUser_password = ds.Tables["User"].Rows[0].ItemArray[4].ToString();
            string editUser_phone = ds.Tables["User"].Rows[0].ItemArray[3].ToString();
            string editUser_sex = ds.Tables["User"].Rows[0].ItemArray[2].ToString();
            string editUser_birthday = ds.Tables["User"].Rows[0].ItemArray[5].ToString();
            con.Close();
            //在子窗口显示
            adminEditAccount.TextBoxUser_Name.Text = editUser_name;
            adminEditAccount.TextBoxUser_Password.Password = editUser_password;
            adminEditAccount.TextBoxUser_Phone.Text = editUser_phone;
            if (editUser_sex == "男")
            {
                adminEditAccount.TextBoxUser_Sex.SelectedIndex = 0;
            }
            else
            {
                adminEditAccount.TextBoxUser_Sex.SelectedIndex = 1;
            }
            
            adminEditAccount.TextBoxUser_Birth.SelectedDate = Convert.ToDateTime(editUser_birthday);
            adminEditAccount.editUser = EditAccountFinish;
            DialogHost.Show(adminEditAccount, "RootDialog");
        }

        private void Search_Click(object sender, RoutedEventArgs e)
        {
            datagrid_user.DataContext = null;
            SqlConnection sqlconn = new SqlConnection(user_con);
            sqlconn.Open();
            string sqlsearch = "select* from movie_User where concat(user_id,user_name,user_phone) like '%" + search_name.Text + "%'";
            DataSet ds = new DataSet();
            SqlDataAdapter da = new SqlDataAdapter(sqlsearch, user_con);
            da.Fill(ds, "User");
            datagrid_user.DataContext = ds.Tables["User"];
            if (datagrid_user.Items.Count == 0)
            {
                MessageBox.Show("信息输入错误，该用户不存在");
                Showdata();
            }
            sqlconn.Close();
            search_name.Text = "";
        }

        private void Showall_Click(object sender, RoutedEventArgs e)
        {
            Showdata();
        }

        /// <summary>
        /// 添加用户弹窗设置
        /// </summary>
        public void AddAccountFinish(string value1, string value2, string value3, string value4, string value5)
        {
            if (value1 != null && value2 != null && value3 != null && value4 != null && value5 != null)
            {
                DataTable dt = new DataTable();
                SqlConnection Conn = new SqlConnection(user_con);
                Conn.Open();
                SqlDataAdapter da = new SqlDataAdapter("select * from movie_User", Conn);
                da.Fill(dt);
                //实现编写用户id功能（日期加序号）
                string datenow = DateTime.Now.ToString("yyyy - MM - dd");
                string datenow1 = datenow.Replace("-", "").Replace(" ", "").Trim();
                string datenow2 = datenow1.Substring(0, 8);//获取当前日期
                SqlCommand cmd1 = Conn.CreateCommand();
                cmd1.CommandText = "select max(user_id) from movie_User";
                string result = cmd1.ExecuteScalar().ToString();
                string result1 = result.Substring(0, 8);//返回查询结果的字符串
                if(datenow2!=result1)
                {
                    string user_idold = datenow2 + "001";
                    Int64 user_id = Convert.ToInt64(user_idold);
                    string SQL = "insert into movie_User(user_id,user_name, user_password, user_phone, user_sex, user_birthday) values ('" + user_id + "','" + value1.ToString() + "','" + value2.ToString() + "','" + value3.ToString() + "','" + value4.ToString() + "','" + value5.ToString() + "')";
                    SqlCommand cmd2 = new SqlCommand(SQL, Conn);
                    cmd2.ExecuteNonQuery();
                }
                else
                {
                    Int64 user_id = Convert.ToInt64(result) + 1;
                    string SQL = "insert into movie_User(user_id,user_name, user_password, user_phone, user_sex, user_birthday) values ('" + user_id + "','" + value1.ToString() + "','" + value2.ToString() + "','" + value3.ToString() + "','" + value4.ToString() + "','" + value5.ToString() + "')";
                    SqlCommand cmd2 = new SqlCommand(SQL, Conn);
                    cmd2.ExecuteNonQuery();
                }
                Showdata();
                Conn.Close();
            }
        }
        private void AdminAddAccount_Click(object sender, RoutedEventArgs e)
        {
            //DialogHost.Show(new AdminAddAccount { }, "RootDialog");
            AdminAddAccount adminAddAccount = new AdminAddAccount();
            adminAddAccount.addAccount = AddAccountFinish;
            DialogHost.Show(adminAddAccount, "RootDialog");
        }
    }
}
