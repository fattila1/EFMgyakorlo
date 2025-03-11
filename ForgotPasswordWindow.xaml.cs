using System.Windows;

namespace EFM
{
    public partial class ForgotPasswordWindow : Window
    {
        public ForgotPasswordWindow()
        {
            InitializeComponent();
        }

        private void SendResetLink_Click(object sender, RoutedEventArgs e)
        {
            string email = EmailTextBox.Text;

            if (string.IsNullOrEmpty(email))
            {
                MessageBox.Show("Please enter your email address.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            // Itt implementálhatod az e-mail küldést
            MessageBox.Show($"A password reset link has been sent to {email}.", "Success", MessageBoxButton.OK, MessageBoxImage.Information);
            this.Close();
        }
    }
}
