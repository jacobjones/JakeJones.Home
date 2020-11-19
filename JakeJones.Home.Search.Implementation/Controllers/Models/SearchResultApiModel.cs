namespace JakeJones.Home.Search.Implementation.Controllers.Models
{
	internal class SearchResultApiModel
	{
		public SearchResultApiModel(string name, string url, string excerpt)
		{

			Name = name;
			Url = url;
			Excerpt = excerpt;
		}

		public string Name { get; }

		public string Url { get; }

		public string Excerpt { get; }
	}
}