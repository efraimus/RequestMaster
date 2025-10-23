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
        SettingsMenuLogger logger;
        Snackbar snackBar;
        public Settings()
        {
            InitializeComponent();
            snackBar = new Snackbar(snackBarXAML);
            db = DatabaseSingleton.CreateInstance();
            logger = new SettingsMenuLogger();
            changeButton.Focus();
            user = db.Users.Where(x => x.Login == AuthWindow.login).FirstOrDefault()!;
            comboBoxWhatToChange.ItemsSource = new List<string>()
            { "логин", "пароль", "почта"};
        }

        private void changeButton_Click(object sender, RoutedEventArgs e)
        {
            if (comboBoxWhatToChange.Text != "") {
                changerStrategy = FactoryForSettings.createChanger(comboBoxWhatToChange.Text,
                    textBoxForNewValue, passwordBoxForNewValue, user);
                turnOffButtons();

                if (comboBoxWhatToChange.Text == "тема") 
                {
                    turnOnButtons();
                    snackBar.show("изменения вступят в силу после перезагрузки");
                    Changing changing = new Changing();
                    changing.Change(changerStrategy!);
                }

                logger.log($"нажата кнопка изменить, значение='{comboBoxWhatToChange.Text}'");
            }
            else 
            {
                snackBar.show("сначала выберите что поменять");
                logger.log($"нажата кнопка изменить без значения'");
            }
        }
        private void confirmButton_Click(object sender, RoutedEventArgs e)
        {
            if (textBoxForNewValue.Text != "" || passwordBoxForNewValue.Password != "")
            {
                Changing changing = new Changing();
                changing.Change(changerStrategy!);

                string whatChanged = comboBoxWhatToChange.Text == "почта" ? "почту" : comboBoxWhatToChange.Text;
                snackBar.show($"вы изменили {whatChanged}");
                logger.log($"пользователь с ID={user.UserID} поменял {whatChanged}");
                comboBoxWhatToChange.Text = "";
                textBoxForNewValue.Text = "";
                turnOnButtons();
            }
            else
            {
                snackBar.show($"поле должно содержать текст");
                logger.log($"нажата кнопка подтвердить с пустым полем");
            }
        }
        private void returnButton_Click(object sender, RoutedEventArgs e)
        {
            turnOnButtons();
            logger.log($"нажата кнопка назад");
        }
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
    }
}
