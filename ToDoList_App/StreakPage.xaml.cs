using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;

namespace ToDoList_App
{
    public partial class TotalTasksDonePage : Page
    {
        public TotalTasksDonePage()
        {
            InitializeComponent();
            LoadTotalTasksDone();
        }

        private void LoadTotalTasksDone()
        {
            if (Application.Current.MainWindow is MainWindow main)
            {
                TotalTasksText.Text = $"✔️ {main.TotalTasksDone} Tasks Completed";
            }
        }

        private void LogOut_Click(object sender, RoutedEventArgs e)
        {
            log_in loginWindow = new log_in();
            loginWindow.Show();
            Window.GetWindow(this)?.Close();
        }

        private void TaskCheckBox_Checked(object sender, RoutedEventArgs e)
        {
            if (sender is CheckBox checkBox && checkBox.DataContext is TaskItem task)
            {
                task.IsChecked = true;

                if (Application.Current.MainWindow is MainWindow main)
                {
                    main.TotalTasksDone++;
                }
            }
        }

        public void UpdateTotalTasksDisplay(int completedTasks)
        {
            TotalTasksText.Text = $"✔️ {completedTasks} Task{(completedTasks == 1 ? "" : "s")} Completed";
        }

    }
}
