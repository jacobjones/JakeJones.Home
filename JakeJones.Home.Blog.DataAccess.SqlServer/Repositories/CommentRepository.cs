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

		public async Task<ICollection<IComment>> GetByPostId(int postId)
		{
			var comments = await (_context.Comments.Where(x => x.Post.Id == postId).ToListAsync());

			return comments.Select(x => _mapper.Map<IComment>(x)).ToList();
		}

		public async Task Add(IComment comment)
		{
			var commentEntity = _mapper.Map<CommentEntity>(comment);
			commentEntity.Post = new PostEntity {Id = comment.PostId};

			await _context.AddAsync(commentEntity);

			await _context.SaveChangesAsync();
		}
	}
}