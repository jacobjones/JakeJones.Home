using System;
using System.Net.Http.Headers;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using JakeJones.Home.Music.DataAccess.LastFm.Clients.UrlParameters;
using JakeJones.Home.Music.DataAccess.LastFm.Configuration;
using JakeJones.Home.Music.DataAccess.LastFm.Models;
using RestSharp;
using RestSharp.Serializers.Json;

namespace JakeJones.Home.Music.DataAccess.LastFm.Clients
{
	internal class LastFmClient : ILastFmClient
	{
		private readonly ILastFmOptions _lastFmOptions;
		private readonly RestClient _restClient;

		private const string BaseUrl = "http://ws.audioscrobbler.com/2.0/";

		public LastFmClient(ILastFmOptions lastFmOptions)
		{
			_lastFmOptions = lastFmOptions;
			
			var options = new RestClientOptions(BaseUrl)
			{
				CachePolicy = new CacheControlHeaderValue {NoCache = true, NoStore = true},
				MaxTimeout = _lastFmOptions.Timeout
			};
			
			_restClient = new RestClient(options);
			
			var jsonOptions = new JsonSerializerOptions
			{
				Converters =
				{
					new JsonStringEnumConverter(JsonNamingPolicy.CamelCase)
				},
				NumberHandling = JsonNumberHandling.AllowReadingFromString
			};
			
			_restClient.UseSystemTextJson(jsonOptions);
			
		}

		public async Task<RecentTracksResult> GetRecentTracksAsync(string user, int limit, DateTime? from = null)
		{
			var request = new RestRequest();

			request.AddParameter("api_key", _lastFmOptions.ApiKey);
			request.AddParameter("method", ApiMethods.RecentTracks);

			//Add a parameter to ensure we return JSON
			request.AddParameter("format", "json");

			request.AddParameter("user", user);
			request.AddParameter("limit", limit);

			if (from.HasValue)
			{
				request.AddParameter("from", ((DateTimeOffset)from).ToUnixTimeSeconds());
			}

			var response = await _restClient.ExecuteGetAsync<RecentTracksResult>(request);

			// TODO: Handle exceptions/errors/timeouts!

			return response.Data;
		}
	}
}