using System.Collections.Generic;
using System.Threading.Tasks;

namespace Application.Interfaces.Services
{
    public interface IExcelService
    {
        Task<IEnumerable<T>> ConvertXLSToObject<T>(string fileLocation) where T : new();
    }
}
