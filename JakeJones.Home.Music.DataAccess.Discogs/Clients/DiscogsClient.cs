using System;
using System.Linq;
using System.Net.Cache;
using System.Threading.Tasks;
using JakeJones.Home.Music.DataAccess.Discogs.Clients.UrlParameters;
using JakeJones.Home.Music.DataAccess.Discogs.Configuration;
using JakeJones.Home.Music.DataAccess.Discogs.Models;
using RestSharp;

namespace JakeJones.Home.Music.DataAccess.Discogs.Clients
{
	internal class DiscogsClient : IDiscogsClient
	{
		private readonly IDiscogsOptions _discogsOptions;
		private readonly IRestClient _restClient;

		private const string BaseUrl = "https://api.discogs.com/";

		public DiscogsClient(IDiscogsOptions discogsOptions)
		{
			_discogsOptions = discogsOptions;

			_restClient = new RestClient(BaseUrl)
			{
				CachePolicy = new RequestCachePolicy(RequestCacheLevel.NoCacheNoStore),
				UserAgent = _discogsOptions.UserAgent,
				Timeout = _discogsOptions.Timeout
			};
		}

		public async Task<SearchResult> SearchByAlbum(string artist, string title, int perPage, int currentPage)
		{
			var request = new RestRequest(ApiMethods.Search, Method.GET);

			request.AddParameter("key", _discogsOptions.ApiKey);
			request.AddParameter("secret", _discogsOptions.ApiSecret);

			request.AddParameter("artist", artist);
			request.AddParameter("release_title", title);
			request.AddParameter("per_page", perPage);
			request.AddParameter("page", currentPage);

			var response = await _restClient.ExecuteGetAsync<SearchResult>(request);

			var remainingHeader = response?.Headers?.FirstOrDefault(x => x.Name.Equals("X-Discogs-Ratelimit-Remaining", StringComparison.Ordinal));

			if (int.TryParse((string) remainingHeader?.Value, out int remaing))
			{
				if (remaing <= 0)
				{
					// Log
				}
			}

			// TODO: Handle exceptions/errors/timeouts!

			return response?.Data;
		}
	}
}