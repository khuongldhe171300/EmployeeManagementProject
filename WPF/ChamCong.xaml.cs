using BusinessObjects.Models;
using DataAssetObjects;
using Repositories.Repository;
using Services;
using Services.InterfaceServie;
using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using System.Windows.Controls;

namespace WPF
{
    public partial class ChamCong : Window
    {
        private readonly IAttendanceService _attendanceService;
        private int currentEmployeeId; // Giả sử ID nhân viên đăng nhập là 1
        public ObservableCollection<Attendance> AttendanceList { get; set; } = new ObservableCollection<Attendance>();

        public ChamCong(int empID)
        {
            InitializeComponent();
			currentEmployeeId = empID;
			var _context = new HrmanagementContext();
            var attendanceDAO = new AttendanceDAO(_context);
            var attendanceRepo = new AttendanceRepository(attendanceDAO);
            _attendanceService = new AttendanceService(attendanceRepo);
            LoadAttendances();
        }

        private void CheckIn_Click(object sender, RoutedEventArgs e)
        {
            var today = DateOnly.FromDateTime(DateTime.Now);
            var attendance = AttendanceList.FirstOrDefault(a => a.EmployeeId == currentEmployeeId && a.WorkDate == today);

            if (attendance == null)
            {
                attendance = new Attendance
                {
                    EmployeeId = currentEmployeeId,
                    WorkDate = today,
                    CheckInTime = DateTime.Now,
                    WorkStatus = "Đang làm việc"
                };
                _attendanceService.AddAttendance(attendance);
            }
            else if (attendance.CheckInTime == null)
            {
                attendance.CheckInTime = DateTime.Now;
                attendance.WorkStatus = "Đang làm việc";
                _attendanceService.UpdateAttendance(attendance);
            }
            else
            {
                MessageBox.Show("Bạn đã chấm công vào rồi!", "Thông báo", MessageBoxButton.OK, MessageBoxImage.Information);
                return;
            }

            LoadAttendances();
        }

        private void CheckOut_Click(object sender, RoutedEventArgs e)
        {
            var today = DateOnly.FromDateTime(DateTime.Now);
            var attendance = AttendanceList.FirstOrDefault(a => a.EmployeeId == currentEmployeeId && a.WorkDate == today);

            if (attendance == null || attendance.CheckInTime == null)
            {
                MessageBox.Show("Bạn chưa chấm công vào!", "Lỗi", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            if (attendance.CheckOutTime == null)
            {
                // Hiển thị hộp thoại xác nhận
                var result = MessageBox.Show(
                    "Bạn có chắc muốn kết thúc ngày làm việc? Chỉ được chấm công ra 1 lần trong ngày!",
                    "Xác nhận chấm công ra",
                    MessageBoxButton.YesNo,
                    MessageBoxImage.Question
                );

                if (result == MessageBoxResult.Yes)
                {
                    attendance.CheckOutTime = DateTime.Now;
                    attendance.WorkHours = (decimal)(attendance.CheckOutTime.Value - attendance.CheckInTime.Value).TotalHours;
                    attendance.WorkStatus = "Đi làm";
                    _attendanceService.UpdateAttendance(attendance);
                }
            }
            else
            {
                MessageBox.Show("Bạn đã chấm công về rồi!", "Thông báo", MessageBoxButton.OK, MessageBoxImage.Information);
            }

            LoadAttendances();
        }


        private void Refresh_Click(object sender, RoutedEventArgs e)
        {
            LoadAttendances();
        }

        private void LoadAttendances()
        {
            try
            {
                AttendanceList.Clear();
                var attendances = _attendanceService.GetAttendanceByEmployeeId(currentEmployeeId);

                var today = DateOnly.FromDateTime(DateTime.Now);

                foreach (var attendance in attendances)
                {
                    // Nếu ngày trong quá khứ có CheckIn nhưng không có CheckOut, cập nhật trạng thái
                    if (attendance.WorkDate < today && attendance.CheckInTime != null && attendance.CheckOutTime == null)
                    {
                        attendance.WorkStatus = "Quên chấm công về";
                        _attendanceService.UpdateAttendance(attendance);
                    }

                    AttendanceList.Add(attendance);
                }

                AttendancesList.ItemsSource = AttendanceList;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi tải dữ liệu chấm công: " + ex.Message, "Lỗi", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }



        private void DataGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            // Có thể thêm xử lý khi chọn một hàng trong DataGrid
        }

        private void Back_Click(object sender, RoutedEventArgs e)
        {
            EmployeeDashboard_Huy employeeDashboard = new EmployeeDashboard_Huy(currentEmployeeId);
            employeeDashboard.Show();
            this.Close(); // Đóng cửa sổ hiện tại, quay về màn hình trước
        }
    }
}
