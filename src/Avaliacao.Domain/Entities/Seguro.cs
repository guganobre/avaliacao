using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Avaliacao.Domain.Entities
{
    public class Seguro
    {
        public Guid Id { get; set; }
        public DateTime CriadoEmUtc { get; set; }

        public int VeiculoId { get; set; }
        public Veiculo Veiculo { get; set; } = default!;
        public int SeguradoId { get; set; }
        public Segurado Segurado { get; set; } = default!;

        public decimal TaxaRisco { get; set; }
        public decimal PremioRisco { get; set; }
        public decimal PremioPuro { get; set; }
        public decimal PremioComercial { get; set; }
        public decimal ValorSeguro { get; set; }
    }
}