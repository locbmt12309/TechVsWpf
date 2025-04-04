using System;
using WpfApp1.Models;

namespace WpfApp1.ViewModels
{
    public class UserViewModel
    {
        private UserModel _userModel;

        public UserViewModel()
        {
            _userModel = new UserModel();
        }

        public bool RegisterUser(string fullName, string username, string password)
        {
            // Kiểm tra xem người dùng đã tồn tại chưa
            if (_userModel.UserExists(username))
            {
                return false; // Người dùng đã tồn tại
            }

            // Đăng ký người dùng mới
            return _userModel.RegisterNewUser(fullName, username, password);
        }
    }
}
