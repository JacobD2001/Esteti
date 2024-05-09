using EFCoreSecondLevelCacheInterceptor;
using Esteti.Application.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace Esteti.Infrastructure.Persistence
{
    public static class SqlDatabaseConfiguration
    {
        public static IServiceCollection AddSqlDatabase(this IServiceCollection services, string connectionString)
        {
            Action<IServiceProvider, DbContextOptionsBuilder> sqlOptions = (serviceProvider, options) => options
                 .UseSqlServer(connectionString, o => o.UseQuerySplittingBehavior(QuerySplittingBehavior.SingleQuery)
                 .MigrationsAssembly("Esteti.Migrations"))
                 .AddInterceptors(serviceProvider.GetRequiredService<SecondLevelCacheInterceptor>());

            services.AddDbContext<IApplicationDbContext, MainDbContext>(sqlOptions);

            return services;
        }

    }
}
