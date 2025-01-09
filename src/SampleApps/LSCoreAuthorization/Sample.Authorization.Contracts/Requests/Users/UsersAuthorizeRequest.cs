namespace Sample.Authorization.Contracts.Requests.Users;

public class UsersAuthorizeRequest
{
    public string Username { get; set; }
    public string Password { get; set; }
}