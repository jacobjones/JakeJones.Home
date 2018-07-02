using JakeJones.Home.Core.Generators;
using JakeJones.Home.Core.Implementation.Configuration;
using JakeJones.Home.Core.Implementation.Generators;
using JakeJones.Home.Core.Implementation.Managers;
using JakeJones.Home.Core.Managers;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;

namespace JakeJones.Home.Core.Implementation.Bootstrappers
{
	public static class CoreImplementationBootstrapper
	{
		public static void Register(IServiceCollection services)
		{
			var configuration = new ConfigurationBuilder().AddJsonFile("JakeJones.Home.Core.Implementation.json").Build();
			services.Configure<LoginOptions>(configuration.GetSection("user"));
			services.AddSingleton<ILoginOptions>(x => x.GetService<IOptions<LoginOptions>>().Value);

			services.AddSingleton<IUserManager, UserManager>();
			services.AddSingleton<ISegmentGenerator, SegmentGenerator>();
		}
	}
}