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
	internal class CommentRepository : ICommentRepository
	{
		private readonly BlogContext _context;
		private readonly IMapper _mapper;

		public CommentRepository(BlogContext context, IMapper mapper)
		{
			_context = context;
			_mapper = mapper;
		}

		public virtual async Task<IComment> GetAsync(int id)
		{
			var comment = await _context.Comments.FindAsync(id);

			return comment == null ? null : _mapper.Map<IComment>(comment);
		}

		public virtual async Task<ICollection<IComment>> GetByPostIdAsync(int postId)
		{
			var comments = await (_context.Comments.Where(x => x.Post.Id == postId).ToListAsync());

			return comments.Select(x => _mapper.Map<IComment>(x)).ToList();
		}

		public virtual async Task<int> AddAsync(IComment comment)
		{
			if (comment == null)
			{
				throw new ArgumentNullException(nameof(comment));
			}

			var commentEntity = _mapper.Map<CommentEntity>(comment);

			var post = await _context.Posts.FindAsync(comment.PostId);

			// TODO: Handle a post not being found

			commentEntity.Post = post;

			await _context.Comments.AddAsync(commentEntity);

			await _context.SaveChangesAsync();

			return commentEntity.Id;
		}

		public virtual async Task DeleteAsync(IComment comment)
		{
			if (comment == null)
			{
				throw new ArgumentNullException(nameof(comment));
			}

			var commentEntity = await _context.Comments.FindAsync(comment.Id);

			if (commentEntity == null)
			{
				return;
			}

			_context.Comments.Remove(commentEntity);
			await _context.SaveChangesAsync();
		}

		public virtual async Task DeleteByPostIdAsync(int postId)
		{
			var comments = await _context.Comments.Where(x => x.Post.Id == postId).ToListAsync();

			if (comments == null)
			{
				return;
			}

			_context.Comments.RemoveRange(comments);
			await _context.SaveChangesAsync();
		}
	}
}