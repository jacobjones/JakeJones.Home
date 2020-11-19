using System.Linq;
using JakeJones.Home.Blog.Models;
using JakeJones.Home.Search.Managers;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using JakeJones.Home.Blog.Managers;
using JakeJones.Home.Blog.Resolvers;
using JakeJones.Home.Search.Implementation.Controllers.Models;
using Microsoft.AspNetCore.Authorization;

namespace JakeJones.Home.Search.Implementation.Controllers.Api
{
	[Route("api/search")]
	public class SearchApiController : ControllerBase
	{
		private readonly ISearchManager _searchManager;
		private readonly IBlogUrlResolver _blogUrlResolver;
		private readonly IBlogManager _blogManager;

		public SearchApiController(ISearchManager searchManager, IBlogUrlResolver blogUrlResolver,
			IBlogManager blogManager)
		{
			_searchManager = searchManager;
			_blogUrlResolver = blogUrlResolver;
			_blogManager = blogManager;
		}

		[Route("reindex")]
		[HttpGet, Authorize]
		public async Task<IActionResult> GetReindexAsync(bool flush = false)
		{
			if (flush)
			{
				await _searchManager.ClearAsync();
			}

			var posts = await _blogManager.GetAllAsync(true);
			await _searchManager.UpdateAsync(posts);

			return Ok();
		}

		[Route("")]
		public async Task<IActionResult> GetQuerySearchAsync(string q)
		{
			var results = await _searchManager.SearchAsync<IPost>(q);

			return Ok(results.Select(x => new SearchResultApiModel(x.Title, _blogUrlResolver.GetUrl(x), x.Excerpt)));
		}
	}
}