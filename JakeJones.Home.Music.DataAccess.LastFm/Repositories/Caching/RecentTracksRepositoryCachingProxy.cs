using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using JakeJones.Home.Music.DataAccess.LastFm.Clients;
using JakeJones.Home.Music.DataAccess.LastFm.Models;
using JakeJones.Home.Music.Models;
using Microsoft.Extensions.Caching.Memory;

namespace JakeJones.Home.Music.DataAccess.LastFm.Repositories.Caching
{
	public class RecentTracksRepositoryCachingProxy : RecentTracksRepository
	{
		private readonly IMemoryCache _memoryCache;

		public RecentTracksRepositoryCachingProxy(IMemoryCache memoryCache, ILastFmClient lastFmApiConnector) : base(lastFmApiConnector)
		{
			_memoryCache = memoryCache;
		}

		public override async Task<IReadOnlyCollection<ITrack>> GetAsync(int limit)
		{
			var cacheKey = $"{nameof(RecentTracksRepository)}:{nameof(GetAsync)}:{limit}";

			if (!_memoryCache.TryGetValue(cacheKey, out RecentTracks recentTracks))
			{
				// Nothing in the cache, so get the full list
				var allRecentTracks = await base.GetAsync(limit);

				if (allRecentTracks == null)
				{
					return null;
				}

				// TODO: Figure out a good expiry time:
				_memoryCache.Set(cacheKey, new RecentTracks(DateTime.UtcNow, allRecentTracks), DateTime.UtcNow.AddHours(1));

				return allRecentTracks;
			}

			if ((DateTime.UtcNow - recentTracks.FetchedDate).TotalMinutes < 1)
			{
				return recentTracks.Tracks;
			}

			// Get any tracks that have been played after the last time we checked
			var latestTracks = await GetRecentlyListenedTracksAsync(limit, recentTracks.FetchedDate) ?? new List<ITrack>();
			latestTracks = latestTracks.Concat(recentTracks.Tracks).Take(limit).ToList();

			_memoryCache.Set(cacheKey, new RecentTracks(DateTime.UtcNow, latestTracks), DateTime.UtcNow.AddHours(1));

			return latestTracks;
		}
	}
}