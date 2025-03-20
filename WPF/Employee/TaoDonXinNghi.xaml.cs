using BusinessObjects.Models;
using DataAssetObjects;
using Repositories.Repository;
using Services.Service;
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

namespace WPF.Employee
{
    /// <summary>
    /// Interaction logic for TaoDonXinNghi.xaml
    /// </summary>
    public partial class TaoDonXinNghi : Window
    {
        private readonly HrmanagementContext _context = new HrmanagementContext();
        private readonly LeaveRequestService _leaveRequestService;
        private readonly EmployeeService _employeeService;
        public event EventHandler LeaveRequestAdded;
        private readonly ActivityLoggerService _activityLoggerService;
        int id = 0;

        public TaoDonXinNghi(int emp)
        {
            var LeaveRequestDAO = new LeaveRequestDAO(_context);
            var LeaveRequestRepository = new LeaveRequestRepository(LeaveRequestDAO);
            var EmployeeDao = new EmployeeDAO(_context);
            var EmployeeRepository = new EmployeeRepository(EmployeeDao);
            var ActivityDao = new ActivityLoggerDAO(_context);
            _employeeService = new EmployeeService(EmployeeRepository);
            _leaveRequestService = new LeaveRequestService(LeaveRequestRepository);
            _activityLoggerService = new ActivityLoggerService(new ActivityLoggerReposirory(ActivityDao));
            id = emp;
            InitializeComponent();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)

        {
            var employees = _employeeService.GetEmployeeByID(id);

            if (employees == null)
            {
                MessageBox.Show("Không tìm thấy nhân viên!", "Thông báo", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            txtName.Text = employees.FullName;

        }
          
        


        private async void SaveButton_Click(object sender, RoutedEventArgs e)
        {
            
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

          

            // Lấy danh sách lịch nghỉ của nhân viên
            var existingLeaveRequests = await _leaveRequestService.GetLeaveRequestByID(id);

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
                EmployeeId = id,
                LeaveType = ((ComboBoxItem)cmbLeaveType.SelectedItem).Content.ToString(),
                StartDate = DateOnly.FromDateTime(startDate),
                EndDate = DateOnly.FromDateTime(endDate),
                TotalDays = (endDate - startDate).Days + 1,
                Reason = txtReason.Text,
                Status = "Chờ duyệt"
            };

            await _leaveRequestService.Add(leaveRequest);

            User user = _activityLoggerService.GetById(leaveRequest.EmployeeId);
            if (user != null)
            {
                _activityLoggerService.LogActivity(user.UserId, "Thêm yêu cầu nghỉ phép", "Thêm yêu cầu nghỉ phép từ " + startDate.ToString("dd/MM/yyyy") + " đến " + endDate.ToString("dd/MM/yyyy"));
            }

            MessageBox.Show("Lưu thành công!", "Thông báo", MessageBoxButton.OK, MessageBoxImage.Information);

            LeaveRequestAdded?.Invoke(this, EventArgs.Empty);

            Close();
        }

    }
}
