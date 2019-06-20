using JakeJones.Home.Music.DataAccess.Discogs.Clients;
using JakeJones.Home.Music.DataAccess.Discogs.Configuration;
using JakeJones.Home.Music.DataAccess.Discogs.Repositories;
using JakeJones.Home.Music.Repositories;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;

namespace JakeJones.Home.Music.DataAccess.Discogs.Bootstrappers
{
	public static class MusicAlbumsDataAccessBootstrapper
	{
		public static void Register(IServiceCollection services, IConfiguration configuration)
		{
			services.Configure<DiscogsOptions>(configuration.GetSection("discogs"));
			services.AddSingleton<IDiscogsOptions>(x => x.GetService<IOptions<DiscogsOptions>>().Value);

			services.AddSingleton<IAlbumRepository, AlbumRepositoryCachingProxy>();
			services.AddSingleton<IDiscogsClient, DiscogsClient>();
		}
	}
}