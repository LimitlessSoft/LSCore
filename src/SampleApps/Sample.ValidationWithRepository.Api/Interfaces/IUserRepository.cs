namespace Sample.ValidationWithRepository.Api.Interfaces;

public interface IUserRepository
{
	bool UsernameOccupied(string username);
	void Register(string requestUsername, string requestPassword);
}
