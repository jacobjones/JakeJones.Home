using AutoMapper;
using JakeJones.Home.Blog.DataAccess.SqlServer.Models;
using JakeJones.Home.Blog.DataAccess.SqlServer.Services;
using JakeJones.Home.Blog.Models;
using JakeJones.Home.Blog.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace JakeJones.Home.Blog.DataAccess.SqlServer.Bootstrappers
{
	public static class BlogDataAccessBootstrapper
	{
		public static void Register(IServiceCollection services, string connectionString)
		{
			services.AddDbContext<BlogContext>(options => options.UseMySql(connectionString));

			services.AddAutoMapper(cfg =>
			{
				cfg.CreateMap<IPost, PostEntity>();
				cfg.CreateMap<PostEntity, IPost>().As<Post>();
			});
			
			services.AddScoped<IBlogService, BlogService>();
		}
	}
}