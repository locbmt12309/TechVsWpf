using System;
using System.Windows.Input;
using System.Windows.Media;

namespace WpfApp1.Models
{
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
            IsChecked = !IsChecked;
            Status = IsChecked ? "On" : "Off";
            IsPendingSave = true;
            _executeAction?.Invoke();
        }
    }
}
