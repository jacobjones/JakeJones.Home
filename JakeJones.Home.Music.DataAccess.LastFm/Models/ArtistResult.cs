using RestSharp.Deserializers;

namespace JakeJones.Home.Music.DataAccess.LastFm.Models
{
	public class ArtistResult
	{
		public string Mbid { get; set; }

		[DeserializeAs(Name = "#text")]
		public string Name { get; set; }
	}
}