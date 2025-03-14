using BusinessObjects.Models;
using DataAssetObjects;
using Repositories.Repository;
using Services;
using Services.Service;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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

namespace WPF
{
    /// <summary>
    /// Interaction logic for BaoCaoChamCong.xaml
    /// </summary>
    public partial class BaoCaoChamCong : Window
    {
        private readonly ILeaveRequestService _leaveRequestService;
        public ObservableCollection<LeaveSummary> leaveSummaryList { get; set; } = new ObservableCollection<LeaveSummary>();

        public BaoCaoChamCong()
        {
            var _context = new HrmanagementContext();
            var leaveRequestDAO = new LeaveRequestDAO(_context);
            var leaveRequestRepo = new LeaveRequestRepository(leaveRequestDAO);
            _leaveRequestService = new LeaveRequestService(leaveRequestRepo);
            InitializeComponent();
        }

        private void GenerateReport_Click(object sender, RoutedEventArgs e)
        {
            int employeeId = 2;

            // Lấy giá trị tháng từ ComboBox
            int month = int.Parse((MonthComboBox.SelectedItem as ComboBoxItem)?.Content.ToString() ?? "0");
            int year = int.Parse(YearTextBox.Text);

            // Xác định tham số employeeId để truyền vào GetLeaveSummary
            int queryEmployeeId = (employeeId == 1) ? -1 : employeeId;

            var leaveSummaryList = _leaveRequestService.GetLeaveSummary(queryEmployeeId, month, year);
            ReportDataGrid.ItemsSource = leaveSummaryList;
        }


        private void Back_Click(object sender, RoutedEventArgs e)
        {
            EmployeeDashboard employeeDashboard = new EmployeeDashboard();
            employeeDashboard.Show();
            this.Close(); // Đóng cửa sổ hiện tại, quay về màn hình trước
        }
    }
}
