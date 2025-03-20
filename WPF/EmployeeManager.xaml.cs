using BusinessObjects.Models;
using Microsoft.Win32;
using Repositories.Interface;
using Repositories.Repository;
using System.Globalization;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Input;
using System.Windows.Media.Imaging;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;
using System.Text.Json.Serialization;
using DataAssetObjects;
using Services.Service;
using WPF.Admin;


namespace WPF
{
    /// <summary>
    /// Interaction logic for EmployeeManager.xaml
    /// </summary>
    public partial class EmployeeManager : UserControl
    {
        private readonly IEmployeeRepository _employee;
        private readonly IDepartmentRepository _department;
        private readonly IPositionRepository _position;
        private readonly ActivityLoggerService loggerService;
        private readonly HrmanagementContext _context = new HrmanagementContext();
        private int _employeeId;

        public List<BusinessObjects.Models.Employee> Employees { get; set; }
        public List<Department> Departments { get; set; }
        public List<Position> Positions { get; set; }
        int useID = 0;

        public EmployeeManager(int _employeeId)
        {
            _employee = new EmployeeRepository();
            _department = new DepartmentReporsitory();
            _position = new PositionRepository();
            loggerService = new ActivityLoggerService(new ActivityLoggerReposirory(new ActivityLoggerDAO(_context)));
            this._employeeId = _employeeId;
            InitializeComponent();

            LoadData();
        }
        public EmployeeManager(User id)
        {
            _employee = new EmployeeRepository();
            _department = new DepartmentReporsitory();
            _position = new PositionRepository();
            loggerService = new ActivityLoggerService(new ActivityLoggerReposirory(new ActivityLoggerDAO(_context)));
            InitializeComponent();
            useID = id.UserId;
            LoadData();
        }

        private void Member_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            if (sender is ListView listView)
            {
                BusinessObjects.Models.Employee employee = listView.SelectedItem as BusinessObjects.Models.Employee;
                if (employee != null)
                {
                    tbId.Text = employee.EmployeeId.ToString();
                    tbName.Text = employee.FullName;

                    rbMale.IsChecked = employee.Gender.Equals("Nam");
                    rbFemale.IsChecked = employee.Gender.Equals("Nữ");

                    dpDate.Text = employee.DateOfBirth.ToString();
                    tbMail.Text = employee.Email;
                    tbphone.Text = employee.PhoneNumber;
                    tbAddress.Text = employee.Address;
                    cbDepartment.SelectedItem = cbDepartment.Items.Cast<Department>()
                        .FirstOrDefault(item => item.DepartmentId == employee.DepartmentId);

                    cbPosition.SelectedItem = cbPosition.Items.Cast<Position>()
                        .FirstOrDefault(item => item.PositionId == employee.PositionId);

                    tbSalary.Text = employee.Position?.BasicSalary.ToString() ?? "";

                    dpStartTime.Text = employee.StartDate.ToString();

                    if (!string.IsNullOrEmpty(employee.Avatar))
                    {
                        imgAvt.Source = new BitmapImage(new Uri(employee.Avatar, UriKind.RelativeOrAbsolute));
                    }

                    _employeeId = employee.EmployeeId;
                }
            }
        }

