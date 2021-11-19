using Application.Interfaces.Requests;
using MediatR;
using Shared.Wrapper;
using System;

namespace Application.Features.ImportDeliveries.Commands
{
    public class InsertImportDeliveryCommand : IRequest<Result<Guid>>
    {
        public IUploadRequest UploadRequest { get; set; }
    }
}
