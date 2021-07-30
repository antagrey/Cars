using System.Threading.Tasks;
using Cars.Domain;
using Microsoft.EntityFrameworkCore;

namespace Cars.Infrastructure.EntityFramework.Repository
{
    public class Repository<TAggregateRoot> : IRepository<TAggregateRoot>
        where TAggregateRoot : Entity
    {
        private readonly CarsDatabaseContext dbContext;

        public Repository(CarsDatabaseContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public void Add(TAggregateRoot aggregateRoot)
        {
            dbContext.Set<TAggregateRoot>().Add(aggregateRoot);
        }

        public Task<TAggregateRoot> FindByIdAsync(int id)
        {
            return dbContext.GetDbSet<TAggregateRoot>().FirstOrDefaultAsync(x => x.Id == id);
        }

        public void Remove(TAggregateRoot aggregateRoot)
        {
            dbContext.Set<TAggregateRoot>().Remove(aggregateRoot);
        }

        public async Task SaveChangesAsync()
        {
            await dbContext.SaveChangesAsync();
        }
    }
}
