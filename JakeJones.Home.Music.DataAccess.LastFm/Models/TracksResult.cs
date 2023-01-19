using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace JakeJones.Home.Music.DataAccess.LastFm.Models
{
	public class TracksResult
	{
		[JsonPropertyName("track")]
		public List<TrackResult> Tracks { get; set; }

		[JsonPropertyName("@attr")]
		public AttributesResult Info { get; set; }
	}
}