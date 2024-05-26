using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using LSCore.Contracts.Interfaces;

namespace LSCore.Repository
{
    /// <summary>
    /// Used to map entity fields for the database.
    /// By default, Id, CreatedAt, IsActive, UpdatedAt and UpdatedBy are mapped.
    /// To add custom mapping, override Map(EntityTypeBuilder&lt;<typeparamref name="TEntity"/>&gt; entityTypeBuilder)
    /// </summary>
    /// <typeparam name="TEntity"></typeparam>
    public abstract class LSCoreEntityMap<TEntity> : ILSCoreEntityMap<TEntity>
        where TEntity : class, ILSCoreEntity
    {
        public abstract Action<EntityTypeBuilder<TEntity>> Mapper { get; }

        public EntityTypeBuilder<TEntity> Map(EntityTypeBuilder<TEntity> entityTypeBuilder)
        {
            entityTypeBuilder
                .HasKey(x => x.Id);

            entityTypeBuilder
                .Property(x => x.CreatedAt)
                .IsRequired();

            entityTypeBuilder
                .Property(x => x.IsActive)
                .HasDefaultValue(true)
                .IsRequired();

            entityTypeBuilder
                .Property(x => x.UpdatedAt)
                .IsRequired(false);

            entityTypeBuilder
                .Property(x => x.UpdatedBy)
                .IsRequired(false);

            foreach (var property in typeof(TEntity).GetProperties())
                if (property.PropertyType == typeof(DateTime))
                    entityTypeBuilder.Property(property.PropertyType, property.Name).HasColumnType("timestamp");
            
            Mapper(entityTypeBuilder);

            return entityTypeBuilder;
        }
    }
}
