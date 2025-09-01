using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
class AuthExceptions : Window
{
    public static void checkLogin(TextBox loginBox)
    {
        if (loginBox.Text.Length < 3)
        {
            loginBox.ToolTip = "Login must be longer than 3 symbols";
            loginBox.Background = Brushes.DarkRed;
            loginBox.Text = "";
        }
    }
    #region makePassword
    public static void checkPassword(PasswordBox passwordBox)
    {
        if (passwordBox.Password.Length < 3)
        {
            passwordBox.ToolTip = "Пароль должен быть длиннее 3 символов";
            passwordBox.Background = Brushes.DarkRed;
            passwordBox.Password = "";
        }
    }
    public static void checkPassword(TextBox textBox)
    {
        if (textBox.Text.Length < 3)
        {
            textBox.ToolTip = "Пароль должен быть длиннее 3 символов";
            textBox.Background = Brushes.DarkRed;
            textBox.Text = "";
        }
    }
    #endregion
    public static void confirmPassword(PasswordBox passwordBox, PasswordBox confirmPasswordBox)
    {
        if (passwordBox.Password != confirmPasswordBox.Password)
        {
            confirmPasswordBox.ToolTip = "Passwords doesn't match";
            confirmPasswordBox.Background = Brushes.DarkRed;
            confirmPasswordBox.Password = "";
        }
    }
    public static void checkEmail(TextBox emailBox)
    {
        if (emailBox.Text != null && !emailBox.Text.Contains('@'))
        {
            emailBox.ToolTip = "Email must contain @ symbol";
            emailBox.Background = Brushes.DarkRed;
            emailBox.Text = "";
        }
    }
}