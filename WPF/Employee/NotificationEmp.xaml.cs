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
using DocumentFormat.OpenXml.EMMA;
using Repositories.Repository;
using Services.Service;

namespace WPF.Employee
{
    /// <summary>
    /// Interaction logic for NotificationEmp.xaml
    /// </summary>
    public partial class NotificationEmp : Window
    {
        private readonly NotificationService notificationService;
        private readonly HrmanagementContext context = new HrmanagementContext();
        int empID = 0;
        public NotificationEmp(int empID)
        {
            InitializeComponent();
            var notificationDao = new NotificationDAO(context);
            var notificationRepository = new NotificationRepository(notificationDao);
            notificationService = new NotificationService(notificationRepository);
            this.empID = empID;
            loadData();
        }
        private async void loadData()
        {
            try
            {
                var notificationList = await notificationService.GetById2(empID);
                NotificationListView.ItemsSource = (System.Collections.IEnumerable)notificationList;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi khi tải thông báo: {ex.Message}", "Lỗi", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void DepartmentListView_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (NotificationListView.SelectedItem is Notification selectedEmp)
            {
                selectedEmp.IsRead = true;
                notificationService.Update(selectedEmp);
                loadData();
            }
        }
    }
}
