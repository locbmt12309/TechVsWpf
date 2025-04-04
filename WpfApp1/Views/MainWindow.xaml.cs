using System;
using System.Windows;
using WpfApp1.ViewModels;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using System;
using System.Collections.Generic;
using System.Linq;
using MaterialDesignThemes.Wpf;
using System.IO;


namespace WpfApp1.Views
{
    public partial class MainWindow : Window
    {
        private LoginViewModel _viewModel;
        public MainWindow()
        {
            InitializeComponent();
            _viewModel = new LoginViewModel();
            this.DataContext = _viewModel;
        }
        private void LoginButton_Click(object sender, RoutedEventArgs e)
        {      
            string username = UserNameTextBox.Text.Trim();
            string password = PasswordTextBox.Password.Trim();
            
            if (_viewModel.Login(username, password))
            {
                ChangeDateTimeWindow changeDateTimeWindow = new ChangeDateTimeWindow(username);
                changeDateTimeWindow.Show();
                this.Close();
            }
            else
            {
                ErrorMessage.Text = "Tên đăng nhập hoặc mật khẩu không đúng!";
                ErrorMessage.Visibility = Visibility.Visible;
            }
        }
        private void Hyperlink_RequestNavigate(object sender, System.Windows.Navigation.RequestNavigateEventArgs e)
        {
            ChangePasswordWindow changePasswordWindow = new ChangePasswordWindow();
            changePasswordWindow.Show();
        }
    }
}
