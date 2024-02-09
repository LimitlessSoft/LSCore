using System.Linq.Expressions;

namespace LSCore.Contracts.Interfaces
{
    public interface ILSCoreIncludes<TEntity>
    {
        List<Expression<Func<TEntity, dynamic?>>> IncludesExpressions { get; set; }
    }
}
