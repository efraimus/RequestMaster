using RequestMaster.Databases.MainDatabase;
using RequestMaster.Patterns;
using RequestMaster.ViewModels;
using System.IO;
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
        Snackbar snackBar;

        public AuthWindow()
        {
            InitializeComponent();
            snackBar = new Snackbar(snackBarXAML);
            db = DatabaseSingleton.CreateInstance();
            logger = new AuthorizationLogger();
            loginButton.Focus();
            confirmPasswordBox.Visibility = Visibility.Hidden;

            if (!new FileInfo("requests.db").Exists)
            {
                Properties.Settings.Default.login = string.Empty;
                Properties.Settings.Default.password = string.Empty;
            }

            if (Properties.Settings.Default.login != string.Empty)
            {
                try
                {
                    login = Properties.Settings.Default.login;
                    password = Properties.Settings.Default.password;
                    loginMethod();
                    Close();
                    mainWindow!.Show();
                }
                catch (InvalidOperationException)
                {

                }
            }
        }

        public void loginMethod()
        {
            user = db.Users.Where(x => x.Login == login && x.Password == password).First();
            email = user.Email;
            mainWindow = new MainWindow();
            mainWindow.DataContext = new MainWindowViewModel();
            logger.log($"вход ID={user.UserID}");
        }

        private void loginButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                login = loginBox.Text;
                password = passwordBox.Password;
                loginMethod();

                if (checkBoxRememberMe.IsChecked == true)
                {
                    Properties.Settings.Default.login = login;
                    Properties.Settings.Default.password = password;
                    Properties.Settings.Default.Save();
                }

                Close();
                mainWindow!.Show();
            }
            catch (InvalidOperationException)
            {
                snackBar.show("неверный логин или пароль");
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
                user.Theme = "светлая";


                if (emailBox.Text != "")
                {
                    user.Email = emailBox.Text;
                }

                db.Users.Add(user);
                db.SaveChanges();
                snackBar.show("вы зарегистрировались");
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