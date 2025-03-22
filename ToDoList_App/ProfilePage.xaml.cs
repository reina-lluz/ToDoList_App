using Microsoft.Win32;
using System;
using System.Collections.Generic;
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
    public partial class ProfilePage : Page
    {
        public ProfilePage()
        {
            InitializeComponent();
        }

        // This method is used to handle the Log Out button click
        private void LogOut_Click(object sender, RoutedEventArgs e)
        {
            // Create a new instance of the login window
            log_in loginWindow = new log_in();

            // Show the login window
            loginWindow.Show();

            // Close the current ProfilePage window (if it's within a Window)
            Window.GetWindow(this)?.Close(); // This will close the parent window

            // Optionally, navigate back to the login window in the context of a Page-based navigation
            // NavigationService.Navigate(new Uri("log_in.xaml", UriKind.Relative));
        }

        // This method handles the button click for uploading the photo
        private void OnUploadPhotoClick(object sender, RoutedEventArgs e)
        {
            // Open file picker dialog to select an image
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "Image Files|*.jpg;*.jpeg;*.png;*.bmp";
            if (openFileDialog.ShowDialog() == true)
            {
                // Set the selected image as the profile picture
                ProfileImage.Source = new BitmapImage(new Uri(openFileDialog.FileName));
            }
        }
    }
}