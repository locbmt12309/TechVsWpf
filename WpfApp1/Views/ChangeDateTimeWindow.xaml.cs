using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Security.Principal;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media;
using System.Diagnostics;

namespace WpfApp1.Views
{
    public partial class ChangeDateTimeWindow : Window
    {
        private const string KeyPath = @"SOFTWARE\Microsoft\Windows\CurrentVersion\Policies\System";
        private const string ValueName = "DisableClockChange";
        private const string CmdPath = @"C:\Windows\System32\cmd.exe";
        private bool IsAdmin;
        private List<ServiceModel> services;

        public ChangeDateTimeWindow()
        {
            InitializeComponent();
            SetUserName("admin");
        }

        public ChangeDateTimeWindow(string username)
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
            try
            {
                // Kiểm tra registry (không ảnh hưởng thực tế, nhưng dùng làm trạng thái hiển thị)
                string keyPath = @"SOFTWARE\Microsoft\Windows\CurrentVersion\Policies\System";
                string valueName = "DisableCMD";

                bool isDisabledInRegistry = false;
                using (RegistryKey key = Registry.LocalMachine.OpenSubKey(keyPath, false))
                {
                    if (key != null)
                    {
                        object currentValue = key.GetValue(valueName, 0);
                        isDisabledInRegistry = (int)currentValue == 1;
                    }
                }

                // Kiểm tra quyền thực tế bằng PowerShell
                string command = "Get-Acl C:\\Windows\\System32\\cmd.exe | Format-List -Property AccessToString";
                string output = ExecutePowerShellCommandWithOutput(command);

                bool isCmdBlocked = output.Contains("Everyone Deny");

                return isDisabledInRegistry || isCmdBlocked;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi khi kiểm tra trạng thái CMD: {ex.Message}", "Lỗi", MessageBoxButton.OK, MessageBoxImage.Error);
                return false;
            }
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

            if (service.IsChecked)
            {
                ChangeDateTimeAccess(true);
            }
            else
            {
                ChangeDateTimeAccess(false);
            }
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

            if (service.IsChecked)
            {
                ChangeCmdAccess(true);
            }
            else
            {
                ChangeCmdAccess(false);
            }
        }

