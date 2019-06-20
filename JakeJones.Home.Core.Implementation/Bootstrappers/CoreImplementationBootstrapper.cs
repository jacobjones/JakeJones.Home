using JakeJones.Home.Core.Configuration;
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
		public static void Register(IServiceCollection services, IConfiguration configuration, IHostingEnvironment hostingEnvironment)
		{
			services.Configure<LoginOptions>(configuration.GetSection("user"));
			services.AddSingleton<ILoginOptions>(x => x.GetService<IOptions<LoginOptions>>().Value);

			services.Configure<NotificationOptions>(configuration.GetSection("notification"));
			services.AddSingleton<INotificationOptions>(x => x.GetService<IOptions<NotificationOptions>>().Value);

			services.AddSingleton<IHoneypotOptions>(x => x.GetService<IOptions<HoneypotOptions>>().Value);

			services.TryAddSingleton<IHttpContextAccessor, HttpContextAccessor>();

			services.AddSingleton<IUserManager, UserManager>();
			services.AddSingleton<IHoneypotManager, HoneypotManager>();
			services.AddSingleton<INotificationManager, EmailNotificationManager>();

			services.AddSingleton<ISegmentGenerator, SegmentGenerator>();
		}
	}
}