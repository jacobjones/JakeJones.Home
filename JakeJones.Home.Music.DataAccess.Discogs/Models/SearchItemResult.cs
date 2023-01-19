using System.Text.Json.Serialization;

namespace JakeJones.Home.Music.DataAccess.Discogs.Models
{
	internal class SearchItemResult
	{
		// This is a combination of the artist and album
		[JsonPropertyName("title")]
		public string Title { get; set; }
		
		[JsonPropertyName("year")]
		public int Year { get; set; }

		[JsonPropertyName("cover_image")]
		public string ImageUrl { get; set; }
	}
}