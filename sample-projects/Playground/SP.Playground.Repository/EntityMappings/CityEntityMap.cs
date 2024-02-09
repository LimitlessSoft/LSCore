using LSCore.Repository;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SP.Playground.Contracts.Entities;

namespace SP.Playground.Repository.EntityMappings
{
    public class CityEntityMap : LSCoreEntityMap<CityEntity>
    {
        public override EntityTypeBuilder<CityEntity> Map(EntityTypeBuilder<CityEntity> entityTypeBuilder)
        {
            base.Map(entityTypeBuilder);

            entityTypeBuilder.Property(x => x.Name)
                .IsRequired()
                .HasMaxLength(100);

            entityTypeBuilder.HasMany(x => x.Users)
                .WithOne(x => x.City)
                .HasForeignKey(x => x.CityId);

            entityTypeBuilder.HasMany(x => x.Streets)
                .WithOne(x => x.City)
                .HasForeignKey(x => x.CityId);

            return entityTypeBuilder;
        }
    }
}
