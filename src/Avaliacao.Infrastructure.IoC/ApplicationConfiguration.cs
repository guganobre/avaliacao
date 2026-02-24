using Avaliacao.Application.Services;
using Avaliacao.Application.UseCases;
using Microsoft.Extensions.DependencyInjection;

namespace Avaliacao.Infrastructure.IoC
{
    public static class ApplicationConfiguration
    {
        public static IServiceCollection ConfigureServicesApplication(this IServiceCollection services)
        {
            // Application Services
            services.AddScoped<SeguroCalculadoraService>

();
            // Use Cases
            services.AddScoped<CriarSeguroUseCase>();
            services.AddScoped<ObterSeguroUseCase>();
            services.AddScoped<ListarSegurosUseCase>();
            services.AddScoped<ObterRelatorioMediasUseCase>();

            return services;
        }
    }
}