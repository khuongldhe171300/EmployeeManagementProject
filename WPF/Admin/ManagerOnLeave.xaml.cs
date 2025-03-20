using BusinessObjects.Models;
using DataAssetObjects;
using DocumentFormat.OpenXml.Wordprocessing;
using Repositories.Repository;
using Services.InterfaceServie;
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

namespace WPF.Admin
{
    
    public partial class ManagerOnLeave : Window
    {

        private readonly LeaveRequestService _leaveRequestService;
        private readonly NotificationService _notificationService;
        private int empID;
        private readonly ActivityLoggerService _activityLoggerService;


        public ManagerOnLeave(int empID)
        {
            var _context = new HrmanagementContext();
            var leaveDAO = new LeaveRequestDAO(_context);
            var notificationDAO = new NotificationDAO(_context);
            var leaveRequestRepository = new LeaveRequestRepository(leaveDAO);
            var notificationRepository = new NotificationRepository(notificationDAO);
            var activityLogDao = new ActivityLoggerDAO(_context);
            var activityLoggerReposirory = new ActivityLoggerReposirory(activityLogDao);
            _leaveRequestService = new LeaveRequestService(leaveRequestRepository);
            _notificationService = new NotificationService(notificationRepository);
            _activityLoggerService = new ActivityLoggerService(activityLoggerReposirory);
            InitializeComponent();

            LoadData();
            this.empID = empID;
        }

        private async void LoadData()
        {
            try
            {
                IEnumerable<LeaveRequest> readers = await _leaveRequestService.GetAll();
                LeaveRequestListView.ItemsSource = readers; 
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi khi tải dữ liệu: {ex.Message}", "Lỗi", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private async void Search_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                string keyword = txtSearch.Text.Trim(); 
                if (string.IsNullOrEmpty(keyword))
                {
                    MessageBox.Show("Vui lòng nhập tên nhân viên để tìm kiếm.", "Thông báo", MessageBoxButton.OK, MessageBoxImage.Warning);
                    return;
                }

                
                IEnumerable<LeaveRequest> results = await _leaveRequestService.SearchByEmployeeName(keyword);

                if (results.Any())
                {
                    LeaveRequestListView.ItemsSource = results;
                }
                else
                {
                    MessageBox.Show("Không tìm thấy yêu cầu nghỉ phép nào cho nhân viên này.", "Thông báo", MessageBoxButton.OK, MessageBoxImage.Information);
                    LeaveRequestListView.ItemsSource = null;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi khi tìm kiếm: {ex.Message}", "Lỗi", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }


        private void Add_Click(object sender, RoutedEventArgs e)
        {
            AddLeaveRequest addLeaveRequest = new AddLeaveRequest();

           
            addLeaveRequest.LeaveRequestAdded += (s, ev) =>
            {
                LoadData(); 
            };

            addLeaveRequest.ShowDialog(); 
        }


        private LeaveRequest selectedLeaveRequest;
        private void LeaveRequestListView_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (LeaveRequestListView.SelectedItem is LeaveRequest request)
            {
                selectedLeaveRequest = request;
            }
        }

        private void Delete_Click(object sender, RoutedEventArgs e)
        {
            if (selectedLeaveRequest == null)
            {
                MessageBox.Show("Vui lòng chọn yêu cầu nghỉ cần xử lý!", "Lỗi", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            selectedLeaveRequest.Status = "Từ chối";


            _leaveRequestService.Update(selectedLeaveRequest);
            
            var notification = new Notification
            {
                ReceiverId = selectedLeaveRequest.EmployeeId,
                Title = "Thông báo nghỉ phép",
                Content = $"Yêu cầu nghỉ phép từ {selectedLeaveRequest.StartDate:dd/MM/yyyy} đến {selectedLeaveRequest.EndDate:dd/MM/yyyy} đã bị từ chối.",
                NotificationType = "LeaveApproval",
                IsRead = false,
                CreatedDate = DateTime.Now
            };

            _notificationService.Add(notification);

          

            MessageBox.Show("Yêu cầu nghỉ phép đã bị từ chối!", "Thông báo", MessageBoxButton.OK, MessageBoxImage.Information);
            LoadData();

        }

        private async void Approve_Click(object sender, RoutedEventArgs e)
        {
            if (selectedLeaveRequest == null)
            {
                MessageBox.Show("Vui lòng chọn yêu cầu nghỉ cần duyệt!", "Lỗi", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            selectedLeaveRequest.Status = "Đã duyệt";

            
            await _leaveRequestService.Update(selectedLeaveRequest);
            // Tạo thông báo mới
            var notification = new Notification
            {
                ReceiverId = selectedLeaveRequest.EmployeeId,
                Title = "thông báo nghỉ phép",
                Content = $"Yêu cầu nghỉ phép từ {selectedLeaveRequest.StartDate:dd/MM/yyyy} đến {selectedLeaveRequest.EndDate:dd/MM/yyyy} đã được duyệt.",
                NotificationType = "LeaveApproval",
                IsRead = false,
                CreatedDate = DateTime.Now
            };

            await _notificationService.Add(notification);


            MessageBox.Show("Yêu cầu nghỉ phép đã được duyệt!", "Thông báo", MessageBoxButton.OK, MessageBoxImage.Information);
            LoadData();

 
            
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            EmployeeDashboard employee = new EmployeeDashboard(empID);
            employee.Show();
            this.Close();
        }
    }
}
