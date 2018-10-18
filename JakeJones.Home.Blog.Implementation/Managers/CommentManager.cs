using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using JakeJones.Home.Blog.Managers;
using JakeJones.Home.Blog.Models;
using JakeJones.Home.Blog.Repositories;
using JakeJones.Home.Core.Managers;

namespace JakeJones.Home.Blog.Implementation.Managers
{
	internal class CommentManager : ICommentManager
	{
		private readonly ICommentRepository _commentRepository;
		private readonly IUserManager _userManager;

		public CommentManager(ICommentRepository commentRepository, IUserManager userManager)
		{
			_commentRepository = commentRepository;
			_userManager = userManager;
		}

		public async Task<ICollection<IComment>> GetByPostId(int id)
		{
			return await _commentRepository.GetByPostId(id);
		}

		public async Task Add(IComment comment)
		{
			if (comment == null)
			{
				throw new ArgumentNullException(nameof(comment));
			}

			// TODO: We should validate that the post exists here

			comment.IsAdmin = _userManager.IsAdmin();
			comment.PublishDate = DateTimeOffset.UtcNow;

			await _commentRepository.Add(comment);
		}
	}
}