using JakeJones.Home.Blog.Builders;
using JakeJones.Home.Blog.Configuration;
using JakeJones.Home.Blog.Implementation.Builders;
using JakeJones.Home.Blog.Implementation.Managers;
using JakeJones.Home.Blog.Implementation.Resolvers;
using JakeJones.Home.Blog.Managers;
using JakeJones.Home.Blog.Resolvers;
using Microsoft.Extensions.DependencyInjection;

namespace JakeJones.Home.Blog.Implementation
{
	public static class ServiceCollectionExtensions
	{
		public static IServiceCollection AddBlog(this IServiceCollection services)
		{
			services.AddSingleton<IBlogOptions, BlogOptions>();
			services.AddSingleton<IBlogUrlResolver, BlogUrlResolver>();
			services.AddSingleton<IImageManager, ImageManager>();
			services.AddScoped<IBlogManager, BlogManager>();
			services.AddScoped<ICommentManager, CommentManager>();
			services.AddScoped<IBlogRssFeedBuilder, BlogRssFeedBuilder>();

			return services;
		}
	}
}