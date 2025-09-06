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
        public static User? user;
        AuthorizationLogger logger;
        MainWindow? mainWindow;
        RequestsContext db;

        public AuthWindow()
        {
            db = DatabaseSingleton.CreateInstance();
            logger = new AuthorizationLogger();
            InitializeComponent();
            loginButton.Focus();
            confirmPasswordBox.Visibility = Visibility.Hidden;
        }

        private void loginButton_Click(object sender, RoutedEventArgs e)
        {
            login = loginBox.Text;
            password = passwordBox.Password;
            user = db.Users.Where(x => x.Login == login && x.Password == password).FirstOrDefault()!;
            if (user != null)
            {
                mainWindow = new MainWindow();
                mainWindow.DataContext = new MainWindowViewModel();
                logger.log($"вход ID={user.UserID}");
                Close();
                mainWindow.Show();
            }
            else
            {
                snackBar.MessageQueue?.Enqueue
                    ("неверный логин или пароль", null, null, null, false, true, TimeSpan.FromSeconds(3));
                logger.log($"неверный логин или пароль");
            }
        }

        private void dontHaveAnAccountYetButton_Click(object sender, RoutedEventArgs e)
        {
            turnOnRegistrationButtons();
            logger.log($"нажата кнопка у меня нет аккаунта");
        }

        private void returnButton_Click(object sender, RoutedEventArgs e)
        {
            turnOffRegistrationButtons();
            logger.log($"нажата кнопка назад");
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
                user.Theme = "Light";


                if (emailBox.Text != "")
                {
                    user.Email = emailBox.Text;
                }

                db.Users.Add(user);
                db.SaveChanges();
                snackBar.MessageQueue?.Enqueue
                    ("вы зарегистрировались", null, null, null, false, true, TimeSpan.FromSeconds(3));
                logger.log($"новый пользователь с ID={user.UserID}");
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
            loginBox.Background = Brushes.Transparent;
            passwordBox.Background = Brushes.Transparent;
            passwordBox.Password = "";
            dontHaveAnAccountYetButton.Visibility = Visibility.Visible;
        }

        private void onWindowClosed(object sender, EventArgs e)
        {
            if (mainWindow == null)
            {
                Application.Current.Shutdown();
                logger.log($"окно закрыто без авторизации");
            }
            else logger.log($"окно закрыто после авторизации");
        }
    }
}