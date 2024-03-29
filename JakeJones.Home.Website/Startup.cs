﻿using JakeJones.Home.Blog.DataAccess.SqlServer;
using JakeJones.Home.Blog.DataAccess.SqlServer.Configuration;
using JakeJones.Home.Blog.Implementation;
using JakeJones.Home.Blog.Implementation.Configuration;
using JakeJones.Home.Books.DataAccess.Goodreads;
using JakeJones.Home.Books.Implementation;
using JakeJones.Home.Core.Implementation;
using JakeJones.Home.Music.DataAccess.Discogs;
using JakeJones.Home.Music.DataAccess.LastFm;
using JakeJones.Home.Music.Implementation;
using JakeJones.Home.Website.Infrastructure;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
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
				cfg.AddProfile<BlogImplementationMapConfiguration>();
			});

			services.AddCore(Configuration, Environment);
			services.AddBlog();
			services.AddBlogDataAccess(Configuration);
			services.AddBooks();
			services.AddBooksDataAccess();
			services.AddMusic();
			services.AddMusicAlbumsDataAccess(Configuration);
			services.AddMusicTracksDataAccess(Configuration);
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
			rewriteOptions.AddRedirectToNonWww(StatusCodes.Status301MovedPermanently);
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
