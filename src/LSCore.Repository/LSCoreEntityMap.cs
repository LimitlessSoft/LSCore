using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using LSCore.Contracts.Interfaces;

namespace LSCore.Repository;

/// <summary>
/// Used to map entity fields for the database.
/// By default, Id, CreatedAt, IsActive, UpdatedAt and UpdatedBy are mapped.
/// To add custom mapping, override Map(EntityTypeBuilder&lt;<typeparamref name="TEntity"/>&gt; entityTypeBuilder)
/// </summary>
/// <typeparam name="TEntity"></typeparam>
public abstract class LSCoreEntityMap<TEntity> : ILSCoreEntityMap<TEntity>
    where TEntity : class, ILSCoreEntity
{
    private bool SuppressDefaultMapping { get; set; } = false;
    public abstract Action<EntityTypeBuilder<TEntity>> Mapper { get; }

    protected LSCoreEntityMap()
    {
        
    }
    
    protected LSCoreEntityMap(bool suppressDefaultMapping)
    {
        SuppressDefaultMapping = suppressDefaultMapping;
    }

    public EntityTypeBuilder<TEntity> Map(EntityTypeBuilder<TEntity> entityTypeBuilder)
    {
        if (SuppressDefaultMapping == false)
            MapDefaultFields(entityTypeBuilder);

        Mapper(entityTypeBuilder);

        return entityTypeBuilder;
    }
    
    private void MapDefaultFields(EntityTypeBuilder<TEntity> entityTypeBuilder)
    {
        entityTypeBuilder
            .HasKey(x => x.Id);

        entityTypeBuilder
            .Property(x => x.CreatedAt)
            .IsRequired();

        entityTypeBuilder
            .Property(x => x.IsActive)
            .IsRequired();

        entityTypeBuilder
            .Property(x => x.UpdatedAt)
            .IsRequired(false);

        entityTypeBuilder
            .Property(x => x.UpdatedBy)
            .IsRequired(false);
    }
}