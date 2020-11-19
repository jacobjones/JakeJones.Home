namespace JakeJones.Home.Search.DataAccess.Elasticsearch.Configuration
{
	public interface ISearchOptions
	{
		string Url { get; }

		string IndexName { get; }
	}
}