using System.Text.Json.Serialization;

namespace JakeJones.Home.Music.DataAccess.LastFm.Models
{
	public class AttributesResult
	{
		[JsonPropertyName("page")]
		public int Page { get; set; }
		
		[JsonPropertyName("perPage")]
		public int PerPage { get; set; }
		
		[JsonPropertyName("totalPages")]
		public int TotalPages { get; set; }
		
		[JsonPropertyName("total")]
		public int Total { get; set; } 
	}
}