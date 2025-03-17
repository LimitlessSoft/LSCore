using Microsoft.EntityFrameworkCore;

namespace LSCore.Repository.Contracts;

public interface ILSCoreDbContext
{
	DbSet<T> Set<T>()
		where T : class;
	int SaveChanges();
}
