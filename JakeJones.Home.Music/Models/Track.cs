using System;

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

		public string Artist { get; }
		public string Title { get; }
		public string AlbumTitle { get; }
	}
}