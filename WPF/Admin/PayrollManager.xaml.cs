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
using DataAssetObjects;
using Repositories.Repository;
using Services.Service;
using BusinessObjects.Models;
using WPF.Employee;
using static System.Net.Mime.MediaTypeNames;
using Org.BouncyCastle.Ocsp;

namespace WPF.Admin
{
    /// <summary>
    /// Interaction logic for PayrollManager.xaml
    /// </summary>
    public partial class PayrollManager : Window
    {
        private PayrollService payrollService;
        private readonly EmployeeService employeeService;
        int EmpID = 0;
        public PayrollManager(int EmployeeId)
        {
            InitializeComponent();
            HrmanagementContext context = new HrmanagementContext();
            PayrollDAO payrollDAO = new PayrollDAO(context);
            PayrollRepository payrollRepository = new PayrollRepository(payrollDAO);
            payrollService = new PayrollService(payrollRepository);
            employeeService = new EmployeeService(new EmployeeRepository(new EmployeeDAO(context)));
            cbMonth.SelectedIndex = DateTime.Now.Month - 1;
            txtYear.Text = DateTime.Now.Year.ToString();
            EmpID = EmployeeId;
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
            else
            {
                // Hiển thị lên UI
                txtTotalWorkingHours.Text = totalWorkingHours.ToString("N0") + " giờ";
                txtTotalOutTime.Text = totalOutTime.ToString("N0") + " giờ";
                txtTotalSalary.Text = totalSalary.ToString("N0") + " VNĐ";
            }
        }

        private void btnBack_Click(object sender, RoutedEventArgs e)
        {
            EmployeeDashboard employeeDashboard = new EmployeeDashboard(EmpID);
            employeeDashboard.Show();
            this.Close(); // Đóng cửa sổ hiện tại, quay về màn hình trước
        }

        private void btnAdd_Click(object sender, RoutedEventArgs e)
        {
            int month = int.Parse((cbMonth.SelectedItem as ComboBoxItem)?.Content.ToString());
            int year = int.Parse(txtYear.Text);
            int totalWorkingHours = payrollService.GetTotalWorkingHours(EmpID, month, year);
            int totalOutTime = payrollService.GetTotalOutTime(EmpID, month, year);
            int totalSalary = payrollService.GetTotalSalary(EmpID, month, year);
            int thisDay = DateTime.Now.Day;
            if (string.IsNullOrWhiteSpace(txtTotalSalary.Text) || string.IsNullOrWhiteSpace(txtTotalOutTime.Text))
            {
                MessageBox.Show("Chưa đủ điều kiện kết toán", "Lỗi", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            if (totalSalary <= 0)
            {
                MessageBox.Show("Lương không hợp lệ", "Lỗi", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            if (month != DateTime.Now.Month - 1 || year != DateTime.Now.Year)
            {
                MessageBox.Show("Thời gian không hợp lệ", "Lỗi", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            var existPayroll = payrollService.GetPayrollByMonthAndYear(EmpID, month, year);
            if (existPayroll != null)
            {
                MessageBox.Show("Đã kết toán lương cho tháng này", "Lỗi", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            try
            {
                var employee = employeeService.GetEmployeeByID(EmpID);
                var newPay = new BusinessObjects.Models.Payroll();
                newPay.EmployeeId = EmpID;
                newPay.PayrollMonth = DateTime.Now.Month - 1;
                newPay.BasicSalary = employee.Position.BasicSalary;
                newPay.Allowance = 500000;
                newPay.Bonus = totalOutTime * 50000;
                newPay.TotalSalary = totalSalary;
                newPay.PaymentDate = DateOnly.FromDateTime(DateTime.Now);
                newPay.PaymentStatus = "Đang xử lý";
                payrollService.AddPayroll(newPay);
                LoadDefaultData();
            }
            catch (Exception ex)
            {
                // Log the exception or handle it as needed
                MessageBox.Show($"An error occurred: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}
