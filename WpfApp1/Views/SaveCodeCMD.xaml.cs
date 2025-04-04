using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Security.Principal;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using System.ComponentModel;

namespace WpfApp1.Views
{
    public partial class SaveCodeCMD : Window
    {
        private const string KeyPath = @"SOFTWARE\Microsoft\Windows\CurrentVersion\Policies\System";
        private const string ValueName = "DisableClockChange";

        private List<ServiceModel> services;

        public SaveCodeCMD()
        {
            InitializeComponent();
            SetUserName("admin");
        }

        public SaveCodeCMD(string username)
        {
            InitializeComponent();
            SetUserName(username);

            bool isDisabled = GetDateTimeChangeStatus();
            bool isCmdDisabled = GetCmdChangeStatus();

            services = new List<ServiceModel>
            {
                new ServiceModel(ToggleDateTimeChange, isDisabled ? "Off" : "On",
                    isDisabled ? new SolidColorBrush(Colors.Red) : new SolidColorBrush(Colors.Purple))
                {
                    STT = 1,
                    ServiceName = "Chỉnh ngày giờ"
                },
                new ServiceModel(ToggleCmdChange, isCmdDisabled ? "Off" : "On",
                isCmdDisabled ? new SolidColorBrush(Colors.Red) : new SolidColorBrush(Colors.Purple))
                {
                    STT = 2,
                    ServiceName = "CMD"
                }
            };

            FunctionDataGrid.ItemsSource = services;
        }
        private bool GetCmdChangeStatus()
        {
            string keyPath = @"SOFTWARE\Microsoft\Windows\CurrentVersion\Policies\System";
            string valueName = "DisableCMD";

            using (RegistryKey key = Registry.LocalMachine.OpenSubKey(keyPath, false))
            {
                if (key != null)
                {
                    object currentValue = key.GetValue(valueName, 0);
                    return (int)currentValue == 1;
                }
            }
            return false;
        }

        private void SetUserName(string userName)
        {
            UserNameTextBlock.Text = userName;
        }
        private bool GetDateTimeChangeStatus()
        {
            using (RegistryKey key = Registry.LocalMachine.OpenSubKey(KeyPath, false))
            {
                if (key != null)
                {
                    object currentValue = key.GetValue(ValueName, 0);
                    return (int)currentValue == 1;
                }
            }
            return false;
        }

        private void ToggleDateTimeChange()
        {
            if (services == null || services.Count == 0) return;

            var service = services[0];
            service.IsChecked = !service.IsChecked;

            service.Status = service.IsChecked ? "On" : "Off";
            service.ButtonColor = service.IsChecked ? new SolidColorBrush(Colors.Purple) : new SolidColorBrush(Colors.Red);

            service.IsPendingSave = true;
            FunctionDataGrid.Items.Refresh();
        }
        private void ToggleCmdChange()
        {
            if (services == null || services.Count < 2) return;

            var service = services[1];
            service.IsChecked = !service.IsChecked;

            service.Status = service.IsChecked ? "On" : "Off";
            service.ButtonColor = service.IsChecked ? new SolidColorBrush(Colors.Purple) : new SolidColorBrush(Colors.Red);

            service.IsPendingSave = true;
            FunctionDataGrid.Items.Refresh();
        }


        private void LogoutButton_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("You have logged out.");
            MainWindow mainWindow = new MainWindow();
            mainWindow.Show();
            this.Close();
        }
        private void UserNameTextBlock_Click(object sender, MouseButtonEventArgs e)
        {
            LogoutButton.Visibility = Visibility.Visible;
        }
        private void SaveButton_Click(object sender, RoutedEventArgs e)
        {
            if (!IsUserAdministrator())
            {
                MessageBox.Show("Please run this application as Administrator.", "Permission Denied", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            foreach (var service in services)
            {
                if (service.IsPendingSave)
                {
                    try
                    {
                        using (RegistryKey key = Registry.LocalMachine.OpenSubKey(KeyPath, true))
                        {
                            if (key == null)
                            {
                                MessageBox.Show("Registry key not found!", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                                return;
                            }

                            int newValue = service.IsChecked ? 0 : 1;
                            string valueName = service.ServiceName == "Chỉnh ngày giờ" ? "DisableClockChange" : "DisableCMD";

                            key.SetValue(valueName, newValue, RegistryValueKind.DWord);

                            string message = (newValue == 1) ? $"{service.ServiceName} has been disabled." : $"{service.ServiceName} has been enabled.";
                            MessageBox.Show(message, "Success", MessageBoxButton.OK, MessageBoxImage.Information);
                        }

                        service.IsPendingSave = false;
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show($"Error: {ex.Message}", "Registry Update Failed", MessageBoxButton.OK, MessageBoxImage.Error);
                    }
                }
            }
        }



        private bool IsUserAdministrator()
        {
            using (WindowsIdentity identity = WindowsIdentity.GetCurrent())
            {
                WindowsPrincipal principal = new WindowsPrincipal(identity);
                return principal.IsInRole(WindowsBuiltInRole.Administrator);
            }
        }

        public class ServiceModel
        {
            public int STT { get; set; }
            public string ServiceName { get; set; }
            public string Status { get; set; } = "Off";
            public SolidColorBrush ButtonColor { get; set; } = new SolidColorBrush(Colors.Red);
            public ICommand ToggleStatusCommand { get; set; }

            public bool IsPendingSave { get; set; } = false;

            private readonly Action _executeAction;

            public bool IsChecked { get; set; }

            public ServiceModel(Action executeAction, string status, SolidColorBrush buttonColor)
            {
                _executeAction = executeAction;
                Status = status;
                ButtonColor = buttonColor;
                IsChecked = (status == "On");

                ToggleStatusCommand = new DelegateCommand(ToggleStatus);
            }

            private void ToggleStatus()
            {

                if (!IsChecked)
                {
                    Status = "Off";

                }
                else
                {

                    Status = "On";

                }



                IsPendingSave = true;
                _executeAction?.Invoke();
            }
        }
    }


}