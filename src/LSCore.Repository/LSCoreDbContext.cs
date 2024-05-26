using Microsoft.EntityFrameworkCore;
using LSCore.Contracts.IManagers;

namespace LSCore.Repository;

public abstract class LSCoreDbContext<TContext> (DbContextOptions<TContext> options)
    : DbContext(options), ILSCoreDbContext
        where TContext : DbContext;