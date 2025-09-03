using RequestMaster;
using RequestMaster.Databases.MainDatabase;
using System.Windows.Controls;

namespace RequestMaster.Patterns
{
    interface IChanger
    {
       void Change();
    }
    class LoginChanger : IChanger
    {
        TextBox textBoxForNewValue;
        User user;
        public LoginChanger(TextBox textBoxForNewValue, User user)
        {
            this.textBoxForNewValue = textBoxForNewValue;
            this.user = user;
        }
        public void Change()
        {
           AuthExceptions.checkLogin(textBoxForNewValue);

           if (textBoxForNewValue.Text != "")
           {
               user.Login = textBoxForNewValue.Text;
               RequestsContext db = DatabaseSingleton.CreateInstance();
               db.SaveChanges();
               AuthWindow.login = user.Login;
           }
        }
    }
    class PasswordChanger : IChanger
    {
        PasswordBox passwordBoxForNewValue;
        User user;
        public PasswordChanger(PasswordBox passwordBoxForNewValue, User user)
        {
            this.passwordBoxForNewValue = passwordBoxForNewValue;
            this.user = user;
        }
        public void Change()
        {
            AuthExceptions.checkPassword(passwordBoxForNewValue);

            if (passwordBoxForNewValue.Password != "")
            {
                user.Password = passwordBoxForNewValue.Password;
                RequestsContext db = DatabaseSingleton.CreateInstance();
                db.SaveChanges();
                AuthWindow.password = user.Password;
            }
        }
    }
    class EmailChanger : IChanger 
    {
        TextBox textBoxForNewValue;
        User user;
        public EmailChanger(TextBox textBoxForNewValue, User user)
        {
            this.textBoxForNewValue = textBoxForNewValue;
            this.user = user;
        }
        public void Change()
        {
            AuthExceptions.checkEmail(textBoxForNewValue);

            if (textBoxForNewValue.Text != "")
            {
                user.Email = textBoxForNewValue.Text;
                RequestsContext db = DatabaseSingleton.CreateInstance();
                db.SaveChanges();
                AuthWindow.email = user.Email;
            }
        }
    }

    class Changing
    {
        public void Change(IChanger changer)
        {
            changer.Change();
        }
    }
}
