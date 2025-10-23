using RequestMaster.Databases.MainDatabase;
using RequestMaster.Exceptions;
using RequestMaster.Patterns;
using System.Globalization;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace RequestMaster.Tabs.RequestsTabMVVM.Models
{
    public partial class CreateRequest : UserControl
    {
        CreateRequestMenuLogger logger;
        Snackbar snackBar;
        public CreateRequest()
        {
            InitializeComponent();
            snackBar = new Snackbar(snackBarXAML);
            logger = new CreateRequestMenuLogger();
        }

        private bool isFieldsCorrect()
        {
            logger.log($"проверка полей на правильность ввода");

            if (textBoxDescription.Text != "" &&
           textBoxTelephoneNumber.Text != "") return true;
            else return false;
        }

        private void clearFields()
        {
            textBoxDescription.Clear();
            textBoxDescription.Background = Brushes.Transparent;
            textBoxTelephoneNumber.Clear();
            textBoxTelephoneNumber.Background = Brushes.Transparent;
            logger.log($"поля очищены");
        }

        public void createRequestButton2_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                RequestCreatingExceptions.checkDescription(textBoxDescription);
                RequestCreatingExceptions.checkTelephoneNumber(textBoxTelephoneNumber);

                using (RequestsContext db = new RequestsContext())
                {
                    int id = db.Requests.Count() + 1;
                    if (isFieldsCorrect())
                    {
                        Request request = new Request();
                        request.Description = textBoxDescription.Text;
                        request.TelephoneNumber = textBoxTelephoneNumber.Text;
                        request.Status = "активна";
                        request.WhoCreatedID = AuthWindow.user!.UserID;
                        request.CreationDate = DateTime.Now;
                        db.Requests.Add(request);
                        db.SaveChanges();
                        clearFields();
                        snackBar.show("заявка создана");
                        logger.log($"заявка №{id} создана");
                    }

                    else
                    {
                        snackBar.show("проверьте правильность ввода данных");
                        logger.log($"некорректно введены данные");
                    }
                }
            }
            catch (Microsoft.EntityFrameworkCore.DbUpdateException)
            {
                snackBar.show("ошибка при добавлении данных");
                logger.log($"ошибка при добавлении данных");
            }
        }
        private void returnButton_Click(object sender, RoutedEventArgs e)
        {
            Requests.requestsMenu.Visibility = Visibility.Visible;
            logger.log($"нажата кнопка назад");
        }
    }
}
