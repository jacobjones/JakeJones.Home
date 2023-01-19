using JakeJones.Home.Books.DataAccess.Goodreads.Clients;
using JakeJones.Home.Books.DataAccess.Goodreads.Repositories;
using JakeJones.Home.Books.Repositories;
using Microsoft.Extensions.DependencyInjection;

namespace JakeJones.Home.Books.DataAccess.Goodreads
{
	public static class ServiceCollectionExtensions
	{
		public static IServiceCollection AddBooksDataAccess(this IServiceCollection services)
		{
			services.AddSingleton<IBookRepository, BookRepository>();
			services.AddSingleton<IGoodreadsClient, GoodreadsClient>();

			return services;
		}
	}
}