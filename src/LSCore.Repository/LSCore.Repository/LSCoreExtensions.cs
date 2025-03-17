using LSCore.Repository.Contracts;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace LSCore.Repository;

public static class LSCoreExtensions
{
	public static EntityTypeBuilder<TEntity> AddMap<TEntity>(
		this EntityTypeBuilder<TEntity> entityTypeBuilder,
		ILSCoreEntityMap<TEntity> map
	)
		where TEntity : class => map.Map(entityTypeBuilder);
}
