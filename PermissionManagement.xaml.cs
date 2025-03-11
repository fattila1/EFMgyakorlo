using System.Collections.Generic;
using System.Windows;

namespace EFM
{
    public partial class PermissionManagement : Window
    {
        public PermissionManagement()
        {
            InitializeComponent();
            LoadUsers();
        }

        private void LoadUsers()
        {
            // Adatbázisból töltsd be a felhasználókat
            UserList.ItemsSource = GetUsers(); // Felhasználók listája
        }

        private void UserList_SelectionChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
        {
            if (UserList.SelectedItem != null)
            {
                int userId = ((User)UserList.SelectedItem).Id;
                PermissionList.ItemsSource = GetUserPermissions(userId); // Jogosultságok betöltése
            }
        }

        private void AddPermission_Click(object sender, RoutedEventArgs e)
        {
            // Hozzáadás logikája
            MessageBox.Show("Jogosultság hozzáadva!");
        }

        private void RemovePermission_Click(object sender, RoutedEventArgs e)
        {
            // Eltávolítás logikája
            MessageBox.Show("Jogosultság eltávolítva!");
        }

        // Példa: Felhasználók lekérdezése
        private List<User> GetUsers()
        {
            return new List<User>
            {
                new User { Id = 1, Username = "admin" },
                new User { Id = 2, Username = "user1" }
            };
        }

        // Példa: Jogosultságok lekérdezése
        private List<string> GetUserPermissions(int userId)
        {
            // Jogosultságok lekérdezése adatbázisból
            return new List<string> { "Edit Users", "View Reports" };
        }
    }

    // Példa User osztály
    public class User
    {
        public int Id { get; set; }
        public required string Username { get; set; }
    }
}