        private void btnAdd_Click(object sender, RoutedEventArgs e)
        {
            if (_employee.CheckEmailExisting(tbMail.Text))
            {
                MessageBox.Show("Email đã được sử dụng");
            }
            else
            {

                BusinessObjects.Models.Employee employee = GetEmployee();
                string password = pbPassword.Password;
                string username = tbUsername.Text;

                if (!string.IsNullOrEmpty(password) && !string.IsNullOrEmpty(username))
                {
                    string filePath = (imgAvt.Source as BitmapImage)?.UriSource?.LocalPath;

                    if (!string.IsNullOrEmpty(filePath) && System.IO.File.Exists(filePath))
                    {
                        string savePath = System.IO.Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Resource", "avatar");

                        if (!System.IO.Directory.Exists(savePath))
                        {
                            System.IO.Directory.CreateDirectory(savePath);
                        }

                        string newFileName = Guid.NewGuid().ToString() + System.IO.Path.GetExtension(filePath);
                        string newFilePath = System.IO.Path.Combine(savePath, newFileName);

                        System.IO.File.Copy(filePath, newFilePath, true);

                        employee.Avatar = System.IO.Path.Combine("Resource", "avatar", newFileName);

                        imgAvt.Source = new BitmapImage(new Uri(newFilePath, UriKind.Absolute));
                    }
                    else
                    {
                        employee.Avatar = "Resource/icon/user1.png";
                    }

                    _employee.AddEmployee(employee, password);

                    User user_Id = loggerService.GetById(employee.EmployeeId);

                    loggerService.LogActivity(user_Id.UserId, "Thêm", $"Thêm nhân viên {employee.FullName}");
                    LoadData();
                    MessageBox.Show("Thêm nhân viên thành công");
                }
                else
                {
                    MessageBox.Show("Chưa có tên đăng nhập và mật khẩu cho tài khoản của nhân viên");
                }
            }
        }



        private void btnUpdate_Click(object sender, RoutedEventArgs e)
        {
            int employeeId;
            if (int.TryParse(tbId.Text, out employeeId))
            {
                BusinessObjects.Models.Employee employee = GetEmployee();
                try
                {
                    employee.EmployeeId = int.Parse(tbId.Text);
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Chưa chọn nhân viên để cập nhập!");
                }

                string filePath = (imgAvt.Source as BitmapImage)?.UriSource?.LocalPath;

                if (!string.IsNullOrEmpty(filePath) && System.IO.File.Exists(filePath))
                {
                    string savePath = System.IO.Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Resource", "avatar");

                    if (!System.IO.Directory.Exists(savePath))
                    {
                        System.IO.Directory.CreateDirectory(savePath);
                    }

                    string newFileName = Guid.NewGuid().ToString() + System.IO.Path.GetExtension(filePath);
                    string newFilePath = System.IO.Path.Combine(savePath, newFileName);

                    System.IO.File.Copy(filePath, newFilePath, true);

                    employee.Avatar = System.IO.Path.Combine("Resource", "avatar", newFileName);

                    imgAvt.Source = new BitmapImage(new Uri(newFilePath, UriKind.Absolute));
                }
                _employee.UpdateEmployeeById(employee);

                User user_Id = loggerService.GetById(employee.EmployeeId);
                if (user_Id != null) { 
                    loggerService.LogActivity(user_Id.UserId, "Cập nhật", $"Cập nhật nhân viên {employee.FullName}");
                }
                LoadData();
                MessageBox.Show("Cập nhật nhân viên thành công");
            }
            else
            {
                MessageBox.Show("Chưa chọn nhân viên để cập nhập!");
            }
        }

        private void btnDelete_Click(object sender, RoutedEventArgs e)
        {
            int employeeId;
            if (int.TryParse(tbId.Text, out employeeId))
            {
                _employee.DeleteEmployee(employeeId);
                LoadData();
                User user_Id = loggerService.GetById(employeeId);
                if (user_Id != null)
                {
                    loggerService.LogActivity(user_Id.UserId, "Xoá ", $"Xoá nhân viên {user_Id.Username}");
                }
                MessageBox.Show("Xoá nhân viên thành công");
            }
            else
            {
                MessageBox.Show("Chưa chọn nhân viên để xoá!");
            }

        }

        private void btnAvatar_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog
            {
                Title = "Chọn ảnh",
                Filter = "Hình ảnh|*.jpg;*.jpeg;*.png",
                Multiselect = false
            };

