using System.Collections.Generic;
using System.Threading.Tasks;

namespace Application.Interfaces.Services
{
    public interface IExcelService
    {
        Task<IEnumerable<T>> ConvertXLSToObjectAsync<T>(string fileLocation) where T : new();
    }
}
