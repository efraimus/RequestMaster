using Microsoft.EntityFrameworkCore;
using OrdersApp.Databases.OrdersDatabase;
using OrdersApp.ViewModels;
using System.IO;
using System.Windows;
using System.Windows.Media;

namespace OrdersApp
{
    public partial class AuthWindow : Window
    {
        public static string? login;
        public static string? password;
        public static string? email;
        public static int balance;
        public static int userID;
        MainWindow? mainWindow;

        public AuthWindow()
        {
            InitializeComponent();
            loginButton.Focus();
            confirmPasswordBox.Visibility = Visibility.Hidden;
        }
        #region ButtonClick

        private void loginButton_Click(object sender, RoutedEventArgs e)
        {
            login = loginBox.Text;
            password = passwordBox.Password;
            User authUser = App.db.Users.Where(b => b.Login == login && b.Password == password).FirstOrDefault()!;
            if (authUser != null)
            {
                email = authUser.Email;
                balance = authUser.Balance;
                userID = authUser.UserId;
                mainWindow = new MainWindow();
                mainWindow.DataContext = new MainWindowViewModel();
                App.logWriter!.WriteLine($"Auth window: User with ID={authUser.UserId} authorized\t\t\t\t{(DateTime.Now).ToLongTimeString()}");
                Close();
                mainWindow.Show();
            }
            else
            {
                snackBar.MessageQueue?.Enqueue
                    ("Incorrect login or password", null, null, null, false, true, TimeSpan.FromSeconds(3));
                App.logWriter!.WriteLine($"Auth window: incorrect login or password\t\t\t{(DateTime.Now).ToLongTimeString()}");
            }
        }
        
        private void registrationButton_Click(object sender, RoutedEventArgs e)
        {
            AuthExceptions.checkLogin(loginBox);
            AuthExceptions.checkPassword(passwordBox);
            AuthExceptions.confirmPassword(passwordBox, confirmPasswordBox);

            if (emailBox.Text != null)
            {
                AuthExceptions.checkEmail(emailBox);
            }

            if (loginBox.Text != "" && passwordBox.Password != "" && confirmPasswordBox.Password != "")
            {
                loginBox.ToolTip = "";
                loginBox.Background = Brushes.Transparent;
                passwordBox.ToolTip = "";
                passwordBox.Background = Brushes.Transparent;
                confirmPasswordBox.ToolTip = "";
                confirmPasswordBox.Background = Brushes.Transparent;
                emailBox.ToolTip = "";
                emailBox.Background = Brushes.Transparent;
                User user = new User();
                user.Login = loginBox.Text;
                user.Password = passwordBox.Password;
                user.Balance = 1000;

                if (email != null) user.Email = emailBox.Text;
                else user.Email = null;

                App.db.Users.Add(user);
                App.db.SaveChanges();
                snackBar.MessageQueue?.Enqueue
                    ("You have been registered!", null, null, null, false, true, TimeSpan.FromSeconds(3));
                App.logWriter!.WriteLine($"Auth window: New registration: ID={user.UserId}\t\t\t\t{(DateTime.Now).ToLongTimeString()}");
                turnOffButtons();
            }
        }

        private void dontHaveAnAccountYetButton_Click(object sender, RoutedEventArgs e)
        {
            confirmPasswordBox.Visibility = Visibility.Visible;
            emailBox.Visibility = Visibility.Visible;
            registrationButton.Visibility = Visibility.Visible;
            returnButton.Visibility = Visibility.Visible;
            loginButton.Visibility = Visibility.Hidden;
            dontHaveAnAccountYetButton.Visibility = Visibility.Hidden;
            App.logWriter!.WriteLine($"Auth window: 'don't have an account yet' button clicked\t\t\t\t{(DateTime.Now).ToLongTimeString()}");
        }

        private void returnButton_Click(object sender, RoutedEventArgs e)
        {
            turnOffButtons();
            App.logWriter!.WriteLine($"Auth window: return button clicked\t\t\t\t{(DateTime.Now).ToLongTimeString()}");
        }

        private void turnOffButtons()
        {
            confirmPasswordBox.Visibility = Visibility.Hidden;
            emailBox.Visibility = Visibility.Hidden;
            registrationButton.Visibility = Visibility.Hidden;
            returnButton.Visibility = Visibility.Hidden;
            loginButton.Visibility = Visibility.Visible;
            loginBox.Text = "";
            passwordBox.Password = "";
        }

        #endregion
        private void onWindowClosed(object sender, EventArgs e)
        {
            if (mainWindow == null)
            {
                Application.Current.Shutdown();
                App.logWriter!.WriteLine($"Auth window closed without authorization\t\t\t\t{(DateTime.Now).ToLongTimeString()}");
            }
            else App.logWriter!.WriteLine($"Auth window closed with authorization\t\t\t\t{(DateTime.Now).ToLongTimeString()}");
        }
    }
}