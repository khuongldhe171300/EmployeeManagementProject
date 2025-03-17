using BusinessObjects.Models;
using DataAssetObjects;
using Services.Service;
using System;
using Repositories.Repository;
using System.Windows;
using System.Windows.Controls;

namespace WPF.Admin
{
    /// <summary>
    /// Interaction logic for AddLeaveRequest.xaml
    /// </summary>
    public partial class AddLeaveRequest : Window
    {
        private readonly HrmanagementContext _context = new HrmanagementContext();
        private readonly LeaveRequestService _leaveRequestService;
        private readonly EmployeeService _employeeService;
        public event EventHandler LeaveRequestAdded;

        public AddLeaveRequest()
        {
            var LeaveRequestDAO = new LeaveRequestDAO(_context);
            var LeaveRequestRepository = new LeaveRequestRepository(LeaveRequestDAO);
            var EmployeeDao = new EmployeeDAO(_context);
            var EmployeeRepository = new EmployeeRepository(EmployeeDao);
            _employeeService = new EmployeeService(EmployeeRepository);
            _leaveRequestService = new LeaveRequestService(LeaveRequestRepository);
            InitializeComponent();
        }

        private async void Window_Loaded(object sender, RoutedEventArgs e)
        {
            var employees = await _employeeService.GetAll();

            if (employees == null || !employees.Any())
            {
                MessageBox.Show("Không có nhân viên nào trong danh sách!", "Thông báo", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            cmbEmployee.ItemsSource = employees;
            cmbEmployee.DisplayMemberPath = "FullName"; // Hiển thị tên nhân viên
            cmbEmployee.SelectedValuePath = "EmployeeId"; // Lưu EmployeeId khi chọn
        }


        private async void SaveButton_Click(object sender, RoutedEventArgs e)
        {
            if (cmbEmployee.SelectedItem == null)
            {
                MessageBox.Show("Vui lòng chọn nhân viên!", "Lỗi", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            if (dpStartDate.SelectedDate == null || dpEndDate.SelectedDate == null)
            {
                MessageBox.Show("Vui lòng nhập đầy đủ thông tin!", "Lỗi", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            DateTime startDate = dpStartDate.SelectedDate.Value;
            DateTime endDate = dpEndDate.SelectedDate.Value;

            if (endDate < startDate)
            {
                MessageBox.Show("Ngày kết thúc phải lớn hơn hoặc bằng ngày bắt đầu!", "Lỗi", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            int employeeId = (int)cmbEmployee.SelectedValue;

            // Lấy danh sách lịch nghỉ của nhân viên
            var existingLeaveRequests = await _leaveRequestService.GetLeaveRequestByID(employeeId);

            // Kiểm tra nếu ngày nghỉ mới trùng với bất kỳ yêu cầu nào trước đó
            foreach (var request in existingLeaveRequests)
            {
                DateTime existingStart = request.StartDate.ToDateTime(TimeOnly.MinValue);
                DateTime existingEnd = request.EndDate.ToDateTime(TimeOnly.MaxValue);

                if (startDate <= existingEnd && endDate >= existingStart)
                {
                    MessageBox.Show($"Nhân viên đã có lịch nghỉ từ {existingStart:dd/MM/yyyy} đến {existingEnd:dd/MM/yyyy}.",
                                    "Lỗi", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }
            }

            // Nếu không có trùng lặp, thêm yêu cầu nghỉ mới
            var leaveRequest = new LeaveRequest
            {
                EmployeeId = employeeId,
                LeaveType = ((ComboBoxItem)cmbLeaveType.SelectedItem).Content.ToString(),
                StartDate = DateOnly.FromDateTime(startDate),
                EndDate = DateOnly.FromDateTime(endDate),
                TotalDays = (endDate - startDate).Days + 1,
                Reason = txtReason.Text,
                Status = "Chờ duyệt"
            };

            await _leaveRequestService.Add(leaveRequest);

            MessageBox.Show("Lưu thành công!", "Thông báo", MessageBoxButton.OK, MessageBoxImage.Information);

            LeaveRequestAdded?.Invoke(this, EventArgs.Empty);

            Close();
        }

    }
}
