using Application.Interfaces.Repositories;
using Infrastructure.Repositories;
using Microsoft.Extensions.DependencyInjection;

namespace Infrastructure.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddRepositories(this IServiceCollection services)
        {
            return services
                .AddTransient<IImportDeliveryRepository, ImportDeliveryRepository>()
                .AddTransient<IImportDeliveryItemRepository, ImportDeliveryItemRepository>();
        }
    }
}