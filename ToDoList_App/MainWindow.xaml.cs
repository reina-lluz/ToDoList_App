using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Controls;

namespace ToDoList_App
{
    public partial class MainWindow : Window
    {
        public ObservableCollection<TaskItem> TaskList { get; set; } = new ObservableCollection<TaskItem>();
        public int TotalTasksDone { get; set; } = 0;
        private bool isSidebarOpen = false; 
        private TotalTasksDonePage totalTasksDonePage; // ✅ Store reference to TotalTasksDonePage

        public MainWindow()
        {
            InitializeComponent();
            totalTasksDonePage = new TotalTasksDonePage(); // ✅ Initialize the page
            MainFrame.Navigate(new HomePage()); 
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
            MainFrame.Navigate(totalTasksDonePage); // ✅ Navigate to existing instance
        }

        private void LogOut_Click(object sender, RoutedEventArgs e)
        {
            log_in loginWindow = new log_in(); 
            loginWindow.Show();
            this.Close();
        }

        // ✅ Method to update TotalTasksDonePage when tasks are completed
        public void UpdateTotalTasksDonePage()
        {
            totalTasksDonePage.UpdateTotalTasksDisplay(TotalTasksDone);
        }
    }
}
