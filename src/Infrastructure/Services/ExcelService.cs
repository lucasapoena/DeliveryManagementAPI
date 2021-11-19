using Application.Interfaces.Services;
using Infrastructure.Extensions;
using OfficeOpenXml;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace Infrastructure.Services
{
    public class ExcelService : IExcelService
    {
        public async Task<IEnumerable<T>> ConvertXLSToObject<T>(string fileLocation) where T : new()
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
    }
}


