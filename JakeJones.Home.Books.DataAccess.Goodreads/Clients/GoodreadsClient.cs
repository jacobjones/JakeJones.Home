using System.Collections.Generic;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using JakeJones.Home.Books.DataAccess.Goodreads.Models;
using RestSharp;

namespace JakeJones.Home.Books.DataAccess.Goodreads.Clients
{
	internal class GoodreadsClient : IGoodreadsClient
	{
		private readonly RestClient _restClient;
		// TODO: Move to config
		private const string UserId = "83924133";

		public GoodreadsClient()
		{
			var options = new RestClientOptions($"https://www.goodreads.com/review/list_rss/{UserId}")
			{
				CachePolicy = new CacheControlHeaderValue {NoCache = true, NoStore = true}
			};

			_restClient = new RestClient(options);
		}

		public async Task<ICollection<ItemXmlModel>> GetBooksAsync(string bookShelf)
		{
			var request = new RestRequest();

			if (!string.IsNullOrEmpty(bookShelf))
			{
				request.AddParameter("shelf", bookShelf);
			}

			var result = await _restClient.ExecuteGetAsync<BookshelfXmlModel>(request);

			if (!result.IsSuccessful)
			{
				// TODO: Log!
				return null;
			}

			return result?.Data?.Channel?.Items;
		}
	}
}