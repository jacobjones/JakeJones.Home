using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using JakeJones.Home.Blog.Managers;
using JakeJones.Home.Blog.Models;
using JakeJones.Home.Blog.Repositories;
using JakeJones.Home.Core.Generators;
using JakeJones.Home.Core.Managers;

namespace JakeJones.Home.Blog.Implementation.Managers
{
	internal class BlogManager : IBlogManager
	{
		private readonly IPostRepository _postRepository;
		private readonly ICommentRepository _commentRepository;
		private readonly ISegmentGenerator _segmentGenerator;
		private readonly IUserManager _userManager;

		public BlogManager(IPostRepository postRepository, ICommentRepository commentRepository,
			ISegmentGenerator segmentGenerator, IUserManager userManager)
		{
			_postRepository = postRepository;
			_commentRepository = commentRepository;
			_segmentGenerator = segmentGenerator;
			_userManager = userManager;
		}

		public async Task<IEnumerable<IPost>> GetAsync(int count, int skip = 0)
		{
			var isPublished = !_userManager.IsAdmin();

			return await _postRepository.GetAsync(isPublished, count, skip);
		}

		public async Task<IPost> GetBySegmentAsync(string segment)
		{
			return await _postRepository.GetBySegmentAsync(segment);
		}

		public async Task<IPost> GetByIdAsync(int id)
		{
			return await _postRepository.GetByIdAsync(id);
		}

		public async Task AddOrUpdateAsync(IPost post)
		{
			// Always set the last modified date
			post.LastModified = DateTimeOffset.UtcNow;

			if (post.Id <= 0)
			{
				await Add(post);
				return;
			}

			var existingPost = await _postRepository.GetByIdAsync(post.Id);

			if (existingPost != null)
			{
				// If the post is being published, and has never previously been published
				// set the published date.
				if (post.IsPublished && !existingPost.IsPublished && !existingPost.PublishDate.HasValue)
				{
					post.PublishDate = DateTimeOffset.UtcNow;
				}
				else
				{
					post.PublishDate = existingPost.PublishDate;
				}

				await _postRepository.UpdateAsync(post);
				return;
			}

			await Add(post);
		}

		public async Task DeleteAsync(int id)
		{
			await _commentRepository.DeleteByPostIdAsync(id);

			await _postRepository.DeleteAsync(id);
		}

		public bool IsVisibleToUser(IPost post)
		{
			if (post == null)
			{
				return false;
			}

			return post.IsPublished || _userManager.IsAdmin();
		}

		private async Task Add(IPost post)
		{
			if (post.IsPublished)
			{
				post.PublishDate = DateTimeOffset.UtcNow;
			}

			if (string.IsNullOrEmpty(post.Segment))
			{
				post.Segment = _segmentGenerator.Get(post.Title);
			}

			await _postRepository.AddAsync(post);
		}
	}
}
