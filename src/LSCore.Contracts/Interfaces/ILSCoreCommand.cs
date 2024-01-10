using LSCore.Contracts.Http.Interfaces;
using LSCore.Contracts.IManagers;

namespace LSCore.Contracts.Interfaces
{
    public interface ILSCoreCommand
    {
        abstract ILSCoreResponse Execute(ILSCoreDbContext dbContext);
    }

    public interface ILSCoreCommand<TRequest>
    {
        TRequest Request { get; set; }
        abstract ILSCoreResponse Execute(ILSCoreDbContext dbContext);
    }
}
