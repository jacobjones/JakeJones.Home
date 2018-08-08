using RestSharp.Deserializers;

namespace JakeJones.Home.Music.DataAccess.Discogs.Models
{
	internal class SearchItemResult
	{
		// This is a combination of the artist and album
		public string Title { get; set; }
		public int Year { get; set; }

		[DeserializeAs(Name = "cover_image")]
		public string ImageUrl { get; set; }
	}
}