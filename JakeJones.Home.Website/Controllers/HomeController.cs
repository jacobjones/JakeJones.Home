using System.Diagnostics;
using JakeJones.Home.Website.Models;
using Microsoft.AspNetCore.Mvc;

namespace JakeJones.Home.Website.Controllers
{
	public class HomeController : Controller
	{
		public IActionResult Index()
		{
			return View();
		}

		[Route("about")]
		public IActionResult About()
		{
			return View();
		}

		public IActionResult Error()
		{
			return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
		}
	}
}
