using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using JakeJones.Home.Books.DataAccess.Goodreads.Clients;
using JakeJones.Home.Books.DataAccess.Goodreads.Clients.UrlParameters;
using JakeJones.Home.Books.Models;
using JakeJones.Home.Books.Repositories;
using JakeJones.Home.Books.Repositories.Properties;

namespace JakeJones.Home.Books.DataAccess.Goodreads.Repositories
{
	internal class BookRepository : IBookRepository
	{
		private readonly IGoodreadsClient _goodreadsClient;

		public BookRepository(IGoodreadsClient goodreadsClient)
		{
			_goodreadsClient = goodreadsClient;
		}

		public async Task<IReadOnlyCollection<IBook>> GetAsync(BookShelf bookShelf)
		{
			var books = await _goodreadsClient.GetBooksAsync(bookShelf.Equals(BookShelf.Read) ? BookShelves.Read : BookShelves.CurrentlyReading);

			return books?.Select(x => new Book(x.Author, x.Title, x.Link, x.Image)).ToList();
		}
	}
}