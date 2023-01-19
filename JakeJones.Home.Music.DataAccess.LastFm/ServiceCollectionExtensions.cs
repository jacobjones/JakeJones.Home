using JakeJones.Home.Music.DataAccess.LastFm.Clients;
using JakeJones.Home.Music.DataAccess.LastFm.Configuration;
using JakeJones.Home.Music.DataAccess.LastFm.Repositories.Caching;
using JakeJones.Home.Music.Repositories;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;

namespace JakeJones.Home.Music.DataAccess.LastFm
{
	public static class ServiceCollectionExtensions
	{
		public static IServiceCollection AddMusicTracksDataAccess(this IServiceCollection services, IConfiguration configuration)
		{
			services.Configure<LastFmOptions>(configuration.GetSection("lastFm"));
			services.AddSingleton<ILastFmOptions>(x => x.GetRequiredService<IOptions<LastFmOptions>>().Value);

			services.AddSingleton<IRecentTracksRepository, RecentTracksRepositoryCachingProxy>();
			services.AddSingleton<ILastFmClient, LastFmClient>();

			return services;
		}
	}
}