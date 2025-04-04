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
using WpfApp1.ViewModels;

namespace WpfApp1.Views
{
    /// <summary>
    /// Interaction logic for RegisterUser.xaml
    /// </summary>
    public partial class RegisterUser : Window
    {
        private UserViewModel _viewModel;

        public RegisterUser()
        {
            InitializeComponent();
            _viewModel = new UserViewModel();
            this.DataContext = _viewModel;
        }

        private void RegisterButton_Click(object sender, RoutedEventArgs e)
        {
            string fullName = FullNameTextBox.Text.Trim();
            string username = UserNameTextBox.Text.Trim();
            string password = PasswordTextBox.Password.Trim();
            string confirmPassword = ConfirmPasswordTextBox.Password.Trim();

            // Kiểm tra mật khẩu xác nhận
            if (password != confirmPassword)
            {
                ErrorMessage.Text = "Mật khẩu và xác nhận mật khẩu không khớp!";
                ErrorMessage.Visibility = Visibility.Visible;
                return;
            }

            // Kiểm tra người dùng đã tồn tại
            if (_viewModel.RegisterUser(fullName, username, password))
            {
                MessageBox.Show("Đăng ký thành công!");
                // Chuyển về trang đăng nhập sau khi đăng ký thành công
                MainWindow loginWindow = new MainWindow();
                loginWindow.Show();
                this.Close();
            }
            else
            {
                ErrorMessage.Text = "Tên đăng nhập đã tồn tại!";
                ErrorMessage.Visibility = Visibility.Visible;
            }
        }

        private void LoginPage_RequestNavigate(object sender, System.Windows.Navigation.RequestNavigateEventArgs e)
        {
            MainWindow loginWindow = new MainWindow();
            loginWindow.Show();
            this.Close();
        }
    }

}
