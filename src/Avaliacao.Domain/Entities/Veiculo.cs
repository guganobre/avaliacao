using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Avaliacao.Domain.Entities
{
    public class Veiculo
    {
        public int Id { get; set; }
        public string MarcaModelo { get; set; } = string.Empty;
        public decimal Valor { get; set; }
    }
}
