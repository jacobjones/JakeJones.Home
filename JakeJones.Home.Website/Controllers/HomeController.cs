using System.Diagnostics;
using JakeJones.Home.Blog.Models;
using JakeJones.Home.Blog.Repositories;
using JakeJones.Home.Website.Models;
using Microsoft.AspNetCore.Mvc;

namespace JakeJones.Home.Website.Controllers
{
	public class HomeController : Controller
	{
		private readonly IPostRepository _postRepository;

		public HomeController(IPostRepository postRepository)
		{
			_postRepository = postRepository;
		}

		public IActionResult Index()
		{
			var post = new Post("Hello, this is a new post!", "Read more", "blah blah blah");
			_postRepository.Create(post);

			return View();
		}

		public IActionResult About()
		{
			ViewData["Message"] = "Your application description page.";

			return View();
		}

		public IActionResult Contact()
		{
			ViewData["Message"] = "Your contact page.";

			return View();
		}

		public IActionResult Error()
		{
			return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
		}
	}
}
