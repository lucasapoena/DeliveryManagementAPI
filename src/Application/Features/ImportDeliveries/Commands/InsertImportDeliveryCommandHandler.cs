using Application.Features.ImportDeliveryItens.Commands;
using Application.Interfaces.Services;
using MediatR;
using Shared.Wrapper;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Features.ImportDeliveries.Commands
{
    public class InsertImportDeliveryCommandHandler : IRequestHandler<InsertImportDeliveryCommand, Result<Guid>>
    {
        private readonly IUploadService _uploadService;
        private readonly IExcelService _excelService;

        public InsertImportDeliveryCommandHandler(IUploadService uploadService, IExcelService excelService)
        {
            _uploadService = uploadService;
            _excelService = excelService;
        }
        public async Task<Result<Guid>> Handle(InsertImportDeliveryCommand command, CancellationToken cancellationToken)
        {            
            var fileLocation = await _uploadService.UploadAsync(new Requests.UploadRequest { File = command.UploadRequest.File });
            if (string.IsNullOrEmpty(fileLocation))
            {
                return await Result<Guid>.FailAsync("O arquivo enviado é inválido!");
            }
            else
            {                

                var teste = await _excelService.ConvertXLSToObject<ImportDeliveryItemCommand>(fileLocation);
                var teste2 = teste.ToList();
                return await Result<Guid>.SuccessAsync("teste");                               
            }                         
        }       
    }
}
