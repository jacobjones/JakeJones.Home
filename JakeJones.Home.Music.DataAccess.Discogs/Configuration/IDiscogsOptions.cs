namespace JakeJones.Home.Music.DataAccess.Discogs.Configuration
{
	internal interface IDiscogsOptions
	{
		string ApiKey { get; }
		string ApiSecret { get; }
		int Timeout { get; }
		string UserAgent { get; }
	}
}