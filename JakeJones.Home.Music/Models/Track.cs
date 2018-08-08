namespace JakeJones.Home.Music.Models
{
	public class Track : ITrack
	{
		public Track(string artist, string title, string albumTitle, string albumMbid)
		{
			Artist = artist;
			Title = title;
			AlbumTitle = albumTitle;
			AlbumMbid = albumMbid;
		}

		public string Artist { get; }
		public string Title { get; }
		public string AlbumTitle { get; }
		public string AlbumMbid { get; }
	}
}