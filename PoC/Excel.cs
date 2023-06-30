using DocumentFormat.OpenXml;
using DocumentFormat.OpenXml.Packaging;
using DocumentFormat.OpenXml.Spreadsheet;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PoC
{
    public partial class Statistics
    {
        public static int rowAT = 0;
        public static string colAT = "A";
        public static int rowCV = 0;
        public static string colCV = "A";
        public static int rowTemp = 0;
        public static string colTemp = "A";
        static void CreateExcelFile(string filePath)
        {

            // Tworzenie nowego pliku Excel
            using (SpreadsheetDocument document = SpreadsheetDocument.Create(filePath, SpreadsheetDocumentType.Workbook))
            {
                WorkbookPart workbookPart = document.AddWorkbookPart();
                workbookPart.Workbook = new Workbook();

                Sheets sheets = workbookPart.Workbook.AppendChild(new Sheets());

                // Dodawanie arkuszy do pliku

                WorksheetPart worksheetPart1 = workbookPart.AddNewPart<WorksheetPart>();
                worksheetPart1.Worksheet = new Worksheet(new SheetData());

                Sheet sheet1 = new Sheet() { Id = workbookPart.GetIdOfPart(worksheetPart1), SheetId = (uint)1, Name = "Annotation Team" };
                sheets.Append(sheet1);

                WorksheetPart worksheetPart2 = workbookPart.AddNewPart<WorksheetPart>();
                worksheetPart2.Worksheet = new Worksheet(new SheetData());

                Sheet sheet2 = new Sheet() { Id = workbookPart.GetIdOfPart(worksheetPart2), SheetId = (uint)2, Name = "Summary for AT" };
                sheets.Append(sheet2);

                WorksheetPart worksheetPart3 = workbookPart.AddNewPart<WorksheetPart>();
                worksheetPart3.Worksheet = new Worksheet(new SheetData());

                Sheet sheet3 = new Sheet() { Id = workbookPart.GetIdOfPart(worksheetPart3), SheetId = (uint)3, Name = "Computer Vision" };
                sheets.Append(sheet3);

                WorksheetPart worksheetPart4 = workbookPart.AddNewPart<WorksheetPart>();
                worksheetPart4.Worksheet = new Worksheet(new SheetData());

                Sheet sheet4 = new Sheet() { Id = workbookPart.GetIdOfPart(worksheetPart4), SheetId = (uint)4, Name = "Summary for CV" };
                sheets.Append(sheet4);

                workbookPart.Workbook.Save();
                document.Dispose();
            }

            MessageBox.Show("Plik Excel został utworzony: " + filePath);
        }

        static void AddDataToSheet(SpreadsheetDocument document, int sheetIndex, string category_name, string category_number)
        {
            WorkbookPart workbookPart = document.WorkbookPart;
            WorksheetPart worksheetPart = workbookPart.WorksheetParts.ElementAtOrDefault(sheetIndex - 1);

            if (worksheetPart != null)
            {
                if(sheetIndex == 1 || sheetIndex == 2)
                {
                    SheetData sheetData = worksheetPart.Worksheet.GetFirstChild<SheetData>();
                    IncreaseFunctionValue(ref rowAT);

                    Row dataRow = new Row();
                    dataRow.Append(CreateCell(colAT, rowAT, category_name, CellValues.String));
                    IncreaseColumn(ref colAT);
                    dataRow.Append(CreateCell(colAT, rowAT, category_number, CellValues.Number));
                    DecreaseColumn(ref colAT);
                    sheetData.AppendChild(dataRow);

                    workbookPart.Workbook.Save();
                }

                if (sheetIndex == 3 || sheetIndex == 4)
                {
                    SheetData sheetData = worksheetPart.Worksheet.GetFirstChild<SheetData>();
                    IncreaseFunctionValue(ref rowCV);

                    Row dataRow = new Row();
                    dataRow.Append(CreateCell(colCV, rowCV, category_name, CellValues.String));
                    IncreaseColumn(ref colCV);
                    dataRow.Append(CreateCell(colCV, rowCV, category_number, CellValues.Number));
                    DecreaseColumn(ref colCV);
                    sheetData.AppendChild(dataRow);

                    workbookPart.Workbook.Save();
                }
            }
        }

        static void IncreaseFunctionValue(ref int value)
        {
            value++;
        }

        static void IncreaseColumn(ref string column)
        {
            int length = column.Length;

            if (column[length - 1] == 'Z')
            {
                column += 'A';
            }
            else
            {
                char lastChar = (char)(column[length - 1] + 1);
                column = column.Substring(0, length - 1) + lastChar;
            }
        }

        static void DecreaseColumn(ref string column)
        {
            int length = column.Length;

            if (column[length - 1] == 'A')
            {
                column = column.Substring(0, length - 1);
            }
            else
            {
                char lastChar = (char)(column[length - 1] - 1);
                column = column.Substring(0, length - 1) + lastChar;
            }
        }

        static Cell CreateCell(string column, int rowIndex, string cellValue, CellValues dataType)
        {
            string cellReference = column + rowIndex;
            Cell cell = new Cell()
            {
                CellReference = cellReference,
                DataType = new EnumValue<CellValues>(dataType)
            };

            cell.Append(new CellValue(cellValue));

            return cell;
        }
    }
}
