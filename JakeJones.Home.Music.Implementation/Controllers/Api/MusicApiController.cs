using System.Threading.Tasks;
using JakeJones.Home.Music.Implementation.Controllers.Api.Models;
using JakeJones.Home.Music.Managers;
using Microsoft.AspNetCore.Mvc;

namespace JakeJones.Home.Music.Implementation.Controllers.Api
{
	[Route("api/music")]
	public class MusicApiController : ControllerBase
	{
		private readonly IRecentMusicManager _recentMusicManager;

		public MusicApiController(IRecentMusicManager recentMusicManager)
		{
			_recentMusicManager = recentMusicManager;
		}

		[Route("current")]
		public async Task<IActionResult> GetCurrent()
		{
			var track = await _recentMusicManager.GetRecentTrackAsync();

			if (track == null)
			{
				return null;
			}

			return Ok(new TrackApiModel($"{track.Track.Artist} - {track.Track.Title}", track.Album.ImageUrl));
		}
	}
}
