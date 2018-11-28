namespace JakeJones.Home.Music.Models
{
	public class Track : ITrack
	{
		public Track(string artist, string title, string albumTitle)
		{
			Artist = artist;
			Title = title;
			AlbumTitle = albumTitle;
		}

		public Track(string artist, string title, string albumTitle, string imageUrl)
		{
			Artist = artist;
			Title = title;
			AlbumTitle = albumTitle;
			ImageUrl = imageUrl;
		}

		public string Artist { get; }
		public string Title { get; }
		public string AlbumTitle { get; }
		public string ImageUrl { get; }
	}
}