using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Repositories.Interface;
using Repositories.Repository;
using WPF.Employee;

namespace WPF
{
    
    public partial class MainWindow : Window
    {
        private readonly IUserRepository _repository;


        public MainWindow()
        {
            _repository = new UserRepository();
            InitializeComponent();
        }

        private void Button_Login(object sender, RoutedEventArgs e)
        {
            try
            {
                string username = tbUsername.Text;
                string password = pbPassword.Password;

                if (string.IsNullOrEmpty(username) || string.IsNullOrEmpty(password))
                {
                    MessageBox.Show("Vui lòng nhập tên đăng nhập và mật khẩu");
                }
                else
                {
                    var user = _repository.GetUserByUserNameAndPassword(username, password);
                    switch (user.UserRole)
                    {
                        case "Admin":
                            new EmployeeDashboard().Show();
                            this.Close();
                            break;
                        case "User":
                            break;
                        case "Employee":
                            new EmployeeProfile(user).Show();
                            this.Close();
                            break;
                        default:
                            MessageBox.Show("Tài khoản chưa được cấp quyền");
                            break;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }
    }
}