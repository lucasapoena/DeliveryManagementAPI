using Application.Interfaces.Services;
using Infrastructure.Extensions;
using OfficeOpenXml;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Infrastructure.Services
{
    public class ExcelService : IExcelService
    {
        public async Task<IEnumerable<T>> ConvertXLSToObject<T>(string fileLocation) where T : new()
        {
            ExcelPackage.LicenseContext = LicenseContext.NonCommercial;

            IEnumerable<T> newcollection;
            using (ExcelPackage package = new ExcelPackage(fileLocation))
            {
                var workSheet = package.Workbook.Worksheets.FirstOrDefault();
                newcollection = workSheet.ConvertSheetToObjects<T>();                
            }
            return newcollection;
        }
    }
}
