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
using System.Data;
using System.Data.SqlClient;
namespace Theater
{
    /// <summary>
    /// UserAccount.xaml 的交互逻辑
    /// </summary>
    public partial class UserAccount : UserControl
    {
        public UserAccount()

        {
            InitializeComponent();
            user_ID.Text = "用户ID：" + Loginxaml.Tag1.ToString();
            Showmaker();
            Showdata();

        }
        private string user_con = MainWindow.DataBasePath.dataBasePath;
        public Int64 num2;
        private void Showdata()
        {
            
            SqlConnection con = new SqlConnection(user_con);
            con.Open();
            string sql = @"select* from movie_User";
            DataSet ds = new DataSet();
            SqlDataAdapter da = new SqlDataAdapter(sql, user_con);
            da.Fill(ds, "User");
            
            con.Close();
        }
        private void EditUser_Click(object sender, RoutedEventArgs e)
        {
            string user_name = TextBoxUser_Name.Text;
            string user_password = TextBoxUser_Password.Password;
            string user_passwordconf = TextBoxUser_PasswordConf.Password;
            string user_phone = TextBoxUser_Phone.Text;
            string user_sex = TextBoxUser_Sex.Text;
            string user_birth = TextBoxUser_Birth.Text;

            //用户名为空的显示
            //用户名为空的显示
            UserNameTip.Text = "";
            PasswordVerify.Text = "";
            PhoneNumber.Text = "";
            PasswordNull.Text = "";
            SexNull.Text = " ";
            //用户名为空的显示
            if (TextBoxUser_Name.Text == "")
            {
                UserNameTip.Text = "用户名为空，重新输入用户名！";
                return;
            }
            //密码两次不一致的显示
            else if (TextBoxUser_Password.Password == "" && TextBoxUser_PasswordConf.Password == "")
            {
                PasswordNull.Text = "未输入密码，重新输入密码";
                return;
            }
            else if (TextBoxUser_Password.Password.Length < 6)
            {
                PasswordNull.Text = "密码小于6位，重新输入密码";
                TextBoxUser_Password.Password = "";
                TextBoxUser_PasswordConf.Password = "";
                return;
            }

            else if (TextBoxUser_Password.Password != TextBoxUser_PasswordConf.Password)
            {
                PasswordVerify.Text = "密码不一致，重新输入密码";
                TextBoxUser_PasswordConf.Password = "";
                return;
            }
            //手机号不符合11位的显示
      

            else if (TextBoxUser_Phone.Text.Length != 11 || Int64.TryParse(TextBoxUser_Phone.Text, out num2) == false)
            {

                PhoneNumber.Text = "手机号格式错误，请重新输入正确的手机号";
                TextBoxUser_Phone.Text = "";
                return;

            }
            else if (TextBoxUser_Sex.Text == "")
            {
                SexNull.Text = "请选择您的性别！！！";
                return;
            }
            else
            {
                UserNameTip.Text = "";
                PasswordVerify.Text = "";
                PhoneNumber.Text = " ";
                xiaohu(TextBoxUser_Name.Text, TextBoxUser_Password.Password, TextBoxUser_Phone.Text, TextBoxUser_Sex.Text, TextBoxUser_Birth.Text);
            }
        }
        public void xiaohu(string value1, string value2, string value3, string value4, string value5)
        {
            if (value1 != null && value2 != null && value3 != null && value4 != null && value5 != null)
            {
                SqlConnection Conn = new SqlConnection(user_con);
                Conn.Open();
              
                string selected_id = Loginxaml.Tag1.ToString();//获取选择行的序列号
                string SQL = string.Format("update movie_User set user_name='{0}', user_password='{1}', user_phone='{2}', user_sex='{3}', user_birthday='{4}'where user_id='{5}'", value1.ToString(), value2.ToString(), value3.ToString(), value4.ToString(), value5.ToString(), selected_id);
                SqlCommand cmd2 = new SqlCommand(SQL, Conn);
                cmd2.ExecuteNonQuery();
                Showdata();
                Conn.Close();
            }
        }

        private void Showmaker()
        {
        
            SqlConnection con = new SqlConnection(user_con);
            //获取选中用户的信息
            con.Open();
            string selected_id = Loginxaml.Tag1.ToString();//获取选择行的序列号

            string sql = @"select* from movie_User where user_id=" + selected_id;
            DataSet ds = new DataSet();
            SqlDataAdapter da = new SqlDataAdapter(sql, user_con);
            da.Fill(ds, "User");
            string editUser_name = ds.Tables["User"].Rows[0].ItemArray[1].ToString();
            string editUser_password = ds.Tables["User"].Rows[0].ItemArray[4].ToString();
            string editUser_phone = ds.Tables["User"].Rows[0].ItemArray[3].ToString();
            string editUser_sex = ds.Tables["User"].Rows[0].ItemArray[2].ToString();
            string editUser_birthday = ds.Tables["User"].Rows[0].ItemArray[5].ToString();
            con.Close();
            //在子窗口显示
            TextBoxUser_Name.Text = editUser_name;
            Tx1.Text = editUser_name;
            TextBoxUser_Password.Password = editUser_password;
            TextBoxUser_Phone.Text = editUser_phone;
            if (editUser_sex == "男")
            {
                TextBoxUser_Sex.SelectedIndex = 0;
            }
            else
            {
                TextBoxUser_Sex.SelectedIndex = 1;
            }

           TextBoxUser_Birth.SelectedDate = Convert.ToDateTime(editUser_birthday);
       
        }
        private void MyTickets_Click(object sender, RoutedEventArgs e)
        {
            var w = Window.GetWindow(MyTickets);
            var g = w.FindName("GridPrincipal") as Grid;
            g.Children.Clear();
            g.Children.Add(new UserControlMyTickets());
        }

        private void ExitAccount_Click(object sender, RoutedEventArgs e)
        {
            var w = Window.GetWindow(ExitAccount);
            LoginWindow loginWindow = new LoginWindow();
            loginWindow.Show();
            w.Close();
        }
    }
}
