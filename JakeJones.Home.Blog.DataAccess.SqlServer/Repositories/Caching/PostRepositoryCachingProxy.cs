using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using JakeJones.Home.Blog.Models;
using LazyCache;

namespace JakeJones.Home.Blog.DataAccess.SqlServer.Repositories.Caching
{
	internal class PostRepositoryCachingProxy : PostRepository
	{
		private readonly IAppCache _cache;

		public PostRepositoryCachingProxy(IAppCache cache, BlogContext context, IMapper mapper) : base(context, mapper)
		{
			_cache = cache;
		}

		public override async Task<IEnumerable<IPost>> Get(bool isPublished, int count, int skip = 0)
		{
			var cacheKey = $"{nameof(PostRepository)}:{nameof(Get)}:{isPublished}:{count}:{skip}";

			var posts = await _cache.GetAsync<IEnumerable<IPost>>(cacheKey);

			if (posts != null)
			{
				return posts;
			}

			posts = await base.Get(isPublished, count, skip);

			if (posts == null)
			{
				return null;
			}

			foreach (var post in posts)
			{
				var segmentCacheKey = $"{nameof(PostRepository)}:{nameof(GetBySegment)}:{post.Segment}";
				var idCacheKey = $"{nameof(PostRepository)}:{nameof(GetById)}:{post.Id}";

				_cache.Add(segmentCacheKey, post);
				_cache.Add(idCacheKey, post);
			}

			_cache.Add(cacheKey, posts);

			return posts;
		}

		public override async Task<IPost> GetBySegment(string segment)
		{
			var cacheKey = $"{nameof(PostRepository)}:{nameof(GetBySegment)}:{segment}";

			return await _cache.GetOrAddAsync(cacheKey, () => base.GetBySegment(segment));
		}

		public override async Task<IPost> GetById(int id)
		{
			var cacheKey = $"{nameof(PostRepository)}:{nameof(GetById)}:{id}";

			return await _cache.GetOrAddAsync(cacheKey, () => base.GetById(id));
		}

		public override async Task Update(IPost post)
		{
			var segmentCacheKey = $"{nameof(PostRepository)}:{nameof(GetBySegment)}:{post.Segment}";
			var idCacheKey  = $"{nameof(PostRepository)}:{nameof(GetById)}:{post.Id}";

			_cache.Remove(segmentCacheKey);
			_cache.Remove(idCacheKey);

			await base.Update(post);
		}

		public override async Task Delete(int id)
		{
			var idCacheKey = $"{nameof(PostRepository)}:{nameof(GetById)}:{id}";
			_cache.Remove(idCacheKey);

			await base.Delete(id);
		}
	}
}