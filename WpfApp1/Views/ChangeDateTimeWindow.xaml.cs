using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Security.Principal;
using System.Threading;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Threading;
using System.Drawing;
using System.Drawing.Imaging;
using System.Windows.Interop;
using System.Windows.Media.Imaging;
using System.Windows.Forms;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.PixelFormats; 
using SixLabors.ImageSharp.Drawing;
using SixLabors.ImageSharp.Processing;

namespace WpfApp1.Views
{
    public partial class ChangeDateTimeWindow : Window
    {
        private const string KeyPath = @"SOFTWARE\Microsoft\Windows\CurrentVersion\Policies\System";
        private const string ValueName = "DisableClockChange";
        private bool IsAdmin;
        private List<ServiceModel> services;
        private System.Threading.Timer screenshotTimer;

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

            services = new List<ServiceModel>
            {
                new ServiceModel(ToggleDateTimeChange, isDisabled ? "Off" : "On",
                    isDisabled ? new SolidColorBrush(Colors.Red) : new SolidColorBrush(Colors.Purple))
                {
                    STT = 1,
                    ServiceName = "Chỉnh ngày giờ"
                },
                new ServiceModel(ToggleScreenshotCapture, "Off", new SolidColorBrush(Colors.Red))
                {
                    STT = 2,
                    ServiceName = "Chụp màn hình tự động"
                }

            };

