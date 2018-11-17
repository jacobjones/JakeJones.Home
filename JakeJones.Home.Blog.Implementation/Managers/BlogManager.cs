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
		private readonly ISegmentGenerator _segmentGenerator;
		private readonly IUserManager _userManager;

		public BlogManager(IPostRepository postRepository, ISegmentGenerator segmentGenerator, IUserManager userManager)
		{
			_postRepository = postRepository;
			_segmentGenerator = segmentGenerator;
			_userManager = userManager;
		}

		public async Task<IEnumerable<IPost>> Get(int count, int skip = 0)
		{
			var isPublished = !_userManager.IsAdmin();

			return await _postRepository.Get(isPublished, count, skip);
		}

		public async Task<IPost> GetBySegment(string segment)
		{
			return await _postRepository.GetBySegment(segment);
		}

		public async Task<IPost> GetById(int id)
		{
			return await _postRepository.GetById(id);
		}

		public async Task AddOrUpdate(IPost post)
		{
			// Always set the last modified date
			post.LastModified = DateTimeOffset.UtcNow;

			if (post.Id <= 0)
			{
				await Add(post);
				return;
			}

			var existingPost = await _postRepository.GetById(post.Id);

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

				await _postRepository.Update(post);
				return;
			}

			await Add(post);
		}

		public async Task Delete(int id)
		{
			await _postRepository.Delete(id);
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

			await _postRepository.Add(post);
		}
	}
}
