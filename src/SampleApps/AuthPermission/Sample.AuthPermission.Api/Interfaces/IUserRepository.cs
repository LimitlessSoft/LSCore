using Sample.AuthPermission.Api.Entities;

namespace Sample.AuthPermission.Api.Interfaces;

public interface IUserRepository
{
	UserEntity? GetOrDefault(string username);
}
