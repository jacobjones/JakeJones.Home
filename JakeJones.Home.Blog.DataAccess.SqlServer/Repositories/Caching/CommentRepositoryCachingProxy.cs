using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using JakeJones.Home.Blog.Models;
using LazyCache;

namespace JakeJones.Home.Blog.DataAccess.SqlServer.Repositories.Caching
{
	internal class CommentRepositoryCachingProxy : CommentRepository
	{
		private readonly IAppCache _cache;

		public CommentRepositoryCachingProxy(BlogContext context, IMapper mapper, IAppCache cache) : base(context, mapper)
		{
			_cache = cache;
		}

		public override async Task<IComment> GetAsync(int id)
		{
			var cacheKey = $"{nameof(CommentRepository)}:{nameof(GetAsync)}:{id}";
			return await _cache.GetOrAddAsync(cacheKey, () => base.GetAsync(id));
		}

		public override async Task<ICollection<IComment>> GetByPostIdAsync(int postId)
		{
			var cacheKey = $"{nameof(CommentRepository)}:{nameof(GetByPostIdAsync)}:{postId}";
			return await _cache.GetOrAddAsync(cacheKey, () => base.GetByPostIdAsync(postId));
		}

		public override async Task<int> AddAsync(IComment comment)
		{
			var cacheKey = $"{nameof(CommentRepository)}:{nameof(GetByPostIdAsync)}:{comment.PostId}";
			_cache.Remove(cacheKey);

			return await base.AddAsync(comment);
		}

		public override async Task DeleteAsync(IComment comment)
		{
			var getCacheKey = $"{nameof(CommentRepository)}:{nameof(GetAsync)}:{comment.Id}";
			_cache.Remove(getCacheKey);

			var getByPostIdCacheKey = $"{nameof(CommentRepository)}:{nameof(GetByPostIdAsync)}:{comment.PostId}";
			_cache.Remove(getByPostIdCacheKey);

			await base.DeleteAsync(comment);
		}
	}
}