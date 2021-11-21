using Application.Interfaces.Requests;
using Microsoft.AspNetCore.Http;

namespace Application.Requests
{
    public class UploadRequest : IUploadRequest
    {
        public IFormFile File { get; set; }
    }
}