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
using System.Windows.Shapes;

namespace WPF
{
    /// <summary>
    /// Interaction logic for EmployeeDashboard.xaml
    /// </summary>
    public partial class EmployeeDashboard : Window
    {
        public EmployeeDashboard()
        {
            InitializeComponent();
        }

        private void Logout_Click(object sender, RoutedEventArgs e)
        {
            this.Close(); // Đóng cửa sổ hiện tại, quay về màn hình trước
        }

        private void ChamCong_Click(object sender, RoutedEventArgs e)
        {
            ChamCong  chamCong = new ChamCong();
            chamCong.Show();
            this.Close();
        }

        private void BaoCaoChamCong_Click(object sender, RoutedEventArgs e)
        {
            BaoCaoChamCong BaoCaoChamCong = new BaoCaoChamCong();
            BaoCaoChamCong.Show();
            this.Close();
        }

        private void BaoCaoLuong_Click(object sender, RoutedEventArgs e)
        {
            this.Close(); // Đóng cửa sổ hiện tại, quay về màn hình trước

        }

        private void Profile_Click(object sender, RoutedEventArgs e)
        {
            this.Close(); // Đóng cửa sổ hiện tại, quay về màn hình trước

        }
    }
}
