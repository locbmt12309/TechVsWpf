using System;
using System.ComponentModel;
using System.Windows.Input;
using WpfApp1.Models;

namespace WpfApp1.ViewModels
{
    public class LoginViewModel : INotifyPropertyChanged
    {
        private string? _username;
        private string? _password;

        public event PropertyChangedEventHandler? PropertyChanged;

        private readonly UserModel _userModel;

        public LoginViewModel()
        {
            _userModel = new UserModel();
            _username = string.Empty;
            _password = string.Empty;
        }

        public string? Username 
        {
            get => _username;
            set
            {
                _username = value;
                OnPropertyChanged(nameof(Username));
            }
        }

        public string? Password
        {
            get => _password;
            set
            {
                _password = value;
                OnPropertyChanged(nameof(Password));
            }
        }

        public bool Login(string username, string password)
        {
            var result= _userModel.ValidateUser(username, password);
            return result;
        }

        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