        private void ChangeDateTimeAccess(bool enable)
        {
            try
            {
                string keyPath = @"SOFTWARE\Microsoft\Windows\CurrentVersion\Policies\System";
                string valueName = "DisableClockChange";
                int newValue = enable ? 0 : 1;

                using (RegistryKey key = Registry.LocalMachine.OpenSubKey(keyPath, true))
                {
                    if (key == null)
                    {
                        MessageBox.Show("Registry key not found!", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                        return;
                    }

                    key.SetValue(valueName, newValue, RegistryValueKind.DWord);

                    string message = enable ? "Date/Time change has been enabled." : "Date/Time change has been disabled.";
                    MessageBox.Show(message, "Success", MessageBoxButton.OK, MessageBoxImage.Information);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error: {ex.Message}", "Failed to change Date/Time access", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void ChangeCmdAccess(bool enable)
        {
            try
            {
                List<string> commands = enable
                    ? new List<string>
                    {
                "icacls \"C:\\Windows\\System32\\cmd.exe\" /grant Everyone:F"
                    }
                    : new List<string>
                    {
                "icacls \"C:\\Windows\\System32\\cmd.exe\" /deny Everyone:F"
                    };

                // Thay đổi quyền sở hữu từ TrustedInstaller sang Administrator
                ChangeOwnershipToAdministrator();

                foreach (var command in commands)
                {
                    ExecutePowerShellCommand(command);
                }

                // Khôi phục quyền sở hữu lại cho TrustedInstaller
                ChangeOwnershipToTrustedInstaller();

                // Cập nhật trạng thái registry
                SetRegistryCmdStatus(!enable);

                // Hiển thị thông báo chỉ khi tác vụ hoàn tất
                string message = enable ? "CMD đã được bật." : "CMD đã bị vô hiệu hóa.";
                MessageBox.Show(message, "Thành công", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            catch (Exception ex)
            {
                // Chỉ thông báo lỗi khi gặp sự cố
                MessageBox.Show($"Lỗi: {ex.Message}", "Không thể thay đổi quyền CMD", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void ChangeOwnershipToAdministrator()
        {
            try
            {
                string command = "takeown /f \"C:\\Windows\\System32\\cmd.exe\" /a";
                ExecutePowerShellCommand(command);

                command = "icacls \"C:\\Windows\\System32\\cmd.exe\" /setowner \"Administrators\"";
                ExecutePowerShellCommand(command);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi khi thay đổi quyền sở hữu CMD: {ex.Message}", "Lỗi", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }


        private void ChangeOwnershipToTrustedInstaller()
        {
            try
            {
                string command = "powershell.exe -Command \"Take-Ownership 'C:\\Windows\\System32\\cmd.exe'; icacls 'C:\\Windows\\System32\\cmd.exe' /setowner 'NT SERVICE\\TrustedInstaller'\"";

                ExecutePowerShellCommand(command);
            }

            //    MessageBox.Show("Quyền sở hữu đã được chuyển lại cho TrustedInstaller.", "Thành công", MessageBoxButton.OK, MessageBoxImage.Information);
            //}
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi khi khôi phục quyền sở hữu CMD: {ex.Message}", "Lỗi", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }



        private void SetRegistryCmdStatus(bool disable)
        {
            try
            {
                string keyPath = @"SOFTWARE\Microsoft\Windows\CurrentVersion\Policies\System";
                string valueName = "DisableCMD";
                int newValue = disable ? 1 : 0;

                using (RegistryKey key = Registry.LocalMachine.OpenSubKey(keyPath, true))
                {
                    if (key != null)
                    {
                        key.SetValue(valueName, newValue, RegistryValueKind.DWord);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi khi cập nhật trạng thái CMD trong registry: {ex.Message}", "Lỗi", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private string ExecutePowerShellCommandWithOutput(string command)
        {
            try
            {
                ProcessStartInfo psi = new ProcessStartInfo
                {
                    FileName = "powershell.exe",
                    Arguments = $"-Command \"{command}\"",
                    RedirectStandardOutput = true,
                    UseShellExecute = false,
                    CreateNoWindow = true
                };

                using (Process process = Process.Start(psi))
                {
                    if (process != null)
                    {
                        string output = process.StandardOutput.ReadToEnd();
                        process.WaitForExit();
                        return output;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi khi chạy lệnh PowerShell: {ex.Message}", "Lỗi", MessageBoxButton.OK, MessageBoxImage.Error);
            }

            return string.Empty;
        }


        private void ExecutePowerShellCommand(string command)
        {
            try
            {
                if (!IsUserAdministrator())
                {
                    MessageBox.Show("Vui lòng chạy ứng dụng với quyền Administrator.", "Quyền bị từ chối", MessageBoxButton.OK, MessageBoxImage.Warning);
                    return;
                }

                ProcessStartInfo psi = new ProcessStartInfo
                {
                    FileName = "powershell.exe",
                    Arguments = $"-Command \"{command}\"",
                    Verb = "runas",
                    CreateNoWindow = true,
                    WindowStyle = ProcessWindowStyle.Hidden,
                    UseShellExecute = false
                };

                using (Process process = Process.Start(psi))
                {
                    if (process != null)
                    {
                        process.WaitForExit();
                        if (process.ExitCode != 0)
                        {
                            Console.WriteLine($"Lệnh thực thi thất bại. Mã thoát: {process.ExitCode}");
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi: {ex.Message}", "Không thể thực thi lệnh PowerShell", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }







        private void ExecuteCommand(string command)
        {
            try
            {
                if (!IsUserAdministrator())
                {
                    MessageBox.Show("Vui lòng chạy ứng dụng với quyền Administrator.", "Quyền bị từ chối", MessageBoxButton.OK, MessageBoxImage.Warning);
                    return;
                }

                ProcessStartInfo pro = new ProcessStartInfo()
                {
                    FileName = @"C:\Windows\System32\cmd.exe",
                    Arguments = "/c " + command,
                    Verb = "runas",
                    CreateNoWindow = true,
                    WindowStyle = ProcessWindowStyle.Hidden,
                    UseShellExecute = true,
                    WorkingDirectory = @"C:\Windows\System32"  
                };

                using (Process process = Process.Start(pro))
                {
                    if (process != null)
                    {
                        process.WaitForExit();
                        if (process.ExitCode != 0)
                        {
                            MessageBox.Show("Lệnh thực thi thất bại. Mã thoát: " + process.ExitCode, "Lỗi thực thi", MessageBoxButton.OK, MessageBoxImage.Error);
                        }
                        else
                        {
                            MessageBox.Show("Lệnh đã được thực thi thành công.", "Thành công", MessageBoxButton.OK, MessageBoxImage.Information);
                        }
                    }
                    else
                    {
                        MessageBox.Show("Không thể khởi động quá trình.", "Lỗi khởi động quá trình", MessageBoxButton.OK, MessageBoxImage.Error);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi: {ex.Message}", "Không thể thực thi lệnh", MessageBoxButton.OK, MessageBoxImage.Error);
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

        private void LogoutButton_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("You have logged out.");
            MainWindow mainWindow = new MainWindow();
            mainWindow.Show();
            this.Close();
        }

        private void UserNameTextBlock_Click(object sender, MouseButtonEventArgs e)
        {
            MessageBox.Show("UserName clicked.");
            LogoutButton.Visibility = Visibility.Visible;
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
