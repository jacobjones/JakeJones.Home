using System.Collections.Generic;

namespace JakeJones.Home.Music.DataAccess.Discogs.Models
{
	internal class SearchItemResult
	{
		// This is a combination of the artist and album
		public string Title { get; set; }
		public int Year { get; set; }
		public List<string> Style { get; set; }
		public List<string> Genre { get; set; }
	}
}