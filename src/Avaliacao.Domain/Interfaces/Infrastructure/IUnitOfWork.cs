using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Avaliacao.Domain.Interfaces.Infrastructure
{
    /// <summary>
    /// Interface para controle de unidade de trabalho (Unit of Work).
    /// </summary>
    public interface IUnitOfWork : IDisposable
    {
        /// <summary>
        /// Salva todas as alterações pendentes no contexto.
        /// </summary>
        /// <returns>Task contendo o número de registros afetados.</returns>
        Task<int> SaveChangesAsync();
    }
}
