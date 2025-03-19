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
using BusinessObjects.Models;
using DataAssetObjects;
using Repositories.Interface;
using Repositories.Repository;
using Services.InterfaceServie;
using Services.Service;
using static System.Net.Mime.MediaTypeNames;

namespace WPF.Employee
{
    /// <summary>
    /// Interaction logic for Payroll.xaml
    /// </summary>
    public partial class Payroll : Window
    {
        private PayrollService payrollService;
        private readonly EmployeeService employeeService;
        int EmpID = 0;
        public Payroll(BusinessObjects.Models.Employee emp)
        {
            InitializeComponent();
            HrmanagementContext context = new HrmanagementContext();
            PayrollDAO payrollDAO = new PayrollDAO(context);
            PayrollRepository payrollRepository = new PayrollRepository(payrollDAO);
            payrollService = new PayrollService(payrollRepository);
            employeeService = new EmployeeService(new EmployeeRepository(new EmployeeDAO(context)));
            cbMonth.SelectedIndex = DateTime.Now.Month -1;
            txtYear.Text = DateTime.Now.Year.ToString();
            EmpID = emp.EmployeeId;
            LoadDefaultData();
        }
        private void LoadDefaultData()
        {
            List<BusinessObjects.Models.Payroll> payrollList = payrollService.GetPayrollByEmp(EmpID);
            dgPayroll.ItemsSource = payrollList;
        }

        private void BtnLoadData_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                // Validate input
                if (!int.TryParse(txtYear.Text, out int year) || year < 2000 || year > DateTime.Now.Year)
                {
                    MessageBox.Show("Vui lòng nhập năm hợp lệ!", "Lỗi", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }

                if (cbMonth.SelectedItem == null)
                {
                    MessageBox.Show("Vui lòng chọn tháng!", "Lỗi", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }

                // Lấy giá trị tháng từ ComboBox
                int month = int.Parse((cbMonth.SelectedItem as ComboBoxItem)?.Content.ToString());

                // Lấy dữ liệu từ service
                LoadPayrollData(month, year);
                LoadStatistics(month, year);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Đã xảy ra lỗi: {ex.Message}", "Lỗi", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void LoadPayrollData(int month, int year)
        {
            dgPayroll.ItemsSource = null;
            // Lọc theo tháng và năm
            List<BusinessObjects.Models.Payroll> payrollList = payrollService.GetPayrollByMonth(EmpID, month, year);
            if (payrollList != null)
            {
                dgPayroll.ItemsSource = payrollList;
            }
            else
            {
                dgPayroll.ItemsSource = null;
                MessageBox.Show("Không tìm thấy dữ liệu lương cho tháng/năm này!",
                    "Thông báo", MessageBoxButton.OK, MessageBoxImage.Information);
            }
        }

        private void LoadStatistics(int month, int year)
        {
            // Lấy dữ liệu thống kê
            int totalWorkingHours = payrollService.GetTotalWorkingHours(EmpID, month, year);
            int totalOutTime = payrollService.GetTotalOutTime(EmpID, month, year);
            int totalSalary = payrollService.GetTotalSalary(EmpID, month, year);
            if (totalSalary <= 0)
            {
                txtTotalWorkingHours.Text = totalWorkingHours.ToString("N0") + " giờ";
                txtTotalOutTime.Text = totalOutTime.ToString("N0") + " giờ";
                txtTotalSalary.Text = "0 VNĐ";
            }
            else {
                // Hiển thị lên UI
                txtTotalWorkingHours.Text = totalWorkingHours.ToString("N0") + " giờ";
                txtTotalOutTime.Text = totalOutTime.ToString("N0") + " giờ";
                txtTotalSalary.Text = totalSalary.ToString("N0") + " VNĐ";
            }
        }

        private void btnBack_Click(object sender, RoutedEventArgs e)
        {
            EmployeeProfile employeeProfile = new EmployeeProfile(EmpID);
            employeeProfile.Show();
            this.Close();
        }
    }
}

