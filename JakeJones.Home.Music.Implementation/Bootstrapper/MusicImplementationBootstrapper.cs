using JakeJones.Home.Music.Implementation.Managers;
using JakeJones.Home.Music.Managers;
using Microsoft.Extensions.DependencyInjection;

namespace JakeJones.Home.Music.Implementation.Bootstrapper
{
	public static class MusicImplementationBootstrapper
	{
		public static void Register(IServiceCollection services)
		{
			services.AddSingleton<IRecentMusicManager, RecentMusicManager>();
		}
	}
}