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
        /// Retorna um registro que satisfaça o predicado especificado.
        /// </summary>
        /// <param name="predicate">Expressão lambda para filtrar o registro.</param>
        /// <returns>Task contendo o registro encontrado ou null.</returns>
        Task<TEntity?> GetByExpressionAsync(Expression<Func<TEntity, bool>> predicate);

        /// <summary>
        /// Verifica se existe algum registro que satisfaça o predicado especificado.
        /// </summary>
        /// <param name="predicate">Expressão lambda para filtrar o registro.</param>
        /// <returns>True se existir pelo menos um registro que satisfaça o predicado, caso contrário, false.</returns>
        bool Exists(Expression<Func<TEntity, bool>> predicate);

        /// <summary>
        /// Retorna uma lista de registros que satisfaçam o predicado especificado, incluindo propriedades relacionadas.
        /// </summary>
        /// <param name="predicate">Expressão lambda para filtrar os registros.</param>
        /// <param name="includeProperties">Propriedades de navegação a serem incluídas na consulta.</param>
        /// <returns>Task contendo a lista de registros encontrados.</returns>
        Task<IEnumerable<TEntity>> GetListByExpressionAsync(
            Expression<Func<TEntity, bool>> predicate,
            params Expression<Func<TEntity, object>>[] includeProperties
        );

        /// <summary>
        /// Retorna uma lista de registros que satisfaçam o predicado especificado, incluindo propriedades relacionadas, sem rastreamento (NoTracking).
        /// </summary>
        /// <param name="predicate">Expressão lambda para filtrar os registros.</param>
        /// <param name="includeProperties">Propriedades de navegação a serem incluídas na consulta.</param>
        /// <returns>Task contendo a lista de registros encontrados sem rastreamento.</returns>
        Task<IEnumerable<TEntity>> GetListByExpressionNoTrackingAsync(
            Expression<Func<TEntity, bool>> predicate,
            params Expression<Func<TEntity, object>>[] includeProperties
        );

        /// <summary>
        /// Retorna uma consulta IQueryable de registros que satisfaçam o predicado especificado, incluindo propriedades relacionadas.
        /// </summary>
        /// <param name="predicate">Expressão lambda para filtrar os registros.</param>
        /// <param name="includeProperties">Propriedades de navegação a serem incluídas na consulta.</param>
        /// <returns>IQueryable contendo os registros encontrados.</returns>
        IQueryable<TEntity> GetListByExpression(
            Expression<Func<TEntity, bool>> predicate,
            params Expression<Func<TEntity, object>>[] includeProperties
        );

        /// <summary>
        /// Retorna todos os registros.
        /// </summary>
        /// <returns>Task contendo todos os registros.</returns>
        Task<IEnumerable<TEntity>> GetAllAsync();

        /// <summary>
        /// Retorna um registro pelo Id.
        /// </summary>
        /// <param name="id">Identificador do registro.</param>
        /// <param name="className">Nome da classe para logging ou controle adicional (opcional).</param>
        /// <returns>Task contendo o registro encontrado ou null.</returns>
        Task<TEntity> GetByIdAsync(object id, string? className = "");

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

        /// <summary>
        /// Atualiza um registro.
        /// </summary>
        /// <param name="entity">Entidade a ser atualizada.</param>
        /// <param name="saveChanges">Indica se as alterações devem ser salvas imediatamente.</param>
        /// <returns>Task contendo a entidade atualizada.</returns>
        Task<TEntity> UpdateAsync(TEntity entity, bool? saveChanges = true);

        /// <summary>
        /// Atualiza vários registros.
        /// </summary>
        /// <param name="entities">Entidades a serem atualizadas.</param>
        /// <returns>Task representando a operação assíncrona.</returns>
        Task UpdateEntitiesAsync(params object[] entities);

        /// <summary>
        /// Remove um registro pelo Id.
        /// </summary>
        /// <param name="id">Identificador do registro a ser removido.</param>
        /// <param name="saveChanges">Indica se as alterações devem ser salvas imediatamente.</param>
        /// <param name="className">Nome da classe para logging ou controle adicional (opcional).</param>
        /// <returns>Task representando a operação assíncrona.</returns>
        Task DeleteAsync(object id, bool? saveChanges = true, string? className = "");

        /// <summary>
        /// Para de rastrear um registro.
        /// </summary>
        /// <param name="entity">Entidade a ter o rastreamento interrompido.</param>
        void StopTracking(TEntity entity);
    }
}
