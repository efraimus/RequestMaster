using RequestMaster.Databases.MainDatabase;
using RequestMaster.Patterns;
using System.Windows;
using System.Windows.Controls;

namespace RequestMaster.Tabs
{
    public partial class Settings : UserControl
    {
        IChanger? changerStrategy;
        User user;
        RequestsContext db;
        public Settings()
        {
            db = DatabaseSingleton.CreateInstance();
            InitializeComponent();
            changeButton.Focus();
            user = db.Users.Where(x => x.Login == AuthWindow.login).FirstOrDefault()!;
            comboBoxWhatToChange.ItemsSource = new List<string>()
            { "логин", "пароль", "почта"};
        }

        #region ButtonClick

        private void changeButton_Click(object sender, RoutedEventArgs e)
        {
            if (comboBoxWhatToChange.Text != "") {
                turnOffButtons();
                if (comboBoxWhatToChange.Text == "логин") changerStrategy = new LoginChanger(textBoxForNewValue, user);
                else if (comboBoxWhatToChange.Text == "пароль") changerStrategy = new PasswordChanger(passwordBoxForNewValue, user);
                else if (comboBoxWhatToChange.Text == "почта") changerStrategy = new EmailChanger(textBoxForNewValue, user);
                App.logWriter!.WriteLine($"настройки: нажата кнопка изменить, значение='{comboBoxWhatToChange.Text}'\t\t\t\t{(DateTime.Now).ToLongTimeString()}");
            }
            else 
            {
                snackBar.MessageQueue?.Enqueue
                    ("сначала выберите что поменять", null, null, null, false, true, TimeSpan.FromSeconds(3));
                App.logWriter!.WriteLine($"настройки: нажата кнопка изменить без значения\t\t\t\t{(DateTime.Now).ToLongTimeString()}");
            }
        }

        private void confirmButton_Click(object sender, RoutedEventArgs e)
        {
            if (textBoxForNewValue.Text != "" || passwordBoxForNewValue.Password != "")
            {
                Changing changing = new Changing();
                changing.Change(changerStrategy!);

                string whatChanged = comboBoxWhatToChange.Text == "почта" ? "почту" : comboBoxWhatToChange.Text;

                snackBar.MessageQueue?.Enqueue
                    ($"вы изменили {whatChanged}",
                    null, null, null, false, true, TimeSpan.FromSeconds(3));

                App.logWriter!.WriteLine($"настройки: пользователь с ID={user.UserID} " +
                    $"поменял {whatChanged}\t\t\t\t{(DateTime.Now).ToLongTimeString()}");
                comboBoxWhatToChange.Text = "";
                textBoxForNewValue.Text = "";
                turnOnButtons();
            }
            else
            {
                snackBar.MessageQueue?.Enqueue
                    ($"поле должно содержать текст",
                    null, null, null, false, true, TimeSpan.FromSeconds(3));
                App.logWriter!.WriteLine($"настройки: нажата кнопка подтвердить с пустым полем\t\t\t\t{(DateTime.Now).ToLongTimeString()}");

            }
        }
        private void returnButton_Click(object sender, RoutedEventArgs e)
        {
            turnOnButtons();
            App.logWriter!.WriteLine($"настройки: нажата кнопка назад\t\t\t\t{(DateTime.Now).ToLongTimeString()}");
        }
        #endregion

        #region TurnOnOffButtons
        private void turnOffButtons()
        {
            if (comboBoxWhatToChange.Text == "пароль")
            {
                passwordBoxForNewValue.Visibility = Visibility.Visible;
                textBoxForNewValue.Visibility = Visibility.Hidden;
            }
            else
            {
                textBoxForNewValue.Visibility = Visibility.Visible;
            }

            changeButton.Visibility = Visibility.Hidden;
            comboBoxWhatToChange.Visibility = Visibility.Hidden;
            confirmButton.Visibility = Visibility.Visible;
            returnButton.Visibility = Visibility.Visible;
        }
        private void turnOnButtons()
        {
            changeButton.Visibility = Visibility.Visible;
            comboBoxWhatToChange.Visibility = Visibility.Visible;
            comboBoxWhatToChange.SelectedItem = null;
            textBoxForNewValue.Visibility = Visibility.Hidden;
            passwordBoxForNewValue.Visibility = Visibility.Hidden;
            confirmButton.Visibility = Visibility.Hidden;
            returnButton.Visibility = Visibility.Hidden;
        }
        #endregion
    }
}
