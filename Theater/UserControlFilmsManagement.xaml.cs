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
    /// UserControlFilmsManagement.xaml 的交互逻辑
    /// </summary>
    public partial class UserControlFilmsManagement : UserControl
    {
        public UserControlFilmsManagement()
        {
            InitializeComponent();
            ShowFilm();
        }
        //public string user_con = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=E:\Java\DBS\重构\Movie8.17.2\Theater\Theater\movie.mdf;Integrated Security=True";
        private string user_con = MainWindow.DataBasePath.dataBasePath;

        private void ShowFilm()
        {
            datagrid_Film.DataContext = null;
            SqlConnection con = new SqlConnection(user_con);
            con.Open();
            string sql = @"select* from movie_Info";
            DataSet ds = new DataSet();
            SqlDataAdapter da = new SqlDataAdapter(sql, user_con);
            da.Fill(ds, "Info");
            datagrid_Film.DataContext = ds.Tables["Info"];
            con.Close();
        }
       /// <summary>
       /// 显示全部信息操作
       /// </summary>
       /// <param name="sender"></param>
       /// <param name="e"></param>
        private void showall_Click(object sender, RoutedEventArgs e)
        {
            ShowFilm();
            /*FileStream fs = new FileStream(@"D:\\Desktop\\各类卷子\\大三下\\数据库课件\\Movie8.16.3\\Theater\\Images\\哪吒.jpeg", FileMode.Open, FileAccess.Read);
            BinaryReader br = new BinaryReader(fs);
            Byte[] imgBytesIn = br.ReadBytes((int)fs.Length);
            fs.Close();
            SqlConnection conn = new SqlConnection(user_con);
            conn.Open();
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = conn;
            cmd.CommandText = "update movie_Info set movie_Image=@imgfile where movie_Name='哪吒之魔童降世'";
            SqlParameter par = new SqlParameter("@imgfile", SqlDbType.Image);
            par.Value = imgBytesIn;
            cmd.Parameters.Add(par);

            int t = (int)(cmd.ExecuteNonQuery());
            if (t > 0)
            {
                MessageBox.Show("插入成功");
            }
            else
            {
                MessageBox.Show("插入失败");
            }
            conn.Close();*/

        }
        /// <summary>
        /// 查询操作
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void SearchFilm_Click(object sender, RoutedEventArgs e)
        {
            datagrid_Film.DataContext = null;
            SqlConnection sqlconn = new SqlConnection(user_con);
            sqlconn.Open();
            string sqlsearch = "select* from movie_Info where concat(movie_Name,movie_Director,movie_Actors,movie_Type,movie_RelDate) like '%" + search_name.Text + "%'";
            DataSet ds = new DataSet();
            SqlDataAdapter da = new SqlDataAdapter(sqlsearch, user_con);
            da.Fill(ds, "User");
            datagrid_Film.DataContext = ds.Tables["User"];
            if (datagrid_Film.Items.Count == 0)
            {
                MessageBox.Show("信息输入错误，该电影不存在");
                ShowFilm();
            }
            sqlconn.Close();
            search_name.Text = "";
        }

        /// <summary>
        /// 删除用户
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void DeleteFilmFinish(int value)
        {
            if (value == 1)
            {
                SqlConnection con = new SqlConnection(user_con);
                con.Open();
                DataRowView dwView = (datagrid_Film.SelectedItem) as DataRowView;//当前选中列
                string selected_name = dwView[0].ToString();//获取选择行的序列号
                if (dwView != null)
                {
                    dwView.Delete();//删除选中列 
                }
                SqlCommand cmd = con.CreateCommand();
                string sqlsearch1 = string.Format("delete from movie_Info where movie_Name='{0}' " , selected_name) ;
                cmd.CommandText = sqlsearch1;
                cmd.ExecuteNonQuery();
                con.Close();
            }
        }
        private void FilmDelete_Click(object sender, RoutedEventArgs e)
        {
           FlimsManagementDeleteConfer filmsdeleteConfer = new FlimsManagementDeleteConfer();
           filmsdeleteConfer.filmdelete = DeleteFilmFinish;
           DialogHost.Show(filmsdeleteConfer, "RootDialog");
        }

        /// <summary>
        /// 添加电影弹窗设置
        /// </summary>
        public void AdminAddFilmFinish(string value1, string value2, string value3, string value4, string value5, string value6, string value7,int value8)
        {
            if (value1 != null && value2 != null && value3 != null && value4 != null && value5 != null && value6 != null && value7 != null && value8 != 0)
            {
                DataTable dt = new DataTable();
                SqlConnection Conn = new SqlConnection(user_con);
                Conn.Open();
                string value7new ="/Images/"+ value7;
                string SQL = "insert into movie_Info(movie_Name,movie_Director, movie_Actors, movie_Type, movie_RelDate, movie_Intro,movie_Image,movie_time) values ('" + value1.ToString() + "','" + value2.ToString() + "','" + value3.ToString() + "','" + value4.ToString() + "','" + value5.ToString() + "','" + value6.ToString()+"','" + value7new.ToString() + "'," + value8 + ")";
                SqlCommand cmd2 = new SqlCommand(SQL, Conn);
                cmd2.ExecuteNonQuery();
                ShowFilm();
                Conn.Close();
            }
        }
        private void AdminAddFilm_Click(object sender, RoutedEventArgs e)
        {
            FilmsManagement_AddFilms Addfilm = new FilmsManagement_AddFilms();
            Addfilm.addFilm = AdminAddFilmFinish;
            DialogHost.Show(Addfilm, "RootDialog");
        }

        /// <summary>
        /// 管理员编辑电影信息界面弹出
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        /// 
        public void EditFilmFinish(string value1, string value2, string value3, string value4, string value5, string value6, string value7,int value8)
        {
            if (value1 != null && value2 != null && value3 != null && value4 != null && value5 != null && value6 != null && value7 != null && value8 != 0)
            {
                SqlConnection Conn = new SqlConnection(user_con);
                Conn.Open();
                string value7new = "/Images/" + value7;
                string SQL = string.Format("update movie_Info set movie_Director='{0}', movie_Actors='{1}', movie_Type='{2}',  movie_RelDate='{3}', movie_Intro='{4}',movie_Image='{5}',movie_time='{6}' where movie_name='{7}'", value2.ToString(), value3.ToString(), value4.ToString(), value5.ToString(), value6.ToString(), value7new.ToString() ,value8,value1.ToString());
                SqlCommand cmd2 = new SqlCommand(SQL, Conn);
                cmd2.ExecuteNonQuery();
                ShowFilm();
                Conn.Close();
            }
        }

        private void AdminEditFilm_Click(object sender, RoutedEventArgs e)
        {
            FilmsManagement_EditFilms adminEditFilm = new FilmsManagement_EditFilms();
            SqlConnection con = new SqlConnection(user_con);
            //获取选中用户的信息
            con.Open();
            DataRowView dwView = (datagrid_Film.SelectedItem) as DataRowView;//当前选中列
            string selected_name = dwView[0].ToString();//获取选择行的序列号
            string sql = string.Format("select* from movie_Info where movie_name='{0}' " ,selected_name);
            DataSet ds = new DataSet();
            SqlDataAdapter da = new SqlDataAdapter(sql, user_con);
            da.Fill(ds, "Film");
            string editMoviename = ds.Tables["Film"].Rows[0].ItemArray[0].ToString();
            string editMovie_director = ds.Tables["Film"].Rows[0].ItemArray[1].ToString();
            string editMovie_actors = ds.Tables["Film"].Rows[0].ItemArray[2].ToString();
            string editMovie_type = ds.Tables["Film"].Rows[0].ItemArray[3].ToString();
            string editMovie_date = ds.Tables["Film"].Rows[0].ItemArray[5].ToString();
            string editMovie_length = ds.Tables["Film"].Rows[0].ItemArray[6].ToString();
            string editMovie_intro = ds.Tables["Film"].Rows[0].ItemArray[7].ToString();
            string editMovie_image = ds.Tables["Film"].Rows[0].ItemArray[4].ToString();
            string editMovie_imagenew = editMovie_image.Replace("/Images/","");
            con.Close();
            //在子窗口显示
            adminEditFilm.Textbox_Moviename.Text = editMoviename;
            adminEditFilm.Textbox_Director.Text = editMovie_director;
            adminEditFilm.Textbox_Actors.Text = editMovie_actors;
            adminEditFilm.Textbox_Type.Text = editMovie_type;
            adminEditFilm.Textbox_MovieIntro.Text = editMovie_intro;
            adminEditFilm.TextBox_Length.Text = editMovie_length;
            adminEditFilm.Textbox_Image.Text = editMovie_imagenew;
            adminEditFilm.Textbox_MovieDate.SelectedDate = Convert.ToDateTime(editMovie_date);
            adminEditFilm.editFilm = EditFilmFinish;
            DialogHost.Show(adminEditFilm, "RootDialog");
        }

    }
}
