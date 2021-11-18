using MediatR;
using Microsoft.AspNetCore.Http;
using Shared.Wrapper;
using System;

namespace Application.Features.ImportDeliveries.Commands
{
    public class InsertImportDeliveryCommand : IRequest<Result<Guid>>
    {        
        public IFormFile File { get; set; }
    }
}
