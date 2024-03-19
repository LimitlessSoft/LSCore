using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SP.Playground.Contracts.Entities;
using LSCore.Repository;

namespace SP.Playground.Repository.EntityMappings
{
    public class HouseEntityMap : LSCoreEntityMap<HouseEntity>
    {
        public override EntityTypeBuilder<HouseEntity> Map(EntityTypeBuilder<HouseEntity> entityTypeBuilder)
        {
            base.Map(entityTypeBuilder);

            entityTypeBuilder.Property(x => x.Number)
                .IsRequired();

            return entityTypeBuilder;
        }
    }
}
