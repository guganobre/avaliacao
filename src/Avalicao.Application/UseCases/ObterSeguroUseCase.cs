using Avaliacao.Domain.DTOs;
using Avaliacao.Domain.Interfaces.Infrastructure;

namespace Avaliacao.Application.UseCases
{
    public class ObterSeguroUseCase
    {
        private readonly ISeguroRepository _seguroRepository;

        public ObterSeguroUseCase(ISeguroRepository seguroRepository)
        {
            _seguroRepository = seguroRepository;
        }

        public async Task<SeguroResponse?> ExecuteAsync(Guid id)
        {
            var seguro = await _seguroRepository.FindWithIncludesAsync(
                s => s.Id == id,
                s => s.Veiculo,
                s => s.Segurado
            );

            if (seguro == null)
                return null;

            return new SeguroResponse
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
            };
        }
    }
}
