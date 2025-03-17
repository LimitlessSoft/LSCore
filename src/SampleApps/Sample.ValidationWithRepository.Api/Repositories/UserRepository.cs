using Sample.ValidationWithRepository.Api.Entities;
using Sample.ValidationWithRepository.Api.Interfaces;

namespace Sample.ValidationWithRepository.Api.Repositories;

public class UserRepository : IUserRepository
{
	private static readonly List<UserEntity> _users = [];

	public bool UsernameOccupied(string username) => _users.Any(x => x.Username == username);

	public void Register(string requestUsername, string requestPassword) =>
		_users.Add(new UserEntity { Username = requestUsername, Password = requestPassword });
}
