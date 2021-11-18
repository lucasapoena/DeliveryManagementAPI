using Application.Interfaces.Repositories;
using Domain.Entities;
using Infrastructure.Persistence.EF;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Infrastructure.Repositories
{
    public class ImportDeliveryItemRepository : IImportDeliveryItemRepository
    {
        private readonly ApiDBContext _dbContext;

        public ImportDeliveryItemRepository(ApiDBContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<ImportDeliveryItem> AddAsync(ImportDeliveryItem entity)
        {
            await _dbContext.Set<ImportDeliveryItem>().AddAsync(entity);
            return entity;
        }

        public async Task<List<ImportDeliveryItem>> GetAllAsync()
        {
            return await _dbContext
                .Set<ImportDeliveryItem>()
                .ToListAsync();
        }

        public async Task<ImportDeliveryItem> GetByIdAsync(Guid id)
        {
            return await _dbContext.Set<ImportDeliveryItem>().FindAsync(id);
        }
    }
}
