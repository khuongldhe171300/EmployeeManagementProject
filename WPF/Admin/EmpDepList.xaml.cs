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
using Repositories.Repository;
using Services.Service;

namespace WPF.Admin
{
    /// <summary>
    /// Interaction logic for EmpDepList.xaml
    /// </summary>
    public partial class EmpDepList : Window
    {
        private readonly DepartmentService departmentService;
        private int departmentId =0;
        private int empID;
        public EmpDepList(Department seletedDepartment, int empID)
        {
            InitializeComponent();
            var context = new HrmanagementContext();
            var departmentDao = new DepartmentDAO(context);
            var departmentRepository = new DepartmentRepository(departmentDao);
            departmentService = new DepartmentService(departmentRepository);
            departmentId = seletedDepartment.DepartmentId;
            this.empID = empID;
            loadEmpDepList();
        }
        private void loadEmpDepList()
        {
            try
            {
                var empList = departmentService.GetEmpByDep(departmentId);
                EmployeeListView.ItemsSource = empList;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi khi tải danh sách: {ex.Message}", "Lỗi", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void DeleteButton_Click(object sender, RoutedEventArgs e)
        {
            MessageBoxResult result = MessageBox.Show(
                    "Bạn có chắc chắn muốn xóa nhân viên ra khỏi phòng ban này không?",
                    "Xác nhận xóa",
                    MessageBoxButton.YesNo,
                    MessageBoxImage.Warning);
            if (result == MessageBoxResult.Yes)
            {
                if (sender is Button deleteButton && deleteButton.Tag is int employeeId)
                {
                    try
                    {
                        departmentService.DeleteEmpDep(employeeId);
                        MessageBox.Show("Xóa nhân viên thành công!", "Thông báo", MessageBoxButton.OK, MessageBoxImage.Information);
                        loadEmpDepList();
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show($"Lỗi khi xóa nhân viên: {ex.Message}", "Lỗi", MessageBoxButton.OK, MessageBoxImage.Error);
                    }
                    }
                else
                {
                    MessageBox.Show("Không thể xác định nhân viên cần xóa", "Thông báo", MessageBoxButton.OK, MessageBoxImage.Information);
                    return;
                }
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            DepManager depManager = new DepManager(empID);
            depManager.Show();
            this.Close();
        }
    }
}
