using System.Configuration;
using System.Data;
using System.IO;
using System.Windows;
using Serilog;

namespace WpfApp1
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        public App()
        {
            Log.Logger = new LoggerConfiguration()
                .WriteTo.File("logs/myapp.txt", rollingInterval: RollingInterval.Day, retainedFileCountLimit: 7)
                .CreateLogger();
        }

        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);
            Log.Information("Ứng dụng TechVS đã được khởi động.");
            CleanupOldLogs();
        }

        protected override void OnExit(ExitEventArgs e)
        {
            Log.Information("Ứng dụng TechVS đã đóng.");
            Log.CloseAndFlush();
            base.OnExit(e);
        }

        private void CleanupOldLogs()
        {
            string logDirectory = "logs";
            if (Directory.Exists(logDirectory))
            {
                var logFiles = Directory.GetFiles(logDirectory, "myapp-*.txt");

                foreach (var logFile in logFiles)
                {
                    FileInfo fileInfo = new FileInfo(logFile);

                    if (fileInfo.LastWriteTime < DateTime.Now.AddDays(-7))
                    {
                        try
                        {
                            File.Delete(logFile);
                            Log.Information("Đã xóa file log cũ: {FileName}", fileInfo.Name);
                        }
                        catch (Exception ex)
                        {
                            Log.Error(ex, "Không thể xóa file log: {FileName}", fileInfo.Name);
                        }
                    }
                }
            }
        }
    }

}