            if (openFileDialog.ShowDialog() == true)
            {
                string filePath = openFileDialog.FileName;
                imgAvt.Source = new BitmapImage(new Uri(filePath));
            }
        }

        private BusinessObjects.Models.Employee GetEmployee()
        {
            BusinessObjects.Models.Employee employee = new BusinessObjects.Models.Employee
            {
                EmployeeCode = GenerateEmployeeCode(),
                FullName = tbName.Text,
                Gender = rbMale.IsChecked == true ? "Nam" : "Nữ",
                DateOfBirth = dpDate.SelectedDate.HasValue ? DateOnly.FromDateTime(dpDate.SelectedDate.Value) : default,
                Email = tbMail.Text,
                PhoneNumber = tbphone.Text,
                Address = tbAddress.Text,
                DepartmentId = (cbDepartment.SelectedItem as Department)?.DepartmentId ?? 0,
                PositionId = (cbPosition.SelectedItem as Position)?.PositionId ?? 0,
                StartDate = dpStartTime.SelectedDate.HasValue ? DateOnly.FromDateTime(dpStartTime.SelectedDate.Value) : default
            };
            return employee;
        }

        private void LoadData()
        {
            Employees = _employee.GetEmployees();
            Departments = _department.GetDepartments();
            Positions = _position.GetPositions();
            lvEmployees.ItemsSource = Employees;
            cbDepartment.ItemsSource = Departments;
            cbPosition.ItemsSource = Positions;
        }

        private string GenerateEmployeeCode()
        {
            return $"EMP{Guid.NewGuid().ToString("N").Substring(0, 6)}";
        }


        private void btnBackup_Click(object sender, RoutedEventArgs e)
        {
            if (lvEmployees.Items.Count == 0)
            {
                MessageBox.Show("Danh sách nhân viên trống!", "Thông báo", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            SaveFileDialog saveFileDialog = new SaveFileDialog
            {
                Filter = "JSON files (*.json)|*.json",
                Title = "Chọn nơi lưu tệp sao lưu"
            };

            if (saveFileDialog.ShowDialog() == true)
            {
                var employees = new List<BusinessObjects.Models.Employee>();
                foreach (var item in lvEmployees.Items)
                {
                    employees.Add((BusinessObjects.Models.Employee)item);
                }

                string jsonData = JsonSerializer.Serialize(employees, new JsonSerializerOptions
                {
                    WriteIndented = true,
                    ReferenceHandler = ReferenceHandler.Preserve,
                    Encoder = System.Text.Encodings.Web.JavaScriptEncoder.UnsafeRelaxedJsonEscaping // Giữ nguyên tiếng Việt

                });
                File.WriteAllText(saveFileDialog.FileName, jsonData);
                MessageBox.Show("Sao lưu thành công!", "Thông báo", MessageBoxButton.OK, MessageBoxImage.Information);
            }
        }

        private void btnRestore_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog
            {
                Filter = "JSON files (*.json)|*.json",
                Title = "Chọn tệp sao lưu"
            };

            if (openFileDialog.ShowDialog() == true)
            {
                try
                {
                    string jsonData = File.ReadAllText(openFileDialog.FileName);
                    var employees = JsonSerializer.Deserialize<List<BusinessObjects.Models.Employee>>(jsonData, new JsonSerializerOptions
                    {
                        ReferenceHandler = ReferenceHandler.Preserve
                    });

                    if (employees != null)
                    {
                        lvEmployees.ItemsSource = employees;
                        MessageBox.Show("Phục hồi thành công!", "Thông báo", MessageBoxButton.OK, MessageBoxImage.Information);
                    }
                    else
                    {
                        MessageBox.Show("Lỗi khi đọc dữ liệu!", "Lỗi", MessageBoxButton.OK, MessageBoxImage.Error);
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Lỗi: {ex.Message}", "Lỗi", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
        }

        private void btnTraLuong_Click(object sender, RoutedEventArgs e)
        {
            PayrollManager a = new PayrollManager(_employeeId);
            a.Show();
        }
    }
}
