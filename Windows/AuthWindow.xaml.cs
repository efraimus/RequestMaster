using RequestMaster.Databases.MainDatabase;
using RequestMaster.ViewModels;
using RequestMaster.Patterns;
using System.Windows;
using System.Windows.Media;

namespace RequestMaster
{
    public partial class AuthWindow : Window
    {
        public static string? login;
        public static string? password;
        public static string? email;
        public static int balance;
        public static int userID;
        MainWindow? mainWindow;
        RequestsContext db;

        public AuthWindow()
        {
            db = DatabaseSingleton.CreateInstance();
            InitializeComponent();
            loginButton.Focus();
            confirmPasswordBox.Visibility = Visibility.Hidden;
        }
        #region ButtonClick

        private void loginButton_Click(object sender, RoutedEventArgs e)
        {
            login = loginBox.Text;
            password = passwordBox.Password;
            User authUser = db.Users.Where(x => x.Login == login && x.Password == password).FirstOrDefault()!;
            if (authUser != null)
            {
                email = authUser.Email;
                userID = authUser.UserID;
                mainWindow = new MainWindow();
                mainWindow.DataContext = new MainWindowViewModel();
                App.logWriter!.WriteLine($"авторизация: зашел пользователь с ID={authUser.UserID} \t\t\t\t{(DateTime.Now).ToLongTimeString()}");
                Close();
                mainWindow.Show();
            }
            else
            {
                snackBar.MessageQueue?.Enqueue
                    ("неверный логин или пароль", null, null, null, false, true, TimeSpan.FromSeconds(3));
                App.logWriter!.WriteLine($"авторизация: неверный логин или пароль\t\t\t{(DateTime.Now).ToLongTimeString()}");
            }
        }

        private void dontHaveAnAccountYetButton_Click(object sender, RoutedEventArgs e)
        {
            turnOnRegistrationButtons();
            App.logWriter!.WriteLine($"авторизация: нажата кнопка у меня нет аккаунта\t\t\t\t{(DateTime.Now).ToLongTimeString()}");
        }

        private void returnButton_Click(object sender, RoutedEventArgs e)
        {
            turnOffRegistrationButtons();
            App.logWriter!.WriteLine($"авторизация: нажата кнопка назад\t\t\t\t{(DateTime.Now).ToLongTimeString()}");
        }

        private void registrationButton_Click(object sender, RoutedEventArgs e)
        {
            AuthExceptions.checkLogin(loginBox);
            AuthExceptions.checkPassword(passwordBox);
            AuthExceptions.confirmPassword(passwordBox, confirmPasswordBox);
            if (emailBox.Text != "")
            {
                AuthExceptions.checkEmail(emailBox);
            }
            else if (loginBox.Text != "" && passwordBox.Password != "" && confirmPasswordBox.Password != "")
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


                if (emailBox.Text != "")
                {
                    user.Email = emailBox.Text;
                }

                db.Users.Add(user);
                db.SaveChanges();
                snackBar.MessageQueue?.Enqueue
                    ("вы зарегистрировались", null, null, null, false, true, TimeSpan.FromSeconds(3));
                App.logWriter!.WriteLine($"авторизация: новый пользователь с ID={user.UserID}\t\t\t\t{(DateTime.Now).ToLongTimeString()}");
                turnOffRegistrationButtons();
            }
        }

        private void turnOnRegistrationButtons()
        {
            confirmPasswordBox.Visibility = Visibility.Visible;
            checkBoxRememberMe.Visibility = Visibility.Hidden;
            emailBox.Visibility = Visibility.Visible;
            registrationButton.Visibility = Visibility.Visible;
            returnButton.Visibility = Visibility.Visible;
            loginButton.Visibility = Visibility.Hidden;
            loginButton.Margin = new Thickness(20);
            dontHaveAnAccountYetButton.Visibility = Visibility.Hidden;
        }


        private void turnOffRegistrationButtons()
        {
            confirmPasswordBox.Visibility = Visibility.Hidden;
            emailBox.Visibility = Visibility.Hidden;
            registrationButton.Visibility = Visibility.Hidden;
            returnButton.Visibility = Visibility.Hidden;
            loginButton.Visibility = Visibility.Visible;
            checkBoxRememberMe.Visibility = Visibility.Visible;
            loginButton.Margin = new Thickness(100);
            loginBox.Text = "";
            loginBox.Background = Brushes.White;
            passwordBox.Background = Brushes.White;
            passwordBox.Password = "";
            dontHaveAnAccountYetButton.Visibility = Visibility.Visible;
        }

        #endregion
        private void onWindowClosed(object sender, EventArgs e)
        {
            if (mainWindow == null)
            {
                Application.Current.Shutdown();
                App.logWriter!.WriteLine($"авторизация: окно закрыто без авторизации\t\t\t\t{(DateTime.Now).ToLongTimeString()}");
            }
            else App.logWriter!.WriteLine($"авторизация: окно закрыто после авторизации\t\t\t\t{(DateTime.Now).ToLongTimeString()}");
        }
    }
}