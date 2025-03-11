using System.Windows;
using System.Windows.Controls;
using EFM.DataAccess;

namespace EFM
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void LoginButton_Click(object sender, RoutedEventArgs e)
        {
            string username = UsernameTextBox.Text;
            string password = PasswordBox.Password;

            string role = AuthenticateUser(username, password);
            if (!string.IsNullOrEmpty(role))
            {
                MessageBox.Show($"Login successful! Role: {role}", "Welcome", MessageBoxButton.OK, MessageBoxImage.Information);

                // Példa: Role alapú navigáció
                if (role == "Admin")
                {
                    // Admin ablak megnyitása
                    AdminWindow adminWindow = new AdminWindow();
                    adminWindow.Show();
                }
                else if (role == "Moderator")
                {
                    // Moderator ablak
                    MessageBox.Show("Welcome, Moderator!");
                }
                else
                {
                    // User ablak
                    MessageBox.Show("Welcome, User!");
                    UserWindow userWindow = new UserWindow(username);
                    userWindow.Show();
                    this.Close();
                }
                this.Close();
            }
            else
            {
                MessageBox.Show("Invalid username or password.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void ForgotPassword_Click(object sender, RoutedEventArgs e)
        {
            ForgotPasswordWindow forgotPasswordWindow = new ForgotPasswordWindow();
            forgotPasswordWindow.ShowDialog();
        }

        private void RegisterButton_Click(object sender, RoutedEventArgs e)
        {
            // Nyisd meg a regisztrációs ablakot
            RegisterWindow registerWindow = new RegisterWindow();
            registerWindow.ShowDialog();
        }

        private string AuthenticateUser(string username, string password)
        {
            // DataAccess használata az adatbázis eléréséhez
            var userRole = DatabaseHelper.AuthenticateUser(username, password);
            return userRole;
        }
    }
}
