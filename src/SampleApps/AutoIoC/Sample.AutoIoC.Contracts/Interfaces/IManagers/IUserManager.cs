using Sample.AutoIoC.Contracts.Dtos.Users;

namespace Sample.AutoIoC.Contracts.Interfaces.IManagers;

public interface IUserManager
{
    UsersGetDto GetUser();
}