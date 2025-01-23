using Microsoft.EntityFrameworkCore;

namespace LSCore.Contracts.IManagers;

public interface ILSCoreDbContext
{
    DbSet<T> Set<T>() where T : class;
    int SaveChanges();
}
