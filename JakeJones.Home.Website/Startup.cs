using JakeJones.Home.Blog.Bootstrappers;
using JakeJones.Home.Blog.DataAccess.SqlServer;
using JakeJones.Home.Blog.DataAccess.SqlServer.Bootstrappers;
using JakeJones.Home.Music.DataAccess.Discogs.Bootstrappers;
using JakeJones.Home.Music.DataAccess.LastFm.Bootstrappers;
using JakeJones.Home.Music.Implementation.Bootstrappers;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace JakeJones.Home.Website
{
	public class Startup
	{
		public Startup(IConfiguration configuration)
		{
			Configuration = configuration;
		}

		public IConfiguration Configuration { get; }

		// This method gets called by the runtime. Use this method to add services to the container.
		public void ConfigureServices(IServiceCollection services)
		{
			services.AddMvc();

			BlogBootstrapper.Register(services);
			BlogDataAccessBootstrapper.Register(services, Configuration.GetConnectionString("DefaultConnection"));

			MusicAlbumsDataAccessBootstrapper.Register(services);
			MusicTracksDataAccessBootstrapper.Register(services);
			MusicImplementationBootstrapper.Register(services);
		}

		// This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
		public void Configure(IApplicationBuilder app, IHostingEnvironment env)
		{
			if (env.IsDevelopment())
			{
				app.UseBrowserLink();
				app.UseDeveloperExceptionPage();
			}
			else
			{
				app.UseExceptionHandler("/Home/Error");
			}

			app.UseStaticFiles();

			app.UseMvc(routes =>
			{
				routes.MapRoute(
					name: "default",
					template: "{controller=Home}/{action=Index}/{id?}");
			});
		}
	}
}
