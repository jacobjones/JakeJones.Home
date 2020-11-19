using JakeJones.Home.Search.DataAccess.Elasticsearch.Configuration;
using JakeJones.Home.Search.DataAccess.Elasticsearch.Infrastructure;
using JakeJones.Home.Search.DataAccess.Elasticsearch.Managers;
using JakeJones.Home.Search.Managers;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;

namespace JakeJones.Home.Search.DataAccess.Elasticsearch.Bootstrappers
{
	public static class SearchDataAccessBootstrapper
	{
		public static void Register(IServiceCollection services, IConfiguration configuration)
		{
			services.Configure<SearchOptions>(configuration.GetSection("search"));
			services.AddSingleton<ISearchOptions>(x => x.GetService<IOptions<SearchOptions>>().Value);

			services.AddSingleton<ISearchManager, SearchManager>();

			var serviceProvider = services.BuildServiceProvider();
			services.AddElasticsearch(serviceProvider.GetService<ISearchOptions>());
		}
	}
}