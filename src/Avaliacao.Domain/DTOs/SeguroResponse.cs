namespace Avaliacao.Domain.DTOs
{
    public class SeguroResponse
    {
        public Guid Id { get; set; }
        public DateTime CriadoEmUtc { get; set; }
        public VeiculoDto Veiculo { get; set; } = default!;
        public SeguradoDto Segurado { get; set; } = default!;
        public decimal TaxaRisco { get; set; }
        public decimal PremioRisco { get; set; }
        public decimal PremioPuro { get; set; }
        public decimal PremioComercial { get; set; }
        public decimal ValorSeguro { get; set; }
    }
}
