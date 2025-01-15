namespace Validators.Contracts;

public interface IUserManager
{
    List<string> GetUsers(GetUsersRequest request);
}