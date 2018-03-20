using System.Net.Cache;
using System.Threading.Tasks;
using JakeJones.Home.Music.DataAccess.LastFm.Configuration;
using JakeJones.Home.Music.DataAccess.LastFm.Connector.UrlParameters;
using JakeJones.Home.Music.DataAccess.LastFm.Models;
using RestSharp;

namespace JakeJones.Home.Music.DataAccess.LastFm.Connector
{
	internal class LastFmApiConnector : ILastFmApiConnector
	{
		private readonly ILastFmOptions _lastFmOptions;
		private readonly IRestClient _restClient;
		

		private const string BaseUrl = "http://ws.audioscrobbler.com/2.0/";

		public LastFmApiConnector(ILastFmOptions lastFmOptions)
		{
			_lastFmOptions = lastFmOptions;

			_restClient = new RestClient(BaseUrl)
			{
				CachePolicy = new RequestCachePolicy(RequestCacheLevel.NoCacheNoStore),
				Timeout = _lastFmOptions.Timeout
			};

		}

		public async Task<RecentTracksResult> GetRecentTracks(string user, int limit)
		{
			var request = new RestRequest(Method.GET);

			request.AddParameter("api_key", _lastFmOptions.ApiKey);
			request.AddParameter("method", ApiMethods.RecentTracks);

			//Add a parameter to ensure we return JSON
			request.AddParameter("format", "json");

			request.AddParameter("user", user);
			request.AddParameter("limit", limit);

			var response = await _restClient.ExecuteGetTaskAsync<RecentTracksResult>(request);

			// TODO: Handle exceptions/errors/timeouts!

			return response.Data;
		}
	}
}