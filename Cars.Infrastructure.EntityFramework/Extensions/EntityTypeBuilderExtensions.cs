using System;
using System.Linq.Expressions;
using Cars.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Cars.Infrastructure.EntityFramework.Extensions
{
    public static class EntityTypeBuilderExtensions
    {
        public static EntityTypeBuilder<TEntity> AddAggregateRootColumns<TEntity>(this EntityTypeBuilder<TEntity> entity) where TEntity : Entity
        {
            entity.HasKey(x => x.Id);

            entity.AddEntityColumns();

            return entity;
        }

        public static EntityTypeBuilder<TEntity> AddEntityColumns<TEntity>(this EntityTypeBuilder<TEntity> entity) where TEntity : Entity
        {
            entity.MapPropertyToColumnName(x => x.Id).IsRequired();
            entity.MapPropertyToColumnName(x => x.CreateDate).IsRequired();

            return entity;
        }

        public static PropertyBuilder<TProperty> MapPropertyToColumnName<TEntity, TProperty>(this EntityTypeBuilder<TEntity> entity, Expression<Func<TEntity, TProperty>> propertyExpression) where TEntity : Entity
        {
            var builder = entity.Property(propertyExpression);

            builder.HasColumnName(builder.Metadata.Name);

            return builder;
        }
    }
}
