using Application.Interfaces.Services;
using Infrastructure.Extensions;
using OfficeOpenXml;
using OfficeOpenXml.Style;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace Infrastructure.Services
{
    public class ExcelService : IExcelService
    {
        public async Task<IEnumerable<T>> ConvertXLSToObjectAsync<T>(string fileLocation) where T : new()
        {
            IEnumerable<T> xlsCollection;
            using (FileStream fileStream = new FileStream(fileLocation, FileMode.Open))
            {
                ExcelPackage.LicenseContext = LicenseContext.NonCommercial;
                ExcelPackage package = new ExcelPackage(fileStream);
                var workSheet = package.Workbook.Worksheets.FirstOrDefault();
                xlsCollection = await workSheet.ConvertSheetToObjectsAsync<T>();
            }

            return xlsCollection;
        }

        public async Task<string> ExportAsync<TData>(IEnumerable<TData> data
           , Dictionary<string, Func<TData, object>> mappers
           , string sheetName = "Sheet1")
        {
            ExcelPackage.LicenseContext = LicenseContext.NonCommercial;
            using var excelPackage = new ExcelPackage();
            excelPackage.Workbook.Properties.Author = "Lucas Apoena";
            excelPackage.Workbook.Worksheets.Add("Sheet01");

            var workSheet = excelPackage.Workbook.Worksheets.FirstOrDefault();
            workSheet.Name = sheetName;
            workSheet.Cells.Style.Font.Size = 11;
            workSheet.Cells.Style.Font.Name = "Calibri";

            var colIndex = 1;
            var rowIndex = 1;

            var headers = mappers.Keys.Select(x => x).ToList();

            foreach (var header in headers)
            {
                var cell = workSheet.Cells[rowIndex, colIndex];

                var fill = cell.Style.Fill;
                fill.PatternType = ExcelFillStyle.Solid;
                fill.BackgroundColor.SetColor(Color.LightSkyBlue);

                var border = cell.Style.Border;
                border.Bottom.Style =
                    border.Top.Style =
                        border.Left.Style =
                            border.Right.Style = ExcelBorderStyle.Thin;

                cell.Value = header;

                colIndex++;
            }

            var dataList = data.ToList();
            foreach (var item in dataList)
            {
                colIndex = 1;
                rowIndex++;

                var result = headers.Select(header => mappers[header](item));

                foreach (var value in result)
                {
                    workSheet.Cells[rowIndex, colIndex++].Value = value;
                }
            }

            using (ExcelRange autoFilterCells = workSheet.Cells[1, 1, dataList.Count + 1, headers.Count])
            {
                autoFilterCells.AutoFilter = true;
                autoFilterCells.AutoFitColumns();
            }

            var byteArray = await excelPackage.GetAsByteArrayAsync();
            return Convert.ToBase64String(byteArray);
        }
    }
}


