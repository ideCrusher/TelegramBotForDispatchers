using OfficeOpenXml;
using System.Data;


namespace NewTelegramBot
{
    public class ExcelCreator
    {
    

        public static byte[] CompletedRequestCient(DataTable dataRequest, DataTable dataEvent, DataTable dataNote)
        {
            ExcelPackage.LicenseContext = LicenseContext.Commercial;
            ExcelPackage.LicenseContext = LicenseContext.NonCommercial;

            var package = new ExcelPackage();

            var sheet = package.Workbook.Worksheets.Add("Report on month");

            sheet.Cells[1, 1].Value = "Заявки";
            sheet.Cells[1, 10].Value = "Обходы";
            sheet.Cells[1, 15].Value = "Заметки";
            var headerCellss = sheet.Cells[1, 1, 1, 50];
            var headerFonts = headerCellss.Style.Font;
            headerFonts.Bold = true;
            headerFonts.Size = 20;  

            sheet.Cells[2, 1, 2, 7].LoadFromArrays(new object[][] { new[] { "Номер заявки", "Клиент", "Дата и время", "Текст заявки","Статус","Выполненные работы","Заявку выполнил" } });         
            sheet.Cells[2, 10, 2, 13].LoadFromArrays(new object[][] { new[] { "Диспетчер", "Время", "Текст" } });
            sheet.Cells[2, 15, 2, 18].LoadFromArrays(new object[][] { new[] { "Диспетчер", "Время", "Текст" } });
            var headerCells = sheet.Cells[2, 1, 2, 50];
            var headerFont = headerCells.Style.Font;
            headerFont.Bold = true;
            headerFont.Size = 16;           

            var row = 3;
            var column = 1;
            var columnRound = 10;
            var columnNote = 15;
            foreach (DataRow item in dataRequest.Rows)
            {
                sheet.Cells[row, column].Value = item[0];
                sheet.Cells[row, column + 1].Value = item[1].ToString();
                sheet.Cells[row, column + 2].Value = item[2].ToString();
                sheet.Cells[row, column + 3].Value = item[3].ToString();
                sheet.Cells[row, column + 4].Value = item[4].ToString();
                sheet.Cells[row, column + 5].Value = item[5].ToString();
                sheet.Cells[row, column + 6].Value = item[6].ToString();
                row++;
            }
                
            row = 3;       
            foreach (DataRow item in dataEvent.Rows)
            {
                sheet.Cells[row, columnRound].Value = item[0];
                sheet.Cells[row, columnRound + 1].Value = item[1].ToString();
                sheet.Cells[row, columnRound + 2].Value = item[2].ToString();

                row++;
            }

            row = 3;
            foreach (DataRow item in dataNote.Rows)
            {
                sheet.Cells[row, columnNote].Value = item[0];
                sheet.Cells[row, columnNote + 1].Value = item[1].ToString();
                sheet.Cells[row, columnNote + 2].Value = item[2].ToString();

                row++;
            }


            return package.GetAsByteArray();
        }

        public static byte[] CompletedRequestCientNew(DataTable dataRequest, DataTable dataEvent, DataTable dataNote)
        {
            ExcelPackage.LicenseContext = LicenseContext.Commercial;
            ExcelPackage.LicenseContext = LicenseContext.NonCommercial;

            var package = new ExcelPackage();

            var sheet = package.Workbook.Worksheets.Add("Report on month");
            sheet.Cells[1, 1].Value = $"Смена дежурного: {DutyShift.Dispather}";
            var headerCellss = sheet.Cells[1, 1, 1, 50];
            var headerFonts = headerCellss.Style.Font;
            headerFonts.Bold = true;
            headerFonts.Size = 20;

            sheet.Cells[2, 1].Value = "Заявки";
            sheet.Cells[2, 10].Value = "Обходы";
            sheet.Cells[2, 15].Value = "Заметки";
            var headerCellsss = sheet.Cells[2, 2, 2, 50];
            var headerFontss = headerCellss.Style.Font;
            headerFontss.Bold = true;
            headerFontss.Size = 18;

            sheet.Cells[3, 1, 2, 7].LoadFromArrays(new object[][] { new[] { "Номер заявки", "Клиент", "Дата и время", "Текст заявки", "Статус", "Выполненные работы", "Заявку выполнил" } });
            sheet.Cells[3, 10, 2, 13].LoadFromArrays(new object[][] { new[] { "Диспетчер", "Время", "Текст" } });
            sheet.Cells[3, 15, 2, 18].LoadFromArrays(new object[][] { new[] { "Диспетчер", "Время", "Текст" } });
            var headerCells = sheet.Cells[3, 3, 3, 50];
            var headerFont = headerCells.Style.Font;
            headerFont.Bold = true;
            headerFont.Size = 16;

            var row = 4;
            var column = 1;
            var columnRound = 10;
            var columnNote = 15;
            foreach (DataRow item in dataRequest.Rows)
            {
                sheet.Cells[row, column].Value = item[0];
                sheet.Cells[row, column + 1].Value = item[1].ToString();
                sheet.Cells[row, column + 2].Value = item[2].ToString();
                sheet.Cells[row, column + 3].Value = item[3].ToString();
                sheet.Cells[row, column + 4].Value = item[4].ToString();
                sheet.Cells[row, column + 5].Value = item[5].ToString();
                sheet.Cells[row, column + 6].Value = item[6].ToString();
                row++;
            }

            row = 4;
            foreach (DataRow item in dataEvent.Rows)
            {
                sheet.Cells[row, columnRound].Value = item[0];
                sheet.Cells[row, columnRound + 1].Value = item[1].ToString();
                sheet.Cells[row, columnRound + 2].Value = item[2].ToString();

                row++;
            }

            row = 4;
            foreach (DataRow item in dataNote.Rows)
            {
                sheet.Cells[row, columnNote].Value = item[0];
                sheet.Cells[row, columnNote + 1].Value = item[1].ToString();
                sheet.Cells[row, columnNote + 2].Value = item[2].ToString();

                row++;
            }
            return package.GetAsByteArray();
        }
    }
}
