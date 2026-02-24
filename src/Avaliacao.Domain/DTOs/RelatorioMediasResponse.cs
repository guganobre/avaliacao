namespace Avaliacao.Domain.DTOs
{
    public class RelatorioMediasResponse
    {
        public decimal MediaValorVeiculo { get; set; }
        public decimal MediaTaxaRisco { get; set; }
        public decimal MediaPremioRisco { get; set; }
        public decimal MediaPremioPuro { get; set; }
        public decimal MediaPremioComercial { get; set; }
        public decimal MediaValorSeguro { get; set; }
        public int TotalSeguros { get; set; }
    }
}
