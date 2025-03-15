using DataAccessLayer;
using DataAssetObjects;
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
	/// Interaction logic for BaoCaoNhanVien.xaml
	/// </summary>
	public partial class BaoCaoNhanVien : Window
	{
		private readonly IEmployeeService employeeService;

		public BaoCaoNhanVien()
		{
			InitializeComponent();
			var _context = new HrmanagementContext();
			var employeeDAO = new EmployeeDAO(_context);
			var employeeRepo = new EmployeeRepository(employeeDAO);
			employeeService = new EmployeeService(employeeRepo);
		}

		private void LoadReport_Click(object sender, RoutedEventArgs e)
		{
			DepartmentReportGrid.ItemsSource = employeeService.GetEmployeeCountByDepartment();
			PositionReportGrid.ItemsSource = employeeService.GetEmployeeCountByPosition();
			GenderReportGrid.ItemsSource = employeeService.GetEmployeeCountByGender();
		}
	}
}
