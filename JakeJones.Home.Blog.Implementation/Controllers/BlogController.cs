using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using JakeJones.Home.Blog.Configuration;
using JakeJones.Home.Blog.Implementation.Models;
using JakeJones.Home.Blog.Managers;
using JakeJones.Home.Blog.Models;
using JakeJones.Home.Blog.Resolvers;
using Microsoft.AspNetCore.Authorization;
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

		public BlogController(IBlogOptions blogOptions, IBlogUrlResolver blogUrlResolver, IMapper mapper, IBlogManager blogManager)
		{
			_blogOptions = blogOptions;
			_blogUrlResolver = blogUrlResolver;
			_mapper = mapper;
			_blogManager = blogManager;
		}

		[Route("{page:int?}")]
		[HttpGet]
		//[OutputCache(Profile = "default")]
		public async Task<IActionResult> Index([FromRoute]int page = 1)
		{
			var posts = await _blogManager.Get(_blogOptions.PostsPerPage, _blogOptions.PostsPerPage * (page - 1));

			ViewData["Title"] = "Blog";
			ViewData["Description"] =  "Hmmmm";

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
					PrevUrl = $"/blog/{(page <= 1 ? null : page - 1 + "/")}",
					NextUrl = $"/blog/{page + 1}/"
				}
			};

			return View("~/Views/Blog/Index.cshtml", model);
		}

		//[Route("/blog/category/{category}/{page:int?}")]
		//[OutputCache(Profile = "default")]
		//public async Task<IActionResult> Category(string category, int page = 0)
		//{
		//	var posts = (await _blog.GetPostsByCategory(category)).Skip(_settings.Value.PostsPerPage * page).Take(_settings.Value.PostsPerPage);
		//	ViewData["Title"] = _manifest.Name + " " + category;
		//	ViewData["Description"] = $"Articles posted in the {category} category";
		//	ViewData["prev"] = $"/blog/category/{category}/{page + 1}/";
		//	ViewData["next"] = $"/blog/category/{category}/{(page <= 1 ? null : page - 1 + "/")}";
		//	return View("~/Views/Blog/Index.cshtml", posts);
		//}

		[Route("{segment?}")]
		[HttpGet]
		//[OutputCache(Profile = "default")]
		public async Task<IActionResult> Post(string segment)
		{
			var post = await _blogManager.GetBySegment(segment);

			if (post == null || !_blogManager.IsVisibleToUser(post))
			{
				return NotFound();
			}

			var model = _mapper.Map<PostViewModel>(post);
			model.AbsoluteUrl = _blogUrlResolver.GetUrl(post);

			return View("~/Views/Blog/Post.cshtml", model);
		}

		[Route("edit/{id?}")]
		[HttpGet, Authorize]
		public async Task<IActionResult> Edit(int? id)
		{
			if (!id.HasValue)
			{
				return View("~/Views/Blog/Edit.cshtml", new PostEditViewModel { IsNew = true });
			}

			var post = await _blogManager.GetById(id.Value);

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
		public async Task<IActionResult> UpdatePost(PostEditViewModel model)
		{
			if (!ModelState.IsValid)
			{
				return View("~/Views/Blog/Edit.cshtml", model);
			}

			// Map this out to a post
			var post = _mapper.Map<IPost>(model);

			await _blogManager.AddOrUpdate(post);

			return Redirect(_blogUrlResolver.GetUrl(post));
		}

		[Route("/blog/delete/{id}")]
		[HttpPost, Authorize, AutoValidateAntiforgeryToken]
		public async Task<IActionResult> DeletePost(int id)
		{
			//TODO: Maybe should check success here?
			await _blogManager.Delete(id);
			return Redirect("/");
		}

		//[Route("/blog/comment/{postId}")]
		//[HttpPost]
		//public async Task<IActionResult> AddComment(string postId, Comment comment)
		//{
		//	var post = await _blog.GetPostById(postId);

		//	if (!ModelState.IsValid)
		//	{
		//		return View("Post", post);
		//	}

		//	if (post == null || !post.AreCommentsOpen(_settings.Value.CommentsCloseAfterDays))
		//	{
		//		return NotFound();
		//	}

		//	comment.IsAdmin = User.Identity.IsAuthenticated;
		//	comment.Content = comment.Content.Trim();
		//	comment.Author = comment.Author.Trim();
		//	comment.Email = comment.Email.Trim();

		//	// the website form key should have been removed by javascript
		//	// unless the comment was posted by a spam robot
		//	if (!Request.Form.ContainsKey("website"))
		//	{
		//		post.Comments.Add(comment);
		//		await _blog.SavePost(post);
		//	}

		//	return Redirect(post.GetLink() + "#" + comment.ID);
		//}

		//[Route("/blog/comment/{postId}/{commentId}")]
		//[Authorize]
		//public async Task<IActionResult> DeleteComment(string postId, string commentId)
		//{
		//	var post = await _blog.GetPostById(postId);

		//	if (post == null)
		//	{
		//		return NotFound();
		//	}

		//	var comment = post.Comments.FirstOrDefault(c => c.ID.Equals(commentId, StringComparison.OrdinalIgnoreCase));

		//	if (comment == null)
		//	{
		//		return NotFound();
		//	}

		//	post.Comments.Remove(comment);
		//	await _blog.SavePost(post);

		//	return Redirect(post.GetLink() + "#comments");
		//}
	}
}
