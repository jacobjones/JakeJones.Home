using System.Threading.Tasks;
using JakeJones.Home.Blog.Configuration;
using JakeJones.Home.Blog.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace JakeJones.Home.Website.Controllers
{
	public class BlogController : Controller
	{
		private readonly IPostRepository _postRepository;
		private readonly IBlogOptions _blogOptions;
		//private readonly WebManifest _manifest;

		public BlogController(IPostRepository postRepository, IBlogOptions blogOptions)
		{
			_postRepository = postRepository;
			_blogOptions = blogOptions;
		}

		[Route("/blog/{page:int?}")]
		//[OutputCache(Profile = "default")]
		public async Task<IActionResult> Index([FromRoute]int page = 1)
		{
			var posts = await _postRepository.Get(_blogOptions.PostsPerPage, _blogOptions.PostsPerPage * (page - 1));
		ViewData["Title"] = "Huh";
			ViewData["Description"] =  "Hmmmm";
			ViewData["prev"] = $"/{page + 1}/";
			ViewData["next"] = $"/{(page <= 1 ? null : page - 1 + "/")}";
			return View("~/Views/Blog/Index.cshtml", posts);
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

		[Route("/blog/{segment?}")]
		//[OutputCache(Profile = "default")]
		public async Task<IActionResult> Post(string segment)
		{
			var post = await _postRepository.GetBySegment(segment);

			if (post != null)
			{
				return View(post);
			}

			return NotFound();
		}

		//[Route("/blog/edit/{id?}")]
		//[HttpGet, Authorize]
		//public async Task<IActionResult> Edit(string id)
		//{
		//	if (string.IsNullOrEmpty(id))
		//	{
		//		return View(new Post());
		//	}

		//	var post = await _blog.GetPostById(id);

		//	if (post != null)
		//	{
		//		return View(post);
		//	}

		//	return NotFound();
		//}

		//[Route("/blog/{slug?}")]
		//[HttpPost, Authorize, AutoValidateAntiforgeryToken]
		//public async Task<IActionResult> UpdatePost(Post post)
		//{
		//	if (!ModelState.IsValid)
		//	{
		//		return View("Edit", post);
		//	}

		//	var existing = await _blog.GetPostById(post.ID) ?? post;
		//	string categories = Request.Form["categories"];

		//	existing.Categories = categories.Split(",", StringSplitOptions.RemoveEmptyEntries).Select(c => c.Trim().ToLowerInvariant()).ToList();
		//	existing.Title = post.Title.Trim();
		//	existing.Slug = !string.IsNullOrWhiteSpace(post.Slug) ? post.Slug.Trim() : Models.Post.CreateSlug(post.Title);
		//	existing.IsPublished = post.IsPublished;
		//	existing.Content = post.Content.Trim();
		//	existing.Excerpt = post.Excerpt.Trim();

		//	await _blog.SavePost(existing);

		//	await SaveFilesToDisk(existing);

		//	return Redirect(post.GetLink());
		//}

		//private async Task SaveFilesToDisk(Post post)
		//{
		//	var imgRegex = new Regex("<img[^>].+ />", RegexOptions.IgnoreCase | RegexOptions.Compiled);
		//	var base64Regex = new Regex("data:[^/]+/(?<ext>[a-z]+);base64,(?<base64>.+)", RegexOptions.IgnoreCase);

		//	foreach (Match match in imgRegex.Matches(post.Content))
		//	{
		//		XmlDocument doc = new XmlDocument();
		//		doc.LoadXml("<root>" + match.Value + "</root>");

		//		var img = doc.FirstChild.FirstChild;
		//		var srcNode = img.Attributes["src"];
		//		var fileNameNode = img.Attributes["data-filename"];

		//		// The HTML editor creates base64 DataURIs which we'll have to convert to image files on disk
		//		if (srcNode != null && fileNameNode != null)
		//		{
		//			var base64Match = base64Regex.Match(srcNode.Value);
		//			if (base64Match.Success)
		//			{
		//				byte[] bytes = Convert.FromBase64String(base64Match.Groups["base64"].Value);
		//				srcNode.Value = await _blog.SaveFile(bytes, fileNameNode.Value).ConfigureAwait(false);

		//				img.Attributes.Remove(fileNameNode);
		//				post.Content = post.Content.Replace(match.Value, img.OuterXml);
		//			}
		//		}
		//	}
		//}

		//[Route("/blog/deletepost/{id}")]
		//[HttpPost, Authorize, AutoValidateAntiforgeryToken]
		//public async Task<IActionResult> DeletePost(string id)
		//{
		//	var existing = await _blog.GetPostById(id);

		//	if (existing != null)
		//	{
		//		await _blog.DeletePost(existing);
		//		return Redirect("/");
		//	}

		//	return NotFound();
		//}

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
