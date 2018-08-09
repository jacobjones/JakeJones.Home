namespace JakeJones.Home.Books.Implementation.Controllers.Api.Models
{
	public class BookApiModel
	{
		public BookApiModel(string title, string imageUrl)
		{
			Title = title;
			ImageUrl = imageUrl;
		}

		public string Title { get; set; }

		public string ImageUrl { get; set; }
	}
}