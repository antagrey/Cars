using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Threading.Tasks;
using Cars.Domain;
using Cars.Infrastructure.EntityFramework.EntityConfigurations;
using Microsoft.EntityFrameworkCore;

namespace Cars.Infrastructure.EntityFramework
{
    [ExcludeFromCodeCoverage]
    public class CarsDatabaseContext : DbContext
    {
        private readonly IDictionary<Type, Func<IQueryable<object>>> dbSetDictionary = new Dictionary<Type, Func<IQueryable<object>>>();

        public CarsDatabaseContext(DbContextOptions options) : base(options)
        {
            dbSetDictionary[typeof(Car)] = () => Cars;
        }

        public DbSet<Car> Cars { get; set; }

        public IQueryable<TAggregateRoot> GetDbSet<TAggregateRoot>() where TAggregateRoot : Entity
        {
            return (IQueryable<TAggregateRoot>)dbSetDictionary[typeof(TAggregateRoot)].Invoke();
        }

        public Task<int> SaveChangesAsync()
        {
            return base.SaveChangesAsync();
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder
                .ConfigureCar();
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            // lazy loading not supported (yet) in ef core so no need to switch off
        }
    }
}
