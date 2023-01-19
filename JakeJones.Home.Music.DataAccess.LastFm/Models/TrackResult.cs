using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace JakeJones.Home.Music.DataAccess.LastFm.Models
{
	public class TrackResult
	{
		[JsonPropertyName("name")]
		public string Name { get; set; }
		
		[JsonPropertyName("artist")]
		public ArtistResult Artist { get; set; }
		
		[JsonPropertyName("album")]
		public AlbumResult Album { get; set; }

		[JsonPropertyName("image")]
		public List<ImageResult> Images { get; set; }
	}
}