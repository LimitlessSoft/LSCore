using LSCore.Domain.Extensions;
using Validators.Contracts;

namespace Validators.Domain;

public class UserManager : IUserManager
{
    public List<string> GetUsers(GetUsersRequest request)
    {
        request.Validate();
        return
        [
            "Alice",
            "Bob",
            "Charlie"
        ];
    }
}