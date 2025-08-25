using OrdersApp.Databases.OrdersDatabase;
using System.Windows;
using System.Windows.Controls;

namespace OrdersApp.Tabs
{
    public partial class Settings : UserControl
    {
        int changingFlag;
        User user;
        public Settings()
        {
            InitializeComponent();
            changeButton.Focus();
            user = App.db.Users.Where(x => x.Login == AuthWindow.login).FirstOrDefault()!;
            comboBoxWhatToChange.ItemsSource = new List<string>()
            { "Login", "Password", "Email"};
        }

        #region ButtonClick

        private void changeButton_Click(object sender, RoutedEventArgs e)
        {
            if (comboBoxWhatToChange.Text != "") {
                turnOffButtons();
                if (comboBoxWhatToChange.Text == "Login") changingFlag = 1;
                else if (comboBoxWhatToChange.Text == "Password") changingFlag = 2;
                else if (comboBoxWhatToChange.Text == "Email") changingFlag = 3;
                App.logWriter!.WriteLine($"Settings tab: change button clicked. Value to be changed = '{comboBoxWhatToChange.Text}'\t\t\t\t{(DateTime.Now).ToLongTimeString()}");
            }
            else 
            {
                snackBar.MessageQueue?.Enqueue
                    ("Firstly choose something in the box above", null, null, null, false, true, TimeSpan.FromSeconds(3));
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
                    ($"You have successfully changed your {comboBoxWhatToChange.Text.ToLower()}",
                    null, null, null, false, true, TimeSpan.FromSeconds(3));

                App.logWriter!.WriteLine($"Settings tab: user with ID={user.UserId} " +
                    $"changed his {comboBoxWhatToChange.Text.ToLower()}\t\t\t\t{(DateTime.Now).ToLongTimeString()}");
                comboBoxWhatToChange.Text = "";
                textBoxForNewValue.Text = "";
                turnOnButtons();
            }
            else
            {
                snackBar.MessageQueue?.Enqueue
                    ($"Text field above must contain something",
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
