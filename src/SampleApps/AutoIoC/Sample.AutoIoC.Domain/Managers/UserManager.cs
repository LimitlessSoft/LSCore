using Sample.AutoIoC.Contracts.Dtos.Users;
using Sample.AutoIoC.Contracts.Interfaces.IManagers;

namespace Sample.AutoIoC.Domain.Managers;

public class UserManager : IUserManager
{
    public UsersGetDto GetUser()
    {
        return new UsersGetDto
        {
            Name = "John Doe",
        };
    }
}