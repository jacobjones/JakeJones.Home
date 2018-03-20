using System.Collections.Generic;
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
		private readonly IAlbumRepository _albumRepository;

		public RecentMusicManager(IRecentTracksRepository recentTracksRepository, IAlbumRepository albumRepository)
		{
			_recentTracksRepository = recentTracksRepository;
			_albumRepository = albumRepository;
		}

		public async Task<IDictionary<IAlbum, IList<ITrack>>> GetRecentTracks(int limit)
		{
			var recentTracks = await _recentTracksRepository.GetRecentlyListenedTracks(limit);

			if (recentTracks == null || !recentTracks.Any())
			{
				return null;
			}

			var recentTracksByAlbum = recentTracks.GroupBy(x => x.AlbumTitle);

			IDictionary<IAlbum, IList<ITrack>> albumTracks = new Dictionary<IAlbum, IList<ITrack>>();

			foreach (var album in recentTracksByAlbum)
			{
				var albumDetails = await _albumRepository.GetAlbum(album.First().Artist, album.Key);

				if (albumDetails == null)
				{
					continue;
				}

				albumTracks.Add(albumDetails, album.ToList());
			}

			return albumTracks;
		}
	}
}