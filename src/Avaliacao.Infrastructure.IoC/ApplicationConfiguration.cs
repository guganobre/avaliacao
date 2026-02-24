using Avaliacao.Application.Services;
using Avaliacao.Application.UseCases;
using Avaliacao.Application.Validators;
using Avaliacao.Domain.DTOs;
using FluentValidation;
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

            // Validators
            services.AddScoped<IValidator<CriarSeguroRequest>, CriarSeguroRequestValidator>();

            // Use Cases
            services.AddScoped<CriarSeguroUseCase>();
            services.AddScoped<ObterSeguroUseCase>();
            services.AddScoped<ListarSegurosUseCase>();
            services.AddScoped<ObterRelatorioMediasUseCase>();

            return services;
        }
    }
}