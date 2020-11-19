using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using JakeJones.Home.Blog.DataAccess.SqlServer.Models;
using JakeJones.Home.Blog.Models;
using JakeJones.Home.Blog.Repositories;
using Microsoft.EntityFrameworkCore;

namespace JakeJones.Home.Blog.DataAccess.SqlServer.Repositories
{
	internal class PostRepository : IPostRepository
	{
		private readonly BlogContext _context;
		private readonly IMapper _mapper;

		public PostRepository(BlogContext context, IMapper mapper)
		{
			_context = context;
			_mapper = mapper;
		}

		public virtual async Task<IEnumerable<IPost>> GetAsync(bool isPublished, int count, int skip = 0)
		{
			if (isPublished)
			{
				return (await _context.Posts.OrderByDescending(x => x.PublishDate ?? DateTimeOffset.MaxValue).Where(x => x.IsPublished).Skip(skip).Take(count).ToListAsync()).Select(x => _mapper.Map<IPost>(x));
			}

			return (await _context.Posts.OrderByDescending(x => x.PublishDate ?? DateTimeOffset.MaxValue).Skip(skip).Take(count).ToListAsync()).Select(x => _mapper.Map<IPost>(x));
		}

		public virtual async Task<IEnumerable<IPost>> GetAllAsync(bool isPublished)
		{
			if (isPublished)
			{
				return (await _context.Posts.OrderByDescending(x => x.PublishDate ?? DateTimeOffset.MaxValue)
					.Where(x => x.IsPublished).ToListAsync()).Select(x => _mapper.Map<IPost>(x));
			}

			return (await _context.Posts.OrderByDescending(x => x.PublishDate ?? DateTimeOffset.MaxValue).ToListAsync())
				.Select(x => _mapper.Map<IPost>(x));
		}

		public async Task<IEnumerable<IPost>> GetByTagAsync(string tag)
		{
			if (string.IsNullOrEmpty(tag))
			{
				return null;
			}

			return (await _context.Posts.Where(x => x.Tags.Contains(tag, StringComparer.OrdinalIgnoreCase)).ToListAsync()).Select(x => _mapper.Map<IPost>(x));
		}

		public virtual async Task<IPost> GetBySegmentAsync(string segment)
		{
			if (string.IsNullOrEmpty(segment))
			{
				return null;
			}

			return _mapper.Map<IPost>(
				await _context.Posts.FirstOrDefaultAsync(x => x.Segment.ToLower() == segment.ToLower()));
		}

		public virtual async Task<IPost> GetByIdAsync(int id)
		{
			if (id <= 0)
			{
				return null;
			}

			var postEntity = await _context.Posts.FindAsync(id);

			return postEntity == null ? null : _mapper.Map<IPost>(postEntity);
		}

		public virtual async Task AddAsync(IPost post)
		{
			var postEntity = _mapper.Map<PostEntity>(post);

			await _context.AddAsync(postEntity);

			await _context.SaveChangesAsync();
		}

		public virtual async Task UpdateAsync(IPost post)
		{
			var postEntity = _mapper.Map<PostEntity>(post);

			var existingEntity = await _context.Posts.FindAsync(post.Id);

			if (existingEntity != null)
			{
				_context.Entry(existingEntity).CurrentValues.SetValues(postEntity);
			}

			await _context.SaveChangesAsync();
		}

		public virtual async Task DeleteAsync(int id)
		{
			if (id <= 0)
			{
				return;
			}

			var postEntity = await _context.Posts.FindAsync(id);

			if (postEntity == null)
			{
				return;
			}

			_context.Posts.Remove(postEntity);
			await _context.SaveChangesAsync();
		}
	}
}