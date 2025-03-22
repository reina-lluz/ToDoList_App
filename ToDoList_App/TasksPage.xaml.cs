using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace ToDoList_App
{
    public partial class TasksPage : Page
    {
        private string filePath = "tasks.txt"; // File to store tasks

        public TasksPage()
        {
            InitializeComponent();
            LoadTasks(); // Load tasks when the page is opened
        }

        private void AddTask_Click(object sender, RoutedEventArgs e)
        {
            string taskName = TaskNameInput.Text;
            string deadline = DeadlineInput.SelectedDate?.ToString("yyyy-MM-dd") ?? "No deadline";

            if (string.IsNullOrWhiteSpace(taskName))
            {
                MessageBox.Show("Task Name cannot be empty.", "Error", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            string newTask = $"{taskName}|{deadline}|False"; // Format: TaskName|Deadline|IsChecked

            try
            {
                // Append the new task to the file
                File.AppendAllText(filePath, newTask + Environment.NewLine);
                MessageBox.Show("Task added successfully!", "Success", MessageBoxButton.OK, MessageBoxImage.Information);

                // Add the task to MainWindow's TaskList
                if (Application.Current.MainWindow is MainWindow main)
                {
                    main.TaskList.Add(new TaskModel { TaskName = taskName, Deadline = deadline, IsChecked = false });
                }

                // Clear inputs
                TaskNameInput.Text = "";
                DeadlineInput.SelectedDate = null;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error saving task: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void LoadTasks()
        {
            try
            {
                if (File.Exists(filePath))
                {
                    string[] lines = File.ReadAllLines(filePath);
                    foreach (string line in lines)
                    {
                        string[] parts = line.Split('|');
                        if (parts.Length == 3)
                        {
                            string taskName = parts[0];
                            string deadline = parts[1];
                            bool isChecked = bool.Parse(parts[2]);

                            // Get reference to MainWindow and add task
                            if (Application.Current.MainWindow is MainWindow main)
                            {
                                main.TaskList.Add(new TaskModel { TaskName = taskName, Deadline = deadline, IsChecked = isChecked });
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error loading tasks: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}