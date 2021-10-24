using System;
using System.Collections.Generic;
using System.Data;
using System.ComponentModel;
using System.Data.SqlClient;
using System.Drawing;
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

namespace Theater
{
    /// <summary>
    /// Theater_ADD.xaml 的交互逻辑
    /// </summary>
    public partial class Theater_ADD : UserControl
    {
        public delegate void AddHall(string value1, string value2, string value3, string value4);
        public AddHall addHall;
        public Theater_ADD()
        {
            InitializeComponent();
        }
        private string user_con = MainWindow.DataBasePath.dataBasePath;
        /// <summary>
        /// 建立影厅号的下拉列表
        /// </summary>

        private void TextboxHall_CinemaId_Loaded(object sender, RoutedEventArgs e)
        {
            SqlConnection con = new SqlConnection(user_con);

            try
            {
                con.Open();
                DataSet ds1 = new DataSet();
                string sqlStr = "select Cinema_ID from Cinema;";
                SqlDataAdapter da = new SqlDataAdapter(sqlStr, con);
                da.Fill(ds1, "Cinema_Id");
                DataTable dtGroup = ds1.Tables["Cinema_Id"];
                for (int i = 0; i < dtGroup.Rows.Count; i++)
                {
                    TextboxHall_CinemaId.Items.Add(dtGroup.Rows[i]["Cinema_Id"].ToString());
                }
                con.Close();
            }
            catch (Exception ex11)
            {
                MessageBox.Show(ex11.Message);
            }
        }
        
            
        
        private void AddHall_Click(object sender, RoutedEventArgs e)
        {
            string Hall_Name = TextboxHall_Name.Text;
            string Hall_CinemaId = TextboxHall_CinemaId.Text ;
            string Hall_SeatNum = TextboxHall_SeatNum.Text;
            string Hall_level = TextboxHall_level.Text ;


            HallName_Tip.Text  = "";
            HallId_Tip.Text = "";
            HallSeatNum_Tip.Text = "";
            HallLevel_Tip.Text  = "";
            //影厅名、所属影院Id、座位数、厅的级别为空的显示
            if (TextboxHall_Name.Text == "")
            {
                HallName_Tip.Text = "影厅名为空，重新输入影厅名！";
                return;
            }
            else if (TextboxHall_CinemaId.Text == "")
            {
                HallId_Tip.Text = "所属影院Id为空，重新选择影院Id！";
                return;
            }
            else if (TextboxHall_SeatNum.Text == "" || TextboxHall_SeatNum.Text!="16")
            {
                HallSeatNum_Tip.Text = "座位数量错误，重新输入座位数！";
                return;
            }
            else if (TextboxHall_level.Text == "")
            {
                HallLevel_Tip.Text = "请选择影厅类型！";
                return;
            }

            else
            {
                HallName_Tip.Text = "";
                HallId_Tip.Text = "";
                HallSeatNum_Tip.Text = "";
                HallLevel_Tip.Text = "";
                addHall(Hall_Name,Hall_CinemaId,Hall_SeatNum,Hall_level);
                DialogHost.CloseDialogCommand.Execute(null, null);
            }

        }


    }
}
