using System;
using System.Threading.Tasks;
using JakeJones.Home.Books.Models;
using JakeJones.Home.Books.Repositories;
using Microsoft.Extensions.Caching.Memory;

namespace JakeJones.Home.Books.Implementation.Managers.Caching
{
	internal class BookManagerCachingProxy : BookManager
	{
		private readonly IMemoryCache _memoryCache;

		public BookManagerCachingProxy(IBookRepository bookRepository, IMemoryCache memoryCache) : base(bookRepository)
		{
			_memoryCache = memoryCache;
		}

		public override async Task<IBook> GetCurrentBookAsync()
		{
			var cacheKey = $"{nameof(BookManagerCachingProxy)}:{nameof(GetCurrentBookAsync)}";

			if (_memoryCache.TryGetValue(cacheKey, out IBook book))
			{
				return book;
			}

			book = await base.GetCurrentBookAsync();
			_memoryCache.Set(cacheKey, book, DateTime.UtcNow.AddHours(1));
			return book;
		}
	}
}