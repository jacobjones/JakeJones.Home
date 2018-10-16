using System.Text;
using System.Threading.Tasks;
using JakeJones.Home.Blog.Builders;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Net.Http.Headers;

namespace JakeJones.Home.Blog.Implementation.Controllers
{
	[Route("blog")]
	public class BlogRssFeedController : Controller
	{
		private readonly IBlogRssFeedBuilder _blogRssFeedBuilder;

		public BlogRssFeedController(IBlogRssFeedBuilder blogRssFeedBuilder)
		{
			_blogRssFeedBuilder = blogRssFeedBuilder;
		}

		// TODO: Add output caching
		[HttpGet]
		[Route("feed")]
		public async Task<IActionResult> Index()
		{
			var mediaType = new MediaTypeHeaderValue("application/rss+xml")
			{
				Charset = Encoding.UTF8.WebName
			};

			return Content(await _blogRssFeedBuilder.Build(), mediaType);
		}
		
	}
}
