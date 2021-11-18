using Application.Interfaces.Repositories;
using AutoMapper;
using MediatR;
using Shared.Wrapper;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Features.ImportDeliveries.Queries.GetById
{
    public class GetImportDeliveryByIdQuery : IRequest<Result<GetImportDeliveryByIdResponse>>
    {
        public Guid Id { get; set; }
    }

    internal class GetImportDeliveryByIdQueryHandler : IRequestHandler<GetImportDeliveryByIdQuery, Result<GetImportDeliveryByIdResponse>>
    {
        private readonly IImportDeliveryRepository _repository;
        private readonly IMapper _mapper;

        public GetImportDeliveryByIdQueryHandler(IImportDeliveryRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<Result<GetImportDeliveryByIdResponse>> Handle(GetImportDeliveryByIdQuery query, CancellationToken cancellationToken)
        {
            var importDelivery = await _repository.GetByIdAsync(query.Id);
            var mappedImportDelivery = _mapper.Map<GetImportDeliveryByIdResponse>(importDelivery);
            if (mappedImportDelivery == null)
            {
                return await Result<GetImportDeliveryByIdResponse>.FailAsync("Não existe importação cadastrada com o ID informado!");
            }
            return await Result<GetImportDeliveryByIdResponse>.SuccessAsync(mappedImportDelivery);
        }
    }
}