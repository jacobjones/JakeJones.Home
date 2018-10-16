using JakeJones.Home.Blog.Builders;
using JakeJones.Home.Blog.Configuration;
using JakeJones.Home.Blog.Implementation.Builders;
using JakeJones.Home.Blog.Implementation.Managers;
using JakeJones.Home.Blog.Implementation.Resolvers;
using JakeJones.Home.Blog.Managers;
using JakeJones.Home.Blog.Resolvers;
using Microsoft.Extensions.DependencyInjection;

namespace JakeJones.Home.Blog.Implementation.Bootstrappers
{
	public static class BlogImplementationBootstrapper
	{
		public static void Register(IServiceCollection services)
		{
			services.AddSingleton<IBlogOptions, BlogOptions>();
			services.AddSingleton<IBlogUrlResolver, BlogUrlResolver>();
			services.AddScoped<IBlogManager, BlogManager>();
			services.AddScoped<IBlogRssFeedBuilder, BlogRssFeedBuilder>();
		}
	}
}