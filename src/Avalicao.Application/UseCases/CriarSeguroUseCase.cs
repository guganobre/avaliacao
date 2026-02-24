using Avaliacao.Application.Services;
using Avaliacao.Domain.DTOs;
using Avaliacao.Domain.Entities;
using Avaliacao.Domain.Interfaces.Infrastructure;

namespace Avaliacao.Application.UseCases
{
    public class CriarSeguroUseCase
    {
        private readonly ISeguroRepository _seguroRepository;
        private readonly IVeiculoRepository _veiculoRepository;
        private readonly ISeguradoRepository _seguradoRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly SeguroCalculadoraService _calculadoraService;

        public CriarSeguroUseCase(
            ISeguroRepository seguroRepository,
            IVeiculoRepository veiculoRepository,
            ISeguradoRepository seguradoRepository,
            IUnitOfWork unitOfWork,
            SeguroCalculadoraService calculadoraService)    
        {
            _seguroRepository = seguroRepository;
            _veiculoRepository = veiculoRepository;
            _seguradoRepository = seguradoRepository;
            _unitOfWork = unitOfWork;
            _calculadoraService = calculadoraService;
        }

        public async Task<SeguroResponse> ExecuteAsync(CriarSeguroRequest request)
        {
            var veiculo = new Veiculo
            {
                MarcaModelo = request.Veiculo.MarcaModelo,
                Valor = request.Veiculo.Valor
            };

            var segurado = new Segurado
            {
                Nome = request.Segurado.Nome,
                Cpf = request.Segurado.Cpf,
                Idade = request.Segurado.Idade
            };

            await _veiculoRepository.AddAsync(veiculo, saveChanges: false);
            await _seguradoRepository.AddAsync(segurado, saveChanges: false);

            var calculo = _calculadoraService.CalcularSeguro(veiculo.Valor);

            var seguro = new Seguro
            {
                Id = Guid.NewGuid(),
                CriadoEmUtc = DateTime.UtcNow,
                Veiculo = veiculo,
                Segurado = segurado,
                TaxaRisco = calculo.TaxaRisco,
                PremioRisco = calculo.PremioRisco,
                PremioPuro = calculo.PremioPuro,
                PremioComercial = calculo.PremioComercial,
                ValorSeguro = calculo.ValorSeguro
            };

            await _seguroRepository.AddAsync(seguro, saveChanges: false);
            await _unitOfWork.SaveChangesAsync();

            return new SeguroResponse
            {
                Id = seguro.Id,
                CriadoEmUtc = seguro.CriadoEmUtc,
                Veiculo = new VeiculoDto
                {
                    MarcaModelo = veiculo.MarcaModelo,
                    Valor = veiculo.Valor
                },
                Segurado = new SeguradoDto
                {
                    Nome = segurado.Nome,
                    Cpf = segurado.Cpf,
                    Idade = segurado.Idade
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
