﻿using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using JakeJones.Home.Blog.Configuration;
using JakeJones.Home.Blog.Implementation.Models;
using JakeJones.Home.Blog.Managers;
using JakeJones.Home.Blog.Models;
using JakeJones.Home.Blog.Resolvers;
using JakeJones.Home.Core.Managers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace JakeJones.Home.Blog.Implementation.Controllers
{
	[Route("blog")]
	public class BlogController : Controller
	{
		private readonly IBlogOptions _blogOptions;
		private readonly IBlogUrlResolver _blogUrlResolver;
		private readonly IMapper _mapper;
		private readonly IBlogManager _blogManager;
		private readonly ICommentManager _commentManager;
		private readonly IHoneypotManager _honeypotManager;
		private readonly IImageManager _imageManager;

		public BlogController(IBlogOptions blogOptions, IBlogUrlResolver blogUrlResolver, IMapper mapper,
			IBlogManager blogManager, ICommentManager commentManager, IHoneypotManager honeypotManager, IImageManager imageManager)
		{
			_blogOptions = blogOptions;
			_blogUrlResolver = blogUrlResolver;
			_mapper = mapper;
			_blogManager = blogManager;
			_commentManager = commentManager;
			_honeypotManager = honeypotManager;
			_imageManager = imageManager;
		}

		[Route("{page:int?}")]
		[HttpGet]
		//[OutputCache(Profile = "default")]
		public async Task<IActionResult> Index([FromRoute]int page = 1)
		{
			var posts = (await _blogManager.GetAsync(_blogOptions.PostsPerPage, _blogOptions.PostsPerPage * (page - 1))).ToList();

			ViewData["Title"] = "Blog";
			//ViewData["Description"] =  "Hmmmm";

			IList<PostListItemViewModel> postModels = new List<PostListItemViewModel>();

			foreach (var post in posts)
			{
				var postModel = _mapper.Map<PostListItemViewModel>(post);
				postModel.AbsoluteUrl = _blogUrlResolver.GetUrl(post);
				postModels.Add(postModel);
			}

			var model = new PostListViewModel
			{
				Posts = postModels,
				Pagination = new PaginationViewModel
				{
					PrevUrl = $"/blog/{page + 1}",
					ShowPrev = posts.Count == _blogOptions.PostsPerPage,
					NextUrl = page == 2 ? "/blog" : $"/blog/{page - 1}",
					ShowNext = page > 1
				}
			};

			return View("~/Views/Blog/Index.cshtml", model);
		}

		[Route("{segment?}")]
		[HttpGet]
		public async Task<IActionResult> PostAsync(string segment)
		{
			var post = await _blogManager.GetBySegmentAsync(segment);

			if (post == null || !_blogManager.IsVisibleToUser(post))
			{
				return NotFound();
			}

			// Get the comments for this post
			var comments = await _commentManager.GetByPostIdAsync(post.Id);

			var model = _mapper.Map<PostViewModel>(post);
			model.AbsoluteUrl = _blogUrlResolver.GetUrl(post);
			model.Comments = comments?.Select(x => _mapper.Map<CommentViewModel>(x)).ToList();

			return View("Post", model);
		}

		[Route("edit/{id?}")]
		[HttpGet, Authorize]
		public async Task<IActionResult> EditAsync(int? id)
		{
			if (!id.HasValue)
			{
				return View("~/Views/Blog/Edit.cshtml", new PostEditViewModel { IsNew = true });
			}

			var post = await _blogManager.GetByIdAsync(id.Value);

			if (post == null)
			{
				return NotFound();
			}

			var model = _mapper.Map<PostEditViewModel>(post);
			model.IsNew = false;

			return View("~/Views/Blog/Edit.cshtml", model);
		}

		[Route("{segment?}")]
		[HttpPost, Authorize, AutoValidateAntiforgeryToken]
		public async Task<IActionResult> UpdatePostAsync(PostEditViewModel model)
		{
			if (!ModelState.IsValid)
			{
				return View("~/Views/Blog/Edit.cshtml", model);
			}

			// Map this out to a post
			var post = _mapper.Map<IPost>(model);

			await _blogManager.AddOrUpdateAsync(post);

			return Redirect(_blogUrlResolver.GetUrl(post));
		}

		[Route("/blog/delete/{id}")]
		[HttpPost, Authorize, AutoValidateAntiforgeryToken]
		public async Task<IActionResult> DeletePostAsync(int id)
		{
			//TODO: Maybe should check success here?
			await _blogManager.DeleteAsync(id);
			return Redirect("/");
		}

		[Route("/blog/comment/{postId}")]
		[HttpPost]
		public async Task<IActionResult> AddCommentAsync(int postId, CommentEditViewModel model)
		{
			var post = await _blogManager.GetByIdAsync(postId);

			if (post == null)
			{
				return NotFound();
			}

			if (_honeypotManager.IsTrapped())
			{
				// Treat this like a success
				return Redirect(_blogUrlResolver.GetUrl(post));
			}

			if (!ModelState.IsValid)
			{
				// TODO: Get some validation feedback for the user in
				return View("Post", await GetPostViewModelAsync(post));
			}

			//if (!post.AreCommentsOpen(_settings.Value.CommentsCloseAfterDays))
			//{
			//	return NotFound();
			//}

			var comment = new Comment
			{
				PostId = postId,
				Author = model.Author.Trim(),
				Email = model.Email.Trim(),
				Content = model.Content.Trim()
			};

			var id = await _commentManager.AddAsync(comment);

			return Redirect($"{_blogUrlResolver.GetUrl(post)}#comment-{id}");
		}

		[Route("/blog/comment/{postId}/delete/{commentId}")]
		[Authorize]
		public async Task<IActionResult> DeleteCommentAsync(int postId, int commentId)
		{
			var post = await _blogManager.GetByIdAsync(postId);

			if (post == null)
			{
				return NotFound();
			}

			if (commentId <= 0)
			{
				return NotFound();
			}

			await _commentManager.DeleteAsync(commentId);

			return Redirect($"{_blogUrlResolver.GetUrl(post)}#comments");
		}

		[Route("/blog/image")]
		[HttpPost]
		[Authorize]
		public async Task<ActionResult> UploadImageAsync(IFormFile file)
		{
			var location = await _imageManager.SaveAsync(file);

			return Json(new { location });
		}

		private async Task<PostViewModel> GetPostViewModelAsync(IPost post)
		{
			// Get the comments for this post
			var comments = await _commentManager.GetByPostIdAsync(post.Id);

			var model = _mapper.Map<PostViewModel>(post);
			model.AbsoluteUrl = _blogUrlResolver.GetUrl(post);
			model.Comments = comments?.Select(x => _mapper.Map<CommentViewModel>(x)).ToList();

			return model;
		}
	}
}
