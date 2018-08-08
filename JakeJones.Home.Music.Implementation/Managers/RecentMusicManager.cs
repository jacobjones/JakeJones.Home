using System;
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

		public async Task<IAlbumTrack> GetRecentTrackAsync()
		{
			// We pull 5 here to try and ensure we have one we can get an album for.
			var recentTracks = await _recentTracksRepository.GetAsync(5);

			if (recentTracks == null || !recentTracks.Any())
			{
				return null;
			}

			var albumTracks = recentTracks.GroupBy(x => x.AlbumTitle);

			foreach (var albumTrack in albumTracks)
			{
				var track = albumTrack.First();

				var album = await _albumRepository.GetAsync(track.Artist, track.AlbumTitle);

				if (album == null)
				{
					continue;
				}

				// This album does not have a valid image
				if (string.IsNullOrEmpty(album.ImageUrl) ||
				    album.ImageUrl.EndsWith("spacer.gif", StringComparison.OrdinalIgnoreCase))
				{
					continue;
				}

				return new AlbumTrack(album, track);
			}

			return null;
		}
	}
}