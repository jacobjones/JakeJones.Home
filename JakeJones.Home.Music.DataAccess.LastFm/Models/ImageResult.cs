
using System.Text.Json.Serialization;

namespace JakeJones.Home.Music.DataAccess.LastFm.Models
{
	public class ImageResult
	{
		[JsonPropertyName("size")]
		public ImageSizeResult? Size { get; set; }

		[JsonPropertyName("#text")]
		public string Url { get; set; }
	}
}