using Application.Requests;
using System.Threading.Tasks;

namespace Application.Interfaces.Services
{
    public interface IUploadService
    {
        Task<string> UploadAsync(UploadRequest request);
    }
}
