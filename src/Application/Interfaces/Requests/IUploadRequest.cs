using Microsoft.AspNetCore.Http;

namespace Application.Interfaces.Requests
{
    public interface IUploadRequest
    {
        IFormFile File { get; set; }
    }
}
