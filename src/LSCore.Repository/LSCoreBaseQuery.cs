using LSCore.Contracts.Http.Interfaces;
using LSCore.Contracts.IManagers;
using LSCore.Contracts.Interfaces;

namespace LSCore.Repository
{
    public abstract class LSCoreBaseQuery<TPayload> : ILSCoreQuery<TPayload>
        where TPayload : class
    {
        public abstract ILSCoreResponse<TPayload> Execute(ILSCoreDbContext dbContext);
    }

    public abstract class LSCoreBaseQuery<TRequest, TPayload> : ILSCoreQuery<TPayload>
        where TPayload : class
    {
        public TRequest? Request { get; set; }
        public abstract ILSCoreResponse<TPayload> Execute(ILSCoreDbContext dbContext);
    }
}
