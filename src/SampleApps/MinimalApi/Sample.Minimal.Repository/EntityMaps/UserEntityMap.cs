using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Sample.Minimal.Contracts.Entities;
using LSCore.Repository;

namespace Sample.Minimal.Repository.EntityMaps;

public class UserEntityMap : LSCoreEntityMap<UserEntity>
{
    public override Action<EntityTypeBuilder<UserEntity>> Mapper { get; } = builder =>
    {
        builder.Property(x => x.Username)
            .IsRequired();
    };
}