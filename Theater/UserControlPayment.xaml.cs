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
    /// UserControlPayment.xaml 的交互逻辑
    /// </summary>
    public partial class UserControlPayment : UserControl
    {
        public UserControlPayment()
        {
            InitializeComponent();
        }
        
        private void Btn1_Click(object sender, RoutedEventArgs e)
        {
            var w = Window.GetWindow(btn1);
            var g = w.FindName("GridPrincipal") as Grid;
            g.Children.Clear();
            g.Children.Add(new UserControlMyTickets());
        }
    }
}