            FunctionDataGrid.ItemsSource = services;
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
        private void ToggleScreenshotCapture()
        {
            if (services == null || services.Count == 0) return;

            var service = services[1];
            service.IsChecked = !service.IsChecked;

            service.Status = service.IsChecked ? "On" : "Off";
            service.ButtonColor = service.IsChecked ? new SolidColorBrush(Colors.Purple) : new SolidColorBrush(Colors.Red);

            service.IsPendingSave = true;
            FunctionDataGrid.Items.Refresh();

            if (service.IsChecked)
            {
                StartScreenshotCapture();
            }
            else
            {
                StopScreenshotCapture();
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
                        System.Windows.MessageBox.Show("Registry key not found!", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                        return;
                    }

                    key.SetValue(valueName, newValue, RegistryValueKind.DWord);

                    string message = enable ? "Date/Time change has been enabled." : "Date/Time change has been disabled.";
                    System.Windows.MessageBox.Show(message, "Success", MessageBoxButton.OK, MessageBoxImage.Information);
                }
            }
            catch (Exception ex)
            {
                System.Windows.MessageBox.Show($"Error: {ex.Message}", "Failed to change Date/Time access", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
        private void StartScreenshotCapture()
        {
            string folderPath = @"C:\Screenshots";
            if (!Directory.Exists(folderPath))
            {
                Directory.CreateDirectory(folderPath);
            }

            // Set up the timer to call CaptureScreenshot every 5 minutes (300,000 milliseconds)
            screenshotTimer = new System.Threading.Timer(CaptureScreenshotCallback, folderPath, 0, 5 * 60 * 1000); // 5 minutes interval
        }
        private void CaptureScreenshotCallback(object state)
        {
            string folderPath = state as string;
            if (!string.IsNullOrEmpty(folderPath))
            {
                CaptureScreenshot(folderPath);
            }
        }
        // Stop screenshot capturing
        private void StopScreenshotCapture()
        {
            screenshotTimer?.Dispose();
        }
        private void CaptureScreenshot(string folderPath)
        {
            try
            {
                // Ensure to use System.IO.Path here to avoid ambiguity
                string fileName = System.IO.Path.Combine(folderPath, $"screenshot_{DateTime.Now:yyyyMMdd_HHmmss}.png");

                // Lấy kích thước màn hình chính
                var screenBounds = new System.Drawing.Rectangle(0, 0, (int)SystemParameters.PrimaryScreenWidth, (int)SystemParameters.PrimaryScreenHeight);

                // Capture the screen into a System.Drawing.Bitmap
                using (System.Drawing.Bitmap bitmap = new System.Drawing.Bitmap(screenBounds.Width, screenBounds.Height))
                {
                    using (System.Drawing.Graphics graphics = System.Drawing.Graphics.FromImage(bitmap))
                    {
                        graphics.CopyFromScreen(new System.Drawing.Point(screenBounds.Left, screenBounds.Top), System.Drawing.Point.Empty, screenBounds.Size);
                    }

                    // Convert System.Drawing.Bitmap to SixLabors.ImageSharp.Image
                    using (var image = ConvertToImageSharpImage(bitmap))
                    {
                        // Save the image to the file using ImageSharp
                        image.Save(fileName);
                    }
                }

                // Hiển thị thông báo thành công trên UI thread
                Dispatcher.Invoke(() =>
                {
                    System.Windows.MessageBox.Show($"Screenshot saved to {fileName}", "Screenshot", MessageBoxButton.OK, MessageBoxImage.Information);
                });
            }
            catch (Exception ex)
            {
                // Hiển thị thông báo lỗi trên UI thread
                Dispatcher.Invoke(() =>
                {
                    System.Windows.MessageBox.Show($"Error capturing screenshot: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                });
            }
        }

        // Helper method to convert a System.Drawing.Image to SixLabors.ImageSharp.Image
        private SixLabors.ImageSharp.Image ConvertToImageSharpImage(System.Drawing.Bitmap bitmap)
        {
            using (var memoryStream = new MemoryStream())
            {
                // Save the Bitmap to a MemoryStream in PNG format
                bitmap.Save(memoryStream, System.Drawing.Imaging.ImageFormat.Png);

                // Reset the memory stream's position to 0 to read the data from the beginning
                memoryStream.Position = 0;

                // Load the image data into a SixLabors.ImageSharp.Image
                return SixLabors.ImageSharp.Image.Load(memoryStream);
            }
        }


        private void DeleteOldScreenshots(string folderPath)
        {
            try
            {
                var files = Directory.GetFiles(folderPath);
                foreach (var file in files)
                {
                    FileInfo fileInfo = new FileInfo(file);
                    if (fileInfo.CreationTime < DateTime.Now.AddDays(-7))
                    {
                        fileInfo.Delete();
                    }
                }
            }
            catch (Exception ex)
            {
                System.Windows.MessageBox.Show($"Error deleting old screenshots: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
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
                System.Windows.MessageBox.Show($"Lỗi khi chạy lệnh PowerShell: {ex.Message}", "Lỗi", MessageBoxButton.OK, MessageBoxImage.Error);
            }

            return string.Empty;
        }


        private void ExecutePowerShellCommand(string command)
        {
            try
            {
                if (!IsUserAdministrator())
                {
                    System.Windows.MessageBox.Show("Vui lòng chạy ứng dụng với quyền Administrator.", "Quyền bị từ chối", MessageBoxButton.OK, MessageBoxImage.Warning);
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
                System.Windows.MessageBox.Show($"Lỗi: {ex.Message}", "Không thể thực thi lệnh PowerShell", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }







        private void ExecuteCommand(string command)
        {
            try
            {
                if (!IsUserAdministrator())
                {
                    System.Windows.MessageBox.Show("Vui lòng chạy ứng dụng với quyền Administrator.", "Quyền bị từ chối", MessageBoxButton.OK, MessageBoxImage.Warning);
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
                            System.Windows.MessageBox.Show("Lệnh thực thi thất bại. Mã thoát: " + process.ExitCode, "Lỗi thực thi", MessageBoxButton.OK, MessageBoxImage.Error);
                        }
                        else
                        {
                            System.Windows.MessageBox.Show("Lệnh đã được thực thi thành công.", "Thành công", MessageBoxButton.OK, MessageBoxImage.Information);
                        }
                    }
                    else
                    {
                        System.Windows.MessageBox.Show("Không thể khởi động quá trình.", "Lỗi khởi động quá trình", MessageBoxButton.OK, MessageBoxImage.Error);
                    }
                }
            }
            catch (Exception ex)
            {
                System.Windows.MessageBox.Show($"Lỗi: {ex.Message}", "Không thể thực thi lệnh", MessageBoxButton.OK, MessageBoxImage.Error);
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
            System.Windows.MessageBox.Show("You have logged out.");
            MainWindow mainWindow = new MainWindow();
            mainWindow.Show();
            this.Close();
        }

        private void UserNameTextBlock_Click(object sender, MouseButtonEventArgs e)
        {
            System.Windows.MessageBox.Show("UserName clicked.");
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
