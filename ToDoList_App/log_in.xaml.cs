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
using System.Windows.Shapes;

namespace ToDoList_App
{
    public partial class log_in : Window
    {
        public log_in()
        {
            InitializeComponent();
        }

        // Sign In Logic
        private void SignIn_Click(object sender, RoutedEventArgs e)
        {
            string username = usernameTextBox.Text;
            string password = passwordBox.Password;

            if (string.IsNullOrEmpty(username) || string.IsNullOrEmpty(password))
            {
                MessageBox.Show("Please enter both username and password.", "Input Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            // Validation logic for credentials
            if (username == "anna" && password == "ppl1")
            {
                MainWindow homePageAnna = new MainWindow();
                homePageAnna.Show();
                this.Close();
            }
            else if (username == "brian" && password == "ppl2")
            {
                WindowBrian homePageBrian = new WindowBrian();
                homePageBrian.Show();
                this.Close();
            }
            else if (username == "claire" && password == "ppl3")
            {
                WindowClaire homePageClaire = new WindowClaire();
                homePageClaire.Show();
                this.Close();
            }
            else
            {
                MessageBox.Show("Incorrect username or password.", "Login Failed", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        // Sign Up Logic
        private void SignUp_Click(object sender, RoutedEventArgs e)
        {
            SignUpWindow signUpWindow = new SignUpWindow();
            signUpWindow.Show();
            this.Close(); // Close the login window
        }
    }
}