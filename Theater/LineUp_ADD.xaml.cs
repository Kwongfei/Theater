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
    /// LineUp_ADD.xaml 的交互逻辑
    /// </summary>
    public partial class LineUp_ADD : UserControl
    {
        public delegate void AddScreening(string value1, string value2, string value3, string value4,string value6);
        public AddScreening addScreening;
        public int num1;
        public LineUp_ADD()
        {
            InitializeComponent();
        }
        private string user_con = MainWindow.DataBasePath.dataBasePath;

        /// <summary>
        /// 建立电影名、电影厅的下拉列表
        /// </summary>
        private void combobox_movieName_Loaded(object sender, RoutedEventArgs e)
        {
            SqlConnection con = new SqlConnection(user_con);

            try
            {
                con.Open();
                DataSet ds1 = new DataSet();
                string sqlStr = "select movie_Name from movie_Info;";
                SqlDataAdapter da = new SqlDataAdapter(sqlStr, con);
                da.Fill(ds1, "movie_Name");
                DataTable dtGroup = ds1.Tables["movie_Name"];
                for (int i = 0; i < dtGroup.Rows.Count; i++)
                {
                    combobox_movieName.Items.Add(dtGroup.Rows[i]["movie_Name"].ToString());
                }
                con.Close();
            }
            catch (Exception ex11)
            {
                MessageBox.Show(ex11.Message);
            }
        }
        private void combobox_HallId_Loaded(object sender, RoutedEventArgs e)
        {
            SqlConnection con = new SqlConnection(user_con);

            try
            {
                con.Open();
                DataSet ds1 = new DataSet();
                string sqlStr = "select Hall_Id from Hall;";
                SqlDataAdapter da = new SqlDataAdapter(sqlStr, con);
                da.Fill(ds1, "Hall_Id");
                DataTable dtGroup = ds1.Tables["Hall_Id"];
                for (int i = 0; i < dtGroup.Rows.Count; i++)
                {
                    combobox_HallId.Items.Add(dtGroup.Rows[i]["Hall_Id"].ToString());
                }
                con.Close();
            }
            catch (Exception ex11)
            {
                MessageBox.Show(ex11.Message);
            }
        }


        private void AddScreening_Click(object sender, RoutedEventArgs e)
        {
            string Screening_movieName = combobox_movieName.Text;
            string Screening_HallId = combobox_HallId.Text;
            string Screening_date = combobox_date.Text;
            string Screening_time = combobox_Time.Text;
            string Screening_Price = Textbox_Price.Text;


            movieName_Tip.Text = "";
            HallId_Tip.Text = "";
            Date_Tip.Text = "";
            Time_Tip.Text = "";
            Price_Tip.Text = "";

            //返回相应的开始时间与结束时间
            SqlConnection con = new SqlConnection(user_con);
            con.Open();
            DataSet ds1 = new DataSet();
            DateTime dateselected = Convert.ToDateTime(Screening_date);
            string dateselected1 = dateselected.ToString("yyyy - MM - dd");
            string datenow1 = dateselected1.Replace("-", "").Replace(" ", "").Trim();
            string datenow2 = datenow1.Substring(2, 6);//获取选择的日期（例：210820）
            string sqlStr = "select Screening_StartTime,Screening_EndTime from Screening where Screening_HallId like '%" + Screening_HallId + "%' and  Screening_Id like '%" + datenow2 + "%'";
            SqlDataAdapter da = new SqlDataAdapter(sqlStr, con);
            da.Fill(ds1, "StartEnd");
            DataTable dtGroup = ds1.Tables["StartEnd"];
            bool judge = false;
            DateTime picked_time = Convert.ToDateTime(Screening_time);
            TimeSpan dspNow= picked_time.TimeOfDay;
            
            for (int i = 0; i < dtGroup.Rows.Count; i++)
            {
                string starttime = dtGroup.Rows[i].ItemArray[0].ToString();
                string endtime = dtGroup.Rows[i].ItemArray[1].ToString();
                TimeSpan starttime1 = DateTime.Parse(starttime).TimeOfDay;
                TimeSpan endtime1 = DateTime.Parse(endtime).TimeOfDay;
                if (dspNow >= starttime1 && dspNow <= endtime1)
                {
                    judge= true;
                    break;
                }
            }

            
            SqlCommand cmd2 = con.CreateCommand();
            cmd2.CommandText = "select movie_RelDate from movie_Info where movie_Name='" + Screening_movieName + "'";
            string Realdate = cmd2.ExecuteScalar().ToString();
            DateTime realdate1 = DateTime.Parse(Realdate).Date;
            DateTime choose = DateTime.Parse(Screening_date).Date;
            
            



            //电影名，电影厅，日期，时间，时长，价格为空的显示(还缺一个时间冲突的设置！)
            if (combobox_movieName.Text == "")
            {
                movieName_Tip.Text = "放映电影名为空，重新选择电影！";
                return;
            }
            else if (combobox_HallId.Text == "")
            {
                HallId_Tip.Text = "放映影院Id为空，重新选择影院！";
                return;
            }
            else if (combobox_date.Text == "" )
            {
                Date_Tip.Text = "放映日期为空，重新选择日期！";
                return;
            }
            else if (combobox_Time.Text == "")
            {
                Time_Tip.Text = "开场时间为空，重新选择开场时间！";
                return;
            }
            else if (Textbox_Price.Text == "" || Convert.ToDouble (Textbox_Price.Text)< 10.00 || Convert.ToDouble(Textbox_Price.Text) > 99.99)
            {
                Price_Tip.Text = "电影票价错误，请重新输入有效票价！";
                return;
            }
            //排片考虑时间冲突的问题！！！
            else if(judge == true )
            {
                Time_Tip.Text = "排片冲突，请合理安排电影放映！！！";
                combobox_Time.Text = "";
                return;
            }
            //避免排片日期在上映日期之前
            else if(choose< realdate1)
            {
                Date_Tip.Text = "放映日期错误，重新选择日期！";
                combobox_date.Text = "";
                return;
            }
            else
            {
                movieName_Tip.Text = "";
                HallId_Tip.Text = "";
                Date_Tip.Text = "";
                Price_Tip.Text = "";
                addScreening(Screening_movieName, Screening_HallId, Screening_date, Screening_time,  Screening_Price);
                DialogHost.CloseDialogCommand.Execute(null, null);
            }

        }


    }
}
