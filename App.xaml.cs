using RequestMaster.Databases.MainDatabase;
using RequestMaster.Patterns;
using System.IO;
using System.Windows;
using System.Windows.Media;

namespace RequestMaster
{
    public partial class App : Application
    {
        string pathForLogging = $"logs\\logs_{(DateTime.Now).ToShortDateString()}.txt";
        static StreamWriter? logWriter;

        public static Brush backgroundColor     { get; set; } = new SolidColorBrush(Color.FromArgb(255, 80, 90, 100));
        public static Brush foregroundColor     { get; set; } = new SolidColorBrush(Color.FromArgb(255, 40, 44, 52));
        public static Brush buttonsColor        { get; set; } = new SolidColorBrush(Color.FromArgb(255, 98, 0, 238));
        public static Brush textColor           { get; set; } = new SolidColorBrush(Color.FromArgb(255, 255, 255, 255));
        public static FontFamily fontFamily     { get; set; } = new FontFamily("Segoe UI");
        public static double fontSize           { get; set; } = 25.0;

        public static void log(string str)
        {
            logWriter!.WriteLine($"{str}\t{(DateTime.Now).ToLongTimeString()}");
        }
        private void onStartup(object sender, StartupEventArgs e)
        {
            if (!Directory.Exists("logs"))
            {
                Directory.CreateDirectory("logs");
            }
            logWriter = new StreamWriter(pathForLogging, true);
            RequestsContext db = DatabaseSingleton.CreateInstance();
            db.Database.EnsureCreated();
        }
        private void onExit(object sender, ExitEventArgs e)
        {
            logWriter!.Close();
        }
    }
}
