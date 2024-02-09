using SP.Playground.Contracts.Entities;
using System.Linq.Expressions;

namespace SP.Playground.Contracts.Enums.SortColumnCodes
{
    public static class UsersSortColumnCodes
    {
        public enum Users
        {
            Id
        }

        public static Dictionary<Users, Expression<Func<UserEntity, object>>> UsersSortRules = new()
        {
            { Users.Id, x => x.Id }
        };
    }
}
