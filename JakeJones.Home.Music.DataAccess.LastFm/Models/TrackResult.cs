using System.Collections.Generic;
using RestSharp.Deserializers;

namespace JakeJones.Home.Music.DataAccess.LastFm.Models
{
	public class TrackResult
	{
		public string Name { get; set; }
		public ArtistResult Artist { get; set; }
		public AlbumResult Album { get; set; }

		[DeserializeAs(Name = "image")]
		public List<ImageResult> Images { get; set; }
	}
}