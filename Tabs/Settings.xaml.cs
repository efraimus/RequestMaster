using OrdersApp.Databases.OrdersDatabase;
using RequestMaster.Patterns;
using System.Windows;
using System.Windows.Controls;

namespace OrdersApp.Tabs
{
    public partial class Settings : UserControl
    {
        int changingFlag;
        User user;
        OrdersContext db;
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
                if (comboBoxWhatToChange.Text == "логин") changingFlag = 1;
                else if (comboBoxWhatToChange.Text == "пароль") changingFlag = 2;
                else if (comboBoxWhatToChange.Text == "почта") changingFlag = 3;
                App.logWriter!.WriteLine($"Settings tab: change button clicked. Value to be changed = '{comboBoxWhatToChange.Text}'\t\t\t\t{(DateTime.Now).ToLongTimeString()}");
            }
            else 
            {
                snackBar.MessageQueue?.Enqueue
                    ("сначала выберите что поменять", null, null, null, false, true, TimeSpan.FromSeconds(3));
                App.logWriter!.WriteLine($"Settings tab: change button clicked without choise\t\t\t\t{(DateTime.Now).ToLongTimeString()}");
            }
        }

        private void confirmButton_Click(object sender, RoutedEventArgs e)
        {
            if (textBoxForNewValue.Text != "")
            {
                Dictionary<int, IChanger> changersDict = new()
                {
                    {1, new LoginChanger(textBoxForNewValue, user)},
                    {2, new PasswordChanger(textBoxForNewValue, user)},
                    {3, new EmailChanger(textBoxForNewValue, user)}
                };
                Changing changer = new Changing();
                changer.Change(changersDict[changingFlag]);

                snackBar.MessageQueue?.Enqueue
                    ($"вы изменили  {(comboBoxWhatToChange.Text == "почта" ? "почту" : comboBoxWhatToChange.Text)}",
                    null, null, null, false, true, TimeSpan.FromSeconds(3));

                App.logWriter!.WriteLine($"Settings tab: user with ID={user.UserId} " +
                    $"changed his {comboBoxWhatToChange.Text}\t\t\t\t{(DateTime.Now).ToLongTimeString()}");
                comboBoxWhatToChange.Text = "";
                textBoxForNewValue.Text = "";
                turnOnButtons();
            }
            else
            {
                snackBar.MessageQueue?.Enqueue
                    ($"поле должно содержать текст",
                    null, null, null, false, true, TimeSpan.FromSeconds(3));
            }
        }
        private void returnButton_Click(object sender, RoutedEventArgs e)
        {
            turnOnButtons();
            App.logWriter!.WriteLine($"Settings tab: return button clicked\t\t\t\t{(DateTime.Now).ToLongTimeString()}");
        }
        #endregion

        #region TurnOnOffButtons
        private void turnOffButtons()
        {
            changeButton.Visibility = Visibility.Hidden;
            comboBoxWhatToChange.Visibility = Visibility.Hidden;
            textBoxForNewValue.Visibility = Visibility.Visible;
            confirmButton.Visibility = Visibility.Visible;
            returnButton.Visibility = Visibility.Visible;
        }
        private void turnOnButtons()
        {
            changingFlag = 0;
            changeButton.Visibility = Visibility.Visible;
            comboBoxWhatToChange.Visibility = Visibility.Visible;
            comboBoxWhatToChange.SelectedItem = null;
            textBoxForNewValue.Visibility = Visibility.Hidden;
            confirmButton.Visibility = Visibility.Hidden;
            returnButton.Visibility = Visibility.Hidden;
        }
        #endregion
    }
}
