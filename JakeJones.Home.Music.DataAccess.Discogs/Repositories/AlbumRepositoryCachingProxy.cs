using System;
using System.Threading.Tasks;
using JakeJones.Home.Music.DataAccess.Discogs.Clients;
using JakeJones.Home.Music.Models;
using Microsoft.Extensions.Caching.Memory;

namespace JakeJones.Home.Music.DataAccess.Discogs.Repositories
{
	internal class AlbumRepositoryCachingProxy : AlbumRepository
	{
		private readonly IMemoryCache _memoryCache;

		public AlbumRepositoryCachingProxy(IMemoryCache memoryCache, IDiscogsClient discogsClient) : base(discogsClient)
		{
			_memoryCache = memoryCache;
		}

		public override async Task<IAlbum> GetAsync(string artist, string title)
		{
			var cacheKey = $"{nameof(AlbumRepository)}:{nameof(GetAsync)}:{artist}:{title}";

			if (_memoryCache.TryGetValue(cacheKey, out IAlbum album))
			{
				return album;
			}

			album = await base.GetAsync(artist, title);

			// TODO: caching to config
			_memoryCache.Set(cacheKey, album, new MemoryCacheEntryOptions {SlidingExpiration = new TimeSpan(0, 5, 0, 0)});

			return album;
		}
	}
}