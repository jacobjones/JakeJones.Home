namespace JakeJones.Home.Core.Implementation.Configuration
{
	internal interface ILoginOptions
	{
		string Username { get; }
		string Password { get; }
	    string Salt { get; }
	}
}