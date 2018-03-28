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

		public async Task<IEnumerable<IPost>> Get(int count, int skip = 0)
		{
			return (await _context.Posts.Skip(0).Take(count).Skip(skip).ToListAsync()).Select(x => _mapper.Map<IPost>(x));
		}

		public async Task<IEnumerable<IPost>> GetByTag(string tag)
		{
			return (await _context.Posts.Where(x => x.Tags.Contains(tag, StringComparer.OrdinalIgnoreCase)).ToListAsync()).Select(x => _mapper.Map<IPost>(x));
		}

		public async Task<IPost> GetBySegment(string segment)
		{
			return _mapper.Map<IPost>(
				await _context.Posts.FirstOrDefaultAsync(x => x.Segment.Equals(segment, StringComparison.OrdinalIgnoreCase)));
		}

		public async Task<IPost> GetById(Guid id)
		{
			var postEntity = await _context.Posts.FindAsync(id);

			return postEntity == null ? null : _mapper.Map<IPost>(postEntity);
		}

		public async Task Create(IPost post)
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

		public async Task Delete(Guid id)
		{
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