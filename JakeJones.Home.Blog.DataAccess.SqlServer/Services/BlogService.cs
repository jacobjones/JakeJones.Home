using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using JakeJones.Home.Blog.DataAccess.SqlServer.Models;
using JakeJones.Home.Blog.Models;
using JakeJones.Home.Blog.Services;
using Microsoft.EntityFrameworkCore;

namespace JakeJones.Home.Blog.DataAccess.SqlServer.Services
{
	internal class BlogService : IBlogService
	{
		private readonly BlogContext _context;
		private readonly IMapper _mapper;

		public BlogService(BlogContext context, IMapper mapper)
		{
			_context = context;
			_mapper = mapper;
		}

		public async Task<IEnumerable<IPost>> GetPosts(int count, int skip = 0)
		{
			return (await _context.Posts.Skip(0).Take(count).Skip(skip).ToListAsync()).Select(x => _mapper.Map<IPost>(x));
		}

		public async Task<IEnumerable<IPost>> GetPostsByCategory(string category)
		{
			return (await _context.Posts.Where(x => x.Categories.Contains(category, StringComparer.OrdinalIgnoreCase)).ToListAsync()).Select(x => _mapper.Map<IPost>(x));
		}

		public async Task<IPost> GetPostBySlug(string slug)
		{
			return _mapper.Map<IPost>(
				await _context.Posts.FirstOrDefaultAsync(x => x.Slug.Equals(slug, StringComparison.OrdinalIgnoreCase)));
		}

		public async Task<IPost> GetPostById(string id)
		{
			var postEntity = await _context.Posts.FindAsync(id);

			return postEntity == null ? null : _mapper.Map<IPost>(postEntity);
		}

		public async Task<IEnumerable<string>> GetCategories()
		{
			throw new NotImplementedException();
		}

		public async Task SavePost(IPost post)
		{
			var postEntity = _mapper.Map<PostEntity>(post);

			var existingEntity = await _context.Posts.FindAsync(post.Id);

			if (existingEntity == null)
			{
				await _context.AddAsync(postEntity);
			}
			else
			{
				_context.Entry(existingEntity).CurrentValues.SetValues(postEntity);
			}
			
			await _context.SaveChangesAsync();
		}

		public Task DeletePost(IPost post)
		{
			throw new System.NotImplementedException();
		}
	}
}