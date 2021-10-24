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
using System.Configuration;
using System.Runtime.InteropServices;
using System.Windows.Interop;

using System.ComponentModel;
using System.Data;
using System.Drawing;
using MySql.Data.MySqlClient;
using MaterialDesignThemes.Wpf;

namespace Theater
{
    /// <summary>
    /// Loginxaml.xaml 的交互逻辑
    /// </summary>
 
    public partial class Loginxaml : UserControl
    {
        public static string Tag1;
        public Loginxaml()
        {
            InitializeComponent();
        }
        private void ButtonLogin_Click(object sender, EventArgs e)
        {
            //登录
            string userId = this.txtBoxUserId.Text;
            string userPassword = this.txtBoxPwd.Password;

            //管理员登录
            if (userId == "admin" && userPassword =="admin")
            {
                AdminWindow adminWindow = new AdminWindow();
                adminWindow.Show();
                var w = Window.GetWindow(ButtonLogin);
                w.Close();
            }

            Label_Password.Content = "";
            Label_User.Content = "";
            if (userId.Equals(""))
            {
                Label_User.Content = "用户id不能为空！";
            }
            else if (userPassword.Equals(""))
            {
                Label_Password.Content = "密码不能为空！";
            }
            else
            {
                string strcon = MainWindow.DataBasePath.dataBasePath;

                SqlConnection con = new SqlConnection(strcon);
                try
                {
                    con.Open();
                    SqlCommand com = new SqlCommand("select user_id,user_password from movie_User where user_id='" + userId + "' and user_password='" + userPassword + "'", con);
                   
                    // 建立SqlDataAdapter和DataSet对象
                    SqlDataAdapter da = new SqlDataAdapter(com);
                    DataSet ds = new DataSet();
                    int n = da.Fill(ds, "register");
                    if (n != 0)
                    {
                        MainWindow mainWindow = new MainWindow();
                        mainWindow.Show();
                        Tag1 = userId;
                        var w = Window.GetWindow(ButtonLogin);
                        w.Close();
                    }
                    else
                    {
                        Label_Password.Content = "用户id或密码错误！";
                    }
                }

                catch (Exception ex)
                {
                    Label_User.Content = "用户ID不存在，请输入正确的用户ID！";
                }
            }
        }
        //public string user_con = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\Dash\Desktop\最新\Theater\Theater\movie.mdf;Integrated Security=True";
        public string user_con = MainWindow.DataBasePath.dataBasePath;

         private void ButtonRegister_Click(object sender, RoutedEventArgs e)
        {
            var w = Window.GetWindow(ButtonRegister);
            var g = w.FindName("LoginWindowGrid") as Grid;
            g.Children.Clear();
            g.Children.Add(new Register());
        }
    }
}

