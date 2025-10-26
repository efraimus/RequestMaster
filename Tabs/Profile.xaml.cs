﻿using RequestMaster.Databases.MainDatabase;
using RequestMaster.Other;
using RequestMaster.Patterns;
using System.Windows;
using System.Windows.Controls;

namespace RequestMaster.Tabs
{
    public partial class Profile : UserControl 
    {
        RequestsContext db;
        ProfileMenuLogger logger;
        List<Request>? requestsList;
        public Profile()
        {
            InitializeComponent();
            logger = new ProfileMenuLogger();
            db = DatabaseSingleton.CreateInstance();
            textBlockLogin.Text = $"{AuthWindow.user!.Login}";
            textBlockPassword.Text = $"{AuthWindow.user.Password}";
            textBlockEmail.Text = $"{AuthWindow.user.Email}";
            requestsList = db.Requests.ToList();

            textBlockHowManyRequestsCreated.Text = $"{requestsList.
                Where(x => x.WhoCreatedID == AuthWindow.user.UserID).Count()}";

            textBlockHowManyRequestsClosed.Text = $"{requestsList.
                Where(x => x.WhoClosedID == AuthWindow.user.UserID).Count()}";
        }

        private void logOutButton_Click(object sender, RoutedEventArgs e)
        {
            Properties.Settings.Default.login = string.Empty;
            Properties.Settings.Default.password = string.Empty;
            Properties.Settings.Default.Save();
            AuthWindow authWindow = new AuthWindow();
            authWindow.Show();
            MainWindow.authWindowOpened = true;
            Window.GetWindow(this).Close();
        }
    }
}