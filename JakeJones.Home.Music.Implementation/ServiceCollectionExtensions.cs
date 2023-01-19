using JakeJones.Home.Music.Implementation.Managers;
using JakeJones.Home.Music.Managers;
using Microsoft.Extensions.DependencyInjection;

namespace JakeJones.Home.Music.Implementation
{
	public static class ServiceCollectionExtensions
	{
		public static IServiceCollection AddMusic(this IServiceCollection services)
		{
			services.AddSingleton<IRecentMusicManager, RecentMusicManager>();

			return services;
		}
	}
}