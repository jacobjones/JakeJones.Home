using JakeJones.Home.Music.DataAccess.LastFm.Configuration;
using JakeJones.Home.Music.DataAccess.LastFm.Connector;
using JakeJones.Home.Music.DataAccess.LastFm.Repositories;
using JakeJones.Home.Music.Repositories;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;

namespace JakeJones.Home.Music.DataAccess.LastFm.Bootstrapper
{
	public static class MusicTracksDataAccessBootstrapper
	{
		public static void Register(IServiceCollection services)
		{
			var configuration = new ConfigurationBuilder().AddJsonFile("JakeJones.Home.Music.DataAccess.LastFm.json").Build();
			services.Configure<LastFmOptions>(configuration.GetSection("lastFm"));
			services.AddSingleton<ILastFmOptions>(x => x.GetService<IOptions<LastFmOptions>>().Value);

			services.AddSingleton<IRecentTracksRepository, RecentTracksRepository>();
			services.AddSingleton<ILastFmApiConnector, LastFmApiConnector>();
		}
	}
}