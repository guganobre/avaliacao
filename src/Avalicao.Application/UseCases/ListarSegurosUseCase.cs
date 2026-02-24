using Avaliacao.Domain.DTOs;
using Avaliacao.Domain.Interfaces.Infrastructure;

namespace Avaliacao.Application.UseCases
{
    public class ListarSegurosUseCase
    {
        private readonly ISeguroRepository _seguroRepository;

        public ListarSegurosUseCase(ISeguroRepository seguroRepository)
        {
            _seguroRepository = seguroRepository;
        }

        public async Task<IEnumerable<SeguroResponse>> ExecuteAsync()
        {
            var seguros = await _seguroRepository.GetAllWithIncludesAsync();

            return seguros.Select(seguro => new SeguroResponse
            {
                Id = seguro.Id,
                CriadoEmUtc = seguro.CriadoEmUtc,
                Veiculo = new VeiculoDto
                {
                    MarcaModelo = seguro.Veiculo.MarcaModelo,
                    Valor = seguro.Veiculo.Valor
                },
                Segurado = new SeguradoDto
                {
                    Nome = seguro.Segurado.Nome,
                    Cpf = seguro.Segurado.Cpf,
                    Idade = seguro.Segurado.Idade
                },
                TaxaRisco = seguro.TaxaRisco,
                PremioRisco = seguro.PremioRisco,
                PremioPuro = seguro.PremioPuro,
                PremioComercial = seguro.PremioComercial,
                ValorSeguro = seguro.ValorSeguro
            }).ToList();
        }
    }
}
