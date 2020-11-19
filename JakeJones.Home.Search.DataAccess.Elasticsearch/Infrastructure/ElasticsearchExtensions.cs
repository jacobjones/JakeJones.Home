using System;
using Elasticsearch.Net;
using JakeJones.Home.Search.DataAccess.Elasticsearch.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Nest;

namespace JakeJones.Home.Search.DataAccess.Elasticsearch.Infrastructure
{
	public static class ElasticsearchExtensions
	{
		public static void AddElasticsearch(this IServiceCollection services, ISearchOptions searchOptions)
		{
			var pool = new SingleNodeConnectionPool(new Uri(searchOptions.Url));

			var connectionSettings = new ConnectionSettings(pool).DefaultIndex(searchOptions.IndexName);

			var client = new ElasticClient(connectionSettings);

			services.AddSingleton<IElasticClient>(client);
		}
	}
}