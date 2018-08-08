namespace JakeJones.Home.Music.Models
{
	public class Album : IAlbum
	{
		public Album(string artist, string title, int year, string imageUrl)
		{
			Artist = artist;
			Title = title;
			Year = year;
			ImageUrl = imageUrl;
		}

		public string Artist { get; }
		public string Title { get; }
		public int Year { get; }
		public string ImageUrl { get; }
	}
}