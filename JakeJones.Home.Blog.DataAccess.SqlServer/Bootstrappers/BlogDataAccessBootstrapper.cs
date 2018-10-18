using JakeJones.Home.Blog.DataAccess.SqlServer.Repositories;
using JakeJones.Home.Blog.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace JakeJones.Home.Blog.DataAccess.SqlServer.Bootstrappers
{
	public static class BlogDataAccessBootstrapper
	{
		public static void Register(IServiceCollection services, string connectionString)
		{
			services.AddDbContext<BlogContext>(options => options.UseSqlServer(connectionString));

			services.AddScoped<IPostRepository, PostRepository>();
			services.AddScoped<ICommentRepository, CommentRepository>();
		}
	}
}