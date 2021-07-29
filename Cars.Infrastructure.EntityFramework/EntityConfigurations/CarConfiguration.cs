using Cars.Domain;
using Cars.Infrastructure.EntityFramework.Extensions;
using Microsoft.EntityFrameworkCore;

namespace Cars.Infrastructure.EntityFramework.EntityConfigurations
{
    public static class CarConfiguration
    {
        public static ModelBuilder ConfigureCar(this ModelBuilder modelBuilder)
        {
            var entity = modelBuilder.Entity<Car>();

            entity.ToTable("Car", "dbo");

            entity.HasKey(x => x.Id);

            entity.AddAggregateRootColumns();

            entity.MapPropertyToColumnName(x => x.Make);
            entity.MapPropertyToColumnName(x => x.Model);
            entity.MapPropertyToColumnName(x => x.Colour);
            entity.MapPropertyToColumnName(x => x.Year);

            return modelBuilder;
        }
    }
}
