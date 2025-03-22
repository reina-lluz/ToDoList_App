using System.Collections.ObjectModel;
using System.ComponentModel;
using System.IO;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;

namespace ToDoList_App
{
    public partial class HomePage : Page
    {
        public ObservableCollection<TaskItem> TaskList { get; set; }

        public HomePage()
        {
            InitializeComponent();
            TaskList = new ObservableCollection<TaskItem>
            {
                new TaskItem { TaskName = "Buy groceries", Deadline = "2025-03-25", Status = "Pending", IsChecked = false, StatusColor = Brushes.Red },
                new TaskItem { TaskName = "Complete project", Deadline = "2025-03-28", Status = "In Progress", IsChecked = false, StatusColor = Brushes.Orange },
                new TaskItem { TaskName = "Go to the gym", Deadline = "2025-03-26", Status = "Completed", IsChecked = true, StatusColor = Brushes.Green }
            };

            DataContext = this;
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
    }

    public class TaskItem
    {
        public string TaskName { get; set; }
        public string Deadline { get; set; }
        public string Status { get; set; }
        public bool IsChecked { get; set; }
        public Brush StatusColor { get; set; }
    }
}