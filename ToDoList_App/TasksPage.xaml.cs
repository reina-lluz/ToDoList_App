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
        private string filePath = @"C:\Users\LEO\source\repos\ToDoList_App\tasks.txt"; // File to store tasks

        public TasksPage()
        {
            InitializeComponent();
            LoadTasks(); // Load tasks when the page is opened
        }

        private void AddTask_Click(object sender, RoutedEventArgs e)
        {
            string taskName = TaskNameInput.Text;
            string deadline = DeadlineInput.SelectedDate?.ToString("yyyy-MM-dd") ?? "No deadline";

            string priority = (PriorityInput.SelectedItem as ComboBoxItem)?.Content.ToString() ?? "Low";
            string category = (CategoryInput.SelectedItem as ComboBoxItem)?.Content.ToString() ?? "Personal";

            if (string.IsNullOrWhiteSpace(taskName))
            {
                MessageBox.Show("Task Name cannot be empty.", "Error", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            // Updated task format with priority and category
            string newTask = $"{taskName}|{deadline}|{priority}|{category}|False";

            try
            {
                // Append the new task to the file
                File.AppendAllText(filePath, newTask + Environment.NewLine);
                MessageBox.Show("Task added successfully!", "Success", MessageBoxButton.OK, MessageBoxImage.Information);

                // Add the task to MainWindow's TaskList
                if (Application.Current.MainWindow is MainWindow main)
                {
                    main.TaskList.Add(new TaskItem
                    {
                        TaskName = taskName,
                        Deadline = deadline,
                        Priority = priority,
                        Category = category,
                        IsChecked = false
                    });
                }

                // Clear inputs after adding
                TaskNameInput.Text = "";
                DeadlineInput.SelectedDate = null;
                PriorityInput.SelectedIndex = 0;
                CategoryInput.SelectedIndex = 0;
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
                        if (parts.Length == 5)  // Check for correct task format
                        {
                            string taskName = parts[0];
                            string deadline = parts[1];
                            string priority = parts[2];
                            string category = parts[3];
                            bool isChecked = bool.Parse(parts[4]);

                            // Add task to MainWindow's TaskList
                            if (Application.Current.MainWindow is MainWindow main)
                            {
                                main.TaskList.Add(new TaskItem
                                {
                                    TaskName = taskName,
                                    Deadline = deadline,
                                    Priority = priority,
                                    Category = category,
                                    IsChecked = isChecked
                                });
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

        private void TaskCheckBox_Checked(object sender, RoutedEventArgs e)
        {
            if (sender is CheckBox checkBox && checkBox.DataContext is TaskItem task)
            {
                task.IsChecked = true;

                // Update MainWindow’s total completed tasks count
                if (Application.Current.MainWindow is MainWindow main)
                {
                    main.TotalTasksDone++;
                }

            }
        }


    }
}