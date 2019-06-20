using JakeJones.Home.Blog.DataAccess.SqlServer.Repositories.Caching;
using JakeJones.Home.Blog.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace JakeJones.Home.Blog.DataAccess.SqlServer.Bootstrappers
{
	public static class BlogDataAccessBootstrapper
	{
		public static void Register(IServiceCollection services, IConfiguration configuration)
		{
			// TODO: Move this out eventually
			services.AddLazyCache();

			services.AddDbContext<BlogContext>(options => options.UseSqlServer(configuration.GetConnectionString("DefaultConnection")));

			services.AddScoped<IPostRepository, PostRepositoryCachingProxy>();
			services.AddScoped<ICommentRepository, CommentRepositoryCachingProxy>();
		}
	}
}