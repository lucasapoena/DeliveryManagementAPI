using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Application.Interfaces.Services
{
    public interface IExcelService
    {
        Task<IEnumerable<T>> ConvertXLSToObjectAsync<T>(string fileLocation) where T : new();

        Task<string> ExportAsync<TData>(IEnumerable<TData> data
            , Dictionary<string, Func<TData, object>> mappers
            , string sheetName = "Sheet1");
    }
}
