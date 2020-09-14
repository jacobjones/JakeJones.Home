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

		public override async Task<IEnumerable<IPost>> GetAsync(bool isPublished, int count, int skip = 0)
		{
			var cacheKey = $"{nameof(PostRepository)}:{nameof(GetAsync)}:{isPublished}:{count}:{skip}";

			var posts = await _cache.GetAsync<IEnumerable<IPost>>(cacheKey);

			if (posts != null)
			{
				return posts;
			}

			posts = await base.GetAsync(isPublished, count, skip);

			if (posts == null)
			{
				return null;
			}

			foreach (var post in posts)
			{
				var segmentCacheKey = $"{nameof(PostRepository)}:{nameof(GetBySegmentAsync)}:{post.Segment}";
				var idCacheKey = $"{nameof(PostRepository)}:{nameof(GetByIdAsync)}:{post.Id}";

				_cache.Add(segmentCacheKey, post);
				_cache.Add(idCacheKey, post);
			}

			_cache.Add(cacheKey, posts);

			return posts;
		}

		public override async Task<IPost> GetBySegmentAsync(string segment)
		{
			var cacheKey = $"{nameof(PostRepository)}:{nameof(GetBySegmentAsync)}:{segment}";

			return await _cache.GetOrAddAsync(cacheKey, () => base.GetBySegmentAsync(segment));
		}

		public override async Task<IPost> GetByIdAsync(int id)
		{
			var cacheKey = $"{nameof(PostRepository)}:{nameof(GetByIdAsync)}:{id}";

			return await _cache.GetOrAddAsync(cacheKey, () => base.GetByIdAsync(id));
		}

		public override async Task UpdateAsync(IPost post)
		{
			var segmentCacheKey = $"{nameof(PostRepository)}:{nameof(GetBySegmentAsync)}:{post.Segment}";
			var idCacheKey  = $"{nameof(PostRepository)}:{nameof(GetByIdAsync)}:{post.Id}";

			_cache.Remove(segmentCacheKey);
			_cache.Remove(idCacheKey);

			await base.UpdateAsync(post);
		}

		public override async Task DeleteAsync(int id)
		{
			var idCacheKey = $"{nameof(PostRepository)}:{nameof(GetByIdAsync)}:{id}";
			_cache.Remove(idCacheKey);

			await base.DeleteAsync(id);
		}
	}
}