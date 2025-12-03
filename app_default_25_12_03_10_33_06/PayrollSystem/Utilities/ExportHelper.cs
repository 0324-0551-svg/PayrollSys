using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using DocumentFormat.OpenXml;
using DocumentFormat.OpenXml.Packaging;
using DocumentFormat.OpenXml.Spreadsheet;
using PdfSharp.Drawing;
using PdfSharp.Pdf;

namespace PayrollSystem.Utilities
{
    public static class ExportHelper
    {
        public static void ExportToPDF(object data, string filepath)
        {
            if (data == null)
                throw new ArgumentNullException(nameof(data));
            if (string.IsNullOrWhiteSpace(filepath))
                throw new ArgumentException("File path is required.", nameof(filepath));

            try
            {
                using var document = new PdfDocument();
                document.Info.Title = "Payslip";

                var page = document.AddPage();
                var gfx = XGraphics.FromPdfPage(page);
                var font = new XFont("Verdana", 12, XFontStyle.Regular);

                int margin = 40;
                int lineHeight = 20;
                int yPoint = margin;

                var props = data.GetType().GetProperties(BindingFlags.Public | BindingFlags.Instance);

                foreach (var prop in props)
                {
                    string line = $"{prop.Name}: {prop.GetValue(data)?.ToString() ?? string.Empty}";
                    gfx.DrawString(line, font, XBrushes.Black, new XRect(margin, yPoint, page.Width - 2 * margin, lineHeight), XStringFormats.TopLeft);
                    yPoint += lineHeight;
                }

                document.Save(filepath);
            }
            catch (Exception ex)
            {
                throw new IOException($"Failed to export PDF: {ex.Message}", ex);
            }
        }

        public static void ExportToExcel<T>(List<T> data, string filepath)
        {
            if (data == null || !data.Any())
                throw new ArgumentException("Data cannot be null or empty.", nameof(data));
            if (string.IsNullOrWhiteSpace(filepath))
                throw new ArgumentException("File path is required.", nameof(filepath));

            try
            {
                using var spreadsheetDocument = SpreadsheetDocument.Create(filepath, SpreadsheetDocumentType.Workbook);
                var workbookpart = spreadsheetDocument.AddWorkbookPart();
                workbookpart.Workbook = new Workbook();

                var worksheetPart = workbookpart.AddNewPart<WorksheetPart>();
                worksheetPart.Worksheet = new Worksheet(new SheetData());

                Sheets sheets = spreadsheetDocument.WorkbookPart.Workbook.AppendChild(new Sheets());
                Sheet sheet = new Sheet()
                {
                    Id = spreadsheetDocument.WorkbookPart.GetIdOfPart(worksheetPart),
                    SheetId = 1,
                    Name = "Report"
                };
                sheets.Append(sheet);

                SheetData sheetData = worksheetPart.Worksheet.GetFirstChild<SheetData>()!;

                // Header row
                var headerRow = new Row();
                var props = typeof(T).GetProperties(BindingFlags.Public | BindingFlags.Instance);
                foreach (var prop in props)
                {
                    var cell = new Cell
                    {
                        DataType = CellValues.String,
                        CellValue = new CellValue(prop.Name)
                    };
                    headerRow.Append(cell);
                }
                sheetData.AppendChild(headerRow);

                // Data rows
                foreach (var item in data)
                {
                    var dataRow = new Row();
                    foreach (var prop in props)
                    {
                        object? value = prop.GetValue(item);
                        string cellValue = value?.ToString() ?? string.Empty;

                        var cell = new Cell();

                        if (value is DateTime dt)
                        {
                            cell.DataType = CellValues.String;
                            cell.CellValue = new CellValue(dt.ToShortDateString());
                        }
                        else if (value is decimal || value is double || value is float || value is int || value is long)
                        {
                            cell.DataType = CellValues.Number;
                            cell.CellValue = new CellValue(cellValue);
                        }
                        else
                        {
                            cell.DataType = CellValues.String;
                            cell.CellValue = new CellValue(cellValue);
                        }
                        dataRow.Append(cell);
                    }
                    sheetData.AppendChild(dataRow);
                }
                workbookpart.Workbook.Save();
            }
            catch (Exception ex)
            {
                throw new IOException($"Failed to export Excel: {ex.Message}", ex);
            }
        }
    }
}
