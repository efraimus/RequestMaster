using System.Windows.Controls;
using System.Windows.Media;

namespace OrdersApp.Exceptions
{
    class OrderCreatingExceptions
    {
        public static void checkPayment(TextBox textBoxPayment)
        {
            if (textBoxPayment.Text == "")
            {
                textBoxPayment.ToolTip = "Payment can't be empty";
                textBoxPayment.Background = Brushes.DarkRed;
                return;
            }
            else
            {
                foreach (char x in textBoxPayment.Text)
                {
                    if (Char.IsControl(x) || Char.IsLetter(x))
                    {
                        textBoxPayment.ToolTip = "Payment must not contain any characters except digits";
                        textBoxPayment.Background = Brushes.DarkRed;
                        textBoxPayment.Text = "";
                    }
                }
            }
        }

        public static void checkDescription(TextBox textBoxDescription)
        {
            if (textBoxDescription.Text == "")
            {
                textBoxDescription.ToolTip = "Description can't be empty";
                textBoxDescription.Background = Brushes.DarkRed;
                textBoxDescription.Text = "";
            }
        }

        public static void checkTelephoneNumber(TextBox textBoxTelephoneNumber)
        {
            if (textBoxTelephoneNumber.Text == "")
            {
                textBoxTelephoneNumber.ToolTip = "Telephone number can't be empty";
                textBoxTelephoneNumber.Background = Brushes.DarkRed;
                textBoxTelephoneNumber.Text = "";
                return;
            }
            else if (textBoxTelephoneNumber.Text.Length != 11)
            {
                textBoxTelephoneNumber.ToolTip = "Telephone can contain at least and no more than 11 characters";
                textBoxTelephoneNumber.Background = Brushes.DarkRed;
                textBoxTelephoneNumber.Text = "";
                return;
            }
            else
            {
                foreach (char x in textBoxTelephoneNumber.Text)
                {
                    if (Char.IsControl(x) || Char.IsLetter(x))
                    {
                        textBoxTelephoneNumber.ToolTip = "Telephone number must not contain any characters except digits";
                        textBoxTelephoneNumber.Background = Brushes.DarkRed;
                        textBoxTelephoneNumber.Text = "";
                    }
                }
                return;
            }
        }
        public static void checkCountOfPeopleNeeded(TextBox textBoxCountOfPeopleNeeded)
        {
            if (textBoxCountOfPeopleNeeded.Text == "")
            {
                textBoxCountOfPeopleNeeded.ToolTip = "Count of people can't be empty";
                textBoxCountOfPeopleNeeded.Background = Brushes.DarkRed;
                textBoxCountOfPeopleNeeded.Text = "";
                return;
            }
            else
            {
                foreach (char x in textBoxCountOfPeopleNeeded.Text)
                {
                    if (Char.IsControl(x) || Char.IsLetter(x))
                    {
                        textBoxCountOfPeopleNeeded.ToolTip = "Count of people needed must not contain any characters except digits";
                        textBoxCountOfPeopleNeeded.Background = Brushes.DarkRed;
                        textBoxCountOfPeopleNeeded.Text = "";
                    }
                }
            }
        }
    }
}
