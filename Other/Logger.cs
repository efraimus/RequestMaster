using System.IO;

namespace RequestMaster.Other
{
    public class Logger
    {
        static string pathForLogging = $"logs\\logs_{DateTime.Now.ToShortDateString()}.txt";
        static StreamWriter logWriter = new StreamWriter(pathForLogging, true);
        const int firstPadding = 10;
        const int menuNamePadding = 20;
        const int descriptionPadding = 40;
        const int dateTimePadding = 8;
        const int appStartPadding = 45;
        const int appExitPadding = 28;
        public static void createCell()
        {
            logWriter.WriteLine(new string('-', firstPadding + menuNamePadding + descriptionPadding + dateTimePadding));
        }
        public static void createFirstLog()
        {
            if (new FileInfo(pathForLogging).Length == 0)
            {
                createCell();
                logWriter.WriteLine($"| {"меню",menuNamePadding} | {"действие",descriptionPadding} | {"время",dateTimePadding} |");
                createCell();
            }
        }
        public static void createAppStartLog()
        {
            createCell();
            logWriter.WriteLine($"| {"приложение открыто",appStartPadding} {"",appExitPadding} |");
            createCell();
        }

        public static void createAppExitLog()
        {
            createCell();
            logWriter.WriteLine($"| {"приложение закрыто",appStartPadding} {"",appExitPadding} |");
            createCell();
            logWriter.Close();
        }
        public void createLog(string str, string menuName)
        {
            logWriter.WriteLine($"| {menuName, menuNamePadding} | " +
                $"{str, descriptionPadding} | " +
                $"{DateTime.Now.ToLongTimeString(), dateTimePadding} |");
        }
    }
    
    public class AuthorizationLogger : Logger
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