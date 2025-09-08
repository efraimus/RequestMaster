using RequestMaster.Databases.MainDatabase;
using RequestMaster.Patterns;
using System.IO;
using System.Windows;
using System.Windows.Media;

namespace RequestMaster
{
    public partial class App : Application
    {
        public static StreamWriter? logWriter;

        string pathForLogging = $"logs\\logs_{(DateTime.Now).ToShortDateString()}.txt";

        public static Brush? backgroundColor { get; set; }

        public static Brush? foregroundColor { get; set; }

        public static Brush? textColor       { get; set; }

        public static Brush buttonsColor     { get; set; } = new SolidColorBrush(Color.FromArgb(255, 98, 0, 238));

        public static FontFamily fontFamily  { get; set; } = new FontFamily("Segoe UI");

        public static double fontSize        { get; set; } = 25.0;

        public static void setLightTheme()
        {
            backgroundColor = new SolidColorBrush(Color.FromArgb(255, 240, 240, 250));
            foregroundColor = new SolidColorBrush(Color.FromArgb(255, 255, 255, 255));
            textColor       = new SolidColorBrush(Color.FromArgb(255, 0, 0, 0));
        }
        public static void setDarkTheme()
        {
            backgroundColor = new SolidColorBrush(Color.FromArgb(255, 80, 90, 100));
            foregroundColor = new SolidColorBrush(Color.FromArgb(255, 40, 44, 52));
            textColor       = new SolidColorBrush(Color.FromArgb(255, 255, 255, 255));
        }
        private void onStartup(object sender, StartupEventArgs e)
        {
            setLightTheme();
            if (!Directory.Exists("logs"))
            {
                Directory.CreateDirectory("logs");
            }

            logWriter = new StreamWriter(pathForLogging, true);

            if (new FileInfo(pathForLogging).Length == 0) 
            {
                logWriter.WriteLine(new string('-', 10 + 20 + 40 + 8));
                logWriter.WriteLine($"| {"меню", 20} | {"действие", 40} | {"время", 8} |");
                logWriter.WriteLine(new string('-', 10 + 20 + 40 + 8));
            }

            logWriter!.WriteLine(new string('-', 10 + 20 + 40 + 8));
            logWriter!.WriteLine($"| {"приложение открыто",45} {"",28} |");
            logWriter!.WriteLine(new string('-', 10 + 20 + 40 + 8));

            RequestsContext db = DatabaseSingleton.CreateInstance();

            db.Database.EnsureCreated();
        }
        private void onExit(object sender, ExitEventArgs e)
        {
            logWriter!.WriteLine(new string('-', 10 + 20 + 40 + 8));
            logWriter!.WriteLine($"| {"приложение закрыто", 45} {"", 28} |");
            logWriter!.WriteLine(new string('-', 10 + 20 + 40 + 8));
            logWriter!.Close();
        }
    }
}
