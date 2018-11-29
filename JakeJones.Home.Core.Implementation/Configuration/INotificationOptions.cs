namespace JakeJones.Home.Core.Implementation.Configuration
{
	public interface INotificationOptions
	{
		string Host { get; }
		int Port { get; }
		string Username { get; }
		string Password { get; }
		string From { get; }
		string To { get; }
	}
}