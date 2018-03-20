namespace JakeJones.Home.Music.DataAccess.LastFm.Configuration
{
	internal interface ILastFmOptions
	{
		string ApiKey { get; set; }
		int Timeout { get; set; }
	}
}