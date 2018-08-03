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

		public async Task<IEnumerable<IPost>> Get(bool isPublished, int count, int skip = 0)
		{
			if (isPublished)
			{
				return (await _context.Posts.OrderByDescending(x => x.PublishDate ?? DateTime.MaxValue).Where(x => x.IsPublished).Skip(skip).Take(count).ToListAsync()).Select(x => _mapper.Map<IPost>(x));
			}

			return (await _context.Posts.OrderByDescending(x => x.PublishDate ?? DateTime.MaxValue).Skip(skip).Take(count).ToListAsync()).Select(x => _mapper.Map<IPost>(x));
		}

		public async Task<IEnumerable<IPost>> GetByTag(string tag)
		{
			if (string.IsNullOrEmpty(tag))
			{
				return null;
			}

			return (await _context.Posts.Where(x => x.Tags.Contains(tag, StringComparer.OrdinalIgnoreCase)).ToListAsync()).Select(x => _mapper.Map<IPost>(x));
		}

		public async Task<IPost> GetBySegment(string segment)
		{
			if (string.IsNullOrEmpty(segment))
			{
				return null;
			}

			return _mapper.Map<IPost>(
				await _context.Posts.FirstOrDefaultAsync(x => x.Segment.Equals(segment, StringComparison.OrdinalIgnoreCase)));
		}

		public async Task<IPost> GetById(int id)
		{
			if (id <= 0)
			{
				return null;
			}

			var postEntity = await _context.Posts.FindAsync(id);

			return postEntity == null ? null : _mapper.Map<IPost>(postEntity);
		}

		public async Task Add(IPost post)
		{
			var postEntity = _mapper.Map<PostEntity>(post);

			await _context.AddAsync(postEntity);

			await _context.SaveChangesAsync();
		}

		public async Task Update(IPost post)
		{
			var postEntity = _mapper.Map<PostEntity>(post);

			var existingEntity = await _context.Posts.FindAsync(post.Id);

			if (existingEntity != null)
			{
				_context.Entry(existingEntity).CurrentValues.SetValues(postEntity);
			}

			await _context.SaveChangesAsync();
		}

		public async Task Delete(int id)
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