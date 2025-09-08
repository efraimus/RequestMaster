using System.Windows;

namespace RequestMaster
{
    public partial class MainWindow : Window
    {
        public static bool authWindowOpened = false;
        public MainWindow()
        {
            InitializeComponent();
        }

        private void OnWindowClosed(object sender, EventArgs e)
        {
            if (!authWindowOpened)
            {
                Application.Current.Shutdown();
            }
        }
    }
}