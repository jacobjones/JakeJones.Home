using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using JakeJones.Home.Blog.Managers;
using JakeJones.Home.Blog.Models;
using JakeJones.Home.Blog.Repositories;
using JakeJones.Home.Blog.Resolvers;
using JakeJones.Home.Core.Managers;

namespace JakeJones.Home.Blog.Implementation.Managers
{
	internal class CommentManager : ICommentManager
	{
		private readonly ICommentRepository _commentRepository;
		private readonly IUserManager _userManager;
		private readonly INotificationManager _notificationManager;
		private readonly IPostRepository _postRepository;
		private readonly IBlogUrlResolver _blogUrlResolver;

		public CommentManager(ICommentRepository commentRepository, IUserManager userManager, INotificationManager notificationManager, IPostRepository postRepository, IBlogUrlResolver blogUrlResolver)
		{
			_commentRepository = commentRepository;
			_userManager = userManager;
			_notificationManager = notificationManager;
			_postRepository = postRepository;
			_blogUrlResolver = blogUrlResolver;
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

			var isAdmin = _userManager.IsAdmin();

			comment.IsAdmin = isAdmin;
			comment.PublishDate = DateTimeOffset.UtcNow;

			var commentId = await _commentRepository.AddAsync(comment);

			if (isAdmin)
			{
				return commentId;
			}

			var post = await _postRepository.GetById(comment.PostId);
			await _notificationManager.SendNotificationAsync($"{comment.Author} commented on a post.", $"{comment.Author} commented on {post.Title} ({_blogUrlResolver.GetUrl(post, true)}).");

			return commentId;
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