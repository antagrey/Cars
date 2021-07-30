using System;
using Cars.Api.Settings;
using Cars.Infrastructure.EntityFramework;
using Cars.Infrastructure.EntityFramework.Repository;
using Microsoft.EntityFrameworkCore;
using SimpleInjector;

namespace Cars.Api.Extensions.ContainerRegistration
{
    public static class EntityFrameworkRegistrationExtensions
    {
        public static Container RegisterEntityFramework(this Container container,
            ApplicationSettings applicationSettings)
        {
            container.Register(() => GetDbContext(applicationSettings), Lifestyle.Scoped);

            container.Register(typeof(IRepository<>), typeof(Repository<>), Lifestyle.Scoped);

            return container;
        }

        private static CarsDatabaseContext GetDbContext(ApplicationSettings applicationSettings)
        {
            var optionsBuilder = new DbContextOptionsBuilder<CarsDatabaseContext>();

            optionsBuilder.UseSqlServer(
                applicationSettings.ConnectionStringSettings.Cars,
                sql =>
                {
                    sql.EnableRetryOnFailure(
                        maxRetryCount: applicationSettings.EntityFrameworkSettings.MaxRetryCount,
                        maxRetryDelay: TimeSpan.FromSeconds(applicationSettings.EntityFrameworkSettings
                            .MaxRetryDelayInSeconds),
                        errorNumbersToAdd: null);
                });

            return new CarsDatabaseContext(optionsBuilder.Options);
        }
    }
}
