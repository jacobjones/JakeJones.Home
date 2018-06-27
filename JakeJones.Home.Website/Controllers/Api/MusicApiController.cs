using System.Linq;
using System.Threading.Tasks;
using JakeJones.Home.Music.Managers;
using Microsoft.AspNetCore.Mvc;

namespace JakeJones.Home.Website.Controllers.Api
{
	[Route("api/music")]
	public class MusicApiController : ControllerBase
	{
		private readonly IRecentMusicManager _recentMusicManager;

		public MusicApiController(IRecentMusicManager recentMusicManager)
		{
			_recentMusicManager = recentMusicManager;
		}

		[Route("test")]
		public async Task<IActionResult> Test()
		{
			var tracks = await _recentMusicManager.GetRecentTracks(50);

			return Ok(tracks.Select(x => new { x.Key.Artist, x.Key.Title, Tracks = x.Value.Select(t => t.Title).ToArray() }));
		}
	}
}
