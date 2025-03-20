using System;
using System.ComponentModel;
using System.Windows;
using System.Windows.Controls;
using WPF.Admin;

namespace WPF
{
    public partial class EmployeeDashboard : Window, INotifyPropertyChanged
    {
        private object _currentView;

        private int _employeeId;

        public EmployeeDashboard(int empID)
        {
            InitializeComponent();

            this.DataContext = this;

            CurrentView = null;
            _employeeId = empID;
        }

        public object CurrentView
        {
            get { return _currentView; }
            set
            {
                _currentView = value;
                OnPropertyChanged(nameof(CurrentView));
            }
        }

        private void ReportEmployee_Click(object sender, RoutedEventArgs e)
        {
            CurrentView = new EmployeeManager(_employeeId);
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        private void QuanLyPhongBan_btn_Click(object sender, RoutedEventArgs e)
        {
            DepManager depManager = new DepManager(_employeeId);
            depManager.Show();
            this.Close();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            MainWindow main = new MainWindow();
            main.Show();
            this.Close();
        }

        private void QuanLyNghiPhep_btn_Click(object sender, RoutedEventArgs e)
        {
            ManagerOnLeave managerOnLeave = new ManagerOnLeave(_employeeId);
            managerOnLeave.Show();
            this.Close();
        }

        private void BaoCaoChamCong_btn_Click(object sender, RoutedEventArgs e)
        {
            BaoCaoChamCong baoCaoChamCong = new BaoCaoChamCong(_employeeId);
            baoCaoChamCong.Show();
            this.Close();
        }

        private void ThongKeNhanVien_btn_Click(object sender, RoutedEventArgs e)
        {
            BaoCaoNhanVien m1 = new BaoCaoNhanVien(_employeeId);
            m1.Show();
            this.Close();
        }

        private void ThongKeLuongNhanVien_btn_Click(object sender, RoutedEventArgs e)
        {
            BaoCaoLuongNhanVien baocao = new BaoCaoLuongNhanVien(_employeeId);
            baocao.Show();
            this.Close();
        }

        private void TraLuong_btn_Click(object sender, RoutedEventArgs e)
        {
            PayrollManager a = new PayrollManager(_employeeId);
            a.Show();
            this.Close();
        }

        private void Thongbao_btn_Click(object sender, RoutedEventArgs e)
        {
            TaoThongBao a = new TaoThongBao(_employeeId);
            a.Show();
        }
    }
}