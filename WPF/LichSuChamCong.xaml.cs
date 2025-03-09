using BusinessObjects.Models;
using DataAssetObjects;
using Repositories.Interface;
using Repositories.Repository;
using Services;
using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows;
using System.Windows.Input;

namespace WPF
{
    public partial class LichSuChamCong : Window, INotifyPropertyChanged
    {
        private readonly IAttendanceService _attendanceService;
        private ObservableCollection<Attendance> _attendanceList;

        public ObservableCollection<Attendance> AttendanceList
        {
            get { return _attendanceList; }
            set
            {
                _attendanceList = value;
                OnPropertyChanged(nameof(AttendanceList));
            }
        }

        public ICommand RefreshCommand { get; }

        public LichSuChamCong()
        {
            InitializeComponent();
            DataContext = this; // Gán DataContext để XAML có thể binding dữ liệu

            var _context = new HrmanagementContext();
            var attendanceDAO = new AttendanceDAO(_context);
            var attendanceRepo = new AttendanceRepository(attendanceDAO);
            _attendanceService = new AttendanceService(attendanceRepo);

            AttendanceList = new ObservableCollection<Attendance>(); // Khởi tạo danh sách rỗng
            LoadData();
        }

        private void BackButton_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void LoadData()
        {
            int currentEmployeeId = GetCurrentEmployeeId();
            List<Attendance> attendances = _attendanceService.GetAttendanceByEmployeeId(currentEmployeeId);

            if (attendances != null)
            {
                AttendanceList.Clear();
                foreach (var item in attendances)
                {
                    AttendanceList.Add(item);
                }
            }
        }

        private int GetCurrentEmployeeId()
        {
            // Giả định App lưu ID nhân viên đăng nhập
            //return App.CurrentUserId; 
            return 2;
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged(string propertyName) =>
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));


        private void RefreshButton_Click(object sender, RoutedEventArgs e)
        {
            LoadData();
        }
    }
}
