using LSCore.Repository;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SP.DbMigration.Contracts.Entities;

namespace SP.DbMigration.Repository.DbMappings
{
    public class TestEntityDbMapping : LSCoreEntityMap<TestEntity>
    {
        public EntityTypeBuilder<TestEntity> Map(EntityTypeBuilder<TestEntity> entityTypeBuilder)
        {
            entityTypeBuilder.Property(x => x.Name)
                .IsRequired()
                .HasMaxLength(32);

            return entityTypeBuilder;
        }
    }
}
