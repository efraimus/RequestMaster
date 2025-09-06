using RequestMaster.Databases.MainDatabase;
using RequestMaster.Exceptions;
using RequestMaster.Patterns;
using System.Windows;
using System.Windows.Controls;

namespace RequestMaster.Tabs.RequestsTabMVVM.Models
{
    public partial class CreateRequest : UserControl
    {
        CreateRequestMenuLogger logger;
        public CreateRequest()
        {
            logger = new CreateRequestMenuLogger();
            InitializeComponent();
        }

        private bool isFieldsCorrect()
        {
            if (textBoxDescription.Text != "" &&
           textBoxTelephoneNumber.Text != "") return true;

            else return false;
        }

        private void clearFields()
        {
            textBoxDescription.Clear();
            textBoxTelephoneNumber.Clear();
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
                        db.Requests.Add(request);
                        db.SaveChanges();
                        clearFields();
                        snackBar.MessageQueue?.Enqueue
                            ("заявка создана", null, null, null, false, true, TimeSpan.FromSeconds(3));
                        logger.log($"заявка №{id} создана");
                    }

                    else
                    {
                        snackBar.MessageQueue?.Enqueue
                            ("проверьте правильность ввода данных", null, null, null, false, true, TimeSpan.FromSeconds(3));
                        logger.log($"некорректно введены данные");
                    }
                }
            }
            catch (Microsoft.EntityFrameworkCore.DbUpdateException)
            {
                snackBar.MessageQueue?.Enqueue
                        ("ошибка при добавлении данных", null, null, null, false, true, TimeSpan.FromSeconds(3));
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
