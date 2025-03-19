using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
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
using Repositories.Repository;
using Services.Service;
using System.Globalization;
using WPF.Admin;
using Org.BouncyCastle.Ocsp;


namespace WPF.Employee
{
    /// <summary>
    /// Interaction logic for EmployeeProfile.xaml
    /// </summary>
    public partial class EmployeeProfile : Window
    {
        private readonly EmployeeService employeeService;
        int employeeId = 0;
        public EmployeeProfile(int empID)
        {
            InitializeComponent();
            var context = new HrmanagementContext();
            var employeeDao = new EmployeeDAO(context);
            var employeeRepository = new EmployeeRepository(employeeDao);
            employeeService = new EmployeeService(employeeRepository);
            employeeId = empID;
            loadData();
        }
        private void loadData()
        {
            try
            {
                var employee = employeeService.GetEmployeeByID(employeeId);
                tbId.Text = employee.EmployeeId.ToString();
                tbName.Text = employee.FullName;
                rbMale.IsChecked = employee.Gender == "Nam" ? true : false;
                rbFemale.IsChecked = employee.Gender == "Nữ" ? true : false;
                dpDate.Text = employee.DateOfBirth.ToString();
                tbMail.Text = employee.Email;
                tbphone.Text = employee.PhoneNumber;
                tbAddress.Text = employee.Address;
                cbDepartment.Text = employee.Department.DepartmentName;
                cbPosition.Text = employee.Position.PositionName;
                tbSalary.Text = employee.Position.BasicSalary.ToString("C0", CultureInfo.GetCultureInfo("vi-VN"));
                dpStartTime.Text = employee.StartDate.ToString();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi khi tải thông tin: {ex.Message}", "Lỗi", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void btnUpdate_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(tbName.Text) ||
                string.IsNullOrEmpty(dpDate.Text) ||
                string.IsNullOrWhiteSpace(tbMail.Text) ||
                string.IsNullOrWhiteSpace(tbphone.Text) ||
                string.IsNullOrWhiteSpace(tbAddress.Text))
            {
                MessageBox.Show("Vui lòng nhập đầy đủ thông tin");
            }
            else if (!Regex.IsMatch(tbphone.Text, @"^(0[35789][0-9]{8})$"))
            {
                MessageBox.Show("Số điện thoại không hợp lệ! Vui lòng nhập số có 10 chữ số và bắt đầu bằng số 0.");
            }
            else if (!Regex.IsMatch(tbMail.Text, @"^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,}$"))
            {
                MessageBox.Show("Email không hợp lệ! Vui lòng nhập đúng định dạng (vd: example@gmail.com).");
            }
            else
            {
                try
                {
                    var employee = employeeService.GetEmployeeByID(employeeId);
                    employee.FullName = tbName.Text;
                    DateOnly.TryParse(dpDate.Text, out DateOnly date);
                    employee.DateOfBirth = date;
                    employee.Email = tbMail.Text;
                    employee.PhoneNumber = tbphone.Text;
                    employee.Address = tbAddress.Text;
                    employeeService.UpdateEmployee(employee);
                    MessageBox.Show("Cập nhật thành công!", "Thông báo", MessageBoxButton.OK, MessageBoxImage.Information);
                    loadData();
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Lỗi khi cập nhật thông tin: {ex.Message}", "Lỗi", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
        }
        private void btnNotification_Click(object sender, RoutedEventArgs e)
        {

        }

        private void btnAvatar_Click(object sender, RoutedEventArgs e)
        {

        }

        private void btnDayOff_Click(object sender, RoutedEventArgs e)
        {

        }

        private void btnPayroll_Click(object sender, RoutedEventArgs e)
        {
            var employee = employeeService.GetEmployeeByID(employeeId);
            Payroll payroll = new Payroll(employee);
            payroll.Show();
            this.Close();
        }

        private void btnPay_Click(object sender, RoutedEventArgs e)
        {
            var employee = employeeService.GetEmployeeByID(employeeId);
            PayrollManager payrollManager = new PayrollManager(employee);
            payrollManager.Show();
            this.Close();
        }
    }
		private void BackBtn_Click(object sender, RoutedEventArgs e)
		{
			EmployeeDashboard_Huy employeeDashboard = new EmployeeDashboard_Huy(employeeId);
			employeeDashboard.Show();
			this.Close(); // Đóng cửa sổ hiện tại, quay về màn hình trước
		}
	}
}