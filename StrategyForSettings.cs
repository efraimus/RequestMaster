using OrdersApp.Databases.OrdersDatabase;
using System.Windows.Controls;
using System.Windows.Media;

namespace OrdersApp
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
               App.db.SaveChanges();
               AuthWindow.login = user.Login;
           }
        }
    }
    class PasswordChanger : IChanger
    {
        TextBox textBoxForNewValue;
        User user;
        public PasswordChanger(TextBox textBoxForNewValue, User user)
        {
            this.textBoxForNewValue = textBoxForNewValue;
            this.user = user;
        }
        public void Change()
        {
            AuthExceptions.checkPassword(textBoxForNewValue);

            if (textBoxForNewValue.Text != "")
            {
                user.Password = textBoxForNewValue.Text;
                App.db.SaveChanges();
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
                App.db.SaveChanges();
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
