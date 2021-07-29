using System.Threading.Tasks;
using Cars.Domain;

namespace Cars.Infrastructure.EntityFramework.Repository
{
    public interface IRepository<TAggregateRoot> where TAggregateRoot : Entity
    {
        void Add(TAggregateRoot aggregateRoot);
        Task<TAggregateRoot> FindByIdAsync(int id);
        void Remove(TAggregateRoot aggregateRoot);
        Task SaveChangesAsync();
    }
}
