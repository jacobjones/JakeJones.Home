using JakeJones.Home.Blog.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace JakeJones.Home.Blog.Bootstrappers
{
	public static class BlogBootstrapper
	{
		public static void Register(IServiceCollection services)
		{
			// Make configurable
			services.AddSingleton<IBlogOptions, BlogOptions>();
		}
	}
}