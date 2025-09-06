namespace RequestMaster.Patterns
{
    class Logger
    {
        public void createLog(string str, string menuName)
        {
            App.logWriter!.WriteLine($"| {menuName, 20} | {str, 40} | {(DateTime.Now).ToLongTimeString(), 8} |");
        }
    }
    
    class AuthorizationLogger : Logger
    {
        public void log(string str)
        {
            createLog(str, "авторизация");
        }
    }

    class RequestsMenuLogger : Logger 
    {
        public void log(string str)
        {
            createLog(str, "заявки");
        }
    }

    class CreateRequestMenuLogger : Logger
    {
        public void log(string str)
        {
            createLog(str, "создание заявки");
        }
    }

    class RequestDetailsMenuLogger : Logger
    {
        public void log(string str)
        {
            createLog(str, "детали заявки");
        }
    }

    class ProfileMenuLogger : Logger
    {
        public void log(string str)
        {
            createLog(str, "профиль");
        }
    }

    class SettingsMenuLogger : Logger
    {
        public void log(string str)
        {
            createLog(str, "настройки");
        }
    }
}