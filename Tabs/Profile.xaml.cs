﻿using OrdersApp.Databases.OrdersDatabase;
using OrdersApp.Exceptions;
using RequestMaster.Patterns;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;

namespace OrdersApp.Tabs
{
    public partial class Profile : UserControl 
    {
        OrdersContext db;
        public Profile()
        {
            db = DatabaseSingleton.CreateInstance();
            InitializeComponent();
            refreshTab();
        }


        #region ButtonClick
        private void refreshButton_Click(object sender, RoutedEventArgs e)
        {
            refreshTab();
            App.logWriter!.WriteLine($"Profile tab refreshed\t\t\t\t{(DateTime.Now).ToLongTimeString()}");
        }

        private void refreshTab()
        {
            textBoxLogin.Text = $"логин: {AuthWindow.login}";
            textBoxPassword.Text = $"пароль: {AuthWindow.password}";
            textBoxEmail.Text = $"почта: {AuthWindow.email}";
            textBoxBalance.Text = $"баланс: {AuthWindow.balance}";

        }

        private void markAsCompletedButton_Click(object sender, RoutedEventArgs e)
        {
        //    string performer = "";
        //    try
        //    {
        //        if (ordersDataGrid.SelectedItem != null)
        //        {
        //            performer = "employer";
        //            markAsCompleted(performer, ordersDataGrid);
        //        }
        //        else if (ordersDataGrid.SelectedItem != null)
        //        {
        //            performer = "employee";
        //            markAsCompleted(performer, ordersDataGrid);
        //        }
        //        else
        //        {
        //            snackBar1.MessageQueue?.Enqueue
        //            ("Firstly choose an order", null, null, null, false, true, TimeSpan.FromSeconds(3));
        //        }
        //    }
        //    catch (OrderNotFoundException)
        //    {
        //        snackBar1.MessageQueue?.Enqueue
        //            ("Order not found", null, null, null, false, true, TimeSpan.FromSeconds(3));
        //    }
        }

        #endregion

        #region Methods
        private void markAsCompleted(string performer, DataGrid dataGrid)
        {

            Order order = db.Orders.Where(x => x.ID == ((Order)dataGrid.SelectedItem).ID).FirstOrDefault()!;
            User user = db.Users.Where(x => x.Login == AuthWindow.login).FirstOrDefault()!;
            if (order != null)
            {
                if (order.Status == "Awaiting confirmation")
                {
                    closeOrder(order, performer);
                }
                else if (order.Status == "Processing")
                {
                    markAwaitingForConfirmation(order, performer);
                }
                else if (order.Status == "Active")
                {
                    closeOrder(order, "employer");
                }

                db.SaveChanges();
                CollectionViewSource.GetDefaultView(dataGrid.ItemsSource).Refresh();
            }
            else throw new OrderNotFoundException("");
        }

        private void markAwaitingForConfirmation(Order order, string whoMarked)
        {
            if (order.WhoConfirmedCompletion != whoMarked)
            {
                order.WhoConfirmedCompletion = whoMarked;
                order.Status = "Awaiting confirmation";

                snackBar1.MessageQueue?.Enqueue
                    ($"Order {order!.ID} marked as completed. ",
                    null, null, null, false, true, TimeSpan.FromSeconds(3));

                App.logWriter!.WriteLine($"Order {order!.ID}" +
                    $" marked as completed by {whoMarked}\t\t\t\t{(DateTime.Now).ToLongTimeString()}");
            }
            else
            {
                youAlreadyHaveMarkedTheOrderAsCompleted(order);
            }
        }

        private void closeOrder(Order order, string whoClosed)
        {
            User employer = db.Users.Where(x => x.UserId == order.EmployerID).FirstOrDefault()!;
            if (order.WhoConfirmedCompletion != whoClosed)
            {
                order.Status = "Closed";
                employer.OrderID = null;

                snackBar1.MessageQueue?.Enqueue
                    ($"You have closed the order {order!.ID}",
                    null, null, null, false, true, TimeSpan.FromSeconds(3));

                App.logWriter!.WriteLine($"Order {order!.ID}" +
                    $" closed by {whoClosed}\t\t\t\t{(DateTime.Now).ToLongTimeString()}");
            }
            else
            {
                youAlreadyHaveMarkedTheOrderAsCompleted(order);
            }
        }

        private void youAlreadyHaveMarkedTheOrderAsCompleted(Order order)
        {
            snackBar1.MessageQueue?.Enqueue
                                ($"Order {order!.ID} is awating for a confirmation",
                                null, null, null, false, true, TimeSpan.FromSeconds(3));
        }

        #endregion

        #region DataGridSelected
        private void ordersEmployerDataGrid_SelectionChanged(object sender, RoutedEventArgs e)
        {
            //ordersEmployeeDataGrid.UnselectAllCells();
        }
        private void ordersEmployeeDataGrid_SelectionChanged(object sender, RoutedEventArgs e)
        {
            //ordersEmployerDataGrid.UnselectAllCells();
        }
        #endregion
    }
}