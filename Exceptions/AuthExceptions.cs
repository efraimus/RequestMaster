using RequestMaster.Databases.MainDatabase;
using RequestMaster.Patterns;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using static MaterialDesignThemes.Wpf.Theme;
class AuthExceptions : Window
{
    static RequestsContext db;
    public static void checkLogin(TextBox loginBox)
    {
        db = DatabaseSingleton.CreateInstance();
        if (loginBox.Text.Length < 3)
        {
            markMistake(loginBox, "логин должен быть длиннее 3 символов");
        }
        if (db.Users.Where(x => x.Login == loginBox.Text) != null)
        {
            markMistake(loginBox, "такой логин уже существует");
        }
    }
    public static void checkPassword(PasswordBox passwordBox)
    {
        if (passwordBox.Password.Length < 3)
        {
            markMistake(passwordBox, "пароль должен быть длиннее 3 символов");
        }
    }
    public static void confirmPassword(PasswordBox passwordBox, PasswordBox confirmPasswordBox)
    {
        if (passwordBox.Password != confirmPasswordBox.Password)
        {
            markMistake(confirmPasswordBox, "пароль должен быть длиннее 3 символов");
        }
    }
    public static void checkEmail(TextBox emailBox)
    {
        if (emailBox.Text != null && !emailBox.Text.Contains('@'))
        {
            markMistake(emailBox, "в почте должен быть символ @");
        }
    }

    private static void markMistake(TextBox textBox, string toolTip)
    {
        textBox.ToolTip = toolTip;
        textBox.Background = Brushes.DarkRed;
        textBox.Text = "";
    }
    private static void markMistake(PasswordBox passwordBox, string toolTip)
    {
        passwordBox.ToolTip = toolTip;
        passwordBox.Background = Brushes.DarkRed;
        passwordBox.Text = "";
    }

}