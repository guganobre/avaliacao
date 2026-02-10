using Avaliacao.Domain.Interfaces.Infrastructure;
using Avaliacao.Infrastructure.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System.Reflection;

namespace Avaliacao.Infrastructure.IoC
{
    public static class InfrastructureConfiguration
    {
        public static IServiceCollection ConfigureServicesInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddMediatR(scan => scan.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly()));

            services.AddDbContext<DbContextAvaliacao>(
                opt => opt.UseSqlServer(
                                configuration.GetConnectionString("SQLConnection"),
                                b => b.MigrationsAssembly(typeof(DbContextAvaliacao).Assembly.FullName)));

            services.AddScoped<IUnitOfWork, AvaliacaoUnitOfWork>();

            return services;
        }

        public static async Task ExecuteMigrationsAsync(this IServiceProvider serviceProvider)
        {
            using var scope = serviceProvider.CreateScope();
            var services = scope.ServiceProvider;

            var context = services.GetRequiredService<DbContextAvaliacao>();
            await context.Database.MigrateAsync();
        }
    }
}