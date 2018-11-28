using System.Linq;
using System.Threading.Tasks;
using JakeJones.Home.Music.Managers;
using JakeJones.Home.Music.Models;
using JakeJones.Home.Music.Repositories;

namespace JakeJones.Home.Music.Implementation.Managers
{
	internal class RecentMusicManager : IRecentMusicManager
	{
		private readonly IRecentTracksRepository _recentTracksRepository;

		public RecentMusicManager(IRecentTracksRepository recentTracksRepository)
		{
			_recentTracksRepository = recentTracksRepository;
		}

		public async Task<ITrack> GetRecentTrackAsync()
		{
			// We pull 5 here to try and ensure we have one we can get an album for.
			var recentTracks = await _recentTracksRepository.GetAsync(5);

			if (recentTracks == null || !recentTracks.Any())
			{
				return null;
			}

			return recentTracks.FirstOrDefault(x => !string.IsNullOrEmpty(x.ImageUrl));
		}
	}
}