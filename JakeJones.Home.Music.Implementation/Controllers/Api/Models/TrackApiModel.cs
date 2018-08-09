namespace JakeJones.Home.Music.Implementation.Controllers.Api.Models
{
	public class TrackApiModel
	{
		public TrackApiModel(string title, string imageUrl)
		{
			Title = title;
			ImageUrl = imageUrl;
		}

		public string Title { get; set; }

		public string ImageUrl { get; set; }
	}
}