using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;
using Microsoft.EntityFrameworkCore;
using LSCore.Contracts.Interfaces;

namespace LSCore.Repository;

public static class LSCoreExtensions
{
    public static EntityTypeBuilder<TEntity> AddMap<TEntity>(this EntityTypeBuilder<TEntity> entityTypeBuilder, ILSCoreEntityMap<TEntity> map)
        where TEntity : class =>
            map.Map(entityTypeBuilder);
}