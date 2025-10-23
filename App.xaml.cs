using RequestMaster.Databases.MainDatabase;
using RequestMaster.Patterns;
using System.IO;
using System.Windows;
using System.Windows.Media;

namespace RequestMaster
{
    public partial class App : Application
    {
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
        private void onStartup(object sender, StartupEventArgs e)
        {
            setLightTheme();
            if (!Directory.Exists("logs"))
            {
                Directory.CreateDirectory("logs");
            }
            Logger.createFirstLog();
            Logger.createAppStartLog();
            RequestsContext db = DatabaseSingleton.CreateInstance();

            db.Database.EnsureCreated();
        }
        private void onExit(object sender, ExitEventArgs e)
        {
            Logger.createAppExitLog();
        }
    }
}
