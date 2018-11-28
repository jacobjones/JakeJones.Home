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

		public async Task<ICollection<IComment>> GetByPostIdAsync(int id)
		{
			return await _commentRepository.GetByPostIdAsync(id);
		}

		public async Task<int> AddAsync(IComment comment)
		{
			if (comment == null)
			{
				throw new ArgumentNullException(nameof(comment));
			}

			comment.IsAdmin = _userManager.IsAdmin();
			comment.PublishDate = DateTimeOffset.UtcNow;

			return await _commentRepository.AddAsync(comment);
		}

		public async Task DeleteAsync(int id)
		{
			var comment = await _commentRepository.GetAsync(id);

			// The comment doesn't exist already
			if (comment == null)
			{
				return;
			}

			await _commentRepository.DeleteAsync(comment);
		}
	}
}