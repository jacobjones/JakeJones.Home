namespace JakeJones.Home.Music.DataAccess.Discogs.Configuration
{
	internal class DiscogsOptions : IDiscogsOptions
	{
		public string ApiKey { get; set; }
		public string ApiSecret { get; set; }
		public int Timeout { get; set; }
		public string UserAgent { get; set; }
	}
}