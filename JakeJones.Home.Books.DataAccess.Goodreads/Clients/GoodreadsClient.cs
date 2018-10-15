using System;
using System.Collections.Generic;
using System.Net.Cache;
using System.Threading.Tasks;
using JakeJones.Home.Books.DataAccess.Goodreads.Models;
using RestSharp;
using RestSharp.Deserializers;

namespace JakeJones.Home.Books.DataAccess.Goodreads.Clients
{
	internal class GoodreadsClient : IGoodreadsClient
	{
		private readonly IRestClient _restClient;
		// TODO: Move to config
		private const string UserId = "83924133";

		public GoodreadsClient()
		{
			_restClient = new RestClient($"https://www.goodreads.com/review/list_rss/{UserId}")
			{
				CachePolicy = new RequestCachePolicy(RequestCacheLevel.NoCacheNoStore)
			};

			// Parsing the item array gave issues with the RestSharp XmlDeserializer,
			// unfortunately this means we have to utilise the .Net one.
			_restClient.ClearHandlers();
			_restClient.AddHandler("application/xml", new DotNetXmlDeserializer());
		}

		public async Task<ICollection<ItemXmlModel>> GetBooksAsync(string bookShelf)
		{
			var request = new RestRequest(Method.GET);

			if (!string.IsNullOrEmpty(bookShelf))
			{
				request.AddParameter("shelf", bookShelf);
			}

			var result = await _restClient.ExecuteGetTaskAsync<BookshelfXmlModel>(request);

			if (!result.IsSuccessful)
			{
				// TODO: Log!
				return null;
			}

			return result?.Data?.Channel?.Items;
		}
	}
}