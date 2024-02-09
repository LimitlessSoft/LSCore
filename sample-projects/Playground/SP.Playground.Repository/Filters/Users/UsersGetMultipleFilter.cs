using SP.Playground.Contracts.Entities;
using LSCore.Contracts.Interfaces;
using System.Linq.Expressions;

namespace SP.Playground.Repository.Filters.Users
{
    public class UsersGetMultipleFilter : ILSCoreFilter<UserEntity>
    {
        public List<Expression<Func<UserEntity, bool>>> FiltersExpressions { get; set; } = new List<Expression<Func<UserEntity, bool>>>()
        {
            x => x.IsActive,
            x => x.Name.Contains("John")
        };
    }
}
