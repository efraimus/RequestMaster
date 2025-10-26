//using RequestMaster.Databases.MainDatabase;
//using RequestMaster.Patterns;
//using OfficeOpenXml;
//using System.IO;

//namespace RequestMaster.Other
//{
//    public class ImportExcel
//    {
//        RequestsContext db;

//        public void ImportExcel(string filePath)
//        {
//            db = DatabaseSingleton.CreateInstance();
//            using (var package = new ExcelPackage(new FileInfo(filePath)))
//            {
//                var worksheet = package.Workbook.Worksheets[1];
//                var rowCount = worksheet.Dimension.End.Row;
//                var colCount = worksheet.Dimension.End.Column;

//                var headers = Enumerable.Range(1, colCount)
//                    .Select(col => worksheet.Cells[1, col].Text)
//                    .ToList();

//                var entities = new List<Request>();

//                for (int row = 2; row <= rowCount; row++)
//                {
//                    var entity = new Request
//                    {
//                         = worksheet.Cells[row, 1].Text,
//                         = worksheet.Cells[row, 2].Text,
//                        // Добавьте остальные поля
//                    };
//                    entities.Add(entity);
//                }

//                db.Requests.AddRange(entities);
//                db.SaveChanges();
//            }
//        }
//    }
//}
