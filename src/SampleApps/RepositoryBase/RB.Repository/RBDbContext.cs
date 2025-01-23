using LSCore.Contracts.IManagers;
using Microsoft.EntityFrameworkCore;
using RB.Contracts.Entities;

namespace RB.Repository;

public class RBDbContext : DbContext, ILSCoreDbContext
{
    public DbSet<ProductEntity> Products { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        base.OnConfiguring(optionsBuilder);
        optionsBuilder.UseInMemoryDatabase("RB");
    }
}