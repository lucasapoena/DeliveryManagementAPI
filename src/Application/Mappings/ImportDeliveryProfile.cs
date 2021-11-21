using Application.Features.ImportDeliveries.Commands;
using Application.Features.ImportDeliveries.Queries.GetAll;
using Application.Features.ImportDeliveries.Queries.GetById;
using AutoMapper;
using Domain.Entities;

namespace Application.Mappings
{
    public class ImportDeliveryProfile : Profile
    {
        public ImportDeliveryProfile()
        {
            CreateMap<ImportDeliveryCommand, ImportDelivery>().ReverseMap();
            CreateMap<GetImportDeliveryByIdResponse, ImportDelivery>().ReverseMap();
            CreateMap<GetAllImportDeliveriesResponse, ImportDelivery>().ReverseMap();
        }
    }
}