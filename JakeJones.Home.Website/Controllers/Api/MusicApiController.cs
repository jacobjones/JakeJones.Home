using System.Linq;
using System.Threading.Tasks;
using JakeJones.Home.Books.Managers;
using JakeJones.Home.Books.Repositories;
using JakeJones.Home.Books.Repositories.Properties;
using JakeJones.Home.Music.Managers;
using Microsoft.AspNetCore.Mvc;

namespace JakeJones.Home.Website.Controllers.Api
{
	[Route("api/music")]
	public class MusicApiController : ControllerBase
	{
		private readonly IRecentMusicManager _recentMusicManager;
		private readonly IBookManager _bookManager;

		public MusicApiController(IRecentMusicManager recentMusicManager, IBookManager bookManager)
		{
			_recentMusicManager = recentMusicManager;
			_bookManager = bookManager;
		}

		[Route("current")]
		public async Task<IActionResult> Test()
		{
			var tracks = await _recentMusicManager.GetRecentTrackAsync();

			//if (track == null)
			//{
			//	return null;
			//}

			return Ok(tracks);
		}
	}
}
