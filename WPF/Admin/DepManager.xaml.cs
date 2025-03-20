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
    /// Interaction logic for DepManager.xaml
    /// </summary>
    public partial class DepManager : Window
    {
        private readonly DepartmentService departmentService;
        private int empID;
        public DepManager(int empID)
        {
            InitializeComponent();
            var context = new HrmanagementContext();
            var departmentDAO = new DepartmentDAO(context);
            var departmentRepository = new DepartmentRepository(departmentDAO);
            departmentService = new DepartmentService(departmentRepository);
            loadDepartments();
            this.empID = empID;
        }
        private void loadDepartments()
        {
            try
            {
                List<Department> departments = departmentService.GetDepartments();
                DepartmentListView.ItemsSource = departments;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi khi tải danh sách: {ex.Message}", "Lỗi", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void AddButton_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(NameTextBox.Text) ||
                string.IsNullOrWhiteSpace(DescriptionTextBox.Text))
            {
                MessageBox.Show("Tên phòng ban không được để trống", "Lỗi", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            if (!System.Text.RegularExpressions.Regex.IsMatch(NameTextBox.Text, @"^[a-zA-ZÀ-ỹ\s]+$"))
            {
                MessageBox.Show("Tên Phòng Ban chỉ được chứa chữ cái và khoảng trắng!", "Lỗi", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            if (!System.Text.RegularExpressions.Regex.IsMatch(DescriptionTextBox.Text, @"^[a-zA-ZÀ-ỹ\s]+$"))
            {
                MessageBox.Show("Mô Tả chỉ được chứa chữ cái và khoảng trắng!", "Lỗi", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            var existDepartment = departmentService.GetDepByName(NameTextBox.Text);
            if (existDepartment != null)
            {
                MessageBox.Show("Phòng ban đã tồn tại", "Lỗi", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            try
            {
                var newDepartment = new Department()
                {
                    DepartmentName = NameTextBox.Text.Trim(),
                    Description = DescriptionTextBox.Text.Trim(),
                    Status = StatusBox.Text.Trim() == "Hoạt Động" ? true : false
                };
                departmentService.AddDepartment(newDepartment);
                MessageBox.Show("Thêm thành công!", "Thông báo", MessageBoxButton.OK, MessageBoxImage.Information);
                loadDepartments();
                resetFields();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Có lỗi xảy ra: {ex.InnerException?.Message ?? ex.Message}", "Lỗi", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void EditButton_Click(object sender, RoutedEventArgs e)
        {
            int DepID = 0;
            if (DepartmentListView.SelectedItem is Department seletedDepartment && seletedDepartment.DepartmentId >0)
            {
                DepID = seletedDepartment.DepartmentId;
            }
            else
            {
                MessageBox.Show("Vui lòng chọn sách để chỉnh sửa!", "Thông báo", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }
            if (string.IsNullOrWhiteSpace(NameTextBox.Text) ||
                string.IsNullOrWhiteSpace(DescriptionTextBox.Text))
            {
                MessageBox.Show("Tên phòng ban không được để trống", "Lỗi", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            if (!System.Text.RegularExpressions.Regex.IsMatch(NameTextBox.Text, @"^[a-zA-ZÀ-ỹ\s]+$"))
            {
                MessageBox.Show("Tên Phòng Ban chỉ được chứa chữ cái và khoảng trắng!", "Lỗi", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            if (!System.Text.RegularExpressions.Regex.IsMatch(DescriptionTextBox.Text, @"^[a-zA-ZÀ-ỹ\s]+$"))
            {
                MessageBox.Show("Mô Tả chỉ được chứa chữ cái và khoảng trắng!", "Lỗi", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            var existDepartment = departmentService.GetDepByName(NameTextBox.Text);
            if (existDepartment != null && existDepartment.DepartmentId != DepID)
            {
                MessageBox.Show("Phòng ban đã tồn tại", "Lỗi", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            var department = departmentService.GetDepartmentByID(DepID);
            if (department == null)
            {
                MessageBox.Show("Phòng ban không tồn tại", "Lỗi", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            try
            {
                department.DepartmentName = NameTextBox.Text;
                department.Description = DescriptionTextBox.Text;
                department.Status = StatusBox.Text.Trim() == "Hoạt Động" ? true : false;
                departmentService.UpdateDepartment(department);
                MessageBox.Show("Cập nhật thành công!", "Thông báo", MessageBoxButton.OK, MessageBoxImage.Information);
                loadDepartments();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Có lỗi xảy ra: {ex.InnerException?.Message ?? ex.Message}", "Lỗi", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            }

        private void DeleteButton_Click(object sender, RoutedEventArgs e)
        {
            if(DepartmentListView.SelectedItem is Department seletedDepartment && seletedDepartment.DepartmentId > 0)
            {
                MessageBoxResult result = MessageBox.Show(
                    "Bạn có chắc chắn muốn xóa phòng ban này không?",
                    "Xác nhận xóa",
                    MessageBoxButton.YesNo,
                    MessageBoxImage.Warning);
                if (result == MessageBoxResult.Yes)
                {
                    try
                    {
                        departmentService.DeleteDepartment(seletedDepartment.DepartmentId);
                        MessageBox.Show("Xóa thành công!", "Thông báo", MessageBoxButton.OK, MessageBoxImage.Information);
                        loadDepartments();
                        resetFields();
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show($"Có lỗi xảy ra: {ex.InnerException?.Message ?? ex.Message}", "Lỗi", MessageBoxButton.OK, MessageBoxImage.Error);
                    }
                }
            }
            else
            {
                MessageBox.Show("Vui lòng chọn phòng ban để xóa!", "Thông báo", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }
        private void resetFields()
        {
            NameTextBox.Clear();
            DescriptionTextBox.Clear();
            StatusBox.SelectedIndex = 0;
        }

        private void SearchButton_Click(object sender, RoutedEventArgs e)
        {
            string searchValue = SearchTextBox.Text.ToLower();
            if (string.IsNullOrWhiteSpace(searchValue))
            {
                loadDepartments();
                return;
            }
            var filterDep = departmentService.GetDepartments()
                                             .Where(d => d.DepartmentName.ToLower().Contains(searchValue))
                                             .ToList();
            DepartmentListView.ItemsSource = filterDep;
            if(!filterDep.Any())
            {
                MessageBox.Show("Không tìm thấy phòng ban nào!", "Thông báo", MessageBoxButton.OK, MessageBoxImage.Information);
            }
        }
        private void DepartmentListView_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (DepartmentListView.SelectedItem is Department seletedDepartment)
            {
                NameTextBox.Text = seletedDepartment.DepartmentName;
                DescriptionTextBox.Text = seletedDepartment.Description;
                if (seletedDepartment.Status == true)
                {
                    StatusBox.Text = "Hoạt Động";
                }
                else
                {
                    StatusBox.Text = "Không Hoạt Động";
                }
            }
        }

        private void EmpNameTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            var keyword = EmpNameTextBox.Text;
            if (!string.IsNullOrWhiteSpace(keyword))
            {
                var emp = departmentService.GetAllEmp();
                var suggestions = emp.Where(r=>r.FullName.Contains(keyword, StringComparison.OrdinalIgnoreCase)).ToList();
                EmpSuggestionList.ItemsSource = suggestions;
                EmpSuggestionList.DisplayMemberPath = "FullName";
                EmpSuggestionList.Visibility = suggestions.Any() ? Visibility.Visible : Visibility.Collapsed;
            }
            else
            {
                EmpSuggestionList.Visibility = Visibility.Collapsed;
            }
        }

        private void EmpSuggestionList_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (EmpSuggestionList.SelectedItem is BusinessObjects.Models.Employee selectedEmp)
            {
                EmpNameTextBox.Text = selectedEmp.FullName;
                EmpNameTextBox.Tag = selectedEmp.EmployeeId;
                EmpSuggestionList.Visibility = Visibility.Collapsed;
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            EmployeeDashboard employeeDashboard = new EmployeeDashboard(empID);
            employeeDashboard.Show();
            this.Close();
        }

        private void AddEmpButton_Click(object sender, RoutedEventArgs e)
        {
            if (DepartmentListView.SelectedItem is Department seletedDepartment && seletedDepartment.DepartmentId > 0)
            {
                if (EmpNameTextBox.Tag is int empID && empID > 0)
                {
                    try
                    {
                        departmentService.UpdateEmpDep(seletedDepartment.DepartmentId, empID);
                        MessageBox.Show("Thêm nhân viên thành công!", "Thông báo", MessageBoxButton.OK, MessageBoxImage.Information);
                        EmpNameTextBox.Clear();
                        EmpNameTextBox.Tag = null;
                        resetFields();
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show($"Có lỗi xảy ra: {ex.InnerException?.Message ?? ex.Message}", "Lỗi", MessageBoxButton.OK, MessageBoxImage.Error);
                    }
                }
                else
                {
                    MessageBox.Show("Vui lòng chọn nhân viên để thêm!", "Thông báo", MessageBoxButton.OK, MessageBoxImage.Warning);
                    return;
                }
            }
            else
            {
                MessageBox.Show("Vui lòng chọn phòng ban để thêm nhân viên!", "Thông báo", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }
        }

        private void ViewEmpList_Click(object sender, RoutedEventArgs e)
        {
            if (DepartmentListView.SelectedItem is Department seletedDepartment && seletedDepartment.DepartmentId > 0)
            {
                EmpDepList empDepList = new EmpDepList(seletedDepartment, empID);
                empDepList.Show();
                this.Close();
            }
            else
            {
                MessageBox.Show("Vui lòng chọn phòng ban để xem danh sách nhân viên!", "Thông báo", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }
        }
    }
}
