namespace LSCore.Auth.UserPass.Domain;

public static class LSCoreAuthUserPassHelpers
{
	/// <summary>
	/// Hash a raw password using a random salt.
	/// </summary>
	/// <param name="rawPassword">The raw password to be hashed.</param>
	/// <returns>The hashed password.</returns>
	public static string HashPassword(string rawPassword) =>
		BCrypt.Net.BCrypt.EnhancedHashPassword(rawPassword, Random.Shared.Next(8, 12));
}
