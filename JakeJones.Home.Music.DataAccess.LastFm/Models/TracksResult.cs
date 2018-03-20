using System.Collections.Generic;
using RestSharp.Deserializers;

namespace JakeJones.Home.Music.DataAccess.LastFm.Models
{
	public class TracksResult
	{
		[DeserializeAs(Name = "track")]
		public List<TrackResult> Tracks { get; set; }

		[DeserializeAs(Name = "@attr")]
		public AttributesResult Info { get; set; }
	}
}