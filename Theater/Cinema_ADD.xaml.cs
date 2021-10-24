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
    /// Cinema_ADD.xaml 的交互逻辑
    /// </summary>
    public partial class Cinema_ADD : UserControl
    {
        public delegate void AddCinema(string value1, string value2, string value3, string value4, string value5, string value6);
        public AddCinema addCinema;
        public Int64 num2;
        public Cinema_ADD()
        {
            InitializeComponent();
        }
        private void AddCinema_Click(object sender, RoutedEventArgs e)
        {
            string Cinema_Name = TextboxCinema_Name.Text;
            string Cinema_Hallnum = TextboxCinema_Hallnum.Text;
            string Cinema_phone = TextboxCinema_Phone.Text;
            string Cinema_address = TextboxCinema_Address.Text;
            string Cinema_province = SelectProvince.Text;
            string Cinema_city = SelectCity.Text;

            CinemaName_Tip.Text = "";
            CinemaHallnum_Tip.Text = "";
            CinemaPhone_Tip.Text = "";
            CinemaAddress_Tip.Text = "";
            City_Tip.Text = "";
            //影院名、影厅数、联系方式、地址为空的显示
            if (TextboxCinema_Name.Text == "")
            {
                CinemaName_Tip.Text = "影院名为空，重新输入影院名！";
                return;
            }
            else if (TextboxCinema_Hallnum.Text == "")
            {
                CinemaHallnum_Tip.Text = "影厅数为空，重新输入影厅数！";
                return;
            }
            else if (TextboxCinema_Phone.Text == "")
            {
                CinemaPhone_Tip.Text = "联系方式为空，重新输入联系方式！";
                return;
            }
            else if (SelectProvince.Text == "" || SelectCity.Text == "")
            {
                City_Tip.Text = "请选择省份与城市！";
                return;
            }
            else if (TextboxCinema_Address.Text == "")
            {
                CinemaAddress_Tip.Text = "影院地址为空，重新输入地址！";
                return;
            }
            //联系方式不符合11位的显示
            else if (TextboxCinema_Phone.Text.Length != 11 || Int64.TryParse(TextboxCinema_Phone.Text, out num2) == false)
            {
                CinemaPhone_Tip.Text = "联系方式格式错误，请重新输入正确的联系方式";
                TextboxCinema_Phone.Text = "";
                return;
            }
 
            else if (Int64.TryParse(TextboxCinema_Hallnum.Text,out num2)==false)//判断输入的电影厅数目是否为整数
            {
                CinemaHallnum_Tip.Text = "影厅数必须为正整数，重新输入影厅数！";
                TextboxCinema_Hallnum.Text = "";
                return;
            }

            else
            {
                CinemaName_Tip.Text = "";
                CinemaHallnum_Tip.Text = "";
                CinemaPhone_Tip.Text = "";
                CinemaAddress_Tip.Text = "";
                City_Tip.Text = "";
                addCinema(Cinema_Name, Cinema_Hallnum, Cinema_phone, Cinema_address,Cinema_province, Cinema_city);
                DialogHost.CloseDialogCommand.Execute(null, null);
            }

        }


    }
}
