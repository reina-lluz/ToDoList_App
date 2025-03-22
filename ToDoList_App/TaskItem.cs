using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;

namespace ToDoList_App
{
    public class TaskItem : INotifyPropertyChanged
    {
        private bool _isChecked;
        private string _taskName;
        private string _deadline;
        private string _status;
        private Brush _statusColor;

        public string TaskName
        {
            get => _taskName;
            set { _taskName = value; OnPropertyChanged(nameof(TaskName)); }
        }

        public string Deadline
        {
            get => _deadline;
            set { _deadline = value; OnPropertyChanged(nameof(Deadline)); }
        }

        public bool IsChecked
        {
            get => _isChecked;
            set
            {
                _isChecked = value;
                Status = value ? "Completed" : "Pending";
                StatusColor = value ? Brushes.Green : Brushes.Red;
                OnPropertyChanged(nameof(IsChecked));
            }
        }

        public string Status
        {
            get => _status;
            set { _status = value; OnPropertyChanged(nameof(Status)); }
        }

        public Brush StatusColor
        {
            get => _statusColor;
            set { _statusColor = value; OnPropertyChanged(nameof(StatusColor)); }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged(string propertyName) =>
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
}