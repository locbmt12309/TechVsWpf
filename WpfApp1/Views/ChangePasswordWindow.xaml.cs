using System;
using System.Windows;
using WpfApp1.Models;
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
namespace WpfApp1.Views
{
    public partial class ChangePasswordWindow : Window
    {
        private readonly UserModel _userModel;

        public ChangePasswordWindow()
        {
            InitializeComponent();
            _userModel = new UserModel();
        }

        private void SaveButton_Click(object sender, RoutedEventArgs e)
        {
            string username = UserNameTextBox.Text;
            string oldPassword = OldPasswordBox.Password;
            string newPassword = NewPasswordBox.Password;
            string confirmNewPassword = ConfirmNewPasswordBox.Password;

            if (newPassword != confirmNewPassword)
            {
                MessageBox.Show("Mật khẩu mới không khớp!");
                return;
            }

            bool isPasswordChanged = _userModel.ChangePassword(username, oldPassword, newPassword);

            if (isPasswordChanged)
            {
                MessageBox.Show("Mật khẩu đã được thay đổi thành công!");
                this.Close();
            }
            else
            {
                MessageBox.Show("Tên đăng nhập hoặc mật khẩu cũ không đúng!");
            }
        }
    }
}
