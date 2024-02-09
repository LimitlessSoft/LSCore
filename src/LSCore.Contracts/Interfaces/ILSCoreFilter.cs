using LSCore.Contracts.Entities;
using System.Linq.Expressions;

namespace LSCore.Contracts.Interfaces
{
    public interface ILSCoreFilter<TEntity>
        where TEntity : LSCoreEntity
    {
        List<Expression<Func<TEntity, bool>>> FiltersExpressions { get; set; }
    }
}
