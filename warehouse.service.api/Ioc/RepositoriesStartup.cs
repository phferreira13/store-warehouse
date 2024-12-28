using warehouse.service.domain.Interfaces.Repositories;
using warehouse.service.entityframework.Repositories;

namespace warehouse.service.api.Ioc
{
    public static class RepositoriesStartup
    {
        public static void AddRepositories(this IServiceCollection services)
        {
            services.AddScoped<IWarehouseRepository, WarehouseRepository>();
            services.AddScoped<IItemRepository, ItemRepository>();
        }
    }
}
