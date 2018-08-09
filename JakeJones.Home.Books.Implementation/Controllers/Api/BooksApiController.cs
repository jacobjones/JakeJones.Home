using System.Threading.Tasks;
using JakeJones.Home.Books.Implementation.Controllers.Api.Models;
using JakeJones.Home.Books.Managers;
using Microsoft.AspNetCore.Mvc;

namespace JakeJones.Home.Books.Implementation.Controllers.Api
{
	[Route("api/books")]
	public class BooksApiController : ControllerBase
	{
		private readonly IBookManager _bookManager;

		public BooksApiController(IBookManager bookManager)
		{
			_bookManager = bookManager;
		}

		[Route("current")]
		public async Task<IActionResult> GetCurrent()
		{
			var book = await _bookManager.GetCurrentBookAsync();

			if (book == null)
			{
				return null;
			}

			return Ok(new BookApiModel($"{book.Author} - {book.Title}", book.ImageUrl));
		}
	}
}
