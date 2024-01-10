using LSCore.Contracts.Http.Interfaces;
using LSCore.Contracts.IManagers;
using LSCore.Contracts.Interfaces;

namespace LSCore.Repository
{
    public abstract class LSCoreBaseCommand : ILSCoreCommand
    {
        public abstract ILSCoreResponse Execute(ILSCoreDbContext dbContext);
    }

    public abstract class LSCoreBaseCommand<TRequest> : ILSCoreCommand<TRequest>
    {
        public TRequest? Request { get; set; }
        public abstract ILSCoreResponse Execute(ILSCoreDbContext dbContext);
    }
}
