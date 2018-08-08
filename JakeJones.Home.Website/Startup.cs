using AutoMapper;
using JakeJones.Home.Blog.DataAccess.SqlServer.Bootstrappers;
using JakeJones.Home.Blog.Implementation.Bootstrappers;
using JakeJones.Home.Books.DataAccess.Goodreads.Bootstrappers;
using JakeJones.Home.Books.Implementation.Bootstrappers;
using JakeJones.Home.Core.Implementation.Bootstrappers;
using JakeJones.Home.Music.DataAccess.Discogs.Bootstrappers;
using JakeJones.Home.Music.DataAccess.LastFm.Bootstrappers;
using JakeJones.Home.Music.Implementation.Bootstrappers;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Infrastructure;
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

			// Cookie authentication.
			services
				.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
				.AddCookie(options =>
				{
					options.LoginPath = "/login/";
					options.LogoutPath = "/logout/";
				});

			services.AddSingleton<IActionContextAccessor, ActionContextAccessor>();

			// Add all out AutoMapper configurations
			services.AddAutoMapper(cfg =>
			{
				cfg.AddProfile<BlogDataAccessMapConfiguration>();
				cfg.AddProfile<BlogImplemenatationMapConfiguration>();
			});

			CoreImplementationBootstrapper.Register(services);
			BlogImplementationBootstrapper.Register(services);
			BlogDataAccessBootstrapper.Register(services, Configuration.GetConnectionString("DefaultConnection"));
			BooksDataAccessBootstrapper.Register(services);
			BooksImplementationBootstrapper.Register(services);

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

			app.UseAuthentication();

			app.UseMvc(routes =>
			{
				routes.MapRoute(
					name: "default",
					template: "{controller=Home}/{action=Index}/{id?}");
			});
		}
	}
}
