using JakeJones.Home.Core.Generators;
using JakeJones.Home.Core.Implementation.Configuration;
using JakeJones.Home.Core.Implementation.Generators;
using JakeJones.Home.Core.Implementation.Managers;
using JakeJones.Home.Core.Managers;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Extensions.Options;

namespace JakeJones.Home.Core.Implementation.Bootstrappers
{
	public static class CoreImplementationBootstrapper
	{
		public static void Register(IServiceCollection services, IHostingEnvironment hostingEnvironment)
		{
			var configuration = new ConfigurationBuilder()
				.AddJsonFile("JakeJones.Home.Core.Implementation.json")
				.AddJsonFile($"JakeJones.Home.Core.Implementation.{hostingEnvironment.EnvironmentName}.json", true)
				.Build();

			services.Configure<LoginOptions>(configuration.GetSection("user"));
			services.AddSingleton<ILoginOptions>(x => x.GetService<IOptions<LoginOptions>>().Value);

			services.TryAddSingleton<IHttpContextAccessor, HttpContextAccessor>();

			services.AddSingleton<IUserManager, UserManager>();
			services.AddSingleton<ISegmentGenerator, SegmentGenerator>();
		}
	}
}