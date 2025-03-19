using DataAssetObjects;
using iTextSharp.text.pdf;
using iTextSharp.text;
using LiveCharts;
using LiveCharts.Wpf;
using Microsoft.Win32;
using Repositories.Repository;
using Services.InterfaceServie;
using Services.Service;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Windows;
using Org.BouncyCastle.Ocsp;

namespace WPF
{
	public partial class BaoCaoNhanVien : Window
	{
		private readonly IEmployeeService employeeService;

		private int empID;

        public ChartValues<int> DepartmentReportValues { get; set; }
		public List<string> DepartmentLabels { get; set; }

		public ChartValues<int> MaleCount { get; set; }
		public ChartValues<int> FemaleCount { get; set; }

		public BaoCaoNhanVien(int empID)
		{
			InitializeComponent();
			var _context = new HrmanagementContext();
			var employeeDAO = new EmployeeDAO(_context);
			var employeeRepo = new EmployeeRepository(employeeDAO);
			employeeService = new EmployeeService(employeeRepo);

			// Khởi tạo biểu đồ
			DepartmentReportValues = new ChartValues<int>();
			DepartmentLabels = new List<string>();

			MaleCount = new ChartValues<int>();
			FemaleCount = new ChartValues<int>();

			DataContext = this; // Gán DataContext cho Window

			this.empID = empID;

			LoadData();
		}

		void LoadData()
		{
			// Thống kê nhân viên theo phòng ban
			var departmentData = employeeService.GetEmployeeCountByDepartment();
			DepartmentReportValues.Clear();
			DepartmentLabels.Clear();
			foreach (var item in departmentData)
			{
				DepartmentReportValues.Add(item.EmployeeCount);
				DepartmentLabels.Add(item.DepartmentName);
			}
			DepartmentChart.Update(); // Cập nhật biểu đồ

			// Thống kê nhân viên theo chức vụ (hiển thị trong bảng)
			PositionReportGrid.ItemsSource = employeeService.GetEmployeeCountByPosition();

			// Thống kê nhân viên theo giới tính
			var genderData = employeeService.GetEmployeeCountByGender();
			MaleCount.Clear();
			FemaleCount.Clear();
			foreach (var item in genderData)
			{
				if (item.Gender == "Nam")
					MaleCount.Add(item.EmployeeCount);
				else if (item.Gender == "Nữ")
					FemaleCount.Add(item.EmployeeCount);
			}
			GenderChart.Update(); // Cập nhật biểu đồ

		}

		private void Back_Click(object sender, RoutedEventArgs e)
		{
            EmployeeDashboard employeeDashboard = new EmployeeDashboard(empID);
            employeeDashboard.Show();
            this.Close(); // Đóng cửa sổ hiện tại, quay về màn hình trước
        }
	}
}
