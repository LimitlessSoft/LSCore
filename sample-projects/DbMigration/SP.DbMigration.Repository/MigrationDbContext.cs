using LSCore.Repository;
using Microsoft.EntityFrameworkCore;
using SP.DbMigration.Contracts.Entities;
using SP.DbMigration.Repository.DbMappings;

namespace SP.DbMigration.Repository
{
    public class MigrationDbContext : DbContext
    {
        public DbSet<TestEntity> Tests { get; set; }

        public MigrationDbContext(DbContextOptions options)
            : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<TestEntity>().AddMap(new TestEntityDbMapping());
        }
    }
}
