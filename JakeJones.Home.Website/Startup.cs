using AutoMapper;
using JakeJones.Home.Blog.DataAccess.SqlServer.Bootstrappers;
using JakeJones.Home.Blog.Implementation.Bootstrappers;
using JakeJones.Home.Books.DataAccess.Goodreads.Bootstrappers;
using JakeJones.Home.Books.Implementation.Bootstrappers;
using JakeJones.Home.Core.Implementation.Bootstrappers;
using JakeJones.Home.Music.DataAccess.Discogs.Bootstrappers;
using JakeJones.Home.Music.DataAccess.LastFm.Bootstrappers;
using JakeJones.Home.Music.Implementation.Bootstrappers;
using JakeJones.Home.Website.Infrastructure;
using JakeJones.Home.Website.Infrastructure.Rewrite;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.AspNetCore.Rewrite;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace JakeJones.Home.Website
{
	public class Startup
	{
		public Startup(IConfiguration configuration, IWebHostEnvironment env)
		{
			Configuration = configuration;
			Environment = env;
			Scheduler = new PreventTimeOutScheduler();
		}

		private IConfiguration Configuration { get; }
		private IWebHostEnvironment Environment { get; }

		private PreventTimeOutScheduler Scheduler { get; }

		// This method gets called by the runtime. Use this method to add services to the container.
		public void ConfigureServices(IServiceCollection services)
		{
			services.AddControllersWithViews();
			services.AddRazorPages();

			// Cookie authentication.
			services
				.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
				.AddCookie(options =>
				{
					options.LoginPath = "/login/";
					options.LogoutPath = "/logout/";
				});

			services.AddSingleton<IActionContextAccessor, ActionContextAccessor>();

			services.AddRouting(options => options.LowercaseUrls = true);

			// Add all our AutoMapper configurations
			services.AddAutoMapper(cfg =>
			{
				cfg.AddProfile<BlogDataAccessMapConfiguration>();
				cfg.AddProfile<BlogImplemenatationMapConfiguration>();
			});

			CoreImplementationBootstrapper.Register(services, Configuration, Environment);
			BlogImplementationBootstrapper.Register(services);
			BlogDataAccessBootstrapper.Register(services, Configuration);
			BooksDataAccessBootstrapper.Register(services);
			BooksImplementationBootstrapper.Register(services);

			MusicAlbumsDataAccessBootstrapper.Register(services, Configuration);
			MusicTracksDataAccessBootstrapper.Register(services, Configuration);
			MusicImplementationBootstrapper.Register(services);
		}

		// This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
		public void Configure(IApplicationBuilder app, IWebHostEnvironment env, IHostApplicationLifetime appLifetime)
		{
			if (env.IsDevelopment())
			{
				app.UseBrowserLink();
				app.UseDeveloperExceptionPage();
			}
			else
			{
				app.UseExceptionHandler("/Home/Error");
				app.UseHsts();
			}

			if (!env.IsDevelopment())
			{
				appLifetime.ApplicationStarted.Register(() => Scheduler.Start(60));
				appLifetime.ApplicationStopping.Register(() => Scheduler.Stop());
			}

			app.UseHttpsRedirection();

			var rewriteOptions = new RewriteOptions();
			rewriteOptions.AddRedirectToNonWww();
			app.UseRewriter(rewriteOptions);

			app.UseStaticFiles();

			app.UseAuthentication();

			app.UseRouting();

			app.UseAuthorization();

			app.UseEndpoints(endpoints =>
			{
				endpoints.MapControllerRoute("default", "{controller=Home}/{action=Index}/{id?}");
			});
		}
	}
}
