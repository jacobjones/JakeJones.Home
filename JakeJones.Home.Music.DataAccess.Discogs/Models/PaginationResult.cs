using Newtonsoft.Json;

namespace JakeJones.Home.Music.DataAccess.Discogs.Models
{
	public class PaginationResult
	{
		[JsonProperty("per_page")]
		public int PerPage { get; set; }
		public int Items { get; set; }
		public int Page { get; set; }
		public int Pages { get; set; }
	}
}