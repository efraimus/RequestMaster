using OrdersApp.Databases.OrdersDatabase;
using System.IO;
using System.Windows;
using System.Windows.Media;

namespace OrdersApp
{
    public partial class App : Application
    {
        public static OrdersContext db;
        public static readonly string pathForLogging = $"..\\..\\..\\Logs\\logs_{(DateTime.Now).ToShortDateString()}.txt";
        public static StreamWriter? logWriter;

        public static Brush colorOfBackground { get; set; } = new SolidColorBrush(Color.FromArgb(255, 255, 255, 255));
        public static Brush colorOfButtons { get; set; } = new SolidColorBrush(Color.FromArgb(255, 98, 0, 238));
        public static FontFamily fontFamily { get; set; } = new FontFamily("Segoe UI");
        public static double fontSize { get; set; } = 25.0;

        private void onStartup(object sender, StartupEventArgs e)
        {
            db = new OrdersContext();
            logWriter = Logging.createFileForLogging();
        }
        private void onExit(object sender, ExitEventArgs e)
        {
            logWriter!.Close();
        }
    }
}
