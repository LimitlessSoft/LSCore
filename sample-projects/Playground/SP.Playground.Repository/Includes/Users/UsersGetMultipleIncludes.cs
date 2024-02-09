using SP.Playground.Contracts.Entities;
using LSCore.Contracts.Interfaces;
using System.Linq.Expressions;

namespace SP.Playground.Repository.Includes.Users
{
    public class UsersGetMultipleIncludes : ILSCoreIncludes<UserEntity>
    {
        public List<Expression<Func<UserEntity, dynamic?>>> IncludesExpressions { get; set; } = new()
        {
            x => x.City,
            x => x.City.Streets
        };
    }
}
