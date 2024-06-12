using System.Linq.Expressions;
using Sample.Minimal.Contracts.Entities;

namespace Sample.Minimal.Contracts.SortColumnCodes;

public static class UsersSortColumnCodes
{
    public enum Users
    {
        Id,
        Username
    }

    public static Dictionary<Users, Expression<Func<UserEntity, object>>> UsersSortRules = new()
    {
        { Users.Id, x => x.Id },
        { Users.Username, x => x.Username }
    };
}