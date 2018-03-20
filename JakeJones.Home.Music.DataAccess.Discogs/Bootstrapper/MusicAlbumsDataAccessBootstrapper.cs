using JakeJones.Home.Music.DataAccess.Discogs.Configuration;
using JakeJones.Home.Music.DataAccess.Discogs.Connectors;
using JakeJones.Home.Music.DataAccess.Discogs.Repositories;
using JakeJones.Home.Music.Repositories;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;

namespace JakeJones.Home.Music.DataAccess.Discogs.Bootstrapper
{
	public static class MusicAlbumsDataAccessBootstrapper
	{
		public static void Register(IServiceCollection services)
		{
			var configuration = new ConfigurationBuilder().AddJsonFile("JakeJones.Home.Music.DataAccess.Discogs.json").Build();
			services.Configure<DiscogsOptions>(configuration.GetSection("discogs"));
			services.AddSingleton<IDiscogsOptions>(x => x.GetService<IOptions<DiscogsOptions>>().Value);

			services.AddSingleton<IAlbumRepository, AlbumRepository>();
			services.AddSingleton<IDiscogsApiConnector, DiscogsApiConnector>();
		}
	}
}