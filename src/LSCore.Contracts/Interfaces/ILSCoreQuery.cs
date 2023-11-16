using LSCore.Contracts.Http.Interfaces;
using LSCore.Contracts.IManagers;

namespace LSCore.Contracts.Interfaces
{
    public interface ILSCoreQuery<TPayload>
        where TPayload : class
    {
        abstract ILSCoreResponse<TPayload> Execute(ILSCoreDbContext dbContext);
    }

    public interface ILSCoreQuery<TRequest, TPayload> : ILSCoreQuery<TPayload>
        where TPayload : class
    {
        TRequest Request { get; set; }
    }
}