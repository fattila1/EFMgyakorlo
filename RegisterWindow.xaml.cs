using MySqlConnector;
using System;
using System.Configuration;
using System.Net;
using System.Net.Mail;
using System.Security.Cryptography;
using System.Text;
using System.Windows;

namespace EFM
{
    public partial class RegisterWindow : Window
    {
        private string captchaCode;

        public RegisterWindow()
        {
            InitializeComponent();
            GenerateCaptcha();
        }

        private void GenerateCaptcha()
        {
            // Generálj egy véletlenszerű számot captcha-hoz
            Random random = new Random();
            captchaCode = random.Next(1000, 9999).ToString();
            CaptchaText.Text = captchaCode;
        }

        private string HashPassword(string password)
        {
            // Jelszó hash-elése SHA-256 használatával
            using (var sha256 = SHA256.Create())
            {
                byte[] bytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(password));
                StringBuilder builder = new StringBuilder();
                foreach (var b in bytes)
                {
                    builder.Append(b.ToString("x2"));
                }
                return builder.ToString();
            }
        }

        private string GenerateVerificationCode()
        {
            // Egyedi megerősítő kód generálása
            return Guid.NewGuid().ToString();
        }

        private void SendVerificationEmail(string email, string verificationCode)
        {
            try
            {
                var smtpClient = new SmtpClient("smtp.gmail.com")
                {
                    Port = 587,
                    Credentials = new NetworkCredential("your_email@gmail.com", "your_email_password"),
                    EnableSsl = true,
                };

                string verificationLink = $"https://yourwebsite.com/verify?code={verificationCode}";
                var mailMessage = new MailMessage
                {
                    From = new MailAddress("your_email@gmail.com"),
                    Subject = "Email Verification",
                    Body = $"Click the link to verify your email: {verificationLink}",
                    IsBodyHtml = true,
                };
                mailMessage.To.Add(email);

                smtpClient.Send(mailMessage);
                MessageBox.Show("A verification email has been sent. Please check your inbox.", "Success", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error sending email: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void RegisterButton_Click(object sender, RoutedEventArgs e)
        {
            string username = UsernameTextBox.Text.Trim();
            string email = EmailTextBox.Text.Trim();
            string password = PasswordBox.Password.Trim();
            string captchaInput = CaptchaInput.Text.Trim();

            if (string.IsNullOrEmpty(username) || string.IsNullOrEmpty(email) || string.IsNullOrEmpty(password))
            {
                MessageBox.Show("All fields are required.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            if (captchaInput != captchaCode)
            {
                MessageBox.Show("Invalid captcha. Please try again.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                GenerateCaptcha();
                return;
            }

            if (!IsValidEmail(email))
            {
                MessageBox.Show("Please enter a valid email address.", "Invalid Email", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            string hashedPassword = HashPassword(password);
            string verificationCode = GenerateVerificationCode();

            string connectionString = ConfigurationManager.ConnectionStrings["EFMDatabase"].ConnectionString;

            try
            {
                using (var connection = new MySqlConnection(connectionString))
                {
                    connection.Open();
                    string userQuery = "INSERT INTO Users (Username, Email, Password, Role) VALUES (@Username, @Email, @Password, 'User')";
                    string verificationQuery = "INSERT INTO EmailVerifications (Email, VerificationCode) VALUES (@Email, @VerificationCode)";

                    using (var userCmd = new MySqlCommand(userQuery, connection))
                    {
                        userCmd.Parameters.AddWithValue("@Username", username);
                        userCmd.Parameters.AddWithValue("@Email", email);
                        userCmd.Parameters.AddWithValue("@Password", hashedPassword);
                        userCmd.ExecuteNonQuery();
                    }

                    using (var verificationCmd = new MySqlCommand(verificationQuery, connection))
                    {
                        verificationCmd.Parameters.AddWithValue("@Email", email);
                        verificationCmd.Parameters.AddWithValue("@VerificationCode", verificationCode);
                        verificationCmd.ExecuteNonQuery();
                    }

                    SendVerificationEmail(email, verificationCode);
                    MessageBox.Show("Registration successful! Please verify your email.", "Success", MessageBoxButton.OK, MessageBoxImage.Information);
                    this.Close();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"An error occurred: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private bool IsValidEmail(string email)
        {
            string emailRegex = @"^[^@\s]+@[^@\s]+\.[^@\s]+$";
            return System.Text.RegularExpressions.Regex.IsMatch(email, emailRegex);
        }
    }
}
