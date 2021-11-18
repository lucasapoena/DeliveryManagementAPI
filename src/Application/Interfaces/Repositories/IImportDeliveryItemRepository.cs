using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Application.Interfaces.Repositories
{
    public interface IImportDeliveryItemRepository
    {
        Task<ImportDeliveryItem> AddAsync(ImportDeliveryItem entity);
        Task<ImportDeliveryItem> GetByIdAsync(Guid id);
        Task<List<ImportDeliveryItem>> GetAllAsync();
    }
}
