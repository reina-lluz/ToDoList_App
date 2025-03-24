using System;
using System.ComponentModel;
using System.Windows.Media;

namespace ToDoList_App
{
    public class TaskItem : INotifyPropertyChanged
    {
        private bool _isChecked;
        private string _taskName;
        private string _deadline;
        private string _priority;
        private string _category;
        private string _status;
        private Brush _statusColor;

        public string TaskName
        {
            get => _taskName;
            set
            {
                _taskName = value;
                OnPropertyChanged(nameof(TaskName));
            }
        }

        public string Deadline
        {
            get => _deadline;
            set
            {
                _deadline = value;
                OnPropertyChanged(nameof(Deadline));
            }
        }

        public string Priority
        {
            get => _priority;
            set
            {
                _priority = value;
                OnPropertyChanged(nameof(Priority));
            }
        }

        public string Category
        {
            get => _category;
            set
            {
                _category = value;
                OnPropertyChanged(nameof(Category));
            }
        }

        public bool IsChecked
        {
            get => _isChecked;
            set
            {
                if (_isChecked != value)
                {
                    _isChecked = value;
                    Status = value ? "Completed" : "Pending";
                    StatusColor = value ? Brushes.Green : Brushes.Red;
                    OnPropertyChanged(nameof(IsChecked));
                }
            }
        }

        public string Status
        {
            get => _status;
            private set
            {
                if (_status != value)
                {
                    _status = value;
                    OnPropertyChanged(nameof(Status));
                }
            }
        }

        public Brush StatusColor
        {
            get => _statusColor;
            private set
            {
                if (_statusColor != value)
                {
                    _statusColor = value;
                    OnPropertyChanged(nameof(StatusColor));
                }
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged(string propertyName) =>
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
}
