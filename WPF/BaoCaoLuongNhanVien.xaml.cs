using BusinessObjects.Models;
using ClosedXML.Excel;
using DataAssetObjects;
using LiveCharts;
using LiveCharts.Wpf;
using Microsoft.Win32;
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

namespace WPF
{
	/// <summary>
	/// Interaction logic for BaoCaoLuongNhanVien.xaml
	/// </summary>
	public partial class BaoCaoLuongNhanVien : Window
	{

		private readonly HrmanagementContext _context = new HrmanagementContext();
		private readonly IPayrollService payrollService;
		private List<Payroll> _payrollData;

		private int empID;

		public List<string> SalaryChartLabels { get; set; }
		public Func<double, string> YFormatter { get; set; }

		public ChartValues<double> SalaryChartValues { get; set; } = new ChartValues<double>();



		public BaoCaoLuongNhanVien(int empID)
		{
			InitializeComponent();
			var _context = new HrmanagementContext();
			var payrollDAO = new PayrollDAO(_context);
			var payrollRepo = new PayrollRepository(payrollDAO);
			payrollService = new PayrollService(payrollRepo);
			LoadYears();
			SalaryChartLabels = new List<string>();
			YFormatter = value => value.ToString("N0") + " VND";

			DataContext = this;
			this.empID = empID;
			LoadData();
		}
		private void LoadYears()
		{
			int currentYear = DateTime.Now.Year;
			for (int year = currentYear - 5; year <= currentYear; year++)
			{
				YearComboBox.Items.Add(new ComboBoxItem { Content = year.ToString() });
			}
			YearComboBox.SelectedIndex = 5; // Mặc định chọn năm hiện tại
		}

		private void LoadData()
		{
			if (empID == 1)
			{
				_payrollData = payrollService.GetAll();
			}
			else
			{
				_payrollData = payrollService.GetPayrollByEmp(empID);
			}
			FilterData();
		}

		private void FilterData()
		{

			if (_payrollData == null || !_payrollData.Any())
			{
				if (PayrollDataGrid != null) // Kiểm tra PayrollDataGrid trước khi gán
				{
					PayrollDataGrid.ItemsSource = null;
				}

				if (TotalSalaryText != null) // Kiểm tra TotalSalaryText trước khi gán
				{
					TotalSalaryText.Text = "Tổng lương: 0 VNĐ";
				}

				SalaryChartValues.Clear();
				return;
			}

			int selectedMonth = MonthComboBox.SelectedIndex;

			var filteredData = _payrollData
				.Where(p => selectedMonth == 0 || p.PayrollMonth == selectedMonth)
				.ToList();

			PayrollDataGrid.ItemsSource = filteredData;
			TotalSalaryText.Text = $"Tổng lương: {filteredData.Sum(p => p.TotalSalary):N0} VNĐ";

			// Cập nhật dữ liệu biểu đồ
			var groupedData = filteredData
				.GroupBy(p => p.PayrollMonth)
				.Select(g => new { Month = g.Key, TotalSalary = g.Sum(p => p.TotalSalary) })
				.OrderBy(g => g.Month)
				.ToList();

			SalaryChartValues.Clear();
			SalaryChartLabels.Clear();

			foreach (var item in groupedData)
			{
				SalaryChartValues.Add((double)item.TotalSalary); // Chuyển đổi từ decimal sang double
				SalaryChartLabels.Add($"Tháng {item.Month}");
			}

		}






		private void OnFilterChanged(object sender, SelectionChangedEventArgs e)
		{
			FilterData();
		}

		private void ExportReport_Click(object sender, RoutedEventArgs e)
		{
			if (PayrollDataGrid.ItemsSource == null || !PayrollDataGrid.Items.OfType<object>().Any())
			{
				MessageBox.Show("Không có dữ liệu để xuất!", "Thông báo", MessageBoxButton.OK, MessageBoxImage.Warning);
				return;
			}

			// Chọn nơi lưu file
			SaveFileDialog saveFileDialog = new SaveFileDialog
			{
				Filter = "Excel Files (*.xlsx)|*.xlsx",
				Title = "Lưu báo cáo",
				FileName = "BaoCaoLuong.xlsx"
			};

			if (saveFileDialog.ShowDialog() == true)
			{
				try
				{
					using (var workbook = new XLWorkbook())
					{
						var worksheet = workbook.Worksheets.Add("Báo cáo lương");
						var currentRow = 1;

						// Thêm tiêu đề cột
						worksheet.Cell(currentRow, 1).Value = "Mã NV";
						worksheet.Cell(currentRow, 2).Value = "Tháng";
						worksheet.Cell(currentRow, 3).Value = "Lương cơ bản";
						worksheet.Cell(currentRow, 4).Value = "Phụ cấp";
						worksheet.Cell(currentRow, 5).Value = "Thưởng";
						worksheet.Cell(currentRow, 6).Value = "Tổng lương";
						worksheet.Cell(currentRow, 7).Value = "Ngày thanh toán";
						worksheet.Cell(currentRow, 8).Value = "Trạng thái";

						// Định dạng tiêu đề
						worksheet.Range(1, 1, 1, 8).Style.Font.Bold = true;
						worksheet.Range(1, 1, 1, 8).Style.Fill.BackgroundColor = XLColor.LightGray;
						worksheet.Columns().AdjustToContents(); // Tự động căn chỉnh cột

						// Thêm dữ liệu từ DataGrid
						foreach (var item in PayrollDataGrid.Items)
						{
							if (item is Payroll payroll)
							{
								currentRow++;
								worksheet.Cell(currentRow, 1).Value = payroll.EmployeeId;
								worksheet.Cell(currentRow, 2).Value = payroll.PayrollMonth;
								worksheet.Cell(currentRow, 3).Value = payroll.BasicSalary;
								worksheet.Cell(currentRow, 4).Value = payroll.Allowance;
								worksheet.Cell(currentRow, 5).Value = payroll.Bonus;
								worksheet.Cell(currentRow, 6).Value = payroll.TotalSalary;
								worksheet.Cell(currentRow, 7).Value = payroll.PaymentDate?.ToString("yyyy-MM-dd") ?? "N/A";
								worksheet.Cell(currentRow, 8).Value = payroll.PaymentStatus;
							}
						}

						// Lưu file Excel
						workbook.SaveAs(saveFileDialog.FileName);
					}

					MessageBox.Show("Xuất báo cáo thành công!", "Thông báo", MessageBoxButton.OK, MessageBoxImage.Information);
				}
				catch (Exception ex)
				{
					MessageBox.Show($"Lỗi khi xuất báo cáo: {ex.Message}", "Lỗi", MessageBoxButton.OK, MessageBoxImage.Error);
				}
			}
		}

		private void Back_Click(object sender, RoutedEventArgs e)
		{
            if (empID == 1)
            {
                EmployeeDashboard employeeDashboard = new EmployeeDashboard(empID);
                employeeDashboard.Show();
                this.Close(); // Đóng cửa sổ hiện tại, quay về màn hình trước
            }
            else
            {
                EmployeeDashboard_Huy employeeDashboard = new EmployeeDashboard_Huy(empID);
                employeeDashboard.Show();
                this.Close(); // Đóng cửa sổ hiện tại, quay về màn hình trước
            }
        }
	}
}





