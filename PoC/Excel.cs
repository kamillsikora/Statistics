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

        /// <summary>
        /// Creates Excel file
        /// </summary>
        /// <param name="filePath">path of the creating file</param>
        static void CreateExcelFile(string filePath)
        {
            // Creating a new Excel file
            using (SpreadsheetDocument document = SpreadsheetDocument.Create(filePath, SpreadsheetDocumentType.Workbook))
            {
                WorkbookPart workbookPart = document.AddWorkbookPart();
                workbookPart.Workbook = new Workbook();

                Sheets sheets = workbookPart.Workbook.AppendChild(new Sheets());

                // Adding sheets to the file

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
        }

        /// <summary>
        /// Adding data to the chosen sheet
        /// </summary>
        /// <param name="document">Created Excel for statistics</param>
        /// <param name="sheetIndex">Number of the sheet into which the data are going to be added</param>
        /// <param name="category_name">first parameter</param>
        /// <param name="category_number">second parameter</param>
        static void AddDataToSheet(SpreadsheetDocument document, int sheetIndex, string category_name, string category_number)
        {
            WorkbookPart workbookPart = document.WorkbookPart;
            WorksheetPart worksheetPart = workbookPart.WorksheetParts.ElementAtOrDefault(sheetIndex - 1);

            if (worksheetPart != null)
            {
                if(sheetIndex == 1 || sheetIndex == 2)
                {
                    SheetData sheetData = worksheetPart.Worksheet.GetFirstChild<SheetData>();
                    IncreaseRow(ref rowAT);

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
                    IncreaseRow(ref rowCV);

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

        /// <summary>
        /// Increases rows in Excel
        /// </summary>
        /// <param name="rows">Number of the current row</param>
        static void IncreaseRow(ref int rows)
        {
            rows++;
        }

        /// <summary>
        /// Increases columns in Excel
        /// </summary>
        /// <param name="column">Number of the current column</param>
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

        /// <summary>
        /// Decreases columns in Excel
        /// </summary>
        /// <param name="column">Number of the current column</param>
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

        /// <summary>
        /// Adds data to the chosen cell in Excel
        /// </summary>
        /// <param name="column">Current column</param>
        /// <param name="rows">Current row</param>
        /// <param name="cellValue">Value that is added</param>
        /// <param name="dataType">Type of the value that is added</param>
        /// <returns></returns>
        static Cell CreateCell(string column, int rows, string cellValue, CellValues dataType)
        {
            string cellReference = column + rows;
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
