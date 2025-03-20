using BusinessObjects.Models;
using DataAssetObjects;
using Microsoft.EntityFrameworkCore;
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
    /// <summary>
    /// Interaction logic for TaoThongBao.xaml
    /// </summary>
    public partial class TaoThongBao : Window
    {

        private readonly EmployeeService employeeService;
        private readonly NotificationService notificationService;
        int id = 0;
        public TaoThongBao(int emp)
        {
            InitializeComponent();
            var context = new HrmanagementContext();
            var notificationDAO = new NotificationDAO(context);
            employeeService = new EmployeeService(new Repositories.Repository.EmployeeRepository());
            notificationService = new NotificationService(new Repositories.Repository.NotificationRepository(notificationDAO));
            id = emp;
            LoadEmployees();
        }

        private void SendNotification_Click(object sender, RoutedEventArgs e)
        {
            string title = txtTitle.Text;
            string content = txtContent.Text;
            string type = ((ComboBoxItem)cbNotificationType.SelectedItem).Content.ToString();

            if (string.IsNullOrWhiteSpace(title) || string.IsNullOrWhiteSpace(content))
            {
                MessageBox.Show("Vui lòng nhập tiêu đề và nội dung thông báo!", "Lỗi", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            var selectedReceiver = cbReceiver.SelectedItem;

            if (selectedReceiver is string && selectedReceiver.ToString() == "Tất cả nhân viên")
            {
                notificationService.SendNotificationToAllEmployee(title, content, type);
                MessageBox.Show("📢 Đã gửi thông báo cho tất cả nhân viên!", "Thành công", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            else
            {
                int receiverId = ((dynamic)selectedReceiver).Id;
                notificationService.SendNotificationToEmployee(receiverId, title, content, type);
                MessageBox.Show("✅ Thông báo đã được gửi!", "Thành công", MessageBoxButton.OK, MessageBoxImage.Information);
            }
        
    }

        private void LoadEmployees()
        {
          
            
                var employees = employeeService.GetEmployees();
                cbReceiver.Items.Add("Tất cả nhân viên"); 

                foreach (var emp in employees)
                {
                    cbReceiver.Items.Add(new {Id = emp.EmployeeId, Name = emp.FullName});
                }

                cbReceiver.SelectedIndex = 0; 
            
        }
    }
}
