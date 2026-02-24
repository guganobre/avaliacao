using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Avaliacao.Domain.Interfaces.Infrastructure
{

    /// <summary>
    /// Interface base para repositórios de entidades, fornecendo operações comuns de acesso a dados.
    /// </summary>
    /// <typeparam name="TEntity">Tipo da entidade gerenciada pelo repositório.</typeparam>
    public interface IBaseRepository<TEntity> where TEntity : class
    {
        /// <summary>
        /// Retorna um registro que satisfaça o predicado especificado, incluindo propriedades relacionadas.
        /// </summary>
        /// <param name="predicate">Expressão lambda para filtrar o registro.</param>
        /// <param name="includeProperties">Propriedades de navegação a serem incluídas na consulta.</param>
        /// <returns>Task contendo o registro encontrado.</returns>
        Task<TEntity> FindWithIncludesAsync(
            Expression<Func<TEntity, bool>> predicate,
            params Expression<Func<TEntity, object>>[] includeProperties
        );

        /// <summary>
        /// Adiciona um registro.
        /// </summary>
        /// <param name="entity">Entidade a ser adicionada.</param>
        /// <param name="saveChanges">Indica se as alterações devem ser salvas imediatamente.</param>
        /// <returns>Task contendo a entidade adicionada.</returns>
        Task<TEntity> AddAsync(TEntity entity, bool? saveChanges = true);
    }
}
