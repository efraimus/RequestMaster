using System.Windows.Controls;
using System.Windows.Media;

namespace RequestMaster.Exceptions
{
    class RequestCreatingExceptions
    {

        public static void checkDescription(TextBox textBoxDescription)
        {
            if (textBoxDescription.Text == "")
            {
                markMistake(textBoxDescription, "Description can't be empty");
            }
        }

        public static void checkTelephoneNumber(TextBox textBoxTelephoneNumber)
        {
            if (textBoxTelephoneNumber.Text == "")
            {
                markMistake(textBoxTelephoneNumber, "Telephone number can't be empty");
                return;
            }
            else if (textBoxTelephoneNumber.Text.Length != 11)
            {
                markMistake(textBoxTelephoneNumber, "Telephone can contain at least and no more than 11 characters");
                return;
            }
            else
            {
                foreach (char x in textBoxTelephoneNumber.Text)
                {
                    if (Char.IsControl(x) || Char.IsLetter(x))
                    {
                        markMistake(textBoxTelephoneNumber, "Telephone number must not contain any characters except digits");
                    }
                }
                return;
            }
        }

        private static void markMistake(TextBox textBox, string str)
        {
            textBox.ToolTip = "Telephone number must not contain any characters except digits";
            textBox.Background = Brushes.DarkRed;
            textBox.Text = "";
        }
    }
}
