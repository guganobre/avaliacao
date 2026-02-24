using Avaliacao.Domain.DTOs;
using Avaliacao.Domain.Interfaces.Infrastructure;

namespace Avaliacao.Application.UseCases
{
    public class ObterRelatorioMediasUseCase
    {
        private readonly ISeguroRepository _seguroRepository;

        public ObterRelatorioMediasUseCase(ISeguroRepository seguroRepository)
        {
            _seguroRepository = seguroRepository;
        }

        public async Task<RelatorioMediasResponse> ExecuteAsync()
        {
            var seguros = await _seguroRepository.GetAllWithIncludesAsync();
            var listaSeguros = seguros.ToList();

            if (!listaSeguros.Any())
            {
                return new RelatorioMediasResponse
                {
                    TotalSeguros = 0
                };
            }

            return new RelatorioMediasResponse
            {
                MediaValorVeiculo = listaSeguros.Average(s => s.Veiculo.Valor),
                MediaTaxaRisco = listaSeguros.Average(s => s.TaxaRisco),
                MediaPremioRisco = listaSeguros.Average(s => s.PremioRisco),
                MediaPremioPuro = listaSeguros.Average(s => s.PremioPuro),
                MediaPremioComercial = listaSeguros.Average(s => s.PremioComercial),
                MediaValorSeguro = listaSeguros.Average(s => s.ValorSeguro),
                TotalSeguros = listaSeguros.Count
            };
        }
    }
}
