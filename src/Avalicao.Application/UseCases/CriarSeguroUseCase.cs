using Avaliacao.Application.Services;
using Avaliacao.Domain.DTOs;
using Avaliacao.Domain.Entities;
using Avaliacao.Domain.Interfaces.Infrastructure;
using FluentValidation;

namespace Avaliacao.Application.UseCases
{
    public class CriarSeguroUseCase
    {
        private readonly ISeguroRepository _seguroRepository;
        private readonly IVeiculoRepository _veiculoRepository;
        private readonly ISeguradoRepository _seguradoRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly SeguroCalculadoraService _calculadoraService;
        private readonly IValidator<CriarSeguroRequest> _validator;

        public CriarSeguroUseCase(
            ISeguroRepository seguroRepository,
            IVeiculoRepository veiculoRepository,
            ISeguradoRepository seguradoRepository,
            IUnitOfWork unitOfWork,
            SeguroCalculadoraService calculadoraService,
            IValidator<CriarSeguroRequest> validator)    
        {
            _seguroRepository = seguroRepository;
            _veiculoRepository = veiculoRepository;
            _seguradoRepository = seguradoRepository;
            _unitOfWork = unitOfWork;
            _calculadoraService = calculadoraService;
            _validator = validator;
        }

        public async Task<SeguroResponse> ExecuteAsync(CriarSeguroRequest request)
        {
            var validationResult = await _validator.ValidateAsync(request);

            if (!validationResult.IsValid)
            {
                var errors = string.Join(" | ", validationResult.Errors.Select(e => e.ErrorMessage));
                throw new ArgumentException(errors);
            }

            try
            {
                var cpfLimpo = LimparCpf(request.Segurado.Cpf);

                var veiculo = new Veiculo
                {
                    MarcaModelo = request.Veiculo.MarcaModelo.Trim(),
                    Valor = request.Veiculo.Valor
                };

                var segurado = new Segurado
                {
                    Nome = request.Segurado.Nome.Trim(),
                    Cpf = cpfLimpo,
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
            catch (ArgumentException)
            {
                throw;
            }
            catch (Exception ex)
            {
                throw new ArgumentException($"Erro ao criar o seguro: {ex.Message}", ex);
            }
        }

        private static string LimparCpf(string cpf)
        {
            if (string.IsNullOrWhiteSpace(cpf))
                return string.Empty;

            return System.Text.RegularExpressions.Regex.Replace(cpf, @"[^\d]", "");
        }
    }
}
