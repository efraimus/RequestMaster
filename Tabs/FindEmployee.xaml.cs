using OrdersApp.Databases.OrdersDatabase;
using OrdersApp.Exceptions;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace OrdersApp.Tabs
{
    public partial class FindEmployee : UserControl
    {
        public FindEmployee()
        {
            InitializeComponent();
            addOrder.Focus();
        }
        private bool isFieldsCorrect()
        {
            if (textBoxPayment.Text != "" &&
                textBoxDescription.Text != "" &&
                textBoxTelephoneNumber.Text != "" &&
                textBoxCountOfPeopleNeeded.Text != "") return true;
            
            else return false;
        }


        private void addOrder_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                OrderCreatingExceptions.checkPayment(textBoxPayment);
                OrderCreatingExceptions.checkDescription(textBoxDescription);
                OrderCreatingExceptions.checkTelephoneNumber(textBoxTelephoneNumber);
                OrderCreatingExceptions.checkCountOfPeopleNeeded(textBoxCountOfPeopleNeeded);
                string status = "Active";
                int id = App.db.Orders.Count() + 1;

                if (isFieldsCorrect())
                {
                    Order order = new Order();
                    order.ID = id;
                    order.Payment = int.Parse(textBoxPayment.Text);
                    order.Description = textBoxDescription.Text;
                    order.TelephoneNumber = textBoxTelephoneNumber.Text;
                    order.CountOfPeopleNeeded = int.Parse(textBoxCountOfPeopleNeeded.Text);
                    order.Status = status;
                    order.EmployerID = AuthWindow.userID;

                    App.db.Orders.Add(order);
                    App.db.SaveChanges();
                    snackBar.MessageQueue?.Enqueue
                        ("Order created!", null, null, null, false, true, TimeSpan.FromSeconds(3));
                    App.logWriter!.WriteLine($"Order number {id} created\t\t\t\t{(DateTime.Now).ToLongTimeString()}");
                }
                else
                {
                    snackBar.MessageQueue?.Enqueue
                        ("Check the accuracy of the entered data", null, null, null, false, true, TimeSpan.FromSeconds(3));
                }
            }
            catch (Microsoft.EntityFrameworkCore.DbUpdateException)
            {
                snackBar.MessageQueue?.Enqueue
                        ("Error while updating db", null, null, null, false, true, TimeSpan.FromSeconds(3));
            }
        }
    }
}
