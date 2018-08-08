using System.Collections.Generic;
using System.Threading.Tasks;
using JakeJones.Home.Books.Models;
using JakeJones.Home.Books.Repositories.Properties;

namespace JakeJones.Home.Books.Repositories
{
	public interface IBookRepository
	{
		Task<IReadOnlyCollection<IBook>> GetAsync(BookShelf bookShelf);
	}
}