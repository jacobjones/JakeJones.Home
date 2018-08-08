using System.Linq;
using System.Threading.Tasks;
using JakeJones.Home.Books.Managers;
using JakeJones.Home.Books.Models;
using JakeJones.Home.Books.Repositories;
using JakeJones.Home.Books.Repositories.Properties;

namespace JakeJones.Home.Books.Implementation.Managers
{
	internal class BookManager : IBookManager
	{
		private readonly IBookRepository _bookRepository;

		public BookManager(IBookRepository bookRepository)
		{
			_bookRepository = bookRepository;
		}

		public virtual async Task<IBook> GetCurrentBookAsync()
		{
			var current = (await _bookRepository.GetAsync(BookShelf.CurrentlyReading))?.FirstOrDefault();

			if (current == null)
			{
				return (await _bookRepository.GetAsync(BookShelf.Read))?.FirstOrDefault();
			}

			return current;
		}
	}
}