using LSCore.Contracts.Requests;
using SP.Playground.Contracts.Enums.SortColumnCodes;

namespace SP.Playground.Contracts.Requests.Users
{
    public class UsersGetMultipleSortedAndPagedRequest : LSCoreSortablePageableRequest<UsersSortColumnCodes.Users>
    {
    }
}
