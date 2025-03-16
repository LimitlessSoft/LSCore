using LSCore.Repository.Contracts;
using Microsoft.EntityFrameworkCore;

namespace LSCore.Repository;

public abstract class LSCoreDbContext<TContext>(DbContextOptions<TContext> options)
	: DbContext(options),
		ILSCoreDbContext
	where TContext : DbContext;
