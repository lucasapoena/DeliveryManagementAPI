using Microsoft.AspNetCore.Http;

namespace Application.Requests
{
    public class UploadRequest
    {
        public IFormFile File { get; set; }
    }
}