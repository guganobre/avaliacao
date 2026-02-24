namespace Avaliacao.Application.Services
{
    public class SeguroCalculadoraService
    {
        private const decimal MARGEM_SEGURANCA = 0.03m;
        private const decimal LUCRO = 0.05m;

        public decimal CalcularTaxaRisco(decimal valorVeiculo)
        {
            if (valorVeiculo <= 0)
                throw new ArgumentException("Valor do veículo deve ser maior que zero.", nameof(valorVeiculo));

            // Taxa de Risco = (Valor do Veículo * 5) / (2 * Valor do Veículo)
            // Simplificando: 5/2 = 2.5 (sempre constante, independente do valor)
            return 2.5m;
        }

        public decimal CalcularPremioRisco(decimal taxaRisco, decimal valorVeiculo)
        {
            // Prêmio de Risco = Taxa de Risco * Valor do Veículo
            // Como Taxa de Risco é 2.5%, precisamos converter para decimal (0.025)
            return (taxaRisco / 100) * valorVeiculo;
        }

        public decimal CalcularPremioPuro(decimal premioRisco)
        {
            return premioRisco * (1 + MARGEM_SEGURANCA);
        }

        public decimal CalcularPremioComercial(decimal premioPuro)
        {
            return LUCRO * premioPuro;
        }

        public (decimal TaxaRisco, decimal PremioRisco, decimal PremioPuro, decimal PremioComercial, decimal ValorSeguro) CalcularSeguro(decimal valorVeiculo)
        {
            var taxaRisco = CalcularTaxaRisco(valorVeiculo);
            var premioRisco = CalcularPremioRisco(taxaRisco, valorVeiculo);
            var premioPuro = CalcularPremioPuro(premioRisco);
            var premioComercial = CalcularPremioComercial(premioPuro);

            return (taxaRisco, premioRisco, premioPuro, premioComercial, premioComercial);
        }
    }
}
