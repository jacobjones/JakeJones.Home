namespace JakeJones.Home.Core.Managers
{
	public interface IUserManager
	{
		bool ValidateUser(string username, string password);

		bool IsAdmin();
	}
}
