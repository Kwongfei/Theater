using MaterialDesignThemes.Wpf;
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

namespace Theater
{
    /// <summary>
    /// AdminAddAccount.xaml 的交互逻辑
    /// </summary>
    public partial class AdminAddAccount : UserControl
    {
        public delegate void AddAccount(string value1, string value2, string value3 , string value4 , string value5 );
        public AddAccount addAccount;
        public Int64 num2;
        public AdminAddAccount()
        {
            InitializeComponent();
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
            else if (TextBoxUser_Phone.Text.Length != 11 || Int64.TryParse(TextBoxUser_Phone.Text, out num2) == false)
            {
                PhoneNumber.Text = "格式错误，请重新输入正确的手机号";
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
                addAccount(user_name, user_password, user_phone, user_sex, user_birth);
                DialogHost.CloseDialogCommand.Execute(null, null);
            }

            

        }
    }
}

