using RequestMaster.Databases.MainDatabase;
using System.Windows.Controls;

namespace RequestMaster.Patterns
{
    class FactoryForSettings
    {
        public static IChanger createChanger(string whatToChange, TextBox textBoxForNewValue, PasswordBox passwordBoxForNewValue, User user) 
        {
            switch (whatToChange)
            {
                case "логин":
                    return new LoginChanger(textBoxForNewValue, user);
                case "пароль":
                    return new PasswordChanger(passwordBoxForNewValue, user);
                case "почта":
                    return new EmailChanger(textBoxForNewValue, user);
                case "тема": 
                    return new ThemeChanger(user);
                default: return null!;
            }
        }
    }
}
