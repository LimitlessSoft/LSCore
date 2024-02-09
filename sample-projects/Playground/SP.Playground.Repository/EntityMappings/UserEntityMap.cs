using LSCore.Repository;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SP.Playground.Contracts.Entities;

namespace SP.Playground.Repository.EntityMappings
{
    public class UserEntityMap : LSCoreEntityMap<UserEntity>
    {
        public override EntityTypeBuilder<UserEntity> Map(EntityTypeBuilder<UserEntity> entityTypeBuilder)
        {
            base.Map(entityTypeBuilder);

            entityTypeBuilder.Property(x => x.Name)
                .IsRequired();

            entityTypeBuilder.HasOne(x => x.City)
                .WithMany()
                .HasForeignKey(x => x.CityId);

            return entityTypeBuilder;
        }
    }
}
