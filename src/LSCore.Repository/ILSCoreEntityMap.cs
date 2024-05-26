using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace LSCore.Repository
{
    public interface ILSCoreEntityMap<TEntity>
        where TEntity : class
    {
        Action<EntityTypeBuilder<TEntity>> Mapper { get; }
        EntityTypeBuilder<TEntity> Map(EntityTypeBuilder<TEntity> entityTypeBuilder);
    }
}
