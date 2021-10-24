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
using MaterialDesignThemes.Wpf;
using System.Data.SqlClient;
using System.Data;
namespace Theater
{
    /// <summary>
    /// Register.xaml 的交互逻辑
    /// </summary>
    public partial class Register : UserControl
    {
        public delegate void AddAccount(string value1, string value2, string value3, string value4, string value5);
        public AddAccount addAccount;
        public Register()
        {
            InitializeComponent();
        }
        public Int64 num2;
        //public string user_con = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=E:\Java\DBS\重构\Theater8.20.1\Theater\Theater\movie.mdf;Integrated Security=True";
        private string user_con = MainWindow.DataBasePath.dataBasePath;
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
        private void AddUser_Click(object sender, RoutedEventArgs e)
        {
            string user_name = TextBoxUser_Name.Text;
            string user_password = TextBoxUser_Password.Password;
            string user_passwordconf = TextBoxUser_PasswordConf.Password;
            string user_phone = TextBoxUser_Phone.Text;
            string user_sex = TextBoxUser_Sex.Text;
            string user_birth = TextBoxUser_Birth.Text;

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
            else if (TextBoxUser_Phone.Text.Length != 11 ||  Int64.TryParse(TextBoxUser_Phone.Text, out num2) == false )
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
                PhoneNumber.Text = "";
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
                if (datenow2 != result1)
                {
                    string user_idold = datenow2 + "001";
                    Int64 user_id = Convert.ToInt64(user_idold);
                    string SQL = "insert into movie_User(user_id,user_name, user_password, user_phone, user_sex, user_birthday) values ('" + user_id + "','" + TextBoxUser_Name.Text + "','" + TextBoxUser_Password.Password + "','" + TextBoxUser_Phone.Text+ "','" + TextBoxUser_Sex.Text + "','" + TextBoxUser_Birth.Text + "')";
                    SqlCommand cmd2 = new SqlCommand(SQL, Conn);
                    cmd2.ExecuteNonQuery();
                    var w = Window.GetWindow(Cancel);
                    var g = w.FindName("LoginWindowGrid") as Grid;
                    g.Children.Clear();
                    g.Children.Add(new Loginxaml());
                    string message_ID_password = "您的用户id为：" + user_id + System.Environment.NewLine +
                        "密码为：" + TextBoxUser_Password.Password + System.Environment.NewLine + "请妥善保存！";
                    MessageBox.Show(message_ID_password);
                }
                else
                {
                    Int64 user_id = Convert.ToInt64(result) + 1;
                    string SQL = "insert into movie_User(user_id,user_name, user_password, user_phone, user_sex, user_birthday) values ('" + user_id + "','" + TextBoxUser_Name.Text + "','" + TextBoxUser_Password.Password + "','" + TextBoxUser_Phone.Text + "','" + TextBoxUser_Sex.Text + "','" + TextBoxUser_Birth.Text + "')";
                    SqlCommand cmd2 = new SqlCommand(SQL, Conn);
                    cmd2.ExecuteNonQuery();
                    var w = Window.GetWindow(Cancel);
                    var g = w.FindName("LoginWindowGrid") as Grid;
                    g.Children.Clear();
                    g.Children.Add(new Loginxaml()); string message_ID_password = "您的用户id为：" + user_id + System.Environment.NewLine +
                         "密码为：" + TextBoxUser_Password.Password + System.Environment.NewLine + "请妥善保存！";
                    MessageBox.Show(message_ID_password);
                }
                Showdata();
                Conn.Close();
            }
        }
        private void Cancel_Click(object sender, RoutedEventArgs e)
        {
            var w = Window.GetWindow(Cancel);
            var g = w.FindName("LoginWindowGrid") as Grid;
            g.Children.Clear();
            g.Children.Add(new Loginxaml());
        }
    }
}
