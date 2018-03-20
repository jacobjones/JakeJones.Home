using System.Collections.Generic;

namespace JakeJones.Home.Music.Models
{
	public class Album : IAlbum
	{
		public Album(string artist, string title, int year, IReadOnlyCollection<string> style, IReadOnlyCollection<string> genre)
		{
			Artist = artist;
			Title = title;
			Year = year;
			Style = style;
			Genre = genre;
		}

		public string Artist { get; }
		public string Title { get; }
		public int Year { get; }
		public IReadOnlyCollection<string> Style { get; }
		public IReadOnlyCollection<string> Genre { get; }
	}
}