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
    /// DeleteConferFlimsManagement.xaml 的交互逻辑
    /// 这是用户管理的删除确认按钮！！！
    /// </summary>
    public partial class FlimsManagementDeleteConfer : UserControl
    {
        public delegate void FilmDelete(int value);//委托函数
        public FilmDelete filmdelete;
        public FlimsManagementDeleteConfer()
        {
            InitializeComponent();
        }
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            filmdelete(1);
        }
    }
}
