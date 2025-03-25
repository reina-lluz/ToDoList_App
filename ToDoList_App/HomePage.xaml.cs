using System.Collections.ObjectModel;
using System.ComponentModel;
using System.IO;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace ToDoList_App
{
    public partial class HomePage : Page, INotifyPropertyChanged
    {
        private readonly string filePath = @"C:\Users\LEO\source\repos\ToDoList_App\tasks.txt";
        public ObservableCollection<TaskItem> TaskList { get; set; } = new ObservableCollection<TaskItem>();

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
            TaskList = (Application.Current.MainWindow as MainWindow)?.TaskList ?? new ObservableCollection<TaskItem>();

            DataContext = this;
            LoadTasks();
            CalculateTaskCompletionPercentage();
        }

        private void LoadTasks()
        {
            try
            {
                if (File.Exists(filePath))
                {
                    string[] lines = File.ReadAllLines(filePath);
                    TaskList.Clear();

                    foreach (string line in lines)
                    {
                        string[] parts = line.Split('|');
                        if (parts.Length == 5 && bool.TryParse(parts[4], out bool isChecked))
                        {
                            TaskList.Add(new TaskItem
                            {
                                TaskName = parts[0],
                                Deadline = parts[1],
                                Priority = parts[2],
                                Category = parts[3],
                                IsChecked = isChecked
                            });
                        }
                    }
                    CalculateTaskCompletionPercentage();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error loading tasks: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void SaveTasks()
        {
            try
            {
                using (StreamWriter writer = new StreamWriter(filePath, false))
                {
                    foreach (var task in TaskList)
                    {
                        writer.WriteLine($"{task.TaskName}|{task.Deadline}|{task.Priority}|{task.Category}|{task.IsChecked}");
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error saving tasks: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void CheckBox_Checked(object sender, RoutedEventArgs e)
        {
            if (sender is CheckBox checkBox && checkBox.DataContext is TaskItem task)
            {
                task.IsChecked = true;
                SaveTasks();
                CalculateTaskCompletionPercentage();

                if (Application.Current.MainWindow is MainWindow main)
                {
                    main.TotalTasksDone++;
                    main.UpdateTotalTasksDonePage();
                }
            }
        }

        private void CheckBox_Unchecked(object sender, RoutedEventArgs e)
        {
            if (sender is CheckBox checkBox && checkBox.DataContext is TaskItem task)
            {
                task.IsChecked = false;
                SaveTasks();
                CalculateTaskCompletionPercentage();

                if (Application.Current.MainWindow is MainWindow main)
                {
                    main.TotalTasksDone = Math.Max(0, main.TotalTasksDone - 1);
                    main.UpdateTotalTasksDonePage();
                }
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

        private void DeleteTask_Click(object sender, RoutedEventArgs e)
        {
            if (TaskListView.SelectedItem is TaskItem selectedTask)
            {
                TaskList.Remove(selectedTask);
                SaveTasks();
                CalculateTaskCompletionPercentage();
            }
        }

        private void ListView_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            if (TaskListView.SelectedItem is TaskItem selectedTask)
            {
                MessageBox.Show(
                    $"Task: {selectedTask.TaskName}\nDeadline: {selectedTask.Deadline}\nPriority: {selectedTask.Priority}\nCategory: {selectedTask.Category}\nStatus: {selectedTask.Status}",
                    "Task Details",
                    MessageBoxButton.OK,
                    MessageBoxImage.Information
                );
            }
        }

        private void EditTask_Click(object sender, RoutedEventArgs e)
        {
            if (TaskListView.SelectedItem is TaskItem selectedTask)
            {
                string newName = Microsoft.VisualBasic.Interaction.InputBox(
                    "Enter new task name:", "Edit Task", selectedTask.TaskName);
                string newDeadline = Microsoft.VisualBasic.Interaction.InputBox(
                    "Enter new deadline (yyyy-MM-dd):", "Edit Deadline", selectedTask.Deadline);
                string newPriority = Microsoft.VisualBasic.Interaction.InputBox(
                    "Enter new priority (Low, Medium, High):", "Edit Priority", selectedTask.Priority);
                string newCategory = Microsoft.VisualBasic.Interaction.InputBox(
                    "Enter new category (Personal, Work, School, Others):", "Edit Category", selectedTask.Category);

                if (!string.IsNullOrWhiteSpace(newName))
                    selectedTask.TaskName = newName;
                if (!string.IsNullOrWhiteSpace(newDeadline))
                    selectedTask.Deadline = newDeadline;
                if (!string.IsNullOrWhiteSpace(newPriority))
                    selectedTask.Priority = newPriority;
                if (!string.IsNullOrWhiteSpace(newCategory))
                    selectedTask.Category = newCategory;

                SaveTasks();
                MessageBox.Show("Task updated successfully!", "Success", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            else
            {
                MessageBox.Show("Please select a task to edit.", "No Task Selected", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
