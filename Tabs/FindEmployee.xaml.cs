using OrdersApp.Databases.OrdersDatabase;
using OrdersApp.Exceptions;
using RequestMaster.Patterns;
using System.Windows;
using System.Windows.Controls;

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

        private void clearFields()
        {
            textBoxPayment.Clear();
            textBoxDescription.Clear();
            textBoxTelephoneNumber.Clear();
            textBoxCountOfPeopleNeeded.Clear();
        }

        private void addOrder_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                OrderCreatingExceptions.checkPayment(textBoxPayment);
                OrderCreatingExceptions.checkDescription(textBoxDescription);
                OrderCreatingExceptions.checkTelephoneNumber(textBoxTelephoneNumber);
                OrderCreatingExceptions.checkCountOfPeopleNeeded(textBoxCountOfPeopleNeeded);
                using (OrdersContext db = new OrdersContext())
                {
                    int id = db.Orders.Count() + 1;
                    if (isFieldsCorrect())
                    {
                        Order order = new Order();
                        order.Payment = int.Parse(textBoxPayment.Text);
                        order.Description = textBoxDescription.Text;
                        order.TelephoneNumber = textBoxTelephoneNumber.Text;
                        order.CountOfPeopleNeeded = int.Parse(textBoxCountOfPeopleNeeded.Text);
                        order.Status = "Active";
                        order.EmployerID = AuthWindow.userID;
                        db.Orders.Add(order);
                        db.SaveChanges();
                        clearFields();
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
            }
            catch (Microsoft.EntityFrameworkCore.DbUpdateException)
            {
                snackBar.MessageQueue?.Enqueue
                        ("Error while updating db", null, null, null, false, true, TimeSpan.FromSeconds(3));
            }
        }
    }
}
