using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace JakeJones.Home.Music.DataAccess.Discogs.Models
{
	internal class SearchResult
	{
		[JsonPropertyName("results")]
		public List<SearchItemResult> Results { get; set; } 
	}
}