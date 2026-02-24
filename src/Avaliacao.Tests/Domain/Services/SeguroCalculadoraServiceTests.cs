using Avaliacao.Application.Services;
using Xunit;

namespace Avaliacao.Tests.Application.Services
{
    public class SeguroCalculadoraServiceTests
    {
        private readonly SeguroCalculadoraService _calculadoraService;

        public SeguroCalculadoraServiceTests()
        {
            _calculadoraService = new SeguroCalculadoraService();
        }

        [Fact]
        public void CalcularTaxaRisco_DeveRetornarValorCorreto()
        {
            // Arrange
            decimal valorVeiculo = 10000m;
            decimal taxaEsperada = 2.5m;

            // Act
            var taxaRisco = _calculadoraService.CalcularTaxaRisco(valorVeiculo);

            // Assert
            Assert.Equal(taxaEsperada, taxaRisco);
        }

        [Fact]
        public void CalcularTaxaRisco_ComValorZero_DeveLancarExcecao()
        {
            // Arrange
            decimal valorVeiculo = 0m;

            // Act & Assert
            Assert.Throws<ArgumentException>(() => _calculadoraService.CalcularTaxaRisco(valorVeiculo));
        }

        [Fact]
        public void CalcularTaxaRisco_ComValorNegativo_DeveLancarExcecao()
        {
            // Arrange
            decimal valorVeiculo = -1000m;

            // Act & Assert
            Assert.Throws<ArgumentException>(() => _calculadoraService.CalcularTaxaRisco(valorVeiculo));
        }

        [Fact]
        public void CalcularPremioRisco_DeveRetornarValorCorreto()
        {
            // Arrange
            decimal taxaRisco = 2.5m;
            decimal valorVeiculo = 10000m;
            decimal premioEsperado = 250m;

            // Act
            var premioRisco = _calculadoraService.CalcularPremioRisco(taxaRisco, valorVeiculo);

            // Assert
            Assert.Equal(premioEsperado, premioRisco);
        }

        [Fact]
        public void CalcularPremioPuro_DeveRetornarValorCorreto()
        {
            // Arrange
            decimal premioRisco = 250m;
            decimal premioEsperado = 257.50m;

            // Act
            var premioPuro = _calculadoraService.CalcularPremioPuro(premioRisco);

            // Assert
            Assert.Equal(premioEsperado, premioPuro);
        }

        [Fact]
        public void CalcularPremioComercial_DeveRetornarValorCorreto()
        {
            // Arrange
            decimal premioPuro = 257.50m;
            decimal premioEsperado = 12.875m;

            // Act
            var premioComercial = _calculadoraService.CalcularPremioComercial(premioPuro);

            // Assert
            Assert.Equal(premioEsperado, premioComercial);
        }

        [Fact]
        public void CalcularSeguro_ComExemplo_DeveRetornarValoresCorretos()
        {
            // Arrange
            decimal valorVeiculo = 10000m;

            // Act
            var resultado = _calculadoraService.CalcularSeguro(valorVeiculo);

            // Assert
            Assert.Equal(2.5m, resultado.TaxaRisco);
            Assert.Equal(250m, resultado.PremioRisco);
            Assert.Equal(257.50m, resultado.PremioPuro);
            Assert.Equal(12.875m, resultado.PremioComercial);
            Assert.Equal(12.875m, resultado.ValorSeguro);
        }

        [Theory]
        [InlineData(5000, 2.5)]
        [InlineData(20000, 2.5)]
        [InlineData(15000, 2.5)]
        public void CalcularTaxaRisco_ComDiferentesValores_DeveRetornarTaxaConstante(decimal valorVeiculo, decimal taxaEsperada)
        {
            // Act
            var taxaRisco = _calculadoraService.CalcularTaxaRisco(valorVeiculo);

            // Assert - A taxa de risco é sempre 2.5% independente do valor
            Assert.Equal(taxaEsperada, taxaRisco);
        }

        [Fact]
        public void CalcularSeguro_DeveCalcularTodosOsValoresEmSequencia()
        {
            // Arrange
            decimal valorVeiculo = 30000m;

            // Act
            var resultado = _calculadoraService.CalcularSeguro(valorVeiculo);

            // Assert - Validar que todos os cálculos seguem a sequência correta
            var taxaRiscoCalculada = 2.5m;
            var premioRiscoCalculado = (taxaRiscoCalculada / 100) * valorVeiculo;
            var premioPuroCalculado = premioRiscoCalculado * 1.03m;
            var premioComercialCalculado = 0.05m * premioPuroCalculado;

            Assert.Equal(taxaRiscoCalculada, resultado.TaxaRisco);
            Assert.Equal(premioRiscoCalculado, resultado.PremioRisco);
            Assert.Equal(premioPuroCalculado, resultado.PremioPuro);
            Assert.Equal(premioComercialCalculado, resultado.PremioComercial);
            Assert.Equal(premioComercialCalculado, resultado.ValorSeguro);
        }
    }
}
