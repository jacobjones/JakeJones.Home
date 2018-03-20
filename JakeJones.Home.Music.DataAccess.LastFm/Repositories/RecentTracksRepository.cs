using System.Collections.Generic;
using System.Threading.Tasks;
using JakeJones.Home.Music.DataAccess.LastFm.Connector;
using JakeJones.Home.Music.Models;
using JakeJones.Home.Music.Repositories;

namespace JakeJones.Home.Music.DataAccess.LastFm.Repositories
{
	public class RecentTracksRepository : IRecentTracksRepository
	{
		//TODO: Move to config?
		private const string Username = "ja-ke";
		private readonly ILastFmApiConnector _lastFmApiConnector;

		public RecentTracksRepository(ILastFmApiConnector lastFmApiConnector)
		{
			_lastFmApiConnector = lastFmApiConnector;
		}

		public async Task<IList<ITrack>> GetRecentlyListenedTracks(int limit)
		{
			var tracksResult = await _lastFmApiConnector.GetRecentTracks(Username, limit);

			if (tracksResult?.RecentTracks?.Tracks == null)
			{
				return null;
			}

			// TODO: Handle exceptions

			IList<ITrack> tracks = new List<ITrack>();

			foreach (var track in tracksResult.RecentTracks.Tracks)
			{
				tracks.Add(new Track(track.Artist.Name, track.Name, track.Album.Name));
			}

			return tracks;
		}
	}
}