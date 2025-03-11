using EFM.DataAccess; // A DAL osztályok használata
using System;
using System.Windows;
using System.Windows.Controls;
using EFM.Tasks; // A feladatokhoz tartozó ablakok használata
using EFM.Examples; // Példa ablakok

namespace EFM
{
    public partial class UserWindow : Window
    {
        private readonly string _username; // A bejelentkezett felhasználó neve

        public UserWindow(string username)
        {
            InitializeComponent();
            _username = username; // Felhasználó nevének mentése
            UsernameText.Text = _username; // Felhasználónév megjelenítése az ablakban
            LoadData(); // Adatok betöltése
        }

        private void LoadData()
        {
            try
            {
                // Statisztikák betöltése az adatbázisból
                var (tasksCompleted, tasksCorrect) = DatabaseHelper.GetUserStatistics(_username);
                TasksCompletedText.Text = tasksCompleted.ToString(); // Megoldott feladatok
                TasksCorrectText.Text = tasksCorrect.ToString(); // Helyes feladatok

                // Feladatok és példák gombok létrehozása
                TasksPanel.Children.Clear();

                for (int i = 1; i <= 8; i++)
                {
                    int taskNumber = i;

                    
                    Button taskButton = new Button
                    {
                        Content = $"Gyakorló feladat a(z) {taskNumber}. témakörből",
                        Margin = new Thickness(5)
                    };

                    taskButton.Click += (s, e) =>
                    {
                        Window taskWindow = taskNumber switch
                        {
                            1 => new Task1Window(_username),
                            2 => new Task2Window(),
                            3 => new Task3Window(),
                            4 => new Task4Window(),
                            5 => new Task5Window(),
                            6 => new Task6Window(),
                            7 => new Task7Window(),
                            8 => new Task8Window(),
                            _ => throw new ArgumentException("Invalid task number")
                        };

                        taskWindow.ShowDialog();
                        LoadData(); // Frissítjük a statisztikát az ablak bezárása után
                    };

                    // Example gomb
                    Button exampleButton = new Button
                    {
                        Content = $"Mintafeladat a(z) {taskNumber}. témában ",
                        Margin = new Thickness(5)
                    };

                    exampleButton.Click += (s, e) =>
                    {
                        Window exampleWindow = taskNumber switch
                        {
                            1 => new Example1Window(),
                            2 => new Example2Window(),
                            3 => new Example3Window(),
                            4 => new Example4Window(),
                            5 => new Example5Window(),
                            6 => new Example6Window(),
                            7 => new Example7Window(),
                            8 => new Example8Window(),
                            _ => throw new ArgumentException("Invalid example number")
                        };

                        exampleWindow.ShowDialog();
                    };

                    // Task és example gombok egy sorban
                    StackPanel taskPanel = new StackPanel { Orientation = Orientation.Horizontal, Margin = new Thickness(5) };
                    taskPanel.Children.Add(taskButton);
                    taskPanel.Children.Add(exampleButton);
                    TasksPanel.Children.Add(taskPanel);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Hiba az adatok betöltésekor: {ex.Message}", "Hiba", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void LogoutButton_Click(object sender, RoutedEventArgs e)
        {
            var loginWindow = new MainWindow();
            loginWindow.Show();
            Close();
        }
    }
}
