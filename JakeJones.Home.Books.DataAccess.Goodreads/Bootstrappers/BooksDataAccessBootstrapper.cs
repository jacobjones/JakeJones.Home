using JakeJones.Home.Books.DataAccess.Goodreads.Clients;
using JakeJones.Home.Books.DataAccess.Goodreads.Repositories;
using JakeJones.Home.Books.Repositories;
using Microsoft.Extensions.DependencyInjection;

namespace JakeJones.Home.Books.DataAccess.Goodreads.Bootstrappers
{
	public static class BooksDataAccessBootstrapper
	{
		public static void Register(IServiceCollection services)
		{
			services.AddSingleton<IBookRepository, BookRepository>();
			services.AddSingleton<IGoodreadsClient, GoodreadsClient>();
		}
	}
}