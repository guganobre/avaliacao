namespace Avaliacao.Domain.DTOs
{
    public class CriarSeguroRequest
    {
        public VeiculoDto Veiculo { get; set; } = default!;
        public SeguradoDto Segurado { get; set; } = default!;
    }
}
