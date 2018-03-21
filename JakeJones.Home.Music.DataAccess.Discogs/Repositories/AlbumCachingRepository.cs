using System;
using System.Threading.Tasks;
using JakeJones.Home.Music.DataAccess.Discogs.Clients;
using JakeJones.Home.Music.Models;
using Microsoft.Extensions.Caching.Memory;

namespace JakeJones.Home.Music.DataAccess.Discogs.Repositories
{
	internal class AlbumCachingRepository : AlbumRepository
	{
		private readonly IMemoryCache _memoryCache;
		private readonly IDiscogsClient _discogsClient;

		public AlbumCachingRepository(IMemoryCache memoryCache, IDiscogsClient discogsClient) : base(discogsClient)
		{
			_memoryCache = memoryCache;
			_discogsClient = discogsClient;
		}

		public override async Task<IAlbum> GetAlbum(string artist, string title)
		{
			var cacheKey = $"{nameof(AlbumRepository)}:{nameof(GetAlbum)}:{artist}:{title}";

			if (_memoryCache.TryGetValue(cacheKey, out IAlbum album))
			{
				return album;
			}

			album = await base.GetAlbum(artist, title);

			// TODO: caching to config
			_memoryCache.Set(cacheKey, album, new MemoryCacheEntryOptions {SlidingExpiration = new TimeSpan(0, 5, 0, 0)});

			return album;
		}
	}
}