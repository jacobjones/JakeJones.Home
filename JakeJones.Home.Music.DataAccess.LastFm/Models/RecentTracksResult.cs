using System.Text.Json.Serialization;

namespace JakeJones.Home.Music.DataAccess.LastFm.Models
{
	public class RecentTracksResult
	{
		[JsonPropertyName("recenttracks")]
		public TracksResult RecentTracks { get; set; }
	}
}