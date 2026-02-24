using Avaliacao.Domain.Entities;

namespace Avaliacao.Domain.Interfaces.Infrastructure
{
    public interface ISeguroRepository : IBaseRepository<Seguro>
    {
        Task<IEnumerable<Seguro>> GetAllWithIncludesAsync();
    }
}
