namespace Sample.Authorization.Contracts.Requests.Users;

public class UsersSetPasswordRequest
{
    public string Username { get; set; }
    public string Password { get; set; }
}