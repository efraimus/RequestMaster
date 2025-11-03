using Microsoft.EntityFrameworkCore;
using Microsoft.Win32;
using OfficeOpenXml;
using RequestMaster.Databases.MainDatabase;
using RequestMaster.Patterns;
using RequestMaster.Tabs;
using System.IO;
using System.Windows;

namespace RequestMaster.Other
{
    public class ImportExcel
    {

        static RequestsContext db;
        public static List<Request> ReadExcelRequests(string excelPath)
        {
            var requests = new List<Request>();
            ExcelPackage.License.SetNonCommercialPersonal("My Name");
            using (var package = new ExcelPackage(new FileInfo(excelPath)))
            {
                var worksheet = package.Workbook.Worksheets[0];
                var rowCount = worksheet.Dimension.End.Row;

                for (int row = 2; row <= rowCount; row++)
                {
                    var request = new Request
                    {
                        RequestID = Convert.ToInt32(worksheet.Cells[row, 1].Value),
                        Description = worksheet.Cells[row, 2].Text,
                        TelephoneNumber = worksheet.Cells[row, 3].Text,
                        Status = worksheet.Cells[row, 4].Text,
                        WhoCreatedID = Convert.ToInt32(worksheet.Cells[row, 5].Value),
                        WhoClosedID = Convert.ToInt32(worksheet.Cells[row, 6].Value),
                        WhoOpenedID = Convert.ToInt32(worksheet.Cells[row, 7].Value),
                        CreationDate = DateTime.Parse(worksheet.Cells[row, 8].Text)
                    };

                    requests.Add(request);
                }
            }

            return requests;
        }
        private static List<Request> ReadJsonRequests(string jsonPath)
        {
            try
            {
                var jsonString = File.ReadAllText(jsonPath);
                return System.Text.Json.JsonSerializer.Deserialize<List<Request>>(jsonString);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public static void ImportRequests()
        {
            db = DatabaseSingleton.CreateInstance();
            try
            {
                OpenFileDialog dialog = new OpenFileDialog
                {
                    Filter = "Excel файлы (*.xlsx)|*.xlsx|JSON файлы (*.json)|*.json",
                    FilterIndex = 1,
                    RestoreDirectory = true
                };


                if (dialog.ShowDialog() == true)
                {
                    string filePath = dialog.FileName;

                    string fileExtension = Path.GetExtension(filePath).ToLower();

                    List<Request> requests;

                    switch (fileExtension)
                    {
                        case ".xlsx":
                            requests = ReadExcelRequests(filePath);
                            break;
                        case ".json":
                            requests = ReadJsonRequests(filePath);
                            break;
                        default:
                            MessageBox.Show("неподдерживаемый формат файла");
                            return;
                    }
                    using (var transaction = db.Database.BeginTransaction())
                    {
                        db.Requests.AddRange(requests);
                        db.SaveChanges();
                        transaction.Commit();
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"{ex.Message}");
            }
        }
    }
}
