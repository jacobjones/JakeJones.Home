using JakeJones.Home.Blog.DataAccess.SqlServer.Repositories.Caching;
using JakeJones.Home.Blog.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace JakeJones.Home.Blog.DataAccess.SqlServer
{
	public static class ServiceCollectionExtensions
	{
		public static IServiceCollection AddBlogDataAccess(this IServiceCollection services, IConfiguration configuration)
		{
			// TODO: Move this out eventually
			services.AddLazyCache();

			services.AddDbContext<BlogContext>(options => options.UseSqlServer(configuration.GetConnectionString("DefaultConnection")));

			services.AddScoped<IPostRepository, PostRepositoryCachingProxy>();
			services.AddScoped<ICommentRepository, CommentRepositoryCachingProxy>();

			return services;
		}
	}
}