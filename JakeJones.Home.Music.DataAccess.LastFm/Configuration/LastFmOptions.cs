namespace JakeJones.Home.Music.DataAccess.LastFm.Configuration
{
	internal class LastFmOptions : ILastFmOptions
	{
		public string ApiKey { get; set; }
		public int Timeout { get; set; }
	}
}