using System.Windows;
using System.Windows.Controls;
using Repositories.Interface;
using Repositories.Repository;

namespace WPF
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
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
                            new EmployeeManager().Show();
                            this.Close();
                            break;
                        case "User":
                            break;
                        default:
                            MessageBox.Show("Tài khoản không có quyền");
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