using System.Collections.ObjectModel;
using System.ComponentModel;
using System.IO;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;

namespace ToDoList_App
{
    public partial class HomePage : Page, INotifyPropertyChanged
    {
        private readonly string filePath = @"C:\Users\AMD Ryzen 3 3200G\source\repos\ToDoList_App\task.txt";
        public ObservableCollection<TaskItem> TaskList { get; set; }

        private double _taskCompletionPercentage;
        public double TaskCompletionPercentage
        {
            get => _taskCompletionPercentage;
            set
            {
                _taskCompletionPercentage = value;
                OnPropertyChanged(nameof(TaskCompletionPercentage));
            }
        }

        public HomePage()
        {
            InitializeComponent();
            TaskList = new ObservableCollection<TaskItem>();
            LoadTasks();
            DataContext = this;
        }

        private void LoadTasks()
        {
            if (File.Exists(filePath))
            {
                string[] lines = File.ReadAllLines(filePath);
                foreach (string line in lines)
                {
                    string[] parts = line.Split('|');
                    if (parts.Length == 3 && bool.TryParse(parts[2], out bool isChecked))
                    {
                        TaskList.Add(new TaskItem
                        {
                            TaskName = parts[0],
                            Deadline = parts[1],
                            IsChecked = isChecked
                        });
                    }
                }
            }
            CalculateTaskCompletionPercentage(); // Calculate initial percentage
        }

        private void SaveTasks()
        {
            using (StreamWriter writer = new StreamWriter(filePath, false))
            {
                foreach (var task in TaskList)
                {
                    writer.WriteLine($"{task.TaskName}|{task.Deadline}|{task.IsChecked}");
                }
            }
        }

        private void CheckBox_Checked(object sender, RoutedEventArgs e)
        {
            if (sender is CheckBox checkBox && checkBox.DataContext is TaskItem task)
            {
                task.IsChecked = true;
                SaveTasks();
                CalculateTaskCompletionPercentage();
            }
        }

        private void CheckBox_Unchecked(object sender, RoutedEventArgs e)
        {
            if (sender is CheckBox checkBox && checkBox.DataContext is TaskItem task)
            {
                task.IsChecked = false;
                SaveTasks();
                CalculateTaskCompletionPercentage();
            }
        }

        private void CalculateTaskCompletionPercentage()
        {
            if (TaskList.Count > 0)
            {
                var completedTasks = TaskList.Count(t => t.IsChecked);
                TaskCompletionPercentage = (double)completedTasks / TaskList.Count * 100;
            }
            else
            {
                TaskCompletionPercentage = 0;
            }
        }

        private void EditTask_Click(object sender, RoutedEventArgs e)
        {
            if (TaskListView.SelectedItem is TaskItem selectedTask)
            {
                string newName = Microsoft.VisualBasic.Interaction.InputBox(
                    "Enter new task name:", "Edit Task", selectedTask.TaskName);
                string newDeadline = Microsoft.VisualBasic.Interaction.InputBox(
                    "Enter new deadline:", "Edit Deadline", selectedTask.Deadline);

                if (!string.IsNullOrWhiteSpace(newName))
                    selectedTask.TaskName = newName;
                if (!string.IsNullOrWhiteSpace(newDeadline))
                    selectedTask.Deadline = newDeadline;

                SaveTasks();
            }
        }

        private void DeleteTask_Click(object sender, RoutedEventArgs e)
        {
            if (TaskListView.SelectedItem is TaskItem selectedTask)
            {
                TaskList.Remove(selectedTask);
                SaveTasks(); // Ensure deletion is saved
                CalculateTaskCompletionPercentage(); // Recalculate after deletion
            }
        }

        private void ListView_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            if (TaskListView.SelectedItem is TaskItem selectedTask)
            {
                MessageBox.Show($"Task: {selectedTask.TaskName}\nDeadline: {selectedTask.Deadline}\nStatus: {selectedTask.Status}",
                                "Task Details",
                                MessageBoxButton.OK,
                                MessageBoxImage.Information);
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}