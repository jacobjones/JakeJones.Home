using System.Collections.Generic;
using System.Threading.Tasks;
using JakeJones.Home.Books.DataAccess.Goodreads.Models;

namespace JakeJones.Home.Books.DataAccess.Goodreads.Clients
{
	public interface IGoodreadsClient
	{
		Task<ICollection<ItemXmlModel>> GetBooksAsync(string bookShelf);
	}
}