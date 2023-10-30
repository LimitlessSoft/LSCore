using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace LSCore.Repository
{
    public interface ILSCoreEntityMap<TEntity>
        where TEntity : class
    {
        EntityTypeBuilder<TEntity> Map(EntityTypeBuilder<TEntity> entityTypeBuilder);
    }
}
