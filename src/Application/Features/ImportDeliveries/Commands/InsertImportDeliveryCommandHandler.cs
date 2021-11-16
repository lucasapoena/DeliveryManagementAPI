using MediatR;
using Shared.Wrapper;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Features.ImportDeliveries.Commands
{
    public class InsertImportDeliveryCommandHandler : IRequestHandler<InsertImportDeliveryCommand, Result<Guid>>
    {
        public async Task<Result<Guid>> Handle(InsertImportDeliveryCommand command, CancellationToken cancellationToken)
        {
            if (command.File == null || command.File.Length == 0)
            {
                return await Result<Guid>.FailAsync("Arquivo em branco");
            }

            return await Result<Guid>.SuccessAsync("teste");
        }
    }
}
