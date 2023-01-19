using System.Runtime.CompilerServices;
using JakeJones.Home.Music.DataAccess.Discogs.Clients;
using JakeJones.Home.Music.DataAccess.Discogs.Configuration;
using JakeJones.Home.Music.DataAccess.Discogs.Repositories;
using JakeJones.Home.Music.Repositories;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;

namespace JakeJones.Home.Music.DataAccess.Discogs
{
	public static class ServiceCollectionExtensions
	{
		public static IServiceCollection AddMusicAlbumsDataAccess(this IServiceCollection services, IConfiguration configuration)
		{
			services.Configure<DiscogsOptions>(configuration.GetSection("discogs"));
			services.AddSingleton<IDiscogsOptions>(x => x.GetRequiredService<IOptions<DiscogsOptions>>().Value);

			services.AddSingleton<IAlbumRepository, AlbumRepositoryCachingProxy>();
			services.AddSingleton<IDiscogsClient, DiscogsClient>();

			return services;
		}
	}
}