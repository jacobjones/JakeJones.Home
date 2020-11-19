namespace JakeJones.Home.Search.DataAccess.Elasticsearch.Configuration
{
	internal class SearchOptions : ISearchOptions
	{
		public string Url { get; set; }

		public string IndexName { get; set; }
	}
}