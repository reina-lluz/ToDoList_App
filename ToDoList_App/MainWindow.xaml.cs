using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Text;
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
    public partial class MainWindow : Window
    {
        public ObservableCollection<TaskItem> TaskList { get; set; }  // ✅ Fix: Add TaskList

        private bool isSidebarOpen = false; // Sidebar starts CLOSED

        public MainWindow()
        {
            InitializeComponent();
            MainFrame.Navigate(new HomePage()); // Default page
            TaskList = new ObservableCollection<TaskItem>();
            // ✅ Initialize TaskList
        }



        private void GoToHomePage(object sender, RoutedEventArgs e)
        {
            MainFrame.Navigate(new HomePage());
        }

        private void GoToTasksPage(object sender, RoutedEventArgs e)
        {
            MainFrame.Navigate(new TasksPage());
        }

        private void GoToProfilePage(object sender, RoutedEventArgs e)
        {
            MainFrame.Navigate(new StreakPage());
        }
        private void LogOut_Click(object sender, RoutedEventArgs e)
        {
            // Open the existing log_in window
            log_in loginWindow = new log_in(); // ✅ Use 'log_in' instead of 'LoginWindow'
            loginWindow.Show();

            // Close the current MainWindow
            this.Close();
        }


    }
}
