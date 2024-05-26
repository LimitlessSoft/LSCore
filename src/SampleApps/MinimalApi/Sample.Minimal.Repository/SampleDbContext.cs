using Sample.Minimal.Repository.EntityMaps;
using Sample.Minimal.Contracts.Entities;
using Microsoft.EntityFrameworkCore;
using LSCore.Repository;

namespace Sample.Minimal.Repository;

public class SampleDbContext (DbContextOptions<SampleDbContext> options)
    : LSCoreDbContext<SampleDbContext>(options)
{
    public DbSet<UserEntity> Users { get; set; }
    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<UserEntity>().AddMap(new UserEntityMap());
    }
}