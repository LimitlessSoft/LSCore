using LSCore.Repository;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SP.Playground.Contracts.Entities;

namespace SP.Playground.Repository.EntityMappings
{
    public class StreetEntityMap : LSCoreEntityMap<StreetEntity>
    {
        public override EntityTypeBuilder<StreetEntity> Map(EntityTypeBuilder<StreetEntity> entityTypeBuilder)
        {
            base.Map(entityTypeBuilder);

            entityTypeBuilder.Property(x => x.Name)
                .IsRequired()
                .HasMaxLength(100);

            entityTypeBuilder.HasOne(x => x.City)
                .WithMany(x => x.Streets)
                .HasForeignKey(x => x.CityId);

            return entityTypeBuilder;
        }
    }
}
