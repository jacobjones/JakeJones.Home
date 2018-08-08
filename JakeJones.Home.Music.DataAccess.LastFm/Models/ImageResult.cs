using RestSharp.Deserializers;

namespace JakeJones.Home.Music.DataAccess.LastFm.Models
{
	public class ImageResult
	{
		public string Size { get; set; }

		[DeserializeAs(Name = "#text")]
		public string Url { get; set; }
	}
}