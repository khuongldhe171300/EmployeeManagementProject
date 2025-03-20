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
using WPF.Employee;

namespace WPF
{
    /// <summary>
    /// Interaction logic for EmployeeDashboard_Huy.xaml
    /// </summary>
    public partial class EmployeeDashboard_Huy : Window
    {
        private int employeeId;

        public EmployeeDashboard_Huy(int employeeId)
        {
            InitializeComponent();
            this.employeeId = employeeId;
        }

        private void Logout_Click(object sender, RoutedEventArgs e)
        {
            MainWindow mainWindow = new MainWindow();
            mainWindow.Show();
            this.Close(); // Đóng cửa sổ hiện tại, quay về màn hình trước
        }

        private void ChamCong_Click(object sender, RoutedEventArgs e)
        {
            ChamCong chamCong = new ChamCong(employeeId);
            chamCong.Show();
            this.Close();
        }

        private void BaoCaoChamCong_Click(object sender, RoutedEventArgs e)
        {
            BaoCaoChamCong BaoCaoChamCong = new BaoCaoChamCong(employeeId);
            BaoCaoChamCong.Show();
            this.Close();
        }

        private void BaoCaoLuong_Click(object sender, RoutedEventArgs e)
        {
            BaoCaoLuongNhanVien baoCaoLuongNhanVien = new BaoCaoLuongNhanVien(employeeId);
            baoCaoLuongNhanVien.Show();
            this.Close(); // Đóng cửa sổ hiện tại, quay về màn hình trước
        }

        private void Profile_Click(object sender, RoutedEventArgs e)
        {

            new EmployeeProfile(employeeId).Show();
            this.Close();
        }

        private void Notification_Click(object sender, RoutedEventArgs e)
        {
            new NotificationEmp(employeeId).Show();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
           
            TaoDonXinNghi taoDonXinNghi = new TaoDonXinNghi(employeeId);
            taoDonXinNghi.Show();
        }
    }
}
